using System;
using System.Web.Http;
using Web.Services;

namespace Web.Controllers
{
    [RoutePrefix("api/lifetime")]
    public class LifetimeApiController : ApiController
    {
        private LifetimeSnapshotProvider LifetimeSnapshotProvider { get; set; }

        public LifetimeApiController(LifetimeSnapshotProvider lifetimeSnapshotProvider)
        {
            if (lifetimeSnapshotProvider == null)
            {
                throw new ArgumentNullException(nameof(lifetimeSnapshotProvider));
            }

            LifetimeSnapshotProvider = lifetimeSnapshotProvider;
        }

        [HttpGet, Route("")]
        public LifetimeSnapshot[] Get()
        {
            var model = LifetimeSnapshotProvider.GetLifetimeSnapshot();
            return model;
        }
    }
}