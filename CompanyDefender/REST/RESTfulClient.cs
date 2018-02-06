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

        public async Task<String> GetMailsAsync(string query)
        {
            if (query == null)
                query = String.Empty;
            String response = null;
            var httpResponse = await client.GetAsync(ApplicationConstant.urlService + ApplicationConstant.mailsAction + query)
                .ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
                response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

        public async Task UploadFileToFoxxAsync(string filePathFromClient, string foxxFileName)
        {
            using (var stream = File.OpenRead(filePathFromClient))
            {
                var fullUrl = ApplicationConstant.urlService + ApplicationConstant.uploadFileAction + foxxFileName;
                var httpResponse = await client.PostAsync(fullUrl, new StreamContent(stream)).ConfigureAwait(false);
                httpResponse.EnsureSuccessStatusCode();
            }
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