using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BugReportMVC5.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Security.Cryptography;
using System.Text;

namespace BugReportMVC5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var value = HttpContext.Session.GetString("user");


            if (value != null)
            {
                User user = JsonConvert.DeserializeObject<User>(value);
                return RedirectToAction("Index", "Dashboard");

                // return View("Customer/Index", user);
            }
            else
            {
                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            // User user = new User();
            var user_name = Request.Form["user_name"];
            var password = Request.Form["password"];


            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }


            password = strBuilder.ToString();


            System.Diagnostics.Debug.WriteLine("eNCRYPTED is " + password);





            Models.UserContext context = HttpContext.RequestServices.GetService(typeof(BugReportMVC5.Models.UserContext)) as Models.UserContext;

            try
            {


                User user = context.FindUser(user_name)[0];


                System.Diagnostics.Debug.WriteLine("Username is " + Request.Form["user_name"]);
                System.Diagnostics.Debug.WriteLine("dATATBASE USERNAME  is " + user.user_name);

                // System.Diagnostics.Debug.WriteLine("Passwod is " + Request.Form["password"]);

                //  if (Convert.ToString(Request.Form["user_name"]) == "admin" && Convert.ToString(Request.Form["password"]) == "admin")
                //{

                if (user.user_name == user_name && user.password == password)
                {
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    return RedirectToAction("Index", "Dashboard");
                }

                else
                {
                    ModelState.AddModelError("Error", "Incorrect Password!!");
                    return View("Index");
                }


            }

            catch
            {
                ModelState.AddModelError("Error", "No such user exists");
                return View("Index");
            }




        }
        public ActionResult Logout()
        {

            var value = HttpContext.Session.GetString("user");


            if (value != null)
            {


                HttpContext.Session.Clear();



                return View("Index");
            }
            else
            {
                return View("Index");
            }

        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
