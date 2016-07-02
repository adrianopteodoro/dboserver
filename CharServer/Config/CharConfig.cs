using System;
using BaseLib.Configs;

namespace CharServer.Configs
{
    public sealed class CharConfig : Config
    {
        public string BindIP { get { return this.GetString("BindIP", "0.0.0.0"); } set { this.Set("BindIP", value); } }
        public int Port { get { return this.GetInt("Port", 50300); } set { this.Set("Port", value); } }
        public int GameServerCount { get { return this.GetInt("GameServerCount", 1); } set { this.Set("GameServerCount", value); } }

        public string GetGameServerName(int id)
        {
            return this.GetString(String.Format("GameServer{0}_Name", id), "[DBOSERVER]");
        }

        public int GetGameServerChannelCount(int id)
        {
            return this.GetInt(String.Format("GameServer{0}_ChannelCount", id), 1);
        }

        public string GetGameServerIP(byte ServerID, byte ChannelID)
        {
            return GetString(String.Format("GameServer{0}_Channel{1}_IP", ServerID, ChannelID), "127.0.0.1");
        }

        public ushort GetGameServerPort(byte ServerID, byte ChannelID)
        {
            return (ushort)GetInt(String.Format("GameServer{0}_Channel{1}_Port", ServerID, ChannelID), 50400);
        }

        private static readonly CharConfig _instance = new CharConfig();
        public static CharConfig Instance { get { return _instance; } }
        private CharConfig() : base("CharServer") { }
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
