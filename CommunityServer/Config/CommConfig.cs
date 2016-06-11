using BaseLib.Configs;

namespace CommunityServer.Configs
{
    public sealed class CommConfig : Config
    {
        public string BindIP { get { return this.GetString("BindIP", "0.0.0.0"); } set { this.Set("BindIP", value); } }
        public int Port { get { return this.GetInt("Port", 50700); } set { this.Set("Port", value); } }

        private static readonly CommConfig _instance = new CommConfig();
        public static CommConfig Instance { get { return _instance; } }
        private CommConfig() : base("CommunityServer") { }
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
