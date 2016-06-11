using System.Collections.Generic;
using BaseLib.Database;
using GameServer.Configs;

namespace GameServer.Database
{
    class GameDB : BaseDB
    {
        public static void UserDataExecute(string query, params object[] args)
        {
            GameDB.Connection = GameDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            GameDB.Execute(query, args);
            GameDB.Connection = null;
        }

        public static List<Dictionary<string, object>> UserDataQuery(string query, params object[] args)
        {
            GameDB.Connection = GameDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            var res = GameDB.Query(query, args);
            GameDB.Connection = null;
            return res;
        }
    }
}
