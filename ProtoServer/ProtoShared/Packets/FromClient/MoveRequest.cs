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

        [ProtoMember(2)] public Vector3D Location;


        public MoveRequest(float x, float y, float z)
            : base(ID)
        {
            Location = new Vector3D(x, y, z);
        }
        public MoveRequest(Vector3D loc)
            : base(ID) {
            Location = new Vector3D(loc);
        }
        public MoveRequest()
            : base(ID)
        {
        }
    }
}