using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugReportMVC5.Models
{
    public class Customer
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Customer_id { get; set; }

        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        [Display(Name = "Customer Name")]
        public String Customer_name { get; set; }

        //   private CustomerContext context;
    }
}
