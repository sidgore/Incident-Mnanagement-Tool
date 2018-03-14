using System;
using MySql.Data.MySqlClient;

namespace BugReportMVC5.Data
{
    public class Connection : IDisposable
    {
        protected MySqlConnection MySQLConnection { get; set; }
        public Connection(string connectionString)
        {
            if (MySQLConnection == null)
            {
                MySQLConnection = new MySqlConnection(connectionString);
                MySQLConnection.Open();
            }


        }

        public void Dispose()
        {
            MySQLConnection.Close();
            MySQLConnection = null;
        }
    }
}
