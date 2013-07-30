using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProtoShared.Data
{
     [ProtoContract]
    public class ToonWorldEntryData
    {
        public int GraphicID;

        public int NetworkID;

        public Vector3 Location;

        public List<Attributes> Attributes;

    }
}
