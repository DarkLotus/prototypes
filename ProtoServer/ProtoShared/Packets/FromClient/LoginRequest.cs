﻿using ProtoBuf;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class LoginRequest : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
         [ProtoMember(2)]
        public string UserName;
         [ProtoMember(3)]
        public string Password;
        public LoginRequest() : base(ID){}

      
    }
}
