using System;
using System.Collections.Generic;

using BugReportMVC5.Data;
using MySql.Data.MySqlClient;

namespace BugReportMVC5.Models
{
    public class CustomerContext : Connection
    {
        public CustomerContext(string connectionString) : base(connectionString)
        {
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();


            MySqlCommand cmd = new MySqlCommand("select * from customer_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Customer()
                    {
                        Customer_id = Convert.ToInt32(reader["customer_id"]),
                        Customer_name = reader["customer_name"].ToString()
                    });
                }

            }
            return list;
        }

        //List of all companies excluding N3N
        public List<Customer> GetOnlyCustomers()
        {
            List<Customer> list = new List<Customer>();


            MySqlCommand cmd = new MySqlCommand("select * from customer_table where customer_id > 0", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Customer()
                    {
                        Customer_id = Convert.ToInt32(reader["customer_id"]),
                        Customer_name = reader["customer_name"].ToString()




                    });
                    System.Diagnostics.Debug.WriteLine("Problem is---------------------GetOnlyCustomers[GET]----------------- " + reader["customer_name"].ToString());
                }

            }
            return list;
        }



        public int createCustomer(Customer c)
        {

            try
            {
                // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES ("+c.Customer_name+")", conn);
                // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_id`,`customer_name`) VALUES(6, \"Ram\")",conn);S
                String name = c.Customer_name;
                String query = "INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES('" + name + "')";
                MySqlCommand cmd = new MySqlCommand(query, this.MySQLConnection);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return 0;
            }
            catch (MySqlException e)
            {
                return e.Number;
               // System.Diagnostics.Debug.WriteLine(e.Message.ToString());
            }


        } 


        public List<Customer> FindCustomer(String customername)
        {
            List<Customer> list = new List<Customer>();


            MySqlCommand cmd = new MySqlCommand("select * from customer_table where customer_name='" + customername + "'", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Customer()
                    {
                        Customer_id = Convert.ToInt32(reader["customer_id"]),
                        Customer_name = reader["customer_name"].ToString()

                    });
                }
            }

            return list;
        }
    }
}
