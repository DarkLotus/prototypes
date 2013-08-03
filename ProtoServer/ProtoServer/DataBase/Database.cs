using ProtoServer.Data;
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
            a.Toons = LoadToonsForAccountID(a);
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
            a.Toons = LoadToonsForAccountID(a);
            return;
        }

        private static List<ServerToon> LoadToonsForAccountID(Account a) {
            if (_characters == null) _characters = new AccountDBDataSetTableAdapters.charactersTableAdapter();
            var data = _characters.GetToonsByOwnerID(a.Serial);
            List<ServerToon> list = new List<ServerToon>();
            foreach (var row in data)
                list.Add(new ServerToon(row));
            return list;

        }




        internal static void CreateToon(Account account) {
            if (_characters == null) _characters = new AccountDBDataSetTableAdapters.charactersTableAdapter();
            ServerToon toon = new ServerToon();
            toon.Name = account.UserName + "Char";
            toon.Location = new UnityEngine.Vector3(905, 13, 593);
            _characters.Insert(account.Serial, toon.Name, toon.GetData());
        }

        internal static void CreateToon(Account p, ProtoShared.Packets.FromClient.CreateCharacter createCharacter) {
            throw new NotImplementedException();
        }
    }
}
