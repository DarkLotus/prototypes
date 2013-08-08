using ProtoBuf;
using ProtoShared;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer.Data
{
    public class Mobile
    {
        [ProtoMember(1)]
        public Vector3D Location;

        [ProtoMember(2)]
        public List<Attrib> Attributes = new List<Attrib>();
        [ProtoMember(3)]
        public string Name;
        [ProtoMember(4)]
        public int Serial;

    }
}
