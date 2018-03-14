using System;


namespace BugReportMVC5.Models
{
    public class Filter
    {
        public int status { get; set; }
        public String customerName { get; set; }
        public String filterByDays { get; set; }
        public int noOfDays { get; set; }
    }
}