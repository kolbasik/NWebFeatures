using System.Web.Mvc;
using Albumprinter.Features;

namespace Albumprinter.AspNet.Mvc
{
    public sealed class MvcAreasFeature : Feature
    {
        public override void Activate(FeaturesBootstrapContext ctx)
        {
            AreaRegistration.RegisterAllAreas(ctx);
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
        }
    }
}