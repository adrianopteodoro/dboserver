using System.Collections.Generic;
using BaseLib.Database;
using CommunityServer.Configs;

namespace CommunityServer.Database
{
    class CommDB : BaseDB
    {
        public static void UserDataExecute(string query, params object[] args)
        {
            CommDB.Connection = CommDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            CommDB.Execute(query, args);
            CommDB.Connection = null;
        }

        public static List<Dictionary<string, object>> UserDataQuery(string query, params object[] args)
        {
            CommDB.Connection = CommDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            var res = CommDB.Query(query, args);
            CommDB.Connection = null;
            return res;
        }
    }
}
