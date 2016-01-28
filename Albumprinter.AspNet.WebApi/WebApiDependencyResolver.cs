using System.Web.Http.Dependencies;
using Albumprinter.AspNet.Core;

namespace Albumprinter.AspNet.WebApi
{
    public sealed class WebApiDependencyResolver : WebApiDependencyScope, IDependencyResolver
    {
        public WebApiDependencyResolver(IServiceProvider services, IDependencyResolver dependencyResolver)
            : base(services, dependencyResolver)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyScope(ServiceProvider, DependencyResolver);
        }
    }
}