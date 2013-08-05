using ProtoServer.Data;
using ProtoShared;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer.DataBase
{
    public static class Database
    {
        static AccountDBDataSetTableAdapters.accountsTableAdapter _accounts;
        static AccountDBDataSetTableAdapters.charactersTableAdapter _characters;

        internal static Account LoadAccount(string p1, string p2) {
            if (_accounts == null) _accounts = new AccountDBDataSetTableAdapters.accountsTableAdapter();
            var data = _accounts.GetAccountID(p1, p2);
            if (data.Rows.Count == 0) { 
            Logger.Log("No User found in DB, Creating...");
            _accounts.Insert(p1, p2);

            return LoadAccount(p1, p2);
                
            }
            Logger.Log("Found User..." + p1 + "  " + data[0].id);
            Account a = new Account();
            a.UserName = p1;
            a.Serial = data[0].id;
            LoadToonsForAccountID(a);
            return a;
            
        }
        internal static void LoadAccountInto(Account a,string p1, string p2) {
            if (_accounts == null) _accounts = new AccountDBDataSetTableAdapters.accountsTableAdapter();
            var data = _accounts.GetAccountID(p1, p2);
            if (data.Rows.Count == 0) {
                Logger.Log("No User found in DB, Creating...");
                _accounts.Insert(p1, p2);

                LoadAccountInto(a, p1, p2);
                return;

            }
            Logger.Log("Found User..." + p1 + "  " + data[0].id);
            a.UserName = p1;
            a.Serial = data[0].id;
            LoadToonsForAccountID(a);
            return;
        }

        private static void LoadToonsForAccountID(Account a) {
            if (_characters == null) _characters = new AccountDBDataSetTableAdapters.charactersTableAdapter();
            var data = _characters.GetToonsByOwnerID(a.Serial);
            List<ServerToon> list = new List<ServerToon>();
            foreach (var row in data)
                a.Toons.Add(new ServerToon(row));

        }




       

        internal static ServerToon CreateToon(Account p, ProtoShared.Packets.FromClient.CreateCharacter createCharacter) {
            if (_characters == null) _characters = new AccountDBDataSetTableAdapters.charactersTableAdapter();
            ServerToon toon = new ServerToon();
            toon.Name = createCharacter.Name;
            toon.Location = new Vector3D(905, 13, 593);
            _characters.Insert(p.Serial, toon.Name, toon.GetData());
            LoadToonsForAccountID(p);
            return p.Toons.Where(t => t.Name.Equals(createCharacter.Name)).First();
        }
    }
}
