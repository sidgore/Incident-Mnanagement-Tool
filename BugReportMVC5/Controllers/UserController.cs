using System;

using System.Linq;

using BugReportMVC5.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace BugReportMVC5.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 3)
                {
                    // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                    TempData["User"] = user;
                    UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
                    return View(context.GetAllUsers());
                }
                else
                {
                    ModelState.AddModelError("Error", "Unauthorized!!");
                    return RedirectToAction("Index", "Home");
                }
            }

            catch
            {
                ModelState.AddModelError("Error", "Login First!!");
                return RedirectToAction("Index", "Home");

            }


        }
        public ActionResult createUser()
        {


            try
            {

                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);

                if (user.role_id == 3)
                {

                    // Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
                    //Models.StatusContext status_context = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;

                    //var bugs = from b in context.GetAllProblems() select b;
                    //var customers = (from c in customer_context.GetAllCustomers() select (c.Customer_id, c.Customer_name)).ToList();

                    var customers = (from c in customer_context.GetOnlyCustomers() select (c.Customer_id, c.Customer_name)).ToList();

                    ViewBag.ListofCustomer = customers;

                    // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                    TempData["User"] = user;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Error", "Unauthorized!!");
                    return RedirectToAction("Index", "Home");
                }
            }

            catch
            {
                ModelState.AddModelError("Error", "Login First!!");
                return RedirectToAction("Index", "Home");

            }





        }

        // POST: /Student/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createUser1()
        {
            Console.WriteLine("---------------------------CREATEUSER---POST-----------------------------------------");
            var value = HttpContext.Session.GetString("user");

            User AdminSession = JsonConvert.DeserializeObject<User>(value);


            Console.WriteLine("--------------------------------------------------------------------");
            // Console.WriteLine(Request.Form["Customer"].ToString().Substring(1, Request.Form["Customer"].ToString().IndexOf(",")));


            try
            {
                String customer = Request.Form["Customer_Name"].ToString();


                System.Diagnostics.Debug.WriteLine("CUSTOMER id" + customer);

                int startIndex = 1; // find out startIndex
                int endIndex = customer.IndexOf(",");
                int length = endIndex - startIndex;


                //  System.Diagnostics.Debug.WriteLine("CUSTOMER id" +customer.Substring(startIndex,length));
                User user = new User();
                // customer.Customer_id= Convert.ToInt32( Request.Form["Customer_id"]);
                user.user_name = Convert.ToString(Request.Form["user_name"]);
                user.email = Convert.ToString(Request.Form["email"]);
                user.phone_number = Convert.ToInt64(Request.Form["phone_number"]);
                user.customer_id = Convert.ToInt32(customer.Substring(startIndex, length));

                // s1.IndexOf("\u00ADn")
                user.role_id = Convert.ToInt32(Request.Form["role_id"]);


                MD5 md5 = new MD5CryptoServiceProvider();
                string updatedPasssword = Convert.ToString(Request.Form["password"]);
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(updatedPasssword));
                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }




                user.password = strBuilder.ToString();
                Models.UserContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;
                context.createUser(user);



                string body = "<p>Welcome to N3N TAR Support. Please login using the below details.</p><p>URL: <a href='https://tarsupport.n3n.io'> TARSupport</a></p><p><b>Username: " + user.user_name + "</b></p><p><b>Password: " + Convert.ToString(Request.Form["password"]) + "</b></p><p>\t Do not forget to update your profile.</p>";





                DateTime now = DateTime.Now;
                Email content = new Email("N3N TAR Support - New User Created", body, user.email, now);
                //EmailHelper mail = new EmailHelper();
                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.EmailContext)) as Models.EmailContext;

                email_context.Send(content);
                // mail.saveEmail(content);  


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                //TempData["User"] = AdminSession;
                string message = e.Message.ToString();
                TempData["Errors"] = message;
                //TempData["Errors"] = "An Error Ocurred!! Kindly enter the details correctly!!";
                ModelState.AddModelError("Error", "Duplicate or Data Error.");
                return RedirectToAction("createUser");
                // return View("createUser");  

            }
        }




        [HttpGet]
        public ActionResult createDeveloper()
        {


            try
            {

                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);

                if (user.role_id == 3)
                {

                    // Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    //Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
                    //Models.StatusContext status_context = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;

                    //var bugs = from b in context.GetAllProblems() select b;
                    //var customers = (from c in customer_context.GetAllCustomers() select (c.Customer_id, c.Customer_name)).ToList();


                    // ViewBag.ListofCustomer = customers;

                    // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                    TempData["User"] = user;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Error", "Unauthorized!!");
                    return RedirectToAction("Index", "Home");
                }
            }

            catch
            {
                ModelState.AddModelError("Error", "Login First!!");
                return RedirectToAction("Index", "Home");

            }


        }


        // POST: /Student/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createDeveloper1()
        {
            Console.WriteLine("-----------------createDeveloper1[POST]---------------------------------------------------");

            var value = HttpContext.Session.GetString("user");

            User AdminSession = JsonConvert.DeserializeObject<User>(value);

            try
            {

                //  System.Diagnostics.Debug.WriteLine("CUSTOMER id" +customer.Substring(startIndex,length));
                User user = new User();
                // customer.Customer_id= Convert.ToInt32( Request.Form["Customer_id"]);
                user.user_name = Convert.ToString(Request.Form["user_name"]);
                user.email = Convert.ToString(Request.Form["email"]);
                user.phone_number = Convert.ToInt64(Request.Form["phone_number"]);
                user.customer_id = 0;

                // s1.IndexOf("\u00ADn")
                user.role_id = Convert.ToInt32(Request.Form["role_id"]);



                MD5 md5 = new MD5CryptoServiceProvider();
                string updatedPasssword = Convert.ToString(Request.Form["password"]);
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(updatedPasssword));
                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }




                user.password = strBuilder.ToString();

                Models.UserContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;
                context.createUser(user);



                //    string body = "/n : " + user.user_name + "/n Password: " + user.password + "/n Please Log In and change the password immideately. Thanks";


                //string Body = " <p>New user has been created with following details.</p> <p>Username : " + user.user_name + "</p> <p> Password: " + Convert.ToString(Request.Form["password"]) + " </p> Please Log In and change the password immideately. Thanks";
                // message.To = user.email;

                string body = "<p>Welcome to N3N TAR Support. Please login using the below details.</p><p>URL: <a href='https://tarsupport.n3n.io'>www.tarsupport.n3n.io</a></p><p><b>Username: " + user.user_name + "</b></p><p><b>Password: " + Convert.ToString(Request.Form["password"]) + "</b></p><p>\t Do not forget to update your profile.</p>";



                DateTime now = DateTime.Now;

                Email content = new Email("N3N TAR Support - New User Created", body, user.email, now);

                // EmailHelper mail = new EmailHelper();
                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.EmailContext)) as Models.EmailContext;

                email_context.Send(content);



                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                //TempData["User"] = AdminSession;
                string message = e.Message.ToString();
                TempData["Errors"] = message;
                // ModelState.AddModelError("Error", "Duplicate or Data Error.");
                return RedirectToAction("createDeveloper");
                // return View("createUser");  

            }

        }



        // POST: /Student/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createUserLocal1()
        {
            Console.WriteLine("--------------------------------------------------------------------");
            // Console.WriteLine(Request.Form["Customer"].ToString().Substring(1, Request.Form["Customer"].ToString().IndexOf(",")));
            // String customer = Request.Form["Customer_Name"].ToString();
            var value = HttpContext.Session.GetString("user");

            User userfromSession = JsonConvert.DeserializeObject<User>(value);
            try
            {
                // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;


                // System.Diagnostics.Debug.WriteLine("CUSTOMER id" + customer);

                // int startIndex = 1; // find out startIndex
                //int endIndex = customer.IndexOf(",");
                //int length = endIndex - startIndex;


                //  System.Diagnostics.Debug.WriteLine("CUSTOMER id" +customer.Substring(startIndex,length));
                User user = new User();
                // customer.Customer_id= Convert.ToInt32( Request.Form["Customer_id"]);
                user.user_name = Convert.ToString(Request.Form["user_name"]);
                user.email = Convert.ToString(Request.Form["email"]);
                user.phone_number = Convert.ToInt64(Request.Form["phone_number"]);


                user.customer_id = userfromSession.customer_id;

                // s1.IndexOf("\u00ADn")
                user.role_id = Convert.ToInt32(Request.Form["role_id"]);

                MD5 md5 = new MD5CryptoServiceProvider();
                string updatedPasssword = Convert.ToString(Request.Form["password"]);
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(updatedPasssword));
                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }




                user.password = strBuilder.ToString();




                // user.password = Convert.ToString(Request.Form["password"]);
                Models.UserContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;
                context.createUser(user);


                // string body = "New user has been created with following details./n Username: " + user.user_name + "/n Password: " + Convert.ToString(Request.Form["password"]) + "/n Please Log In and change the password immideately. Thanks";
                string body = "<p>Welcome to N3N TAR Support. Please login using the below details.</p><p>URL: <a href='https://tarsupport.n3n.io'>www.tarsupport.n3n.io</a></p><p><b>Username: " + user.user_name + "</b></p><p><b>Password: " + Convert.ToString(Request.Form["password"]) + "</b></p><p>\t Do not forget to update your profile.</p>";



                DateTime now = DateTime.Now;
                Email content = new Email("N3N TAR Support - New User Created", body, user.email, now);
                // EmailHelper mail = new EmailHelper();
                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.EmailContext)) as Models.EmailContext;

                email_context.Send(content);



                // TempData["User"] = userfromSession;

                return RedirectToAction("Index");
            }
            catch
            {
                //TempData["User"] = AdminSession;
                TempData["Errors"] = "An Error Ocurred!! Kindly enter the details correctly!!";
                ModelState.AddModelError("Error", "Duplicate or Data Error.");
                return RedirectToAction("createUserLocal", "User");
                // return View("createUser");  

            }



        }

        [HttpGet]
        public ActionResult createUserLocal()
        {


            try
            {

                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 4)
                {


                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
                    //Models.StatusContext status_context = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;

                    //var bugs = from b in context.GetAllProblems() select b;
                    // var customers = (from c in customer_context.GetAllCustomers() select (c.Customer_id, c.Customer_name)).ToList();


                    // ViewBag.ListofCustomer = customers;


                    // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                    TempData["User"] = user;

                    return View("createUserLocal");



                }
                else
                {
                    ModelState.AddModelError("Error", "Unauthorized!!");
                    return RedirectToAction("Index", "Home");
                }
            }

            catch
            {
                ModelState.AddModelError("Error", "Login First!!");
                return RedirectToAction("Index", "Home");

            }

        }





        public IActionResult viewLocalUsers()
        {
            try
            {
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 4)
                {

                    // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                    TempData["User"] = user;
                    Models.UserContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;
                    return View("viewLocalUsers", context.GetAllLocalUsers(user.customer_id));
                }
                else
                {
                    ModelState.AddModelError("Error", "Unauthorized!!");
                    return RedirectToAction("Index", "Home");
                }
            }

            catch
            {
                ModelState.AddModelError("Error", "Login First!!");
                return RedirectToAction("Index", "Home");

            }
        }



        [HttpGet]
        public ActionResult UpdateProfile()
        {
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            TempData["User"] = user;
            if (user.role_id == 1)
            { return View("updateProfileUser"); }
            else if (user.role_id == 3)
            {
                return View("updateProfile");
            }
            else if (user.role_id == 2)
            {
                return View("updateProfileDeveloper");
            }
            else return View("updateProfileCustomer");
        }

        [HttpPost]

        public ActionResult updateProfile()
        {

            var value = HttpContext.Session.GetString("user");
            User userSession = JsonConvert.DeserializeObject<User>(value);
            //const User u = userSession;
            try
            {
                User UpdatedUser = new User(userSession);

                //  UpdatedUser = userSession;
                Console.WriteLine("--------------------------------------------------------------------");
                //  Console.WriteLine(Request.Form["Customer"].ToString().Substring(1, 1));
                //System.Diagnostics.Debug.WriteLine("CUSTOMER is " + Convert.ToInt32(Request.Form["Customer"].ToString().Substring(1, 1)));
                //User user = new User();
                // customer.Customer_id= Convert.ToInt32( Request.Form["Customer_id"]);
                //user.user_name = Convert.ToString(Request.Form["user_name"]);

                Console.WriteLine("user anme is " + userSession.user_name);
                UpdatedUser.email = Convert.ToString(Request.Form["email"]);
                Console.WriteLine("email  is " + userSession.email);
                Console.WriteLine("updated email  is " + UpdatedUser.email);
                // user.phone_number = Convert.ToInt32(Request.Form["phone_number"]);
                //user.point_of_contact = Convert.ToString(Request.Form["point_of_contact"]);
                //user.customer_id = Convert.ToInt32(Request.Form["Customer"].ToString().Substring(1, 1));
                //user.role_id = Convert.ToInt32(Request.Form["role_id"]);
                if (!Convert.ToString(Request.Form["password"]).Equals(userSession.password))
                {
                    // MD5 ENNCRYPTION-----
                    MD5 md5 = new MD5CryptoServiceProvider();
                    string updatedPasssword = Convert.ToString(Request.Form["password"]);
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(updatedPasssword));
                    byte[] result = md5.Hash;
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits
                        //for each byte
                        strBuilder.Append(result[i].ToString("x2"));
                    }

                    UpdatedUser.password = strBuilder.ToString();

                }

                //  UpdatedUser.password = Convert.ToString(Request.Form["password"]);
                Console.WriteLine("password " + UpdatedUser.password);
                Console.WriteLine("password " + UpdatedUser.password);
                UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as Models.UserContext;
                //  User us=context.FindUser(user.user_name)[0];


                // var value = HttpContext.Session.GetString("user");

                // User user = JsonConvert.DeserializeObject<User>(value);
                // Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;


                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.user_id);

                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.user_name);

                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.password);

                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.email);

                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.phone_number);

                System.Diagnostics.Debug.WriteLine("user info-----------------------------------------------" + UpdatedUser.role_id);






                context.updateProfile(UpdatedUser);
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(UpdatedUser));
                TempData["User"] = UpdatedUser;
                TempData["Errors"] = "Succesfuly modified!!";
                ModelState.AddModelError("Errors", "Profile Succesfully Modified!!");


                switch (UpdatedUser.role_id)
                {
                    case 1: return View("updateProfileUser");
                    case 2: return View("updateProfileDeveloper");
                    case 3: return View("updateProfile");
                    case 4: return View("updateProfileCustomer");
                    default: return View("Index", "Home");
                }

            }

            catch (Exception e)
            {
                //HttpContext.Session.SetString("user", JsonConvert.SerializeObject(userSession));
                TempData["User"] = userSession;
                string message = e.Message.ToString();
                TempData["Errors"] = message;
                // TempData["Errors"] = "An Error Ocurred!! Kindly enter the details correctly!!";
                ModelState.AddModelError("Errors", "An Error Ocurred!! Kindly enter the details correctly!!");

                switch (userSession.role_id)
                {
                    case 1: return View("updateProfileUser");
                    case 2: return View("updateProfileDeveloper");
                    case 3: return View("updateProfile");
                    case 4: return View("updateProfileCustomer");
                    default: return View("Index", "Home");
                }
            }

        }



        [HttpPost]
        public ActionResult deleteUser(string User_id)
        {
            string value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            try
            {

               

                // int  ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));
                int user_id = Convert.ToInt32(User_id);

                UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as Models.UserContext;
                context.deleteUser(user_id);
                TempData["User"] = user;

                return View("Index",context.GetAllUsers());


            }

            catch
            {
                TempData["User"] = user;
                return View("Index");
            }

        }
    }
}