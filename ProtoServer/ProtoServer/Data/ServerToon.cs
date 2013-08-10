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
    public class ServerToon : Toon
    {
        

       
        public ServerToon() {
        }

       /* public static Toon LoadDataBlob(byte[] p) {
            return Serializer.DeserializeWithLengthPrefix<Toon>(new MemoryStream(p), PrefixStyle.Base128);
        }*/



        internal static Toon LoadDataBlob(DataBase.AccountDBDataSet.charactersRow row) {
            Toon t = Serializer.DeserializeWithLengthPrefix<Toon>(new MemoryStream(row.serialized_data), PrefixStyle.Base128);
            t.Serial = row.toonid;
            t.Name = row.name;
            return t;
        }
    }
}
