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

        public ActionResult AllMails(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            if(personMailFullViewModelFromForm.StartDate == null)
                personMailFullViewModelFromForm.StartDate = new DateTime(2017, 12, 22).ToString("yyyy-MM-dd");
            if (personMailFullViewModelFromForm.EndDate == null)
                personMailFullViewModelFromForm.EndDate = new DateTime(2018, 01, 30).ToString("yyyy-MM-dd");

            var jsonResponse = restfulClient.GetAllMails(personMailFullViewModelFromForm.StartDate,
                personMailFullViewModelFromForm.EndDate);
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);

            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);
            personMailViewModel.StartDate = personMailFullViewModelFromForm.StartDate;
            personMailViewModel.EndDate = personMailFullViewModelFromForm.EndDate;

            return View("CorrespondenceAnalysis", personMailViewModel);
        }

        public ActionResult SearchByBody(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var query = personMailFullViewModelFromForm.Query;
            var startDate = personMailFullViewModelFromForm.StartDate;
            var endDate = personMailFullViewModelFromForm.EndDate;
            var jsonResponse = restfulClient.SearchMailsByBody(query, startDate, endDate);
            List<MailRecord> mails = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse);
            var personMailViewModel = personMailGraphVMCreator.CreateFromMailRecords(mails);

            return View("CorrespondenceAnalysis", personMailViewModel);
        }

        public ActionResult SearchByAttachment(PersonMailFullViewModel personMailFullViewModelFromForm)
        {
            var query = personMailFullViewModelFromForm.Query;
            var startDate = personMailFullViewModelFromForm.StartDate;
            var endDate = personMailFullViewModelFromForm.EndDate;
            var jsonResponse = restfulClient.SearchMailsByAttachment(query, startDate, endDate);
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
                startDate = new DateTime(2017, 12, 22).ToString("yyyy-MM-dd"); 
            if (endDate == null)
                endDate = new DateTime(2018, 1, 21).ToString("yyyy-MM-dd");

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            var jsonResponseLineChart = restfulClient.GetDateForAntivirusLineChart(startDate, endDate);
            var antivirusLineChartData = JsonConvert.DeserializeObject<List<AntivirusUpdateLineChartData>>(jsonResponseLineChart);
            var deviceLogsLineChartVMCreator = new DeviceLogsLineGraphVMCreator(antivirusLineChartData,
               startDate, endDate);
            var antivirusUpdateLineChartViewModel = deviceLogsLineChartVMCreator.CreateFromAntivirusUpdateData();
            ViewBag.LineChartData = antivirusUpdateLineChartViewModel;

            var jsonResponsePieChart = restfulClient.GetDateForAntivirusPieChart(startDate, endDate);
            var antivirusPieChartData = JsonConvert.DeserializeObject<List<AntivirusUpdatePieChart>>(jsonResponsePieChart);
            ViewBag.PieChartData = antivirusPieChartData[0];

            return View();
        }

        public PartialViewResult _AntivirusPopup(string startDate, string endDate)
        {
            var jsonResponse = restfulClient.GetPeopleWhoDoNotUpdateAntivirus(startDate, endDate);
            var employee = JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);
            ViewBag.Employee = employee;
            return PartialView();
        }

        public PartialViewResult _PersonDetailsPopup(string key)
        {
            var jsonResponse = restfulClient.GetPersonDetails(key);
            var employee = JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);
            return PartialView("_PersonDetailsPopup", employee[0]);
        }
    }
}