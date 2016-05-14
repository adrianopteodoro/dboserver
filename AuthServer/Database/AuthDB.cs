using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib.Database;
using AuthServer.Configs;

namespace AuthServer.Database
{
    public class AuthDB : BaseDB
    {
        public static int CheckAccount(string username, string password)
        {
            int _res = 0;
            var res = AuthDB.UserDataQuery("CALL `getAccountAuthResult`('{0}', '{1}');", username, password);
            foreach (var r in res[0]) _res = (int)r.Value;
            return _res;
        }

        public static int GetAccountID(string username)
        {
            int _res = 0;
            var res = AuthDB.UserDataQuery("CALL `getAccountID`('{0}');", username);
            foreach (var r in res[0]) _res = (int)r.Value;
            return _res;
        }

        public static void UserDataExecute(string query, params object[] args)
        {
            AuthDB.Connection = AuthDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            AuthDB.Execute(query, args);
            AuthDB.Connection = null;
        }

        public static List<Dictionary<string, object>> UserDataQuery(string query, params object[] args)
        {
            AuthDB.Connection = AuthDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            var res = AuthDB.Query(query, args);
            AuthDB.Connection = null;
            return res;
        }
    }
}
