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

        public String GetPeopleWhoDoNotUpdateAntivirusAsync(string startDate, string endDate)
        {
            String response = null;
            var httpResponse = client.GetAsync(ApplicationConstant.urlService
                + ApplicationConstant.getPeopleWhoDoNotUpdateAntivirus
                + startDate + "/" + endDate)
                .Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = httpResponse.Content;
                response = responseContent.ReadAsStringAsync().Result;
            }

            return response;
        }

        public async Task<String> GetDateForAntivirusPieChartAsync(string startDate, string endDate)
        {
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService
                + ApplicationConstant.getAntivirusDateForPieChart
                + startDate + "/" + endDate)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<String> GetDateForAntivirusLineChartAsync(string startDate, string endDate)
        {
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService 
                + ApplicationConstant.getAntivirusDateForLineChart
                + startDate + "/" + endDate)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<String> GetAllMailsAsync()
        {
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService + ApplicationConstant.getAllMails)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<String> SearchMailsByBodyAsync(string query)
        {
            if (query == null)
                query = String.Empty;
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService + ApplicationConstant.searchMailsByBodyAction + query)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<String> SearchMailsByAttachmentAsync(string query)
        {
            if (query == null)
                return await SearchMailsByBodyAsync(String.Empty);
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService + ApplicationConstant.searchMailsByAttachmentAction + query)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<byte[]> DownloadFileFromFoxxAsync(string foxxFileName)
        {
            var fullUrl = ApplicationConstant.urlService + ApplicationConstant.downloadFileAction + foxxFileName;
            var httpResponse = await client.GetAsync(fullUrl).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

            byte[] response = null;
            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsByteArrayAsync();
            return response;
        }
    }
}