using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ProtoServer.Data
{
    public class Account
    {
        public int Serial;

        public string UserName;

        public List<ServerToon> Toons = new List<ServerToon>();

        public Toon CurrentToon;


        public TcpClient Client;

        public Account(System.Net.Sockets.TcpClient Client) {
            // TODO: Complete member initialization
            this.Client = Client;
        }
        public Account() { }
        internal string[] GetToonNames() {
            
            string[] toons = new string[Toons.Count];
            int i = 0;
            foreach(var t in Toons)
                toons[i++] = t.Name;
            return toons;
        }
    }
}
