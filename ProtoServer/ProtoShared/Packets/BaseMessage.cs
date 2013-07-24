using ProtoBuf;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoShared.Packets
{
    /// <summary>
    /// Enum of Packets, new packets must be added here and to GameMessage on Client and Server.
    /// </summary>
    [ProtoContract]
    public enum PacketType
    {
        LoginRequest = 1,
        LoginResponse = 2,
        SyncClient = 3
    }



    [ProtoContract, ProtoInclude(200, typeof(LoginRequest)), ProtoInclude(201, typeof(LoginResponse)),
   ProtoInclude(202, typeof(SyncClient))]
    public class BaseMessage
    {
        [ProtoMember(1)]
        public PacketType PacketType;

        public BaseMessage(PacketType type) {
            PacketType = type;
        }
        public BaseMessage() {
        }

    }
}
