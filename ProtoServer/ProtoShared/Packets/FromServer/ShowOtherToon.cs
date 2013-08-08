using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using ProtoShared.Data;

namespace ProtoShared.Packets.FromServer
{
    [ProtoContract]
    public class ShowOtherToon : ProtoShared.Packets.BaseMessage
    {
        //TODO maybe this should be like EnterWorldMobile or something and be generic to mobiles/items/players etc
        public static Int16 ID;
        [ProtoMember(2)]
        public Toon Toon;


        public ShowOtherToon() : base(ID) { }

        public ShowOtherToon(Toon toon)
            : base(ID) {
                this.Toon = (Toon)toon;
        }
    }
}
