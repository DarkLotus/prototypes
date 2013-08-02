using ProtoBuf;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProtoShared.Data
{
     [ProtoContract]
    public class Toon
    {
        public Vector3 Location;

        public List<Attributes> Attributes;
        public string Name;
        public int Serial;


      


       
    }
}
