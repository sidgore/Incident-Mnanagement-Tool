using System;

namespace BugReportMVC5.Models
{
    public class Knowledge
    {
        public int knowledge_base_id { get; set; }
        public int user_id { get; set; }
        public String user_name { get; set; }
        public String description { get; set; }
        public int ticket_no { get; set; }
        public DateTime comment_date { get; set; }
    }
}
