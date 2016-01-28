using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Albumprinter.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiMediaTypeFormattersFeature : Feature
    {
        public WebApiMediaTypeFormattersFeature()
        {
            UseJson = true;
            UseXml = false;
        }

        public bool UseJson { get; set; }
        public bool UseXml { get; set; }

        private MediaTypeFormatter[] Previous { get; set; }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            var formatters = ctx.Get<HttpConfiguration>().Formatters;
            Previous = formatters.ToArray();

            var json = formatters.JsonFormatter;
            var xml = formatters.XmlFormatter;

            formatters.Clear();

            if (UseJson)
            {
                json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                json.SerializerSettings.Converters.Add(new StringEnumConverter());
                formatters.Add(json);
            }

            if (UseXml)
            {
                xml.UseXmlSerializer = true;
                formatters.Add(xml);
            }
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
            if (Previous != null)
            {
                var formatters = ctx.Get<HttpConfiguration>().Formatters;
                formatters.Clear();
                formatters.AddRange(Previous);
            }
        }
    }
}