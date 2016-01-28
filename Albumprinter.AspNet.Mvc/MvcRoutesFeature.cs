using System.Web.Mvc;
using System.Web.Routing;
using Albumprinter.Features;

namespace Albumprinter.AspNet.Mvc
{
    public sealed class MvcRoutesFeature : Feature
    {
        public MvcRoutesFeature(RouteCollection routes)
        {
            Url = "{controller}/{action}/{id}";
            Defaults = new { controller = "Home", action = "Index", id = UrlParameter.Optional };
            Routes = routes;
        }

        public string Url { get; set; }
        public object Defaults { get; set; }
        public RouteCollection Routes { get; private set; }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            Routes.MapRoute("Default", Url, Defaults);
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
        }
    }
}