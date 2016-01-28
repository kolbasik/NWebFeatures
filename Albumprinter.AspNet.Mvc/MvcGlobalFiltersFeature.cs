using System.Collections.Generic;
using System.Web.Mvc;
using Albumprinter.Features;

namespace Albumprinter.AspNet.Mvc
{
    public sealed class MvcGlobalFiltersFeature : Feature
    {
        public MvcGlobalFiltersFeature(GlobalFilterCollection globalFilters)
        {
            GlobalFilters = globalFilters;
            FilterAttributes = new List<FilterAttribute>();
        }

        public GlobalFilterCollection GlobalFilters { get; private set; }
        private List<FilterAttribute> FilterAttributes { get; set; }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            Deactivate(ctx);
            FilterAttributes.Add(new HandleErrorAttribute());
            FilterAttributes.ForEach(GlobalFilters.Add);
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
            FilterAttributes.ForEach(GlobalFilters.Remove);
            FilterAttributes.Clear();
        }
    }
}