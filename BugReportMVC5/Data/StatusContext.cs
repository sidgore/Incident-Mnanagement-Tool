using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using BugReportMVC5.Data;

namespace BugReportMVC5.Models
{
    public class StatusContext : Connection
    {



        public StatusContext(string connectionString) : base(connectionString)
        {

        }


        public List<Status> GetAllStatus()
        {
            List<Status> list = new List<Status>();



            MySqlCommand cmd = new MySqlCommand("select * from status_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Status()
                    {
                        Status_id = Convert.ToInt32(reader["status_id"]),
                        Status_name = reader["status_name"].ToString()
                    });
                }


            }
             
            return list;
        }

    }
}