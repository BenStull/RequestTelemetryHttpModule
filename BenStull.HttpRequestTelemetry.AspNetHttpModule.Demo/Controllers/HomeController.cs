using System;
using System.Linq;
using System.Web.Mvc;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            // Add a random string to the page so the response body size varies
            const string chars = "    abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var length = random.Next(1024, 10240);

            ViewBag.RandomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return View();
        }
    }
}