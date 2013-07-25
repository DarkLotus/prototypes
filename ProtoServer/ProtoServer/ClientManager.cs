using ProtoShared.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer
{
    public class ClientManager
    {
        public List<Player> Clients = new List<Player>();
        private static ClientManager _instance;
        public static ClientManager Instance { get { if (_instance == null) _instance = new ClientManager(); return _instance; } }

        internal Player getPlayer(ProtoShared.Packets.FromClient.LoginRequest loginMessage) {
            throw new NotImplementedException();
        }
    }
}
