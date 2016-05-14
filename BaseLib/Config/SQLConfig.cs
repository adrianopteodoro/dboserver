using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib.Configs
{
    public sealed class SQLConfig : Config
    {
        public string Host { get { return this.GetString("Host", "localhost"); } set { this.Set("Host", value); } }
        public string Username { get { return this.GetString("Username", ""); } set { this.Set("Username", value); } }
        public string Password { get { return this.GetString("Password", ""); } set { this.Set("Password", value); } }
        public string Database { get { return this.GetString("Database", ""); } set { this.Set("Database", value); } }

        private static readonly SQLConfig _instance = new SQLConfig();
        public static SQLConfig Instance { get { return _instance; } }
        private SQLConfig() : base("Database") { }
    }
}
