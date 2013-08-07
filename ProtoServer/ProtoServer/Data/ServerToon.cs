using ProtoBuf;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ProtoServer.Data
{
    [ProtoContract, ProtoInclude(400,typeof(Toon))]
    public class ServerToon : Toon
    {
        

        public ServerToon(DataBase.AccountDBDataSet.charactersRow row) {
            Name = row.name;
            Serial = row.toonid;
            
        }

        public ServerToon() {
            // TODO: Complete member initialization
        }

        public static ServerToon LoadDataBlob(byte[] p) {
            return Serializer.DeserializeWithLengthPrefix<ServerToon>(new MemoryStream(p), PrefixStyle.Base128);
        }

        internal byte[] GetData() {
            using(var stream = new MemoryStream()){
                Serializer.SerializeWithLengthPrefix<ServerToon>(stream,this,PrefixStyle.Base128);
                return stream.GetBuffer();
            }
        }
    }
}
