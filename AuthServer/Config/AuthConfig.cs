using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib.Configs;

namespace AuthServer.Configs
{
    public sealed class AuthConfig : Config
    {
        public string BindIP { get { return this.GetString("BindIP", "0.0.0.0"); } set { this.Set("BindIP", value); } }
        public int Port { get { return this.GetInt("Port", 50200); } set { this.Set("Port", value); } }
        public int CharServerCount { get { return this.GetInt("CharServerCount", 1); } set { this.Set("CharServerCount", value); } }

        public KeyValuePair<int, string> GetCharServerAddress(int id)
        {
            return new KeyValuePair<int, string>(this.GetInt(String.Format("CharServer{0}_Port", id), 6999), this.GetString(String.Format("CharServer{0}_IP", id), "127.0.0.1"));
        }
 
           
        private static readonly AuthConfig _instance = new AuthConfig();
        public static AuthConfig Instance { get { return _instance; } }
        private AuthConfig() : base("AuthServer") { }
    }

    public sealed class UserDataDBConfig : Config
    {
        public string Host { get { return this.GetString("Host", "localhost"); } set { this.Set("Host", value); } }
        public string Username { get { return this.GetString("Username", ""); } set { this.Set("Username", value); } }
        public string Password { get { return this.GetString("Password", ""); } set { this.Set("Password", value); } }
        public string Database { get { return this.GetString("Database", ""); } set { this.Set("Database", value); } }

        private static readonly UserDataDBConfig _instance = new UserDataDBConfig();
        public static UserDataDBConfig Instance { get { return _instance; } }
        private UserDataDBConfig() : base("UserDatabase") { }
    }

    public sealed class GameDataDBConfig : Config
    {
        public string Host { get { return this.GetString("Host", "localhost"); } set { this.Set("Host", value); } }
        public string Username { get { return this.GetString("Username", ""); } set { this.Set("Username", value); } }
        public string Password { get { return this.GetString("Password", ""); } set { this.Set("Password", value); } }
        public string Database { get { return this.GetString("Database", ""); } set { this.Set("Database", value); } }

        private static readonly GameDataDBConfig _instance = new GameDataDBConfig();
        public static GameDataDBConfig Instance { get { return _instance; } }
        private GameDataDBConfig() : base("GameDatabase") { }
    }
}
