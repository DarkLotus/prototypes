using ProtoBuf;
using ProtoBuf.Meta;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ProtoShared.Packets
{

    public static class MessageTypes {
        public static void Init() {
            var outstream = new System.IO.StreamWriter("OpCodes.cs", false);
            outstream.Write("namespace ProtoShared\n{\npublic static class OpCodes\n{\n");
            int i = 100;
            foreach(var t in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(BaseMessage))).OrderBy(type => type.Name)){
                FieldInfo info = t.GetField("ID", BindingFlags.Public | BindingFlags.Static);
                info.SetValue(null, (Int16)i);
               
                Console.WriteLine("Added Message: " + t.Name + " With NetworkID: " + i);
                try {
                    UnityEngine.Debug.Log("Added Message: " + t.Name + " With NetworkID: " + i);
                }
                catch { }
                RuntimeTypeModel.Default.Add(typeof(BaseMessage),true).AddSubType(i,t);
                var name = "";
                if (t.FullName.Contains("FromServer."))
                    name += "S_";
                else if (t.FullName.Contains("FromClient."))
                    name += "C_";
                else
                    name += "S_";
                name += t.Name;
                outstream.WriteLine("public const short " + name + " = " + i + ";");
                i++;
       
        }
            outstream.WriteLine("}\n}");
            outstream.Close();
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
        public Int16 PacketType;

        public BaseMessage(Int16 ID) {
            PacketType = ID;
        }
        public BaseMessage() {
        }

        

        public void Send(Stream stream) {
            var t = this.GetType();
            if (stream == null || !stream.CanWrite)
                return;
            try {
                Serializer.SerializeWithLengthPrefix<BaseMessage>(stream, this, PrefixStyle.Base128);
            }
            catch { Logger.Log("Message failed to send: " + this.ToString());return; }
            stream.Flush();
            Logger.Log("Sent" + this.ToString()) ;
        }

        public override string ToString() {
            string str = this.GetType().Name + ">";
            foreach (var x in this.GetType().GetFields())
                str += x.Name + ":" + x.GetValue(this) + ",";
            return str;
        }

    }
}
