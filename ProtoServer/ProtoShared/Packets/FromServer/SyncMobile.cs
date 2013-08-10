using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using ProtoShared.Data;

namespace ProtoShared.Packets.FromServer
{
    /// <summary>
    /// Sent when a mobile has moved
    /// </summary>
    [ProtoContract]
    public class SyncMobile : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int Serial;

        [ProtoMember(3)]
        public Vector3D Location;


        public SyncMobile() : base(ID) { }
    }
}
