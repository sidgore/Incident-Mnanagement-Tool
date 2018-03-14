using System;


namespace BugReportMVC5.Models
{
    [Serializable]
    public class User
    {
        public User() 
        {
            
        }
        public User(User userSession)
        {

            this.customer_id = userSession.customer_id;
            this.email = userSession.email;
            this.password = userSession.password;
            this.phone_number = userSession.phone_number;
            this.role_id = userSession.role_id;
            this.user_id = userSession.user_id; 
            this.user_name = userSession.user_name;
        }

        // private UserContext context;
        // [Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public String user_name { get; set; }


        public String email { get; set; }
        public long phone_number { get; set; }
       
        public String password { get; set; }

        public int customer_id { get; set; }
         
        public int role_id { get; set; }

    }
}
