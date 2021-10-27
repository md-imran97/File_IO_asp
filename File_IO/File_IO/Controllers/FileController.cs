using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// for large file upto 30 mb you need to add glabal web config

//<system.web>
//    <compilation debug = "true" targetFramework="4.7.1"/>
//    <httpRuntime targetFramework = "4.7.1" maxRequestLength="2147483647" executionTimeout="1600" requestLengthDiskThreshold="2147483647" />

//</system.web>

// local web config in system.webserver

//<security>
//      <requestFiltering>
//        <requestLimits maxAllowedContentLength = "2147483647" />
//      </ requestFiltering >
//    </ security >

namespace File_IO.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            int a = 10;
            int b = a;
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/res/"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("upload");
            //return View();
        }
        public ActionResult Show()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/res/"));

            ViewBag.path = filePaths;
            return View();
        }
        public ActionResult down(string name)
        {
            int a = 0;
            download(name);
           return RedirectToAction("/File/show");
        }
        public FileResult download(string name)
        {
            string path = Server.MapPath("~/res/") + name;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", "yaboon.jpg");
        }
        
        public ActionResult delete(string name)
        {
            string fullPath = Request.MapPath("~/res/" + name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return RedirectToAction("show","File");
        }
    }
}