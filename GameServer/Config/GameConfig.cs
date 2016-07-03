using BaseLib.Configs;

namespace GameServer.Configs
{
    public sealed class GameConfig : Config
    {
        public string BindIP { get { return GetString("BindIP", "0.0.0.0"); } set { Set("BindIP", value); } }
        public int Port { get { return GetInt("Port", 50400); } set { Set("Port", value); } }
        public string CommunityServerIP { get { return GetString("CommunityServer_IP", "127.0.0.1"); } set { Set("CommunityServer_IP", value); } }
        public ushort CommunityServerPort { get { return (ushort)GetInt("CommunityServer_Port", 50700); } set { Set("CommunityServer_Port", value); } }

        private static readonly GameConfig _instance = new GameConfig();
        public static GameConfig Instance { get { return _instance; } }
        private GameConfig() : base("GameServer") { }
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
