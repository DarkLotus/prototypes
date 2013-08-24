using System;
using ProtoBuf;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class DissconnectRequest : BaseMessage
    {
        public static Int16 ID;

        public DissconnectRequest() : base(ID)
        {
        }
    }
}