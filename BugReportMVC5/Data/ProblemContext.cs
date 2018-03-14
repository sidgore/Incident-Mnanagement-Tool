using System;
using System.Collections.Generic;
using BugReportMVC5.Data;
using MySql.Data.MySqlClient;

namespace BugReportMVC5.Models
{


    //public class DataPoint
    //{
    //  public string Status_name;
    // public int count;

    //    public ViewStatus(string name, int c)
    //  {
    //    Status_name = name;
    // count = c;
    //}



    //  }


    public class ViewStatus
    {
        public string Status_name;
        public int count;

        public ViewStatus(string name, int c)
        {
            Status_name = name;
            count = c;
        }



    }

    public class ViewStatusByDeveloper
    {
        public string Developer_name;
        public int count;
        public ViewStatusByDeveloper(string name, int c)
        {
            Developer_name = name;
            count = c;
        }
    }

    public class ViewStatusByCustomer
    {
        public string Customer_name;
        public int count;
        public ViewStatusByCustomer(string name, int c)
        {
            Customer_name = name;
            count = c;
        }
    }




    public class ProblemContext : Connection
    {
        // public string ConnectionString { get; set; }

        public ProblemContext(string connectionString) : base(connectionString)
        {

        }


        public void Add(Problem p)
        {

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.User_Id);

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.Problem_Desc);

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.Summary);

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.Severity);

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.Status_Id);

            System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + p.Created_By);

            try
            {


                DateTime now = DateTime.Now;

                DateTime output = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                DateTime estimated;
                if (p.Severity == 1)
                {
                    estimated = DateTime.Now.AddHours(2);
                }
                else if (p.Severity == 2)
                {
                    estimated = DateTime.Now.AddHours(24);
                }
                else
                {
                    estimated = DateTime.Now.AddHours(72);
                }
                //estimated = new DateTime(output.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                System.Diagnostics.Debug.WriteLine(output.ToString("yyyy-MM-dd HH:mm:ss"));
                MySqlCommand cmd = new MySqlCommand("Insert into problem_table (`user_id`,`problem_desc`,`summary`,`severity`,`status_id`,`create_date`,`last_update_date`,`created_by`,`updated_by`, `module_id`,`estimated_complete_date`,`emailnotificationstatus`,`emaillist`)values(@User_Id, @Problem_Desc,@Summary,@Severity,@Status_Id,@Create_Date,@Last_Update_Date,@Created_By,@Updated_by, @Module_id,@Estimated_Complete_Date,@EmailNotificationstatus,@EmailList)", this.MySQLConnection);
                //  cmd.Parameters.AddWithValue("@Ticket_Number", p.Ticket_Number);
                //  cmd.Parameters.AddWithValue("@Assigneer_User_Id", null);
                cmd.Parameters.AddWithValue("@User_Id", p.User_Id);
                cmd.Parameters.AddWithValue("@Problem_Desc", p.Problem_Desc);
                cmd.Parameters.AddWithValue("@Summary", p.Summary);
                cmd.Parameters.AddWithValue("@Severity", p.Severity);
                //   cmd.Parameters.AddWithValue("@Attachment", p.Attachment);

                cmd.Parameters.AddWithValue("@Status_Id", 1);
                cmd.Parameters.AddWithValue("@Create_Date", output.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Last_Update_Date", output.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Created_By", p.Created_By);
                cmd.Parameters.AddWithValue("@Updated_by", p.User_Id);
                cmd.Parameters.AddWithValue("@Module_id", Convert.ToInt32(p.Module_Name));
                cmd.Parameters.AddWithValue("@Estimated_Complete_Date", estimated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EmailNotificationstatus", Convert.ToBoolean(p.EmailNotificationstatus));
                cmd.Parameters.AddWithValue("@EmailList", p.EmailList);


                System.Diagnostics.Debug.WriteLine($"Problem is-------------------------------------- {cmd}");
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error is "+e.Message);
            }



        }



        internal List<ViewStatus> GetAllTicketsForLocalUsers(int customer_id)
        {
            List<ViewStatus> list = new List<ViewStatus>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT  u.user_name, count(*) as numbers FROM problem_table as p INNER JOIN user_table as u ON p.user_id =u.user_id WHERE p.created_by =" + customer_id + " GROUP BY p.user_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatus(reader["user_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("User is" + reader["user_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }




        internal List<ViewStatus> GetAllTicketsForADeveloper(int developer_id)
        {
            List<ViewStatus> list = new List<ViewStatus>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT s.status_name, count(*) as numbers FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id WHERE p.assignee_user_id = " + developer_id + " GROUP BY p.status_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatus(reader["status_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Status is" + reader["status_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }



        internal List<ViewStatus> GetAllTicketsForAUser(int user_id)
        {
            List<ViewStatus> list = new List<ViewStatus>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT s.status_name, count(*) as numbers FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id WHERE p.user_id = " + user_id + " GROUP BY p.status_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatus(reader["status_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Status is" + reader["status_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }



        internal List<ViewStatus> GetAllTicketsForACustomer(int customer_id)
        {
            List<ViewStatus> list = new List<ViewStatus>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT s.status_name, count(*) as numbers FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id WHERE p.created_by = " + customer_id + " GROUP BY p.status_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatus(reader["status_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Status is" + reader["status_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }

        public List<ViewStatusByCustomer> GetAllTicketsByCustomers()
        {
            List<ViewStatusByCustomer> list = new List<ViewStatusByCustomer>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT c.customer_name, count(*) as numbers FROM problem_table as p INNER JOIN customer_table as c ON p.created_by = c.customer_id GROUP BY p.created_by;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatusByCustomer(reader["customer_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Name is" + reader["customer_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }

        public List<ViewStatusByDeveloper> GetAllTicketsByDeveloper()
        {
            List<ViewStatusByDeveloper> list = new List<ViewStatusByDeveloper>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT u.user_name, count(*) as numbers FROM problem_table as p INNER JOIN user_table as u ON p.assignee_user_id = u.user_id GROUP BY p.assignee_user_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatusByDeveloper(reader["user_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Name is" + reader["user_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }

        public List<ViewStatus> GetAllTicketStatus()
        {
            List<ViewStatus> list = new List<ViewStatus>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT s.status_name, count(*) as numbers FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id GROUP BY p.status_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ViewStatus(reader["status_name"].ToString(), Convert.ToInt32(reader["numbers"])));

                        System.Diagnostics.Debug.WriteLine("Status is" + reader["status_name"].ToString());
                        System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }

        public List<int> GetAllTicketStatusforChart()
        {
            List<int> list = new List<int>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as numbers FROM problem_table as p INNER JOIN status_table as s ON p.status_id = s.status_id GROUP BY p.status_id;", this.MySQLConnection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // list.Add(new ViewStatus(reader["status_name"].ToString(), Convert.ToInt32(reader["numbers"])));
                        list.Add(Convert.ToInt32(reader["numbers"]));
                        // System.Diagnostics.Debug.WriteLine("Status is" + reader["status_name"].ToString());
                        //System.Diagnostics.Debug.WriteLine("Count is" + Convert.ToInt32(reader["numbers"]));

                    }
                }
            }
            finally
            {

            }

            return list;
        }


        public List<Problem> GetAllProblems()
        {
            List<Problem> list = new List<Problem>();


            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT p.ticket_number,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id;", this.MySQLConnection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Problem()
                        {
                            Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                            //Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]),
                            User_Id = Convert.ToInt32(reader["user_id"]),
                            Problem_Desc = reader["problem_desc"].ToString(),
                            Summary = reader["summary"].ToString(),
                            Severity = Convert.ToInt32(reader["severity"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"]),
                            Status_Id = Convert.ToInt32(reader["status_id"]),
                            Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                            Created_By = reader["created_by"].ToString(),
                            Updated_By = reader["updated_by"].ToString(),
                            Status_Name = reader["status_name"].ToString()
                        });
                    }
                }
            }
            finally
            {

            }

            return list;
        }

        //public List<Problem> GetFilteredBugs(String customerName, int status, int noOfDays, String firstDate, String secondDate)
        public List<Problem> GetFilteredBugs(String customerName, String developerName, int status, int noOfDays)
        {
            List<Problem> list = new List<Problem>();
            customerName = customerName.Equals("") ? "%" : customerName;
            string str_status = status == 0 ? "%" : "" + status;
            developerName = developerName.Equals("") ? "%" : developerName;


            //Below query for custom date filter
            //MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.created_by IN (Select customer_id FROM customer_table WHERE customer_name LIKE '" + customerName + "' AND (create_date >= '" + firstDate + "'AND create_date <= '" + secondDate + "'))", conn);

            //Below query for not using custom date filter
            MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.assignee_user_id IN (SELECT user_id FROM user_table WHERE user_name LIKE '" + developerName + "')AND p.created_by IN (Select customer_id FROM customer_table WHERE customer_name LIKE '" + customerName + "')", this.MySQLConnection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Problem()
                    {
                        Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                        User_Id = Convert.ToInt32(reader["user_id"]),
                        Problem_Desc = reader["problem_desc"].ToString(),
                        Summary = reader["summary"].ToString(),
                        Severity = Convert.ToInt32(reader["severity"]),
                        Create_Date = Convert.ToDateTime(reader["create_date"]),
                        Status_Id = Convert.ToInt32(reader["status_id"]),
                        Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                        Created_By = reader["created_by"].ToString(),
                        Updated_By = reader["updated_by"].ToString(),
                        Status_Name = reader["status_name"].ToString()
                    });
                }
            }

            return list;
        }

        public List<Problem> GetProblemInfo(int ticket_number) /// <summary>
                                                         /// Return the specific problem decsription on the basis of ticket number selected
                                                         /// </summary>
                                                         /// <returns>The all customer problems.</returns>
                                                         /// <param name="customer_id">Customer identifier.</param>
        {
            List<Problem> list = new List<Problem>();


            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT p.ticket_number,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date, p.status_id, p.assignee_user_id, p.estimated_complete_date,p.emailnotificationstatus,p.emaillist FROM problem_table AS p WHERE p.ticket_number =" + ticket_number + ";", this.MySQLConnection);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Int32 developer_id = 0;
                        DateTime estimated_date = new DateTime();
                        if (reader["assignee_user_id"] != DBNull.Value)
                            developer_id = Convert.ToInt32(reader["assignee_user_id"]);
                        if (reader["estimated_complete_date"] != DBNull.Value)
                            estimated_date = Convert.ToDateTime(reader["estimated_complete_date"]);

                        list.Add(new Problem()
                        {
                            Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                            Problem_Desc = reader["problem_desc"].ToString(),
                            Summary = reader["summary"].ToString(),
                            Severity = Convert.ToInt32(reader["severity"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"]),
                            Status_Id = Convert.ToInt32(reader["status_id"]),
                            Assignee_User_Id = developer_id,
                            Estimated_Complete_Date = estimated_date,
                           EmailNotificationstatus = Convert.ToBoolean(reader["emailnotificationstatus"]),
                            EmailList= reader["emaillist"].ToString()
                        });
                      
                    }



                }

            }
            catch
            {

            }
            return list;

        }
        //Returns all the tickets logged by users of single company
        public List<Problem> GetAllCustomerProblems(int customer_id)
        {
            List<Problem> list = new List<Problem>();


            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT p.ticket_number,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id WHERE p.created_by=" + customer_id, this.MySQLConnection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Problem()
                        {
                            Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                            // Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]), 
                            User_Id = Convert.ToInt32(reader["user_id"]),
                            Problem_Desc = reader["problem_desc"].ToString(),
                            Summary = reader["summary"].ToString(),
                            Severity = Convert.ToInt32(reader["severity"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"]),
                            Status_Id = Convert.ToInt32(reader["status_id"]),
                            Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                            Created_By = reader["created_by"].ToString(),
                            Updated_By = reader["updated_by"].ToString(),
                            Status_Name = reader["status_name"].ToString()
                        });
                    }
                }
            }
            finally
            {

            }

            return list;
        }







        //public List<Problem> GetFilteredCompanyBugs(int customerId, int status, int noOfDays, String firstDate, String secondDate)
        public List<Problem> GetFilteredCompanyBugs(int customerId, int status, int noOfDays)
        {
            List<Problem> list = new List<Problem>();
            string str_status = status == 0 ? "%" : "" + status;


            //MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where (create_date >= '" + firstDate + "'AND create_date <= '" + secondDate + "') AND DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.created_by =" + customerId, conn);
            MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id WHERE DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.created_by =" + customerId, this.MySQLConnection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Problem()
                    {
                        Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                        // Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]),
                        User_Id = Convert.ToInt32(reader["user_id"]),
                        Problem_Desc = reader["problem_desc"].ToString(),
                        Summary = reader["summary"].ToString(),
                        Severity = Convert.ToInt32(reader["severity"]),
                        Create_Date = Convert.ToDateTime(reader["create_date"]),
                        Status_Id = Convert.ToInt32(reader["status_id"]),
                        Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                        Created_By = reader["created_by"].ToString(),
                        Updated_By = reader["updated_by"].ToString(),
                        Status_Name = reader["status_name"].ToString()
                    });
                }
            }

            return list;

        }







        public List<Problem> GetAllUserProblems(int user_id)
        {
            List<Problem> list = new List<Problem>();


            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT p.ticket_number,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id WHERE p.user_id=" + user_id, this.MySQLConnection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Problem()
                        {
                            Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                            // Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]),
                            User_Id = Convert.ToInt32(reader["user_id"]),
                            Problem_Desc = reader["problem_desc"].ToString(),
                            Summary = reader["summary"].ToString(),
                            Severity = Convert.ToInt32(reader["severity"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"]),
                            Status_Id = Convert.ToInt32(reader["status_id"]),
                            Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                            Created_By = reader["created_by"].ToString(),
                            Updated_By = reader["updated_by"].ToString(),
                            Status_Name = reader["status_name"].ToString()
                        });
                    }
                }
            }
            finally
            {

            }

            return list;
        }




        //public List<Problem> GetFilteredUserBugs(int customerId, int status, int noOfDays, String firstDate, String secondDate)
        public List<Problem> GetFilteredUserBugs(int customerId, int status, int noOfDays)
        {
            List<Problem> list = new List<Problem>();
            string str_status = status == 0 ? "%" : "" + status;


            //MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where (create_date >= '" + firstDate + "'AND create_date <= '" + secondDate + "') AND DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.created_by =" + customerId, conn);
            MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id WHERE DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.created_by =" + customerId, this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Problem()
                    {
                        Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                        Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]),
                        User_Id = Convert.ToInt32(reader["user_id"]),
                        Problem_Desc = reader["problem_desc"].ToString(),
                        Summary = reader["summary"].ToString(),
                        Severity = Convert.ToInt32(reader["severity"]),
                        Create_Date = Convert.ToDateTime(reader["create_date"]),
                        Status_Id = Convert.ToInt32(reader["status_id"]),
                        Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                        Created_By = reader["created_by"].ToString(),
                        Updated_By = reader["updated_by"].ToString(),
                        Status_Name = reader["status_name"].ToString()
                    });
                }
            }

            return list;

        }







        //Show only the tickets ssigned to a particular developer based on his user id 

        public List<Problem> GetAllAssignedProblems(int developer_id)
        {
            List<Problem> list = new List<Problem>();

            try
            {

                MySqlCommand cmd = new MySqlCommand("SELECT p.ticket_number,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id WHERE p.assignee_user_id=" + developer_id, this.MySQLConnection);


                System.Diagnostics.Debug.WriteLine("Problem is-------------------------------------- " + developer_id);




                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Problem()
                        {

                            Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                            User_Id = Convert.ToInt32(reader["user_id"]),
                            Problem_Desc = reader["problem_desc"].ToString(),
                            Summary = reader["summary"].ToString(),
                            Severity = Convert.ToInt32(reader["severity"]),
                            Create_Date = Convert.ToDateTime(reader["create_date"]),
                            Status_Id = Convert.ToInt32(reader["status_id"]),
                            Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                            Created_By = reader["created_by"].ToString(),
                            Updated_By = reader["updated_by"].ToString(),
                            Status_Name = reader["status_name"].ToString()
                        });
                    }
                }
            }
            finally
            {

            }

            return list;
        }

        //public List<Problem> GetFilteredDeveloperBugs(int developerId, int status, int noOfDays, String firstDate, String secondDate)
        public List<Problem> GetFilteredDeveloperBugs(int developerId, int status, int noOfDays)
        {
            List<Problem> list = new List<Problem>();
            string str_status = (status == 0) ? "%" : "" + status;
            String developer_id = (developerId == 0) ? "%" : "" + developerId;


            //MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where (create_date >= '" + firstDate + "'AND create_date <= '" + secondDate + "') AND DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.assignee_user_id =" + developerId, conn);
            MySqlCommand cmd = new MySqlCommand("select p.ticket_number,p.assignee_user_id,p.user_id,p.problem_desc,p.summary,p.severity,p.attachment,p.create_date,p.status_id,p.last_update_date, p.created_by, p.updated_by, s.status_name FROM problem_table as p inner join status_table as s ON p.status_id = s.status_id where DATEDIFF(NOW(), p.create_date) < " + noOfDays + " AND p.status_id LIKE '" + str_status + "' AND p.assignee_user_id LIKE '" + developer_id + "'", this.MySQLConnection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Problem()
                    {
                        Ticket_Number = Convert.ToInt32(reader["ticket_number"]),
                        Assignee_User_Id = Convert.ToInt32(reader["assignee_user_id"]),
                        User_Id = Convert.ToInt32(reader["user_id"]),
                        Problem_Desc = reader["problem_desc"].ToString(),
                        Summary = reader["summary"].ToString(),
                        Severity = Convert.ToInt32(reader["severity"]),
                        Create_Date = Convert.ToDateTime(reader["create_date"]),
                        Status_Id = Convert.ToInt32(reader["status_id"]),
                        Last_Update_Date = Convert.ToDateTime(reader["last_update_date"]),
                        Created_By = reader["created_by"].ToString(),
                        Updated_By = reader["updated_by"].ToString(),
                        Status_Name = reader["status_name"].ToString()
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
                        comment_date = Convert.ToDateTime(reader["comment_date"])
                    });
                }
            }

            return list;
        }

        public List<Status> GetAllProblemStatus()
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

        public void ChangeStatus(String StatusName, Int32 ticketNo)
        {


            MySqlCommand cmd = new MySqlCommand("UPDATE `problem_table` SET `status_id` = (SELECT `status_id` FROM `status_table` WHERE `status_name`='" + StatusName + "') WHERE `ticket_number`=" + ticketNo, this.MySQLConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();


        }

        public void UpdateEstimatedDate(String estimatedDate, Int32 ticketNo)
        {


            MySqlCommand cmd = new MySqlCommand("UPDATE `problem_table` SET `estimated_complete_date` = '" + estimatedDate + "' WHERE `ticket_number`=" + ticketNo, this.MySQLConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();



        }


        public List<User> getUserByTicketId(int ticket_id)
        {
            List<User> list = new List<User>();



            MySqlCommand cmd = new MySqlCommand("select * from user_table where user_id=(select user_id from problem_table WHERE `ticket_number`=" + ticket_id + ");", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        user_id = Convert.ToInt32(reader["user_id"]),
                        user_name = reader["user_name"].ToString(),
                        email = reader["email"].ToString(),
                        // email =


                        // phone_number 
                        //  point_of_contact


                        //customer_id
                        //  role_id =
                    });
                }


            }

            return list;


        }

        public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();



            MySqlCommand cmd = new MySqlCommand("select * from category_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Category()
                    {
                        Category_id = Convert.ToInt32(reader["category_id"]),
                        Category_name = reader["category_name"].ToString()
                    });
                }

            }

            return list;
        }

        public List<Module> GetAllModules()
        {
            List<Module> list = new List<Module>();



            MySqlCommand cmd = new MySqlCommand("select * from module_table", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Module()
                    {
                        Module_id = Convert.ToInt32(reader["module_id"]),
                        Category_id = Convert.ToInt32(reader["category_id"]),
                        Module_name = reader["module_name"].ToString()
                    });
                }

            }

            return list;
        }

    }
}