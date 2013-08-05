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
        [ProtoMember(1)]
        public Vector3D Location;

         [ProtoMember(2)]
        public List<Attributes> Attributes;
        [ProtoMember(3)]
         public string Name;
        [ProtoMember(4)]
         public int Serial;


      


       
    }
}
