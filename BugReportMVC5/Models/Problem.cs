using System;


namespace BugReportMVC5.Models
{

    public class Problem
    {
        public int Ticket_Number { get; set; }
        public int Assignee_User_Id { get; set; }
        public int User_Id { get; set; }
        public String Problem_Desc { get; set; }
        public String Summary { get; set; }
        public int Severity { get; set; }
        public byte[] Attachment { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Last_Update_Date { get; set; }
        public int Status_Id { get; set; }
        public String Created_By { get; set; }
        public String Updated_By { get; set; }
        public String Status_Name { get; set; }
        public DateTime Estimated_Complete_Date { get; set; }
        public int Module_Name { get; set; }
        public bool EmailNotificationstatus { get; set; }
        public string EmailList { get; set; }

    }
}

