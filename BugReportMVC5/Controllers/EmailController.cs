using System;
 
using BugReportMVC5.Models;

using Microsoft.AspNetCore.Http;



using Microsoft.AspNetCore.Mvc;


using Newtonsoft.Json;


namespace BugReportMVC5.Controllers
{
    public class EmailController : Controller
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
                    EmailContext context = HttpContext.RequestServices.GetService(typeof(EmailContext)) as EmailContext;
                    //  List<Email> EmailList = context.GetAllEmails();
                    //ViewBag.ListofEmails = EmailList;

                    return View(context.GetAllEmails());
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
        public ActionResult deleteEmails()
        {
            Console.WriteLine("---------------------------DELETEEMAILS---POST-----------------------------------------");
           var value = HttpContext.Session.GetString("user");

            User user = JsonConvert.DeserializeObject<User>(value);

            Console.WriteLine("--------------------------------------------------------------------");
            // Console.WriteLine(Request.Form["Customer"].ToString().Substring(1, Request.Form["Customer"].ToString().IndexOf(",")));


            try
            {
                //  String customer = Request.Form["Customer_Name"].ToString();


        

                Models.EmailContext email_context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.EmailContext)) as Models.EmailContext;

                email_context.deleteEmails();
                // mail.saveEmail(content);  
                 
                TempData["User"] = user; 
                return View("Index",email_context.GetAllEmails());
            } 
            catch
            {
                //TempData["User"] = AdminSession;
                TempData["Errors"] = "An Error Ocurred!! Kindly enter the details correctly!!";
                ModelState.AddModelError("Error", "Duplicate or Data Error.");
                return RedirectToAction("Index");
                // return View("createUser");  

            }
        } 







    }
}