using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class MoveRequest : BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public float x;

        [ProtoMember(3)]
        public float y;
        [ProtoMember(4)]
        public float z;

        
        public MoveRequest(float x, float y, float z)
            : base(ID) {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        public MoveRequest()
            : base(ID) {
        }

    }


}
