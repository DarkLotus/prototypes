using ProtoBuf;
using ProtoBuf.Meta;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProtoShared.Packets
{

    public static class MessageTypes {
        public static void Init() {
            int i = 100;
            foreach(var t in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(BaseMessage)))){
                RuntimeTypeModel.Default.Add(typeof(BaseMessage),true).AddSubType(i++,t);   
        }
        }
    }

    /// <summary>
    /// Enum of Packets, new packets must be added here and to GameMessage on Client and Server.
    /// </summary>
    [ProtoContract]
    public enum PacketType
    {
        LoginRequest = 1,
        LoginResponse = 2,
        SyncClient = 3,
        EnterWorld =4
    }


    //, ProtoInclude(200, typeof(LoginRequest)), ProtoInclude(201, typeof(LoginResponse)), ProtoInclude(202, typeof(SyncClient))
    [ProtoContract]
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
