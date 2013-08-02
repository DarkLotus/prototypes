using ProtoBuf;
using ProtoServer.Data;
using ProtoServer.DataBase;
using ProtoShared;
using ProtoShared.Data;
using ProtoShared.Packets;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ProtoServer
{
    public static class ClientManager
    {

        public static List<Account> OnlineAccounts = new List<Account>();

       
        internal static void Update(long delta) {
            foreach (Account p in OnlineAccounts) {
                HandleClientMessages(p);
            }
        }

        private static void HandleClientMessages(Account p) {
            if (p.Client == null || !p.Client.Connected)
                return;
            while (p.Client.Available > 1) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(p.Client.GetStream(), PrefixStyle.Base128);
                //Logger.Log(data.GetType().ToString() + "    " + data.PacketType);
                        switch (data.PacketType) {
                            case OpCodes.C_LoginRequest:
                                handleClientAuthReq(p.Client.GetStream(), (LoginRequest)data,p);
                                break;
                            case OpCodes.C_SelectCharacter:
                                handleSelectToon(p.Client.GetStream(), (SelectCharacter)data, p);
                                break;
                            case OpCodes.C_MoveRequest:
                                handleMoveRequest(p.CurrentToon, (MoveRequest)data);
                                break;

                        }

            }

        }

        internal static void ClientConnected(System.Net.Sockets.TcpClient Client) {
            Client.NoDelay = true;
            OnlineAccounts.Add(new Account(Client));
            Logger.Log(Client.Client.RemoteEndPoint.ToString() + " Connected");
        }


   

        private static void handleMoveRequest(Toon p, MoveRequest syncClient) {
            Logger.Log(p.Name + " Moved to " + syncClient.x + "," + syncClient.y);
            p.Location.x = syncClient.x;
            p.Location.y = syncClient.y;
            p.Location.z = syncClient.z;
            SendMovementUpdate(syncClient,p.Serial);
            
        }

        private static void SendMovementUpdate(MoveRequest syncClient,int serial) {
            SyncMobile m = new SyncMobile();
            m.Serial = serial;
            m.Location = new Vector3(syncClient.x, syncClient.y, syncClient.z);
            foreach (Account p in ClientManager.OnlineAccounts)
                m.Send(p.Client.GetStream());
            Logger.Log("Sent SyncMobile to: " + ClientManager.OnlineAccounts.Count + " players");
        }

        private static void handleSelectToon(NetworkStream client, SelectCharacter selectCharacter, Account account) {
            account.CurrentToon = account.Toons[selectCharacter.ToonID];
            new EnterWorld((Toon)account.CurrentToon).Send(client);
            return;
            
        }

        private static void handleClientAuthReq(NetworkStream client, LoginRequest loginRequest,Account p) {
            Logger.Log("User: " + loginRequest.UserName + " Connecting...");
            Database.LoadAccountInto(p,loginRequest.UserName, loginRequest.Password);
            new LoginResponse(p.GetToonNames()).Send(client);
            Logger.Log("Sent Login Response");
            return;
        }

    }
}
