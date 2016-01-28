using System.Web.Mvc;
using System.Web.Routing;
using Albumprinter.Features;

namespace Albumprinter.AspNet.Mvc
{
    public sealed class MvcFeature : CompositeFeature
    {
        public MvcFeature()
        {
            DependencyResolver = new MvcDependencyResolverFeature();
            Areas = new MvcAreasFeature();
            Filters = new MvcGlobalFiltersFeature(GlobalFilters.Filters);
            Routes = new MvcRoutesFeature(RouteTable.Routes);

            Features.AddRange(new Feature[] { DependencyResolver, Areas, Filters, Routes });
        }

        public MvcDependencyResolverFeature DependencyResolver { get; private set; }
        public MvcAreasFeature Areas { get; private set; }
        public MvcGlobalFiltersFeature Filters { get; private set; }
        public MvcRoutesFeature Routes { get; private set; }
    }
}