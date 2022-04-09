using System.Collections.Generic;
using BaseLib.Database;
using AuthServer.Configs;

namespace AuthServer.Database
{
    public class AuthDB : BaseDB
    {
        public static int CheckAccount(string username, string password)
        {
            int _res = 0;
            var res = UserDataQuery("CALL `getAccountAuthResult`('{0}', '{1}');", username, password);
            foreach (var r in res[0]) _res = (int)r.Value;
            return _res;
        }

        public static int GetAccountID(string username)
        {
            int _res = 0;
            var res = UserDataQuery("CALL `getAccountID`('{0}');", username);
            foreach (var r in res[0]) _res = (int)r.Value;
            return _res;
        }

        public static (int lastServerID, int lastChannelID) GetLastConnInfo(uint accountid)
        {
            var res = UserDataQuery("CALL `getAccountLastConnInfo`('{0}');", accountid);
            if (res.Count != 0) return ((int)res[0]["lastServerID"], (int)res[0]["lastChannelID"]);
            return (255, 255);
        }


        public static void UserDataExecute(string query, params object[] args)
        {
            Connection = Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            Execute(query, args);
            Connection = null;
        }

        public static List<Dictionary<string, object>> UserDataQuery(string query, params object[] args)
        {
            Connection = Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            var res = Query(query, args);
            Connection = null;
            return res;
        }
    }
}
