using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.Shared
{
    /// <summary>
    /// Sent when a mobile has moved
    /// </summary>
    [ProtoContract]
    public class Ping : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int TickID;

        [ProtoMember(3)]
        public long TimeStamp;


        public Ping() : base(ID) { }
    }
}