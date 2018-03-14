
 

using Newtonsoft.Json;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BugReportMVC5.Models;

using System;

namespace BugReportMVC5.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var value = HttpContext.Session.GetString("user");

            User user = JsonConvert.DeserializeObject<User>(value);

            var ticketno = Convert.ToInt32(Request.Form["tt"]); 
            System.Diagnostics.Debug.WriteLine("ticketno id" + ticketno);

            long size = files.Sum(f => f.Length);

            // full path to file in temp location

            var filePath = "";
            // filePath += Path.GetTempFileName(); 

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = "Uploads/";
                    filePath += user.user_name;
                    // Directory.CreateDirectory(filePath);


                    //using (var stream = new FileStream(Path.Combine(filePath, formFile.FileName), FileMode.Create))
                    //{

                    //   await formFile.CopyToAsync(stream);

                    //}

                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        byte[] file = memoryStream.ToArray();

                        FileUploadContext context = HttpContext.RequestServices.GetService(typeof(FileUploadContext)) as FileUploadContext;
                        context.SaveToDatabase(file, formFile.FileName, formFile.ContentType,ticketno);
                        //context.GetFileNames();
                    }

                }
            }






            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            // return Ok(new { count = files.Count, size, filePath });
            // TempData["User"] = user;
            return RedirectToAction("Index", "Dashboard", user);
            //  return View("Index","Dashboard");
        }
           
        //public List<string> GetFileNames()
        //{
        //   // FileUploadContext context = HttpContext.RequestServices.GetService(typeof(FileUploadContext)) as FileUploadContext;


        //    //List < File > files = context.GetFiles();

        //    //ViewBag.ListOfFiles = files;

        //    //return context.GetFileNames();

        //}
          

    }
}