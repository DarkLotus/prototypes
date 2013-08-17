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
    public class ClientManager
    {
        private static ClientManager _instance;
        public static ClientManager Instance { get { if(_instance == null) _instance = new ClientManager();return _instance;}}
       
        public delegate void PacketHandler(Account owner,BaseMessage msg);
        public event PacketHandler OnPacketarrival;

        public  List<Account> OnlineAccounts = new List<Account>();

        private  List<Account> RemoveMe = new List<Account>();

        internal void Update(long delta) {
            for (int i = OnlineAccounts.Count -1; i >= 0; i--) {
                if (!HandleClientMessages(OnlineAccounts[i], delta))
                    RemoveMe.Add(OnlineAccounts[i]);
            }
               
            foreach (var serial in RemoveMe) {
                Database.SaveToon(serial.CurrentToon);
                WorldManager.PlayerLeaveScene(serial);
                Logger.Log("Account : " + serial.Serial + " Logged off");
                if (serial.Client != null && serial.Client.Connected)
                    serial.Client.Close();
                OnlineAccounts.Remove(serial);
            }
            RemoveMe.Clear();


        }

        internal void ClientConnected(System.Net.Sockets.TcpClient Client) {
            Client.NoDelay = true;

            OnlineAccounts.Add(new Account(Client));
            Logger.Log(Client.Client.RemoteEndPoint.ToString() + " Connected");
        }

        
        private bool HandleClientMessages(Account p,long delta) {
            if (p.Client == null || !p.Client.Connected)
                return false;
            p.TimeSincePingSent += delta;
            //Send ping every 30 seconds
            if (p.TimeSincePingSent > (30 * 1000)) {
                if (!new ProtoShared.Packets.Shared.Ping() { TimeStamp = System.DateTime.Now.ToFileTimeUtc() }.Send(p)) { return false; }
                p.TimeSincePingSent = 0;
            }

            //If last recieved ping back was more than 30 seconds, DC
            if (p.LastPing != 0 && p.LastPing > DateTime.Now.ToFileTimeUtc() + (30 * 1000)) {
                Logger.Log("Dissconnecting client " + p.UserName + " due to no pings");
                return false;
            }
                      

            while (p.Client.Available > 1) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(p.Client.GetStream(), PrefixStyle.Base128);
                //Logger.Log(data.GetType().ToString() + "    " + data.PacketType);
                        switch (data.PacketType) {
                            case OpCodes.C_LoginRequest:
                                handleClientAuthReq(p.Client.GetStream(), (LoginRequest)data,p);
                                break;
                            case OpCodes.S_Ping:
                                handlePing(p, (ProtoShared.Packets.Shared.Ping)data);
                                break;
                            case OpCodes.C_CreateCharacter:
                                handleCreateToon(p, (CreateCharacter)data);
                                break;
                            case OpCodes.C_SelectCharacter:
                                handleSelectToon(p.Client.GetStream(), (SelectCharacter)data, p);
                                break;
                            default:
                                if (OnPacketarrival != null)
                                    OnPacketarrival(p, data);
                                break;

                        }

            }
            return true;
        }

        private void handlePing(Account p, ProtoShared.Packets.Shared.Ping ping) {
            p.LastPing = DateTime.Now.ToFileTimeUtc();
        }

     

        private void handleCreateToon(Account p, CreateCharacter createCharacter) {
            p.CurrentToon = Database.CreateToon(p, createCharacter);
            WorldManager.PlayerJoinScene(p);
            new EnterWorld((Toon)p.CurrentToon).Send(p.Client.GetStream());
        }



           

        private void handleSelectToon(NetworkStream client, SelectCharacter selectCharacter, Account account) {
            int index = Math.Max(0,Math.Min(selectCharacter.ToonID, account.Toons.Count));
            account.CurrentToon = account.Toons[index];
            Logger.Log("Entering world..." + account.CurrentToon.Name);
            WorldManager.PlayerJoinScene(account);
            new EnterWorld((Toon)account.CurrentToon).Send(client);
            return;
            
        }

        private void handleClientAuthReq(NetworkStream client, LoginRequest loginRequest,Account p) {
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
