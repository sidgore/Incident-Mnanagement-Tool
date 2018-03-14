using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;


using System.ComponentModel;

using System.Net;
using System.Net.Mail;

using BugReportMVC5.Data;

namespace BugReportMVC5.Models
{
    public class EmailContext : Connection
    {
        // public string ConnectionString { get; set; }

        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string N3NEmailId { get; set; }
        public string N3NPassword { get; set; }
        private SmtpClient Client { get; set; }


        public EmailContext(string connectionString) : base(connectionString)
        {
            // this.ConnectionString = connectionString;
            SmtpHost = "smtp.gmail.com";
            N3NEmailId = "bugportaln3n@gmail.com";
            N3NPassword = "Admin@n3n";
            // N3NPassword = ConfigurationManager.AppSettings.Get("N3NPassword");
            SmtpPort = 587;//465;
        }





        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}

        public List<Email> GetAllEmails()
        {
            List<Email> list = new List<Email>();


            //conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from Emails", this.MySQLConnection);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Email()
                    {
                        Subject = reader["Subject"].ToString(),
                        Text = reader["Text"].ToString(),
                        EmailId = reader["EmailId"].ToString(),
                        Timestamp = DateTime.Parse(reader["Timestamp"].ToString())

                    });
                }
            }

            return list;
        }





        public void Send(Email input)
        {
            if (SmtpPort <= 0)
                Client = new SmtpClient(SmtpHost);
            else
                Client = new SmtpClient(SmtpHost, SmtpPort);

            //Client.Credentials = CredentialCache.DefaultNetworkCredentials;
            Client.UseDefaultCredentials = false;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.Credentials = new NetworkCredential(N3NEmailId, N3NPassword);
            Client.EnableSsl = true;

            MailAddress from = new MailAddress(N3NEmailId, "n3n notification center");

          //  MailAddress to = new MailAddress();
          //  MailAddressCollection gh = input.EmailId;
           // MailAddressCollection


            MailMessage message = new MailMessage("n3n notification center<"+N3NEmailId+">",input.EmailId);

            message.IsBodyHtml = true;  

            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            someArrows = "";
            string signature = "<div ><p><h3>Thanks,</h3></p> <p><h3 style='color:blue'>TAR Support Team | N3N </h3></p> <p style='color:blue'>(669)254 - 6905 </p> <p style='color:blue'>2833 Junction Ave, Suite 110, San Jose, CA 95134</p></div>";
            message.Body = input.Text+signature;
              
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = input.Subject + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string token = "email";
            Client.SendAsync(message, token);
            //message.Dispose();

            // DateTime now = DateTime.Now;

            // DateTime output = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            // conn.Open();
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES ("+c.Customer_name+")", conn);
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_id`,`customer_name`) VALUES(6, \"Ram\")",conn);S
            // String name = user.user_name;
            MySqlCommand cmd = new MySqlCommand("Insert into Emails(`Subject`,`Text`,`EmailId`,`Timestamp`)values(@Subject,@Text,@EmailId,@Timestamp)", this.MySQLConnection);
            //  cmd.Parameters.AddWithValue("@Ticket_Number", p.Ticket_Number);
            //  cmd.Parameters.AddWithValue("@Assigneer_User_Id", null); 
            cmd.Parameters.AddWithValue("@Subject", input.Subject);
            cmd.Parameters.AddWithValue("@Text", input.Text);
            cmd.Parameters.AddWithValue("@EmailId", input.EmailId);
            cmd.Parameters.AddWithValue("@Timestamp", input.Timestamp);
            //cmd.Parameters.AddWithValue("@Timestamp", output.ToString("yyyy-MM-dd HH:mm:ss"));




            cmd.ExecuteNonQuery();
            cmd.Dispose();





        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            // mailSent = true; 
        }




        public void deleteEmails()
        {

            // DateTime now = DateTime.Now;

            // DateTime output = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            // conn.Open();
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_name`) VALUES ("+c.Customer_name+")", conn);
            // MySqlCommand cmd = new MySqlCommand("INSERT INTO `bug_tracking_system`.`customer_table`(`customer_id`,`customer_name`) VALUES(6, \"Ram\")",conn);S
            // String name = user.user_name;
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Emails", this.MySQLConnection);

            //cmd.Parameters.AddWithValue("@Timestamp", output.ToString("yyyy-MM-dd HH:mm:ss"));




            cmd.ExecuteNonQuery();
            cmd.Dispose();



             


        }


    }
}




