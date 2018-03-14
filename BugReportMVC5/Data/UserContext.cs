using System;
using System.Collections.Generic;

using BugReportMVC5.Data;
using MySql.Data.MySqlClient;

namespace BugReportMVC5.Models
{
    public class UserContext : Connection
    {


        public UserContext(string connectionString) : base(connectionString)
        {

        }


        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();



            MySqlCommand cmd = new MySqlCommand("select * from user_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = Convert.ToInt64(reader["phone_number"]),

                        customer_id = Convert.ToInt32(reader["customer_id"]),
                        role_id = Convert.ToInt32(reader["role_id"]),
                        password = reader["password"].ToString()
                    });
                }
            }

            return list;
        }




        public List<User> GetAllLocalUsers(int customer_id)
        {
            List<User> list = new List<User>();


            MySqlCommand cmd = new MySqlCommand("select * from user_table where customer_id = " + customer_id, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = Convert.ToInt64(reader["phone_number"]),

                        customer_id = Convert.ToInt32(reader["customer_id"]),
                        role_id = Convert.ToInt32(reader["role_id"]),
                        password = reader["password"].ToString()
                    });
                }
            }

            return list;
        }




        public List<User> FindUser(String username)
        {
            List<User> list = new List<User>();



            MySqlCommand cmd = new MySqlCommand("select * from user_table where user_name='" + username + "'", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = Convert.ToInt64(reader["phone_number"]),

                        customer_id = Convert.ToInt32(reader["customer_id"]),
                        role_id = Convert.ToInt32(reader["role_id"]),
                        password = reader["password"].ToString()
                    });
                }
            }

            return list;
        }
        // MySqlCommand cmd = new MySqlCommand("select * from user_table where user_name='" + user_name + "'", conn);

        public void createUser(User user)
        {


            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES ("+c.Customer_name+")", conn);
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_id`,`customer_name`) VALUES(6, \"Ram\")",conn);S
            String name = user.user_name;
            String query = "INSERT INTO `bug_tracking_system`.`user_table`(`user_name`,`email`,`phone_number`,`customer_id`,`role_id`,`password`) VALUES('" + name + "','" + user.email + "'," + user.phone_number + "," + user.customer_id + "," + user.role_id + ",'" + user.password + "')";
            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

            cmd.ExecuteNonQuery();
            cmd.Dispose();



        }





        public void deleteUser(int user_id)
        {
            
            string query1 = "Delete From knowledge_base_table where user_id = " + user_id + ""; 
            MySqlCommand cmd = new MySqlCommand(query1, this.MySQLConnection);

            cmd.ExecuteNonQuery();
            string query2 = "Delete From problem_table where user_id = " + user_id + ""; 

            cmd = new MySqlCommand(query2, this.MySQLConnection);
            cmd.ExecuteNonQuery();

            string query3 = "Delete From user_table where user_id = " + user_id + ""; 

            cmd = new MySqlCommand(query3, this.MySQLConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();


        }
        public List<User> GetAllDevelopers()
        {
            List<User> developerList = new List<User>();


            MySqlCommand cmd = new MySqlCommand("select * from user_table where role_id=" + 2, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    developerList.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = Convert.ToInt64(reader["phone_number"]),

                        customer_id = Convert.ToInt32(reader["customer_id"]),
                        role_id = Convert.ToInt32(reader["role_id"]),
                        password = reader["password"].ToString()
                    });
                }
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();


            return developerList;
        }

        public void AssignDeveloper(string developerName, int ticketNo)
        {


            String query = "UPDATE `problem_table` SET `status_id` = " + 2 + ", `assignee_user_id`= (SELECT user_id FROM user_table WHERE user_name = '" + developerName + "') WHERE `ticket_number` =" + ticketNo;
            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);
            cmd.ExecuteNonQuery();

            cmd.Dispose();





        }

        public void DeveloperSolution(string userName, int ticketNo, int user_id, string solution)
        {


            //String query = "UPDATE `problem_table` SET `status_id` = " + 3 + ", `assignee_user_id`= (SELECT user_id FROM user_table WHERE user_name = '" + developerName + "') WHERE `ticket_number` =" + ticketNo;
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //cmd.ExecuteNonQuery();
            //cmd.Dispose();
            String query1 = "INSERT INTO `bug_tracking_system`.`knowledge_base_table`(`user_id`,`user_name`,`description`,`ticket_no`, `comment_date`)VALUES(" + user_id + ",'" + userName + "','" + solution + "'," + ticketNo + ", NOW())";
            MySqlCommand cmd1 = new MySqlCommand(query1, this.MySQLConnection);
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();



        }

        public void updateProfile(User user)
        {


            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES ("+c.Customer_name+")", conn);
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_id`,`customer_name`) VALUES(6, \"Ram\")",conn);S
            //  String name = user.user_name;
            //  String query = "INSERT INTO `bug_tracking_system`.`user_table`(`user_name`,`email`,`phone_number`,`point_of_contact`,`customer_id`,`role_id`,`password`) VALUES('" + name + "','" + user.email + "'," + user.phone_number + ",'" + user.point_of_contact + "'," + user.customer_id + "," + user.role_id + ",'" + user.password + "')";

            String query = "UPDATE bug_tracking_system.user_table SET email ='" + user.email + "',password = '" + user.password + "' WHERE user_name ='" + user.user_name + "'";



            MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

            cmd.ExecuteNonQuery();
            cmd.Dispose();



        } 


        //Get the developer info to display in Ticket information page
        public List<User> GetDeveloperInfo(Int32 developerId)
        {
            List<User> developerList = new List<User>();

            MySqlCommand cmd = new MySqlCommand("select * from user_table where user_id = " + developerId + " AND role_id =" + 2, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    developerList.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = Convert.ToInt64(reader["phone_number"]),

                        customer_id = Convert.ToInt32(reader["customer_id"]),
                        role_id = Convert.ToInt32(reader["role_id"]),
                    });
                }
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();


            return developerList;
        }
    }
}
