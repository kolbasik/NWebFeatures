using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using IServiceProvider = Albumprinter.AspNet.Core.IServiceProvider;

namespace Albumprinter.AspNet.WebApi
{
    public class WebApiDependencyScope : IDependencyScope
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        protected IDependencyResolver DependencyResolver { get; private set; }

        public WebApiDependencyScope(IServiceProvider services, IDependencyResolver dependencyResolver)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (dependencyResolver == null)
            {
                throw new ArgumentNullException(nameof(dependencyResolver));
            }

            ServiceProvider = services.BeginScope();
            DependencyResolver = dependencyResolver;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return ServiceProvider.Get(serviceType);
            }
            catch
            {
                return DependencyResolver.GetService(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceProvider.GetAll(serviceType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServiceProvider.Dispose();
            }
        }
    }
}