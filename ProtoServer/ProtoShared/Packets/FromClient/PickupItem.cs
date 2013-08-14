using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    /// <summary>
    /// Sent when the client uses an Object, ie double click tool and target or Double click a door etc
    /// </summary>
    [ProtoContract]
    public class PickupItem : ProtoShared.Packets.BaseMessage
    {
        /// <summary>
        /// Set via reflection at startup, provides a method to determine packet type.
        /// </summary>
        public static Int16 ID;

        [ProtoMember(2)]
        public int Serial;

        public PickupItem() : base(ID) { }

                 
    }
}
