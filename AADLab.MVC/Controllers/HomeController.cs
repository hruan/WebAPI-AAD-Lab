using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using AADLab.MVC.Models;

namespace AADLab.MVC.Controllers
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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Orders()
        {
            IEnumerable<Order> orders;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:12345");
                var resp = await client.GetAsync("/orders/some-customer-id");
                resp.EnsureSuccessStatusCode();

                orders = await resp.Content.ReadAsAsync<IEnumerable<Order>>();
            }

            return View(orders);
        }
    }
}