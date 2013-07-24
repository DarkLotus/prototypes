using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ProtoShared.Network
{
    [ProtoContract]
    public class ClientMove
    {
        [ProtoMember(1)]
        public int Serial { get; set; }
        [ProtoMember(2)]
        public int CharPosX { get; set; }
        [ProtoMember(3)]
        public int CharPosY { get; set; }
    }
}
