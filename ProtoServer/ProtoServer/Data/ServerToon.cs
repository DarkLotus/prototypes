using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ProtoServer.Data
{
    public class ServerToon : Toon
    {
        

        public ServerToon(DataBase.AccountDBDataSet.charactersRow row) {
            Name = row.name;
            Serial = row.toonid;
            LoadDataBlob(row.serialized_data);
        }

        public ServerToon() {
            // TODO: Complete member initialization
        }

        private void LoadDataBlob(byte[] p) {
            return;
        }

        internal byte[] GetData() {
            return new byte[0];
        }
    }
}
