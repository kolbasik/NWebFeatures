using System.Web.Http;
using Albumprinter.Features;
using Owin;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiFeature : CompositeFeature
    {
        public WebApiFeature()
        {
            UseGlobalConfiguration = false;

            DependencyResolver = new WebApiDependencyResolverFeature();
            Routes = new WebApiRoutesFeature();
            Swagger = new WebApiSwaggerFeature();
            MediaTypeFormatters = new WebApiMediaTypeFormattersFeature();

            Features.AddRange(new Feature[] { DependencyResolver, Routes, Swagger, MediaTypeFormatters });
        }

        public bool UseGlobalConfiguration { get; set; }
        public WebApiDependencyResolverFeature DependencyResolver { get; private set; }
        public WebApiRoutesFeature Routes { get; private set; }
        public WebApiSwaggerFeature Swagger { get; private set; }
        public WebApiMediaTypeFormattersFeature MediaTypeFormatters { get; private set; }

        public override void Init(FeaturesBootstrapContext ctx)
        {
            base.Init(ctx);
            ctx.Pulse.On(FeaturesBootstrapState.RegisteredServices, RegisteredServices);
        }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            if (UseGlobalConfiguration)
            {
                GlobalConfiguration.Configure(config => Activate(ctx, config));
            }
            else
            {
                Activate(ctx, new HttpConfiguration());
            }
        }

        private void Activate(FeaturesBootstrapContext ctx, HttpConfiguration config)
        {
            ctx.Set(config);
            base.Activate(ctx);
        }

        private static void RegisteredServices(FeaturesBootstrapContext ctx)
        {
            var app = ctx.Get<IAppBuilder>();
            var configuration = ctx.Get<HttpConfiguration>();
            app.UseWebApi(configuration);
        }
    }
}