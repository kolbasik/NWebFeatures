using System.Web.Http;
using System.Web.Http.Dependencies;
using Albumprinter.AspNet.Core;
using Albumprinter.Features;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiDependencyResolverFeature : Feature
    {
        private IDependencyResolver Previous { get; set; }

        public override void Init(FeaturesBootstrapContext ctx)
        {
            base.Init(ctx);
            ctx.Pulse.On(FeaturesBootstrapState.RegisteredServices, OnRegisteredServices);
        }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            var config = ctx.Get<HttpConfiguration>();
            Previous = config.DependencyResolver;
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
            var dependencyResolver = Previous;
            if (dependencyResolver != null)
            {
                var config = ctx.Get<HttpConfiguration>();
                config.DependencyResolver = dependencyResolver;
            }
        }

        private void OnRegisteredServices(FeaturesBootstrapContext ctx)
        {
            var config = ctx.Get<HttpConfiguration>();
            var serviceProvider = ctx.Get<IServiceProvider>();
            config.DependencyResolver = new WebApiDependencyResolver(serviceProvider, Previous);
        }
    }
}