﻿using ProtoBuf;
using ProtoShared;
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
        public static Dictionary<int, Player> Clients = new Dictionary<int, Player>();

        static internal Player getPlayer(int ID) {
            if(Clients.ContainsKey(ID))
                return Clients[ID];
            Player p = new Player();
            p.Serial = ID;
            p.Location = new Vector3(1460, 0, 250);
            p.Name = "ToonTest";
            Clients.Add(ID, p);
            return getPlayer(ID);

        }

        static internal void AddPlayer(Player player) {
            Clients.Add(player.Serial, player);
        }

        internal static void Update(long delta) {
            foreach (Player p in Clients.Values) {
            
            }
        }

        internal static void ClientConnected(System.Net.Sockets.TcpClient Client) {
            var thread = new Thread(new ParameterizedThreadStart(ClientMessageLoop));
            thread.Start(Client);
        }


        private static void ClientMessageLoop(object obj) {
            Console.WriteLine("Client Socket Opened");
            TcpClient client = (TcpClient)obj;
            AccountData p = null; Player pp = null;
            while (client.Connected) {
                Thread.Sleep(16);
                if (client.Available > 0) {
                    int len = 0;
                    // Serializer.TryReadLengthPrefix(client.GetStream(), PrefixStyle.Base128, out len);
                    if (len <= client.Available) {
                        //byte[] buf = new byte[client.Available];
                        Logger.Log("incoming, len: " + len + " / " + client.Available);
                        //client.GetStream().Read(buf, 0, buf.Length);
                        var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(client.GetStream(), PrefixStyle.Base128);
                        Logger.Log(data.GetType().ToString() + "    " + data.PacketType);
                        switch (data.PacketType) {
                            case OpCodes.C_LOGIN_REQUEST:
                                p = handleClientAuthReq(client, (LoginRequest)data);
                                break;
                            case OpCodes.C_SELECT_CHARACTER:
                                pp = handleSelectToon(client, (SelectCharacter)data);
                                ClientManager.AddPlayer(pp);
                                break;
                            case OpCodes.C_SYNC_CLIENT:
                                handleSyncClient(pp, (SyncClient)data);
                                break;

                        }

                    }



                }
            }
            Logger.Log(client.ToString() + " Dissconnected");
        }

        private static void handleSyncClient(Player p, SyncClient syncClient) {
            Logger.Log(p.Name + " Moved to " + syncClient.x + "," + syncClient.y);
            p.Location.x = syncClient.x;
            p.Location.y = syncClient.y;
            p.Location.z = syncClient.z;
        }

        private static Player handleSelectToon(TcpClient client, SelectCharacter selectCharacter) {
            Player p = new Player();
            p.Name = "testChar1";
            p.Serial = 5;
            p.Location = new Vector3(2400f, 0, 250f);
            LoginResponse response = new LoginResponse();
            if (p == null)
                response.ResultCode = 0;
            else
                response.ResultCode = 1;
            Serializer.SerializeWithLengthPrefix<LoginResponse>(client.GetStream(), response, PrefixStyle.Base128);
            client.GetStream().Flush();
            if (response.ResultCode == 0) {
                client.Close();
                return null;
            }
            EnterWorld enter = new EnterWorld();
            enter.Player = p;
            Serializer.SerializeWithLengthPrefix<EnterWorld>(client.GetStream(), enter, PrefixStyle.Base128);
            return p;

        }

        private static AccountData handleClientAuthReq(TcpClient client, LoginRequest loginRequest) {
            //TODO Load from DB
            AccountData a = new AccountData();
            a.Characters = new String[] { "testChar1", "testchar2" };
            LoginResponse x = new LoginResponse();
            x.Characters = a.Characters;
            x.ResultCode = 1;
            Serializer.SerializeWithLengthPrefix<LoginResponse>(client.GetStream(), x, PrefixStyle.Base128);
            Logger.Log("Sent Login Response");
            return a;

        }

    }
}