using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class SelectCharacter : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int ToonID;
         public SelectCharacter() : base(ID) { }

         public SelectCharacter(int toonid) : base(ID) { ToonID = toonid; }
         
    }
}
