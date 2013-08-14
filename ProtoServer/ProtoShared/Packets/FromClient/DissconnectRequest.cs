using ProtoBuf;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class DissconnectRequest : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
         public DissconnectRequest() : base(ID) { }

      
    }
}
