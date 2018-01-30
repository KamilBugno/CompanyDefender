using CompanyDefender.REST;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyDefender.Controllers
{
    public class HomeController : Controller
    {
        private RESTfulClient restfulClient;

        public HomeController()
        {
            restfulClient = new RESTfulClient();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var response = restfulClient.GetHelloAsync().Result;

            return View((object)response);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}