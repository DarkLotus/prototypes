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

        internal static int LoadAccount(string p1, string p2) {
            if (_accounts == null) _accounts = new AccountDBDataSetTableAdapters.accountsTableAdapter();
            var data = _accounts.GetAccountID(p1, p2);
            if (data.Rows.Count == 0) { 
            Logger.Log("No User found in DB, Creating...");
            _accounts.Insert(p1, p2);
            return LoadAccount(p1, p2);
                
            }
            return data[0].id;
            
        }

        internal static string[] LoadCharactersForAccountID(int accountid) {
            if (_characters == null) _characters = new AccountDBDataSetTableAdapters.charactersTableAdapter();
            var data = _characters.GetCharacterNames(accountid);
            string[] chars = new string[data.Rows.Count];
            int cnt = 0;
            foreach (var row in data) { chars[cnt++] = row.name; }
            if (chars.Length < 1) return new string[] { "testAutoGen" };
            return chars;
        }
    }
}
