using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Database;
using BaseLib.Entities;
using CharServer.Network;
using CharServer.Configs;

namespace CharServer.Database
{
    public class CharDB : BaseDB
    {
        public static void UserDataExecute(string query, params object[] args)
        {
            CharDB.Connection = CharDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            CharDB.Execute(query, args);
            CharDB.Connection = null;
        }

        public static List<Dictionary<string, object>> UserDataQuery(string query, params object[] args)
        {
            CharDB.Connection = CharDB.Connect(UserDataDBConfig.Instance.Host, UserDataDBConfig.Instance.Username, UserDataDBConfig.Instance.Password, UserDataDBConfig.Instance.Database);
            var res = CharDB.Query(query, args);
            CharDB.Connection = null;
            return res;
        }
    }
}
