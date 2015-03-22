using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebJobsDemo.Sample;

namespace WebJobsDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("order");
        }

        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Email = "anthony@anthonychu.ca",
                NumberOfWidgets = 6
            };
            var orderProcessor = new OrderProcessor(new EmailService());

            await orderProcessor.Process(newOrder);

            ViewBag.TimeTaken = stopwatch.ElapsedMilliseconds;

            return View("Order", newOrder);
        }


        public ActionResult OrderAsync()
        {
            return View("Order");
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrderAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Email = "anthony@anthonychu.ca",
                NumberOfWidgets = 6
            };
            var orderProcessor = new QueueOrderProcessor();

            await orderProcessor.Process(newOrder);
            ViewBag.TimeTaken = stopwatch.ElapsedMilliseconds;

            return View("Order", newOrder);
        }
    }
}
