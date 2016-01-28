using System.Web.Http;
using Albumprinter.Features;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiRoutesFeature : Feature
    {
        public override void Activate(FeaturesBootstrapContext ctx)
        {
            var config = ctx.Get<HttpConfiguration>();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
        }
    }
}