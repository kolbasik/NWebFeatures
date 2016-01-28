using System.Web.Http;
using Albumprinter.Features;
using Swashbuckle.Application;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiSwaggerFeature : Feature
    {
        public WebApiSwaggerFeature()
        {
            UseSwaggerUi = true;
        }

        public bool UseSwaggerUi { get; set; }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            var config = ctx.Get<HttpConfiguration>();

            var swagger = config.EnableSwagger(ConfigureSwaggerDocs);
            if (UseSwaggerUi)
            {
                swagger.EnableSwaggerUi();
            }
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
        }

        private void ConfigureSwaggerDocs(SwaggerDocsConfig swaggerDocsConfig)
        {
            swaggerDocsConfig.SingleApiVersion("v1", this.GetType().Assembly.GetName().Name);
            swaggerDocsConfig.UseFullTypeNameInSchemaIds();
        }
    }
}