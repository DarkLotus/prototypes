using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.FromClient
{
    [ProtoContract]
    public class LoginRequest : ProtoShared.Packets.BaseMessage
    {
         [ProtoMember(2)]
        public string UserName;
         [ProtoMember(3)]
        public string Password;
        public LoginRequest() : base(PacketType.LoginRequest){}
    }
}
