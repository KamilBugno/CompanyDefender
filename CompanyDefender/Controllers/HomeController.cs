using CompanyDefender.Models;
using CompanyDefender.REST;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyDefender.Controllers
{
    public class HomeController : Controller
    {
        private RESTfulClient restfulClient;
        private PersonMailGraphVMCreator personMailGraphVMCreator;

        public HomeController()
        {
            restfulClient = new RESTfulClient();
            personMailGraphVMCreator = new PersonMailGraphVMCreator();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllMails()
        {
            var jsonResponse = restfulClient.GetAllMailsAsync().Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("MailsViewer", personMailViewModel);
        }

        public ActionResult SearchByBody(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var jsonResponse = restfulClient.SearchMailsByBodyAsync(personMailFullViewModelFromForm.Query).Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("MailsViewer", personMailViewModel);
        }

        public ActionResult SearchByAttachment(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var jsonResponse = restfulClient.SearchMailsByAttachmentAsync(personMailFullViewModelFromForm.Query).Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("MailsViewer", personMailViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            

            return View((object)"hello");
        }

        public FileResult Contact()
        {


            byte[] fileBytes = restfulClient.DownloadFileFromFoxxAsync("23.pdf").Result;//System.IO.File.ReadAllBytes(@"d:\rok-1984.jpg");
            string fileName = "23.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}