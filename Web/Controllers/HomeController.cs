using System;
using System.Web.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IDateTimeService DateTimeService { get; set; }

        public HomeController(IDateTimeService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            DateTimeService = service;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Start Page";

            return View(new IndexOutput { UtcTime = DateTimeService.GetUtcTime() });
        }

        public class IndexOutput
        {
            public DateTime UtcTime { get; set; }
        }
    }
}