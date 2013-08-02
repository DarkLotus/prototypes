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
    public class LoginResponse : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public int ResultCode;
        [ProtoMember(3)]
        public string[] Characters;

    
        public LoginResponse() : base(ID){}

        public LoginResponse(string[] chars) {
            // TODO: Complete member initialization
            this.Characters = chars;
            ResultCode = 1;
        }

     
    }

   
}
