using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace ProtoShared.Packets.FromServer
{
    public class LoginResponse : ProtoShared.Packets.BaseMessage
    {
        [ProtoMember(2)]
        public int ResultCode;
        
    }

    public class EnterWorld : ProtoShared.Packets.BaseMessage
    {
        [ProtoMember(2)]
        public string CharacterName;
        [ProtoMember(3)]
        public Vector3 CharLocation;
    }
}
