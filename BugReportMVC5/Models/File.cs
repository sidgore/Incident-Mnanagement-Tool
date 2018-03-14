using System;


namespace BugReportMVC5.Models
{
    [Serializable]
    public class File
    {
        public File()
        {

        }
      

        // private UserContext context;
        // [Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Name { get; set; }


        public byte[] Image { get; set; }
        public long phone_number { get; set; }

        public String Filetype { get; set; }

         

    }
}
