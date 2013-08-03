using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets.Shared
{
    /// <summary>
    /// Sent when a mobile has moved
    /// </summary>
    public class ChatMessage : ProtoShared.Packets.BaseMessage
    {
        public static Int16 ID;
        [ProtoMember(2)]
        public string Sender;

        [ProtoMember(3)]
        public string Message;


        public ChatMessage() : base(ID) { }
    }
}