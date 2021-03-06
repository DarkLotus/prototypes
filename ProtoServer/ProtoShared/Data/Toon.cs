﻿using ProtoBuf;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Attrib> Attributes = new List<Attrib>();
        [ProtoMember(3)]
         public string Name;
        [ProtoMember(4)]
         public int Serial;


         /// <summary>
         /// Serial of the Scene Toon is in
         /// </summary>
        [ProtoMember(5)]
        public int SceneSerial;


        public byte[] GetData() {
            using (var stream = new MemoryStream()) {
                Serializer.SerializeWithLengthPrefix<Toon>(stream, this, PrefixStyle.Base128);
                return stream.GetBuffer();
            }
        }

       
    }
}
