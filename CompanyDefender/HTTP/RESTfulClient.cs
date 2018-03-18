using CompanyDefender.Constant;
using CompanyDefender.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CompanyDefender.HTTP
{
    public class RESTfulClient : Client
    {
        public String GetPersonDetails(string key)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
               ApplicationConstant.getPersonDetails, key);
        }

        public String GetPeopleWhoDoNotUpdateAntivirus(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
               ApplicationConstant.getPeopleWhoDoNotUpdateAntivirus, startDate, endDate);
        }

        public string GetDateForAntivirusPieChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
               ApplicationConstant.getAntivirusDateForPieChart, startDate, endDate);
        }

        public string GetDateForAntivirusLineChart(string startDate, string endDate)
        {
           return GetAction(ApplicationConstant.urlFoxxService, 
                ApplicationConstant.getAntivirusDateForLineChart, startDate, endDate);
        }

        public string GetMailByKey(string key)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
                 ApplicationConstant.getMailByKey, key);
        }

        public string GetNameForFailedLoginLineChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
               ApplicationConstant.getNameForEmployeesAccountFailedLogin, startDate, endDate);
        }

        public string GetIpForFailedLoginLineChart(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService,
               ApplicationConstant.getIpForEmployeesAccountFailedLogin, startDate, endDate);
        }

        public string GetAllMails(string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService, ApplicationConstant.getAllMails, startDate, endDate);
        }

        public string SearchMailsByBody(string query, string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService, ApplicationConstant.searchMailsByBodyAction, query, startDate, endDate);
        }

        public string SearchMailsByAttachment(string query, string startDate, string endDate)
        {
            return GetAction(ApplicationConstant.urlFoxxService, ApplicationConstant.searchMailsByAttachmentAction, query, startDate, endDate);
        }

        public async Task<byte[]> DownloadFileFromFoxxAsync(string foxxFileName)
        {
            var fullUrl = CreateUrl(ApplicationConstant.urlFoxxService, 
                ApplicationConstant.downloadFileAction, foxxFileName);
            var httpResponse = await client.GetAsync(fullUrl).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

            byte[] response = null;
            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsByteArrayAsync();
            return response;
        }
    }
}