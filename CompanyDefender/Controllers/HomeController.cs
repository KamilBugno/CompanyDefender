using CompanyDefender.Models;
using CompanyDefender.REST;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyDefender.Controllers
{
    public class HomeController : Controller
    {
        private RESTfulClient restfulClient;

        public HomeController()
        {
            restfulClient = new RESTfulClient();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailsViewer()
        {
            var mails = new List<Mail>
            {
                new Mail(1, 2),
                new Mail(2, 3)
            };

            return View(mails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var response = restfulClient.GetHelloAsync().Result;

            return View((object)response);
        }

        public FileResult Contact()
        {
            var result = restfulClient.UploadFileToFoxxAsync(@"d:\rok-1984.jpg", "rok-1984.jpg");

            byte[] fileBytes = restfulClient.DownloadFileFromFoxxAsync("1.jpg").Result;//System.IO.File.ReadAllBytes(@"d:\rok-1984.jpg");
            string fileName = "rok-1984.jpg";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}