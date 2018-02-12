using CompanyDefender.Models;
using CompanyDefender.Models.DeviceLogsAnalysis;
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
        private CorrespondenceAnalysisVMCreator personMailGraphVMCreator;

        public HomeController()
        {
            restfulClient = new RESTfulClient();
            personMailGraphVMCreator = new CorrespondenceAnalysisVMCreator();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult AllMails()
        {
            var jsonResponse = restfulClient.GetAllMailsAsync().Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("CorrespondenceAnalysis", personMailViewModel);
        }

        public ActionResult SearchByBody(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var jsonResponse = restfulClient.SearchMailsByBodyAsync(personMailFullViewModelFromForm.Query).Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("CorrespondenceAnalysis", personMailViewModel);
        }

        public ActionResult SearchByAttachment(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var jsonResponse = restfulClient.SearchMailsByAttachmentAsync(personMailFullViewModelFromForm.Query).Result;
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("CorrespondenceAnalysis", personMailViewModel);
        }

        public ActionResult InternalSystemsAnalysis()
        {
            ViewBag.Message = "InternalSystemsAnalysis";

            return View();
        }

        public FileResult DownloadAttachment(string fileName)
        {
            var fullFileName = fileName + ".pdf";
            byte[] fileBytes = restfulClient.DownloadFileFromFoxxAsync(fullFileName).Result;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fullFileName);
        }

        public ActionResult DeviceLogsAnalysis(string startDate = null, string endDate = null)
        {
            if (startDate == null)
                startDate = new DateTime(2017, 12, 22).ToString();
            if (endDate == null)
                endDate = new DateTime(2018, 1, 23).ToString();

            var jsonResponse = restfulClient.GetDateForAntivirusLineChartAsync(startDate, endDate).Result;
            List<AntivirusUpdateData> antivirusData = JsonConvert.DeserializeObject<List<AntivirusUpdateData>>(jsonResponse);

            var deviceLogsAntivirusAnalysisVMCreator = new DeviceLogsAntivirusAnalysisVMCreator(antivirusData, 
                startDate, endDate);
            var antivirusUpdateLineChartViewModel = deviceLogsAntivirusAnalysisVMCreator.CreateFromAntivirusUpdateData();

            ViewBag.LineChartData = antivirusUpdateLineChartViewModel;

            return View();
        }
    }
}