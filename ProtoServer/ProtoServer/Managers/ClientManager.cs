using ProtoBuf;
using ProtoServer.Data;
using ProtoServer.DataBase;
using ProtoShared;
using ProtoShared.Data;
using ProtoShared.Packets;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using ProtoShared.Packets.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ProtoServer.Managers
{
    public static class ClientManager
    {
        

        public static List<Account> OnlineAccounts = new List<Account>();

        private static List<Account> RemoveMe = new List<Account>();
        internal static void Update(long delta) {
            for (int i = OnlineAccounts.Count -1; i >= 0; i--) {
            if(!HandleClientMessages(OnlineAccounts[i],delta))
                RemoveMe.Add(OnlineAccounts[i]);
            }
               
            foreach (var serial in RemoveMe) {
                WorldManager.PlayerLeaveScene(serial);
                Logger.Log("Account : " + serial.Serial + " Logged off");
                if (serial.Client != null && serial.Client.Connected)
                    serial.Client.Close();
                OnlineAccounts.Remove(serial);
            }
            RemoveMe.Clear();


        }

        private static bool HandleClientMessages(Account p,long delta) {
            if (p.Client == null || !p.Client.Connected)
                return false;

            //Replace this with a counter in a Keep Alive/Ping packet
            if (p.Client.Available == 0) {
                p.idleTime += delta;
                /*if(p.idleTime > (60*1000))
                return false;*/
                return true;
            }

            while (p.Client.Available > 1) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(p.Client.GetStream(), PrefixStyle.Base128);
                //Logger.Log(data.GetType().ToString() + "    " + data.PacketType);
                        switch (data.PacketType) {
                            case OpCodes.C_LoginRequest:
                                handleClientAuthReq(p.Client.GetStream(), (LoginRequest)data,p);
                                break;
                            case OpCodes.C_CreateCharacter:
                                handleCreateToon(p, (CreateCharacter)data);
                                break;
                            case OpCodes.C_SelectCharacter:
                                handleSelectToon(p.Client.GetStream(), (SelectCharacter)data, p);
                                break;
                            case OpCodes.C_MoveRequest:
                                handleMoveRequest(p.CurrentToon, (MoveRequest)data);
                                break;
                            case OpCodes.S_ChatMessage:
                                handleChatMessage(p, (ChatMessage)data);
                                break;

                        }

            }
            return true;
        }

        private static void handleChatMessage(Account a, ChatMessage chatMessage) {
            Logger.Log(chatMessage.Sender + ": " + chatMessage.Message);
            foreach (var p in OnlineAccounts)
                if(a.Serial != p.Serial)
                chatMessage.Send(p.Client.GetStream());
        }

        private static void handleCreateToon(Account p, CreateCharacter createCharacter) {
            p.CurrentToon = Database.CreateToon(p, createCharacter);
            WorldManager.PlayerJoinScene(p);
            new EnterWorld((Toon)p.CurrentToon).Send(p.Client.GetStream());
        }

        internal static void ClientConnected(System.Net.Sockets.TcpClient Client) {
            Client.NoDelay = true;
            
            OnlineAccounts.Add(new Account(Client));
            Logger.Log(Client.Client.RemoteEndPoint.ToString() + " Connected");
        }


   

        private static void handleMoveRequest(Toon p, MoveRequest syncClient) {
            Logger.Log(p.Name + " Moved to " + syncClient.x + "," + syncClient.y);
            p.Location.X = syncClient.x;
            p.Location.Y = syncClient.y;
            p.Location.Z = syncClient.z;
            SendMovementUpdate(syncClient,p.Serial);
            
        }

        private static void SendMovementUpdate(MoveRequest syncClient,int serial) {
            SyncMobile m = new SyncMobile();
            m.Serial = serial;
            m.Location = new Vector3D(syncClient.x, syncClient.y, syncClient.z);
            foreach (Account p in ClientManager.OnlineAccounts)
                if(p.CurrentToon.Serial != serial)
                m.Send(p.Client.GetStream());
            Logger.Log("Sent SyncMobile to: " + ClientManager.OnlineAccounts.Count + " players");
        }

        private static void handleSelectToon(NetworkStream client, SelectCharacter selectCharacter, Account account) {
            int index = Math.Max(0,Math.Min(selectCharacter.ToonID, account.Toons.Count));
            account.CurrentToon = account.Toons[index];
            Logger.Log("Entering world..." + account.CurrentToon.Name);
            WorldManager.PlayerJoinScene(account);
            new EnterWorld((Toon)account.CurrentToon).Send(client);
            return;
            
        }

        private static void handleClientAuthReq(NetworkStream client, LoginRequest loginRequest,Account p) {
            Logger.Log("User: " + loginRequest.UserName + " Connecting...");
            Database.LoadAccountInto(p,loginRequest.UserName, loginRequest.Password);
            //if (OnlineAccounts.Where(a => a.Serial == p.Serial).Count() > 1)
            //    OnlineAccounts.Remove(OnlineAccounts.Where(a => a.Serial == p.Serial).First());
            new LoginResponse(p.GetToonNames()).Send(client);
            Logger.Log("Sent Login Response");
            return;
        }

    }
}
