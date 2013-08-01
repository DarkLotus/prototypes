using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ProtoBuf;
using ProtoShared;
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
            Console.WriteLine("Started Listening on " + IPAddress.Any.ToString());
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
                ClientManager.Update(sw.ElapsedMilliseconds);
                if (sw.ElapsedMilliseconds < 16f)
                    Thread.Sleep((int)(16 - sw.ElapsedMilliseconds));
                Thread.Sleep(1);
            }
        }



        private static void AcceptClientConnection(IAsyncResult ar)
        {
            Console.WriteLine("Accepted a Connection");
            TcpListener client = (TcpListener)ar.AsyncState;

            TcpClient Client = client.EndAcceptTcpClient(ar);
            ClientManager.ClientConnected(Client);
            
            //client.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnection), tcpListener);

            
        }

       

        private static void _handleSyncClient(Player client, MoveRequest syncClient) {
            Logger.Log(client.Name + " Moved to " + syncClient.x + "," + syncClient.y);
            client.Location.x = syncClient.x;
            client.Location.y = syncClient.y;
            client.Location.z = syncClient.z;
        }

            


       
      
    }
}
