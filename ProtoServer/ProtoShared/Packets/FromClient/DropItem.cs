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
    public class DropItem : ProtoShared.Packets.BaseMessage
    {
        /// <summary>
        /// Set via reflection at startup, provides a method to determine packet type.
        /// </summary>
        public static Int16 ID;


        [ProtoMember(2)]
        public int Serial;


        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(3)]
        public Vector3D Location;

        /// <summary>
        /// Containers ID, 0 if in world
        /// </summary>
        [ProtoMember(4)]
        public int ContainerSerial;

        public DropItem() : base(ID) { }

                 
    }
}
