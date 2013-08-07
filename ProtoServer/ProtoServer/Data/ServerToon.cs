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

        public static Toon LoadDataBlob(byte[] p) {
            return Serializer.DeserializeWithLengthPrefix<Toon>(new MemoryStream(p), PrefixStyle.Base128);
        }

        
    }
}
