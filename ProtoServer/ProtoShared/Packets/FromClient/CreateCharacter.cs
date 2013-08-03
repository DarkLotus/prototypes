using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class CreateCharacter : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public string Name;

        [ProtoMember(3)]
        public bool isMale;


         public CreateCharacter() : base(ID) { }
         
    }
}
