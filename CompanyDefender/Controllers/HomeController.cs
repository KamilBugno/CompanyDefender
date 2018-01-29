using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyDefender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var client = new HttpClient();
            var response = GetHelloAsync();

            var a = response.Result;

            return View((object)a);
        }

        static async Task<String> GetHelloAsync()
        {
            String hello = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8529/_db/_system/foxx-service/hello-world")
                .ConfigureAwait(false); 
            if (response.IsSuccessStatusCode)
            {
                hello = await response.Content.ReadAsStringAsync();
            }
            return hello;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}