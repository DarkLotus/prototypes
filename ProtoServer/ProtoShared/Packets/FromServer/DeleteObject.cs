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
    public class DeleteObject : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int Serial;


        public DeleteObject() : base(ID) { }

        public DeleteObject(int Serial)
            : base(ID) {
                this.Serial = Serial;
        }
    }
}
