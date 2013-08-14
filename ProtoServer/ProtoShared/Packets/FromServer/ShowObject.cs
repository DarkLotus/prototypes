using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using ProtoShared.Data;

namespace ProtoShared.Packets.FromServer
{
    [ProtoContract]
    public class ShowObject : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int Serial;

        [ProtoMember(2)]
        public int Prefab;

        //TODO
        /*
         what data to send?
         
         */

        public ShowObject() : base(ID) { }

       
    }
}
