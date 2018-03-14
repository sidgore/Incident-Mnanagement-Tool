
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

 
using BugReportMVC5.Models;
using Microsoft.AspNetCore.Http;

 
namespace BugReportMVC5.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {

            try{
                 
                
            
                var value = HttpContext.Session.GetString("user");

                User user = JsonConvert.DeserializeObject<User>(value);

                System.Diagnostics.Debug.WriteLine("User role after updation  is----------------------" + user.role_id);


                if (user.role_id == 3)  //admin
                {

                    Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    //var statusView = (from v in problem_context.GetAllTicketStatus() select * ).ToList();

                    List<ViewStatus> p = problem_context.GetAllTicketStatus();
                     

                   

                    //List<int> ChartData = problem_context.GetAllTicketStatusforChart();
                  //  ViewBag.DataPoints = JsonConvert.SerializeObject(ChartData); 
                  //  JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

                    //ViewBag.DataPoints = JsonConvert.SerializeObject(ChartData, _jsonSetting); 
                   // System.Diagnostics.Debug.WriteLine("json---------------------");
                      
                    //System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ChartData));  

                    // var json = ViewStatus.ToJson(p)
                    List<ViewStatusByDeveloper> d = problem_context.GetAllTicketsByDeveloper();
                    List<ViewStatusByCustomer> c = problem_context.GetAllTicketsByCustomers();

                    //List<ColumnSeriesData> yDataList = new List<ColumnSeriesData>(); 
                   // List<ColumnSeriesData> Data = new List<ColumnSeriesData>();

                   // p.ForEach(i => yDataList.Add(new ColumnSeriesData { Y = i.count })); 

                    ViewBag.ListofTableByStatus = p;

                    int listCount = p.Count();
                    ViewBag.listCount = listCount;
                    ViewBag.ListByDevelopers = d;   

                    ViewBag.ListByCustomers = c;
                    //ViewData["yDataList"] = yDataList; 



                    //ViewBag.intList=(ViewBag.ListofTableByStatus as IEnumerable<ViewStatus>).Select(x => x.Status_name).ToList<String>();
                    //ViewBag.xList=
                    TempData["User"] = user;
                    return View("AdminDashboard", user);
                }

                if (user.role_id == 2)    //developer
                {
                    Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    List<ViewStatus> c = problem_context.GetAllTicketsForADeveloper(user.user_id);
                    ViewBag.ListOfTicketsByStatus = c;
                    TempData["User"] = user;
                    return View("DeveloperDashboard", user);

                }
                if (user.role_id == 1)  //user
                {
                    Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;

                    List<ViewStatus> c = problem_context.GetAllTicketsForAUser(user.user_id);
                    ViewBag.ListOfTicketsByStatus = c;
                    TempData["User"] = user;
                    return View("UserDashboard", user);
                }
                if (user.role_id == 4)  //user
                {
                    Models.ProblemContext problem_context = HttpContext.RequestServices.GetService(typeof(Models.ProblemContext)) as Models.ProblemContext;



                    List<ViewStatus> c = problem_context.GetAllTicketsForACustomer(user.customer_id);
                    List<ViewStatus> userstickets = problem_context.GetAllTicketsForLocalUsers(user.customer_id);

                    ViewBag.ListByCustomerTickets = c;
                    ViewBag.UserTickets = userstickets;
                    //ViewBag.ListByDevelopers = d;  
                    // ViewBag.ListByCustomers = c; 


                    TempData["User"] = user;
                    return View("CustomerDashboard", user);
                }

                else
                { 
                    return View("Index", "Home");

                }


            }

            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }


    }
}