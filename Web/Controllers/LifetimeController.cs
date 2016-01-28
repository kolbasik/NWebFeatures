using System;
using System.Web.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class LifetimeController : Controller
    {
        private LifetimeSnapshotProvider LifetimeSnapshotProvider { get; }

        public LifetimeController(LifetimeSnapshotProvider lifetimeSnapshotProvider)
        {
            if (lifetimeSnapshotProvider == null)
            {
                throw new ArgumentNullException(nameof(lifetimeSnapshotProvider));
            }

            LifetimeSnapshotProvider = lifetimeSnapshotProvider;
        }

        public ActionResult Index()
        {
            var model = LifetimeSnapshotProvider.GetLifetimeSnapshot();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}