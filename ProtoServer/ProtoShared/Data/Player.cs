using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ProtoBuf;

namespace ProtoShared.Player
{
    [ProtoContract]
    public class Player
    {
        [ProtoMember(1)]
        public int Serial;
        [ProtoMember(2)]
        public float CharPosX { get; set; }
        [ProtoMember(3)]
        public float CharPosY { get; set; }
        [ProtoMember(4)]
        public string Name { get; set; }
         



        public Player()
        { }



        public NetworkStream Stream { get; set; }
    }



    [ProtoContract]
    public class Account
    {
        [ProtoMember(1)]
        public Player Player{ get; set; }
        [ProtoMember(2)]
        public string UserName{ get; set; }
        [ProtoMember(3)]
        private string _password{ get; set; }
    }


  
}
