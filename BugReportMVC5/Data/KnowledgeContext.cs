using System;
using System.Collections.Generic;
using BugReportMVC5.Data;
using MySql.Data.MySqlClient;

namespace BugReportMVC5.Models
{
    public class KnowledgeContext : Connection
    {
        //public string ConnectionString { get; set; }

        public KnowledgeContext(string connectionString) : base(connectionString)
        {

        }


        public List<Knowledge> GetAllSolutions()
        {
            List<Knowledge> list = new List<Knowledge>();


            //conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from knowledge_base_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Knowledge()
                    {
                        knowledge_base_id = Convert.ToInt32(reader["knowledge_base_id"]),
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        description = reader["description"].ToString(),
                        ticket_no = Convert.ToInt32(reader["ticket_no"])
                    });
                }
            }

            return list;
        }


        public List<Knowledge> GetAllComments(Int32 ticketNo)
        {
            List<Knowledge> list = new List<Knowledge>();
            Console.WriteLine("Inside the get comments-------------------------------------------------");

            Console.WriteLine("inside the knowledge context.....");

            MySqlCommand cmd = new MySqlCommand("select * from knowledge_base_table WHERE ticket_no = " + ticketNo, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Knowledge()
                    {
                        knowledge_base_id = Convert.ToInt32(reader["knowledge_base_id"]),
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        description = reader["description"].ToString(),
                        ticket_no = Convert.ToInt32(reader["ticket_no"]),
                    });
                }
            }

            return list;
        }

        public void AssignSolution(string solution, int ticketNo)
        {



            String query = "INSERT INTO `knowledge_base_table`(`user_id`,`solution`) VALUES(" + ticketNo + ",'" + solution + "')";
            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

            cmd.ExecuteNonQuery();
            cmd.Dispose();



        }
    }
}
 