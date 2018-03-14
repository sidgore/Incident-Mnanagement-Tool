using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using Newtonsoft.Json;
using BugReportMVC5.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugReportMVC5.Controllers
{
    public class ProblemController : Controller
    {
        private static int ticketNo = int.MinValue;
        private static int raised_user_id = int.MinValue;





        [HttpGet]
        public IActionResult CreateTicketCustomerAdmin()
        {
            try
            {
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 4)
                {
                    //  Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;


                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var categories = (from c in context.GetAllCategories() select c).ToList();
                    categories.Insert(0, new Category { Category_id = 0, Category_name = "----SELECT----" });
                    ViewBag.ListofCategories = categories;

                    TempData["User"] = user;
                    return View("createTicketCustomerAdmin");

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
        public IActionResult Index()
        {

            try
            {
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 1)
                {
                    //  Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;


                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var categories = (from c in context.GetAllCategories() select c).ToList();
                    categories.Insert(0, new Category { Category_id = 0, Category_name = "----SELECT----" });
                    ViewBag.ListofCategories = categories;

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

        [HttpPost]
        public IActionResult Index(Models.Problem problem)
        {
            var value = HttpContext.Session.GetString("user");

            User user = JsonConvert.DeserializeObject<User>(value);

            Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            try
            {
                if (ModelState.IsValid)
                {
                    //bool EmailNotificationstatus = Convert.ToBoolean(Request.Form["statusticket"]);


                    context.Add(problem);
                    Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
                    User admin = user_context.FindUser("admin").First();
                    DateTime now = DateTime.Now;
                    string emailList = admin.email + "," + problem.EmailList;
                    Email email = new Email("N3N TAR Support - Ticket Created", "A new ticket has been created by " + user.user_name + "<p> Please <a href='https://tarsupport.n3n.io'>log-in </a> to check further details.</p>", emailList, now);
                    Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(Models.EmailContext)) as Models.EmailContext;




                    //  System.Diagnostics.Debug.WriteLine("CUSTOMER id" + status);
                    // EmailHelper e = new EmailHelper();
                    email_context.Send(email);

                    TempData["Errors"] = "Success!!";

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                TempData["Errors"] = "Error Occured.Kindly Recheck the entered Information!!";
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }






        [HttpGet]
        public IActionResult RaiseATicketDev()
        {

            try
            {
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 2)
                {
                    //  Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;


                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var categories = (from c in context.GetAllCategories() select c).ToList();
                    categories.Insert(0, new Category { Category_id = 0, Category_name = "----SELECT----" });
                    ViewBag.ListofCategories = categories;

                    TempData["User"] = user;
                    return View("RaiseATicketDev");

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


        [HttpPost]
        public IActionResult RaiseATicketDev(Models.Problem problem)
        {

            Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            try
            {
                if (ModelState.IsValid)
                {
                    context.Add(problem);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }






        public JsonResult GetModule(int Category_id)
        {


            Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            var moduleList = (from m in context.GetAllModules() select m).ToList();
            ViewBag.ListofModules = moduleList;
            return Json(new SelectList(moduleList, "Module_id", "Module_name"));
        }

        public IActionResult ViewStatus(string sortOrder, string searchString)
        {
            try
            {

                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 3)
                {
                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
                    //Models.StatusContext status_context = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;

                    var bugs = from b in context.GetAllProblems() select b;
                    var customers = (from c in customer_context.GetAllCustomers() select c.Customer_name).ToList();


                    ViewBag.ListofCustomer = customers;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        bugs = bugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                        || b.Problem_Desc.ToLower().Contains(searchString));
                    }

                    ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
                    ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";
                    ViewBag.Create_Date = sortOrder == "Create_Date" ? "Create_Date_desc" : "Create_Date";
                    //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                    Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
                    var developers = (from d in user_context.GetAllDevelopers() select d.user_name).ToList();

                    ViewBag.ListofDevelopers = developers;

                    switch (sortOrder)
                    {
                        case "Ticket_Number_desc":
                            bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                            break;
                        case "Ticket_Number":
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                        case "Severity_desc":
                            bugs = bugs.OrderByDescending(b => b.Severity);
                            break;
                        case "Severity":
                            bugs = bugs.OrderBy(b => b.Severity);
                            break;
                        case "Create_Date":
                            bugs = bugs.OrderBy(b => b.Create_Date);
                            break;
                        case "Create_Date_desc":
                            bugs = bugs.OrderByDescending(b => b.Create_Date);
                            break;
                        default:
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                    }



                    TempData["User"] = user;
                    return View(bugs.ToList());

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


        //Get Logged Tickets for the admin
        [HttpPost]
        public IActionResult ViewStatus(Models.Filter filter, string sortOrder, string searchString)
        {
            string customerName = Request.Form["Customer_name"].ToString();
            int status = Convert.ToInt32(Request.Form["status"]);
            string filterByDays = Request.Form["filter"].ToString();
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            string developerName = Request.Form["Developer_name"].ToString();

            int noOfDays = Int32.MaxValue;
            switch (filterByDays)
            {
                case "day":
                    noOfDays = 1;
                    break;
                case "week":
                    noOfDays = 7;
                    break;
                case "month":
                    noOfDays = 31;
                    break;
                case "year":
                    noOfDays = 365;
                    break;
                default:
                    noOfDays = Int32.MaxValue;
                    break;
            }
            //Custom date filter
            //string firstDate = Request.Form["FirstDate"].ToString() + " 00:00:00";
            //string secondDate = Request.Form["SecondDate"].ToString() + " 23:59:59";

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            //var bugs = from b in FilterContext.GetFilteredBugs(customerName, status, noOfDays, firstDate, secondDate) select b;
            var bugs = from b in FilterContext.GetFilteredBugs(customerName, developerName, status, noOfDays) select b;


            Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
            var customers = (from c in customer_context.GetAllCustomers() select c.Customer_name).ToList();
            ViewBag.ListofCustomer = customers;

            Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
            var developers = (from d in user_context.GetAllDevelopers() select d.user_name).ToList();
            ViewBag.ListofDevelopers = developers;

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchBugs = from b in FilterContext.GetAllProblems() select b;
                bugs = searchBugs.Where(b => b.Summary.ToLower().Contains(searchString) || b.Problem_Desc.ToLower().Contains(searchString));
            }

            ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
            ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";


            switch (sortOrder)
            {
                case "Ticket_Number_desc":
                    bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                    break;
                case "Ticket_Number":
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
                case "Severity_desc":
                    bugs = bugs.OrderByDescending(b => b.Severity);
                    break;
                case "Severity":
                    bugs = bugs.OrderBy(b => b.Severity);
                    break;
                default:
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
            }

            TempData["User"] = user;
            return View(bugs.ToList());
        }

        public IActionResult ViewStatusCustomer(string sortOrder, string searchString)
        {
            try
            {


                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 4)
                {
                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    var bugs = from b in context.GetAllCustomerProblems(user.customer_id) select b;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        bugs = bugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                        || b.Problem_Desc.ToLower().Contains(searchString));
                    }

                    ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
                    ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";
                    ViewBag.Create_Date = sortOrder == "Create_Date" ? "Create_Date_desc" : "Create_Date";

                    switch (sortOrder)
                    {
                        case "Ticket_Number_desc":
                            bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                            break;
                        case "Ticket_Number":
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                        case "Severity_desc":
                            bugs = bugs.OrderByDescending(b => b.Severity);
                            break;
                        case "Severity":
                            bugs = bugs.OrderBy(b => b.Severity);
                            break;
                        case "Create_Date":
                            bugs = bugs.OrderBy(b => b.Create_Date);
                            break;
                        case "Create_Date_desc":
                            bugs = bugs.OrderByDescending(b => b.Create_Date);
                            break;
                        default:
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                    }



                    TempData["User"] = user;
                    return View(bugs.ToList());
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


        //Get Logged Tickets for the customer admin(company)
        [HttpPost]
        public IActionResult ViewStatusCustomer(Models.Filter filter, string sortOrder, string searchString)
        {

            int status = Convert.ToInt32(Request.Form["status"]);
            string filterByDays = Request.Form["filter"].ToString();
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            //string firstDate = Request.Form["FirstDate"].ToString() + " 00:00:00";
            //string secondDate = Request.Form["SecondDate"].ToString() + " 23:59:59";

            int noOfDays = Int32.MaxValue;
            switch (filterByDays)
            {
                case "day":
                    noOfDays = 1;
                    break;
                case "week":
                    noOfDays = 7;
                    break;
                case "month":
                    noOfDays = 31;
                    break;
                case "year":
                    noOfDays = 365;
                    break;
                default:
                    noOfDays = Int32.MaxValue;
                    break;
            }

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            //var bugs = from b in FilterContext.GetFilteredCompanyBugs(user.customer_id, status, noOfDays, firstDate, secondDate) select b;      //For custom date filter
            var bugs = from b in FilterContext.GetFilteredCompanyBugs(user.customer_id, status, noOfDays) select b;

            Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
            var customers = (from c in customer_context.GetAllCustomers() select c.Customer_name).ToList();
            ViewBag.ListofCustomer = customers;

            if (!String.IsNullOrEmpty(searchString))
            {
                bugs = bugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                || b.Problem_Desc.ToLower().Contains(searchString));
            }

            ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
            ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";

            switch (sortOrder)
            {
                case "Ticket_Number_desc":
                    bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                    break;
                case "Ticket_Number":
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
                case "Severity_desc":
                    bugs = bugs.OrderByDescending(b => b.Severity);
                    break;
                case "Severity":
                    bugs = bugs.OrderBy(b => b.Severity);
                    break;
                default:
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
            }

            TempData["User"] = user;
            return View(bugs.ToList());

        }



        [HttpGet]
        public IActionResult ViewStatusUser(string sortOrder, string searchString)
        {
            try
            {
                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 1)
                {
                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    var bugs = from b in context.GetAllUserProblems(user.user_id) select b;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        bugs = bugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                        || b.Problem_Desc.ToLower().Contains(searchString));
                    }

                    ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
                    ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";
                    ViewBag.Create_Date = sortOrder == "Create_Date" ? "Create_Date_desc" : "Create_Date";

                    switch (sortOrder)
                    {
                        case "Ticket_Number_desc":
                            bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                            break;
                        case "Ticket_Number":
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                        case "Severity_desc":
                            bugs = bugs.OrderByDescending(b => b.Severity);
                            break;
                        case "Severity":
                            bugs = bugs.OrderBy(b => b.Severity);
                            break;
                        case "Create_Date":
                            bugs = bugs.OrderBy(b => b.Create_Date);
                            break;
                        case "Create_Date_desc":
                            bugs = bugs.OrderByDescending(b => b.Create_Date);
                            break;
                        default:
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                    }

                    TempData["User"] = user;
                    return View(bugs.ToList());
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


        //Get Logged Tickets for the user
        [HttpPost]
        public IActionResult ViewStatusUser(Models.Filter filter, string sortOrder, string searchString)
        {

            int status = Convert.ToInt32(Request.Form["status"]);
            string filterByDays = Request.Form["filter"].ToString();
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            //string firstDate = Request.Form["FirstDate"].ToString() + " 00:00:00";
            //string secondDate = Request.Form["SecondDate"].ToString() + " 23:59:59";

            int noOfDays = Int32.MaxValue;
            switch (filterByDays)
            {
                case "day":
                    noOfDays = 1;
                    break;
                case "week":
                    noOfDays = 7;
                    break;
                case "month":
                    noOfDays = 31;
                    break;
                case "year":
                    noOfDays = 365;
                    break;
                default:
                    noOfDays = Int32.MaxValue;
                    break;
            }

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            //var bugs = from b in FilterContext.GetFilteredCompanyBugs(user.customer_id, status, noOfDays, firstDate, secondDate) select b;                    //For custom date filter
            var bugs = from b in FilterContext.GetFilteredCompanyBugs(user.customer_id, status, noOfDays) select b;

            Models.CustomerContext customer_context = HttpContext.RequestServices.GetService(typeof(Models.CustomerContext)) as Models.CustomerContext;
            var customers = (from c in customer_context.GetAllCustomers() select c.Customer_name).ToList();
            ViewBag.ListofCustomer = customers;

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchBugs = from b in FilterContext.GetAllUserProblems(user.user_id) select b;
                bugs = searchBugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                || b.Problem_Desc.ToLower().Contains(searchString));
            }

            ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
            ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";

            switch (sortOrder)
            {
                case "Ticket_Number_desc":
                    bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                    break;
                case "Ticket_Number":
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
                case "Severity_desc":
                    bugs = bugs.OrderByDescending(b => b.Severity);
                    break;
                case "Severity":
                    bugs = bugs.OrderBy(b => b.Severity);
                    break;
                default:
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
            }

            TempData["User"] = user;
            return View(bugs.ToList());
        }


        [HttpGet]
        public IActionResult Landing()
        {
            // Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

            var value = HttpContext.Session.GetString("user");

            User user = JsonConvert.DeserializeObject<User>(value);
            //  Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
            TempData["User"] = user;


            return View();

        }

        //Get all solutions of from the knowledge base
        [HttpPost]
        public IActionResult Landing(string searchString)
        {
            Models.KnowledgeContext knowledge_context = HttpContext.RequestServices.GetService(typeof(Models.KnowledgeContext)) as Models.KnowledgeContext;
            var solutions = from s in knowledge_context.GetAllSolutions() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                solutions = solutions.Where(s => s.description.ToLower().Contains(searchString));
            }

            return View(solutions.ToList());

        }



        [HttpGet]
        public ActionResult Assign()
        {
            try
            {


                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 3)
                {
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

        //Get the information for a specific ticket selected and give list of developers to assign
        [HttpPost]
        public ActionResult assign(string assign)
        {
            ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            var bugs = from bInfo in FilterContext.GetProblemInfo(ticketNo) select bInfo;

            Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
            var developers = (from d in user_context.GetAllDevelopers() select d.user_name).ToList();

            ViewBag.ListofDevelopers = developers;

            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            //  Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
            TempData["User"] = user;
            return View(bugs.ToList());
        }

        //Assign developer to a ticket
        public ActionResult AssignDeveloper(Models.User user)
        {
            string userName = Request.Form["user_name"].ToString();

            Console.WriteLine(ticketNo);
            Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
            user_context.AssignDeveloper(userName, ticketNo);

            Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

            User userWhoRaisedTicket = problem_context.getUserByTicketId(ticketNo)[0];
            DateTime now = DateTime.Now;
            Email email = new Email("N3N TAR Support - Ticket Assigned", "Thank you for opening the ticket " + ticketNo + " with N3N TAR Support. Your ticket has been picked by our Developer " + userName + " <p> Please <a href='https://tarsupport.n3n.io'>log-in </a> to check further details.</p>", userWhoRaisedTicket.email, now);
            Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(Models.EmailContext)) as Models.EmailContext;

            // EmailHelper e = new EmailHelper();
            email_context.Send(email);

            //Boolean assigned = true; 
            return RedirectToAction("ViewStatus");
        }

        //Get detailed tickeds when someone click details for a ticket
        [HttpPost]
        public ActionResult TicketInfo(string assign)
        {
            ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            var bugs = from bInfo in FilterContext.GetProblemInfo(ticketNo) select bInfo;
            var comments = from cInfo in FilterContext.GetAllComments(ticketNo) select cInfo;
            ViewBag.AllComments = comments;

            var statusList = from sInfo in FilterContext.GetAllProblemStatus() select sInfo;

            ViewBag.StatusListName = statusList.Select(l => l.Status_name);
            ViewBag.StatusListId = statusList.Select(l => l.Status_id);

            //Get the details of current ticket
            ViewBag.StatusId = bugs.ElementAt(0).Status_Id;
            ViewBag.CurrentStatus = statusList.Select(l => l.Status_name).ElementAt(bugs.ElementAt(0).Status_Id - 1);

            String completeDate = null;
            if (bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString() != "1/1/0001")
                completeDate = bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString();
            else
                completeDate = "Not yet decided";

            ViewBag.EstimatedCompleteDate = completeDate;
            //Get Developer name and other info
            Int32 developerId = bugs.ElementAt(0).Assignee_User_Id;

            String DeveloperName = "Ticket not yet Assigned";
            if (developerId != 0)
            {
                Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
                var developerName = user_context.GetDeveloperInfo(developerId);
                DeveloperName = developerName.ElementAt(0).user_name;
            }

            ViewBag.DeveloperName = DeveloperName;




            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            TempData["User"] = user;

            return View(bugs.ToList());
        }



        //Get the list of all tickets assigned to a developer (View all logged tickets for developer)
        public IActionResult ViewStatusDeveloper(string sortOrder, string searchString)
        {
            try
            {


                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 2)
                {
                    Models.ProblemContext context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    var bugs = from b in context.GetAllAssignedProblems(user.user_id) select b;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        bugs = bugs.Where(b => b.Summary.ToLower().Contains(searchString)
                                        || b.Problem_Desc.ToLower().Contains(searchString));
                    }

                    ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
                    ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";
                    ViewBag.Create_Date = sortOrder == "Create_Date" ? "Create_Date_desc" : "Create_Date";

                    switch (sortOrder)
                    {
                        case "Ticket_Number_desc":
                            bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                            break;
                        case "Ticket_Number":
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                        case "Severity_desc":
                            bugs = bugs.OrderByDescending(b => b.Severity);
                            break;
                        case "Severity":
                            bugs = bugs.OrderBy(b => b.Severity);
                            break;
                        case "Create_Date":
                            bugs = bugs.OrderBy(b => b.Create_Date);
                            break;
                        case "Create_Date_desc":
                            bugs = bugs.OrderByDescending(b => b.Create_Date);
                            break;
                        default:
                            bugs = bugs.OrderBy(b => b.Ticket_Number);
                            break;
                    }



                    TempData["User"] = user;
                    return View(bugs.ToList());

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

        //Get the list of all tickets assigned to a developer after applying filters
        [HttpPost]
        public IActionResult ViewStatusDeveloper(Models.Filter filter, string sortOrder, string searchString)
        {

            int status = Convert.ToInt32(Request.Form["status"]);
            string filterByDays = Request.Form["filter"].ToString();
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            //string firstDate = Request.Form["FirstDate"].ToString() + " 00:00:00";
            //string secondDate = Request.Form["SecondDate"].ToString() + " 23:59:59";

            int noOfDays = Int32.MaxValue;
            switch (filterByDays)
            {
                case "day":
                    noOfDays = 1;
                    break;
                case "week":
                    noOfDays = 7;
                    break;
                case "month":
                    noOfDays = 31;
                    break;
                case "year":
                    noOfDays = 365;
                    break;
                default:
                    noOfDays = Int32.MaxValue;
                    break;
            }

            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            //var bugs = from b in FilterContext.GetFilteredDeveloperBugs(user.customer_id, status, noOfDays, firstDate, secondDate) select b;            //For custom date filter

            var bugs = from b in FilterContext.GetFilteredDeveloperBugs(user.customer_id, status, noOfDays) select b;

            if (!String.IsNullOrEmpty(searchString))
            {

                var searchBugs = from sb in FilterContext.GetAllAssignedProblems(user.user_id) select sb;
                bugs = searchBugs.Where(sb => sb.Summary.ToLower().Contains(searchString)
                                || sb.Problem_Desc.ToLower().Contains(searchString));
            }

            ViewBag.Ticket_Number = sortOrder == "Ticket_Number" ? "Ticket_Number_desc" : "Ticket_Number";
            ViewBag.Severity = sortOrder == "Severity" ? "Severity_desc" : "Severity";

            switch (sortOrder)
            {
                case "Ticket_Number_desc":
                    bugs = bugs.OrderByDescending(b => b.Ticket_Number);
                    break;
                case "Ticket_Number":
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
                case "Severity_desc":
                    bugs = bugs.OrderByDescending(b => b.Severity);
                    break;
                case "Severity":
                    bugs = bugs.OrderBy(b => b.Severity);
                    break;
                default:
                    bugs = bugs.OrderBy(b => b.Ticket_Number);
                    break;
            }

            TempData["User"] = user;

            return View(bugs.ToList());

        }


        //Show the detailed ticket information and ability to comment, provide comments to developers
        public IActionResult TicketInfoDeveloper(string assign, string user_id)
        {
            try
            {


                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 2)
                {
                    ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));
                    raised_user_id = Convert.ToInt32(user_id);
                    System.Diagnostics.Debug.WriteLine("Ticket Numer is-------------------------------------- " + ticketNo);
                    System.Diagnostics.Debug.WriteLine("User id of the person to raise ticket is " + Convert.ToInt32(user_id));

                    Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var bugs = from bInfo in FilterContext.GetProblemInfo(ticketNo) select bInfo;
                    var comments = from cInfo in FilterContext.GetAllComments(ticketNo) select cInfo;
                    ViewBag.AllComments = comments;

                    //Models.StatusContext status = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;
                    var statusList = from sInfo in FilterContext.GetAllProblemStatus() select sInfo;

                    ViewBag.StatusListName = statusList.Select(l => l.Status_name);
                    ViewBag.StatusListId = statusList.Select(l => l.Status_id);

                    //Get the details of current ticket
                    ViewBag.StatusId = bugs.ElementAt(0).Status_Id;
                    ViewBag.CurrentStatus = statusList.Select(l => l.Status_name).ElementAt(bugs.ElementAt(0).Status_Id - 1);
                    String completeDate = null;
                    if (bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString() != "1/1/0001")
                        completeDate = bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString();
                    else
                        completeDate = "Not yet decided";

                    ViewBag.EstimatedCompleteDate = completeDate;

                    TempData["User"] = user;

                    return View(bugs.ToList());
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



        //Show the detailed ticket information and ability to comment, provide comments to developers
        public IActionResult TicketInfoCustomer(string assign, string user_id)
        {
            try
            {


                var value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 4)
                {
                    ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));
                    raised_user_id = Convert.ToInt32(user_id);
                    System.Diagnostics.Debug.WriteLine("Ticket Numer is-------------------------------------- " + ticketNo);
                    System.Diagnostics.Debug.WriteLine("User id of the person to raise ticket is " + Convert.ToInt32(user_id));

                    Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var bugs = from bInfo in FilterContext.GetProblemInfo(ticketNo) select bInfo;
                    var comments = from cInfo in FilterContext.GetAllComments(ticketNo) select cInfo;
                    ViewBag.AllComments = comments;

                    //Models.StatusContext status = HttpContext.RequestServices.GetService(typeof(Models.StatusContext)) as Models.StatusContext;
                    var statusList = from sInfo in FilterContext.GetAllProblemStatus() select sInfo;

                    ViewBag.StatusListName = statusList.Select(l => l.Status_name);
                    ViewBag.StatusListId = statusList.Select(l => l.Status_id);

                    //Get the details of current ticket
                    ViewBag.StatusId = bugs.ElementAt(0).Status_Id;
                    ViewBag.CurrentStatus = statusList.Select(l => l.Status_name).ElementAt(bugs.ElementAt(0).Status_Id - 1);



                    String completeDate = null;
                    if (bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString() != "1/1/0001")
                        completeDate = bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString();
                    else
                        completeDate = "Not yet decided";

                    ViewBag.EstimatedCompleteDate = completeDate;
                    //Get Developer name and other info
                    Int32 developerId = bugs.ElementAt(0).Assignee_User_Id;

                    String DeveloperName = "Ticket not yet Assigned";
                    if (developerId != 0)
                    {
                        Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
                        var developerName = user_context.GetDeveloperInfo(developerId);
                        DeveloperName = developerName.ElementAt(0).user_name;
                    }

                    ViewBag.DeveloperName = DeveloperName;






                    TempData["User"] = user;

                    return View(bugs.ToList());
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






        //Show the detailed ticket information and ability to comment, provide comments to the user
        public IActionResult TicketInfoUser(string assign, string user_id)
        {
            try
            {

                string value = HttpContext.Session.GetString("user");
                User user = JsonConvert.DeserializeObject<User>(value);
                if (user.role_id == 1)
                {
                    ticketNo = Convert.ToInt32(assign.Substring(assign.LastIndexOf('#') + 1));
                    raised_user_id = Convert.ToInt32(user_id);

                    Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                    var bugs = from bInfo in FilterContext.GetProblemInfo(ticketNo) select bInfo;

                    var statusList = from sInfo in FilterContext.GetAllProblemStatus() select sInfo;

                    ViewBag.StatusListName = statusList.Select(l => l.Status_name);
                    ViewBag.StatusListId = statusList.Select(l => l.Status_id);

                    //Get the details of current ticket
                    // System.Diagnostics.Debug.WriteLine("User id of the person to raise ticket is " + bugs.ElementAt(0).Status_Id);



                    ViewBag.StatusId = bugs.ElementAt(0).Status_Id;
                    ViewBag.CurrentStatus = statusList.Select(l => l.Status_name).ElementAt(bugs.ElementAt(0).Status_Id - 1);

                    //Get all conversations
                    var comments = from cInfo in FilterContext.GetAllComments(ticketNo) select cInfo;
                    ViewBag.AllComments = comments;

                    //Get the details of current ticket
                    ViewBag.StatusId = bugs.ElementAt(0).Status_Id;
                    String completeDate = null;
                    if (bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString() != "1/1/0001")
                        completeDate = bugs.ElementAt(0).Estimated_Complete_Date.Date.ToShortDateString();
                    else
                        completeDate = "Not yet decided";

                    ViewBag.EstimatedCompleteDate = completeDate;
                    //Get Developer name and other info
                    Int32 developerId = bugs.ElementAt(0).Assignee_User_Id;

                    String DeveloperName = "Ticket not yet Assigned";
                    if (developerId != 0)
                    {
                        Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
                        var developerName = user_context.GetDeveloperInfo(developerId);
                        DeveloperName = developerName.ElementAt(0).user_name;
                    }

                    ViewBag.DeveloperName = DeveloperName;


                    FileUploadContext Filecontext = HttpContext.RequestServices.GetService(typeof(FileUploadContext)) as FileUploadContext;
                    List<File> files = Filecontext.GetFiles(ticketNo);
                    ViewBag.ListOfFiles = files;
                    //  files.ForEach(file => );
                    //ViewBag.ListOfFiles.Add()

                    TempData["User"] = user;

                    return View(bugs.ToList());
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

        //Add the comments to a given ticket
        [HttpPost]
        public ActionResult TicketComment(String user_id)
        {
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);
            String estimated_date = Request.Form["CompleteDate"].ToString();

            string comment = Request.Form["Comment"].ToString();
            String statusName = (Request.Form["Status_name"].ToString() == null) ? null : Request.Form["Status_name"].ToString();

            Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(Models.UserContext)) as Models.UserContext;
            user_context.DeveloperSolution(user.user_name, ticketNo, raised_user_id, comment);



            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

            Problem pp = FilterContext.GetProblemInfo(ticketNo).First();
            User userWhoRaisedTicket = FilterContext.getUserByTicketId(ticketNo)[0];
            if (pp.EmailNotificationstatus)
            {
                DateTime now = DateTime.Now;
                string emailList = userWhoRaisedTicket.email + "," + pp.EmailList;
                Email email = new Email("N3N TAR Support-  Ticket Comment", "<p>There is an update to the The ticket " + ticketNo + " </p><p> " + comment + " </p>", emailList, now);


                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(Models.EmailContext)) as Models.EmailContext;
                email_context.Send(email);
            }


            if (estimated_date != "")
                FilterContext.UpdateEstimatedDate(estimated_date, ticketNo);

            if (statusName != "")
            {
                FilterContext.ChangeStatus(statusName, ticketNo);

                // Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(Models.EmailContext)) as Models.EmailContext;

                //User userWhoRaisedTicket = FilterContext.getUserByTicketId(ticketNo)[0];
                DateTime now = DateTime.Now;
                Email email = new Email("N3N TAR Support- Ticket Status Changed", "<p>There is an update to the Ticket you opened.</p><p>The ticket " + ticketNo + " has been been " + statusName + " </p><p>", userWhoRaisedTicket.email, now);

                // EmailHelper e = new EmailHelper();
                email_context.Send(email);



            }


            return RedirectToAction("Index", "Dashboard", user);
        }

        public ActionResult ChangeStatusController()
        {
            var value = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(value);

            Console.WriteLine("This is called");
            var decision = Request.Form["decideSolution"].ToString();
            Console.WriteLine("******************************************************");
            Models.ProblemContext FilterContext = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;
            if (decision == "Accept")
                FilterContext.ChangeStatus("Complete", ticketNo);
            else if (decision == "Reject" || decision == "Reopen")
                FilterContext.ChangeStatus("In Progress", ticketNo);
            else
                Console.WriteLine("***///////////////////////////No changes required////////////////////////***");
            return RedirectToAction("Index", "Dashboard", user);
        }
    }
}