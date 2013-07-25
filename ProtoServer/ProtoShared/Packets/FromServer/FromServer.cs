using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using ProtoShared.Data;

namespace ProtoShared.Packets.FromServer
{
    public class LoginResponse : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int ResultCode;

        public Toon[] Characters;
    
        public LoginResponse() : base(ID){}
    }

    public class EnterWorld : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public string CharacterName;
        [ProtoMember(3)]
        public Vector3 CharLocation;

        public EnterWorld() : base(ID) { }
    }
}
