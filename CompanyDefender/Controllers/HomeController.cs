using CompanyDefender.HTTP;
using CompanyDefender.Models;
using CompanyDefender.Models.DeviceLogsAnalysis;
using CompanyDefender.Models.InternalSystemsLogsAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private GraphQLClient graphQLClient;
        private CorrespondenceAnalysisVMCreator personMailGraphVMCreator;

        public HomeController()
        {
            restfulClient = new RESTfulClient();
            graphQLClient = new GraphQLClient();
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

        public ActionResult InternalSystemsAnalysis(string startDate = null, string endDate = null)
        {
            if (startDate == null)
                startDate = new DateTime(2017, 12, 22).ToString("yyyy-MM-dd");
            if (endDate == null)
                endDate = new DateTime(2018, 3, 30).ToString("yyyy-MM-dd");

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            var jsonResponseLineChart = restfulClient.GetNameForFailedLoginLineChart(startDate, endDate);
            var nameFailedLoginLineChart = JsonConvert.DeserializeObject<List<EmployeesAccountNameFailedLogins>>(jsonResponseLineChart);
            var nameFailedLoginVMCreator = new FailedLoginsNameVMCreator(nameFailedLoginLineChart);
            var nameFailedLoginLineChartViewModel = nameFailedLoginVMCreator.CreateFromEmployeesAccountFailedLogins();
            ViewBag.NameLineChartData = nameFailedLoginLineChartViewModel;

            jsonResponseLineChart = restfulClient.GetIpForFailedLoginLineChart(startDate, endDate);
            var ipFailedLoginLineChart = JsonConvert.DeserializeObject<List<EmployeesAccountIpFailedLogins>>(jsonResponseLineChart);
            var ipFailedLoginVMCreator = new FailedLoginsIpVMCreator(ipFailedLoginLineChart);
            var ipFailedLoginLineChartViewModel = ipFailedLoginVMCreator.CreateFromEmployeesAccountFailedLogins();
            ViewBag.IpLineChartData = ipFailedLoginLineChartViewModel;

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
            var employeeKeys = JsonConvert.DeserializeObject<List<EmployeeKey>>(jsonResponse);
            var employees = new List<EmployeeWithDeviceNumber>();
            foreach(EmployeeKey ek in employeeKeys)
            {
                var fullJson = graphQLClient.GetPersonWithDevicesNumber(ek.key);
                JObject parsedJson = JObject.Parse(fullJson);
                JObject personJson = (JObject)parsedJson["data"]["person"];
                var employee = JsonConvert.DeserializeObject<EmployeeWithDeviceNumber>(personJson.ToString());
                employees.Add(employee);
            }
            ViewBag.Employee = employees;
            return PartialView();
        }

        public PartialViewResult _PersonDetailsPopup(string key)
        {
            var fullJson = graphQLClient.GetPerson(key);
            JObject parsedJson = JObject.Parse(fullJson);
            JObject personJson = (JObject)parsedJson["data"]["person"];
            var employee = JsonConvert.DeserializeObject<Employee>(personJson.ToString());
            return PartialView("_PersonDetailsPopup", employee);
        }
    }
}