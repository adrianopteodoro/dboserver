using System;
using System.Collections.Generic;
using BaseLib.Configs;

namespace AuthServer.Configs
{
    public sealed class AuthConfig : Config
    {
        public string BindIP { get { return GetString("BindIP", "0.0.0.0"); } set { Set("BindIP", value); } }
        public int Port { get { return GetInt("Port", 50200); } set { Set("Port", value); } }
        public int CharServerCount { get { return GetInt("CharServerCount", 1); } set { Set("CharServerCount", value); } }

        public KeyValuePair<int, string> GetCharServerAddress(int id)
        {
            return new KeyValuePair<int, string>(GetInt(String.Format("CharServer{0}_Port", id), 6999), GetString(String.Format("CharServer{0}_IP", id), "127.0.0.1"));
        }
 
           
        private static readonly AuthConfig _instance = new AuthConfig();
        public static AuthConfig Instance { get { return _instance; } }
        private AuthConfig() : base("AuthServer") { }
    }

    public sealed class UserDataDBConfig : Config
    {
        public string Host { get { return GetString("Host", "localhost"); } set { Set("Host", value); } }
        public string Username { get { return GetString("Username", ""); } set { Set("Username", value); } }
        public string Password { get { return GetString("Password", ""); } set { Set("Password", value); } }
        public string Database { get { return GetString("Database", ""); } set { Set("Database", value); } }

        private static readonly UserDataDBConfig _instance = new UserDataDBConfig();
        public static UserDataDBConfig Instance { get { return _instance; } }
        private UserDataDBConfig() : base("UserDatabase") { }
    }

    public sealed class GameDataDBConfig : Config
    {
        public string Host { get { return GetString("Host", "localhost"); } set { Set("Host", value); } }
        public string Username { get { return GetString("Username", ""); } set { Set("Username", value); } }
        public string Password { get { return GetString("Password", ""); } set { Set("Password", value); } }
        public string Database { get { return GetString("Database", ""); } set { Set("Database", value); } }

        private static readonly GameDataDBConfig _instance = new GameDataDBConfig();
        public static GameDataDBConfig Instance { get { return _instance; } }
        private GameDataDBConfig() : base("GameDatabase") { }
    }
}
