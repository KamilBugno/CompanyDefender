using CompanyDefender.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CompanyDefender.REST
{
    public class RESTfulClient
    {
        private HttpClient client;

        public RESTfulClient()
        {
            client = new HttpClient();
        }

        public String GetPersonDetails(string key)
        {
            return GetAction(ApplicationConstant.urlService,
               ApplicationConstant.getPersonDetails, key);
        }

        public String GetPeopleWhoDoNotUpdateAntivirus(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService,
               ApplicationConstant.getPeopleWhoDoNotUpdateAntivirus, startDate, endDate);
        }

        public string GetDateForAntivirusPieChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService,
               ApplicationConstant.getAntivirusDateForPieChart, startDate, endDate);
        }

        public string GetDateForAntivirusLineChart(string startDate, string endDate)
        {
           return GetAction(ApplicationConstant.urlService, 
                ApplicationConstant.getAntivirusDateForLineChart, startDate, endDate);
        }

        public string GetNameForFailedLoginLineChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService,
               ApplicationConstant.getNameForEmployeesAccountFailedLogin, startDate, endDate);
        }

        public string GetIpForFailedLoginLineChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService,
               ApplicationConstant.getIpForEmployeesAccountFailedLogin, startDate, endDate);
        }

        public string GetAllMails(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService, ApplicationConstant.getAllMails, startDate, endDate);
        }

        public string SearchMailsByBody(string query, string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService, ApplicationConstant.searchMailsByBodyAction, query, startDate, endDate);
        }

        public string SearchMailsByAttachment(string query, string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlService, ApplicationConstant.searchMailsByAttachmentAction, query, startDate, endDate);
        }

        public async Task<byte[]> DownloadFileFromFoxxAsync(string foxxFileName)
        {
            var fullUrl = CreateUrl(ApplicationConstant.urlService, 
                ApplicationConstant.downloadFileAction, foxxFileName);
            var httpResponse = await client.GetAsync(fullUrl).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

            byte[] response = null;
            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsByteArrayAsync();
            return response;
        }

        private string GetAction(string urlService, string urlAction, params string[] args)
        {
            String response = null;
            var fullUrl = CreateUrl(urlService, urlAction, args);

            var httpResponse = client.GetAsync(fullUrl).Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = httpResponse.Content;
                response = responseContent.ReadAsStringAsync().Result;
            }

            return response;
        }

        private string CreateUrl(string urlService, string urlAction, params string[] args)
        {
            var fullUrl = urlService + urlAction;

            foreach (string arg in args)
            {
                fullUrl += "/" + arg;
            }

            return fullUrl;
        }
    }
}