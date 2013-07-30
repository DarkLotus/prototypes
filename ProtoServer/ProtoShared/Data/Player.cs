using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using System.IO;
using ProtoShared.Data;

namespace ProtoShared
{
    [ProtoContract]
    public class Player
    {
        [ProtoMember(1)]
        public int Serial;
        [ProtoMember(2)]
        public Vector3 Location;
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public int GraphicID;
        [ProtoMember(5)]
        public List<Attributes> Attributes;



        public Player()
        { }



        public Stream Stream { get; set; }
    }



    

  
}
