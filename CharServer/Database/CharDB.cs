using System.Collections.Generic;
using BaseLib.Database;
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

        public static int InsertCharacter(uint AccID, byte ServerID, string Name, byte Race, byte Class, byte Gender, byte Face, byte Hair, byte HairColor, byte SkinColor)
        {
            int _res = 0;
            //
            var res = CharDB.UserDataQuery("CALL `DFInsertCharacter`('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');", AccID, ServerID, Name, Race, Class, Gender, Face, Hair, HairColor, SkinColor);
            foreach (var r in res[0]) _res = (int)r.Value;
            return _res;
        }
    }
}
