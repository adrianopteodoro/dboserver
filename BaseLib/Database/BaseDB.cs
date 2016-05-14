/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 1/12/2011
 * Hora: 16:30
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using BaseLib.Configs;

namespace BaseLib.Database
{
	/// <summary>
	/// Description of Database.
	/// </summary>
    public partial class BaseDB
    {
        private static MySqlConnection m_con;
        public static MySqlConnection Connection
        {
            get
            {
                if (m_con == null || m_con.State != ConnectionState.Open || m_con.Database == "")
                    m_con = Connect();
                return m_con;
            }
            set { m_con = value; }
        }

        static BaseDB()
        {
            Connection = Connect();
        }

        /// <summary>
        /// Connect to the mysql db
        /// </summary>
        /// <returns>A connection to the database</returns>
        public static MySqlConnection Connect()
        {
            return BaseDB.Connect(SQLConfig.Instance.Host, SQLConfig.Instance.Username, SQLConfig.Instance.Password, SQLConfig.Instance.Database);
        }

        public static MySqlConnection Connect(string server, string user, string pwd, string database)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};", server, user, pwd, database));
                conn.Open();
                return conn;
            }
            catch (MySqlException)
            {
                return null;
            }
        }

        public static void Execute(string query, params object[] args)
        {
            MySqlCommand cmd = new MySqlCommand(String.Format(query, args), BaseDB.Connection);
            cmd.ExecuteNonQuery();
        }

        public static List<Dictionary<string, object>> Query(string query, params object[] args)
        {
            MySqlCommand cmd = new MySqlCommand(String.Format(query, args), BaseDB.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader.GetName(i), reader.GetValue(i));
                }
                rows.Add(row);
            }
            reader.Close();

            return rows;
        }
    }
}
