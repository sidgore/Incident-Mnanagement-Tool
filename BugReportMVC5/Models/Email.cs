using System;
namespace BugReportMVC5.Models
{
    public class Email
    {
        public String EmailId { get; set; }

        public String Text { get; set; }

        public String Subject { get; set; }
        public DateTime Timestamp { get; set; }


        //  public String EmailId { get; set; }

        public Email()
        {
            this.EmailId = "";
            this.Subject = "";
            this.Text = "";
            this.Timestamp = DateTime.Now;

              

        }
        public Email(string subject, string text, string email,DateTime time)
        {
            this.EmailId = email;
            this.Subject = subject;
            this.Text = text;
            this.Timestamp = time;
        }

    }
}
