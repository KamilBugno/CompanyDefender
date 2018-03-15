using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CompanyDefender.HTTP
{
    public class Client
    {
        protected HttpClient client;

        public Client()
        {
            client = new HttpClient();
        }

        protected string GetAction(string urlService, string urlAction, params string[] args)
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

        protected string CreateUrl(string urlService, string urlAction, params string[] args)
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