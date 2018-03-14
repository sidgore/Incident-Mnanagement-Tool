using System;
using System.Data.SqlClient;
using BugReportMVC5.Models;
using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace BugReportMVC5.Controllers
{
    public class CustomerController : Controller
    {

        [HttpGet]
        public ActionResult createCustomer()
        {



            try
            {

                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);

                if (user.role_id == 3)
                {


                    Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
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
        public ActionResult createCustomer1()
        {
            try
            {
                Customer customer = new Customer();
                // customer.Customer_id= Convert.ToInt32( Request.Form["Customer_id"]);
                customer.Customer_name = Convert.ToString(Request.Form["Customer_name"]);
                Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                if (context.createCustomer(customer) == 0)
                {
                    Customer custumorInserted = context.FindCustomer(customer.Customer_name)[0];

                    Models.UserContext user_context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;

                    User user = new User();
                    user.customer_id = custumorInserted.Customer_id;
                    user.email = custumorInserted.Customer_name + "default";
                    user.password = "202cb962ac59075b964b07152d234b70";
                    user.phone_number = 123;
                    user.role_id = 4;
                    user.user_name = custumorInserted.Customer_name + "admin";



                    user_context.createUser(user);
                    // TempData["User"] = user; 

                    return RedirectToAction("Index");
                }

                else
                {
                    TempData["Errors"] = "Duplicate entry!!";
                    return RedirectToAction("createCustomer");
                }
            }

            catch (SqlException ex) 
            {
                
                string message = ex.Message.ToString();//e.Data.ToString();//+ " " + e.InnerException.ToString() + "  "+e.Message.ToString();
                TempData["Errors"] = message;
               // TempData["Errors"] = "Duplicate records!! Please enter unique values";
                return RedirectToAction("createCustomer");
            }

            //catch
            //{
            //    // TempData["User"] = user;
            //    TempData["Errors"] = "An Error Ocurred!! Kindly enter the details correctly!!";
            //    return RedirectToAction("createCustomer");
            //}
        }


        public IActionResult Index()
        {



            try
            {

                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);
                Models.CustomerContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.CustomerContext)) as Models.CustomerContext;
                TempData["User"] = user;
                if (user.role_id == 3)
                {
                    return View(context.GetAllCustomers());
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


    }
}