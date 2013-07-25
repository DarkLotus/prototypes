using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ProtoBuf;
using ProtoShared;
using ProtoShared.Player;
using System.Diagnostics;
using ProtoShared.Packets.FromServer;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets;
namespace ProtoServer
{
    class Program
    {
        
        static TcpListener tcpListener;
        private static List<Player> Players = new List<Player>();

        static void Main(string[] args)
        {
            MessageTypes.Init();
            tcpListener = new TcpListener(IPAddress.Any, 2594);
            tcpListener.Start();
            Console.WriteLine("Started Listening on " + IPAddress.Loopback.ToString());
            Thread t = new Thread(new ThreadStart(MainLoop));
            t.IsBackground = true;
            t.Start();
            while (true)
            {
            if(tcpListener.Pending())
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnection), tcpListener);
            Thread.Sleep(10);
            }
        }

        private static void MainLoop() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true) {
            //Do game loop
                Update(sw.ElapsedMilliseconds);
                if (sw.ElapsedMilliseconds < 16f)
                    Thread.Sleep((int)(16 - sw.ElapsedMilliseconds));
            
            }
        }

        private static int _playerCount = 0;
        private static void Update(long delta) {
            //Logger.Log("Delta:" + delta);
            if (Players.Count > _playerCount) {
                _playerCount = Players.Count;
                Logger.Log("Players Online: " + _playerCount);
            }
        }


        private static void AcceptClientConnection(IAsyncResult ar)
        {
            TcpListener client = (TcpListener)ar.AsyncState;

            TcpClient UOClient = client.EndAcceptTcpClient(ar);
            var thread = new Thread(new ParameterizedThreadStart(ClientMessageLoop));
            thread.Start(UOClient);
            //client.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnection), tcpListener);

            
        }

        private static void ClientMessageLoop(object obj)
        {
            Console.WriteLine("Client Socket Opened");
            TcpClient client = (TcpClient)obj;
            Thread.Sleep(500);
            Player p = null;
            while (client.Connected)
            {
                Thread.Sleep(16);
                if (client.Available > 0)
                {
                    int len = 0;
                   // Serializer.TryReadLengthPrefix(client.GetStream(), PrefixStyle.Base128, out len);
                    if (len <= client.Available)
                    {
                        //byte[] buf = new byte[client.Available];
                        Logger.Log("incoming, len: " + len + " / " + client.Available);
                        //client.GetStream().Read(buf, 0, buf.Length);
                        var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(client.GetStream(), PrefixStyle.Base128);
                        Logger.Log(data.GetType().ToString() + "    " + data.PacketType);
                        switch (data.PacketType)
                        {
                            case 1:
                                p = _handleClientAuthReq(client, (LoginRequest)data);
                                break;
                            case 2:
                                _handleSyncClient(p, (SyncClient)data);
                                break;

                        }

                    }

                  
                    
                }
            }
            Logger.Log(client.ToString() + " Dissconnected");
        }

        private static void _handleSyncClient(Player client, SyncClient syncClient) {
            Logger.Log(client.Name + " Moved to " + syncClient.x + "," + syncClient.y);
            client.CharPosX = syncClient.x;
            client.CharPosY = syncClient.y;
        }

        private static Player _handleClientAuthReq(TcpClient client, LoginRequest loginMessage)
        {
            Logger.Log(loginMessage.UserName + " Connecting");
            var player = ClientManager.Instance.getPlayer(loginMessage);
            LoginResponse response = new LoginResponse();
            if (player == null)
                response.ResultCode = 0;
            else
                response.ResultCode = 1;
             Serializer.SerializeWithLengthPrefix<LoginResponse>(client.GetStream(), response, PrefixStyle.Base128);
            client.GetStream().Flush();
            if (response.ResultCode == 0) {
                client.Close();
                return null;
            }
            player.Stream = client.GetStream();
            Players.Add(player);
            return player;
        }

       


       
      
    }
}
