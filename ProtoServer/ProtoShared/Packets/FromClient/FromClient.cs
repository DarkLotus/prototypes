using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ProtoShared.Packets.FromClient
{


   

    [ProtoContract]
    public class SyncClient : BaseMessage
    {
        [ProtoMember(2)]
        public float x;

        [ProtoMember(3)]
        public float y;
        [ProtoMember(4)]
        public float z;

        
        public SyncClient(float x, float y, float z)
            : base(PacketType.SyncClient) {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        public SyncClient()
            : base(PacketType.SyncClient) {
        }

    }


}
