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
using System.IO;
using ProtoServer.Managers;
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
            Thread.Sleep(100);
            }
        }

        private static void MainLoop() {
            Stopwatch sw = new Stopwatch();
            long timer = 0;
            sw.Start();
            while (true) {
            //Do game loop
                ClientManager.Update(timer);
                WorldManager.Update(timer);
                if (sw.ElapsedMilliseconds > 16)
                    Logger.Log("Update running behind : " + sw.ElapsedMilliseconds);
                if (sw.ElapsedMilliseconds < 16)
                    Thread.Sleep((int)(16 - sw.ElapsedMilliseconds));
                timer = sw.ElapsedMilliseconds;
                sw.Reset();
                sw.Start();
            }
        }



        private static void AcceptClientConnection(IAsyncResult ar) {
            Console.WriteLine("Accepted a Connection");
            TcpListener client = (TcpListener)ar.AsyncState;

            TcpClient Client = client.EndAcceptTcpClient(ar);
            ClientManager.ClientConnected(Client);

            //client.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnection), tcpListener);


        }  
    }
}
