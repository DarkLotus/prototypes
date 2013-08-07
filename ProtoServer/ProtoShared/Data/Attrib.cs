using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Data
{
     [ProtoContract]
    public class Attrib
    {
         [ProtoMember(1)]
         public AttribType ID;

         [ProtoMember(2)]
         public float Value;

    }

     public enum AttribType
     {
         Health, MaxHealth, Strength, Intelligence, Dexterity, Mining
     }
}
