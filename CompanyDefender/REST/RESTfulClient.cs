using CompanyDefender.Constant;
using System;
using System.Collections.Generic;
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

        public async Task<String> GetHelloAsync()
        {
            String hello = null;
            HttpResponseMessage response = await client.GetAsync(ApplicationConstant.urlService + ApplicationConstant.helloWorldAction)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                hello = await response.Content.ReadAsStringAsync();
            
            return hello;
        }
    }
}