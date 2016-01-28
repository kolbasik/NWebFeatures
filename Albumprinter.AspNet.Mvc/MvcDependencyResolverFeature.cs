using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Albumprinter.Features;
using IServiceProvider = Albumprinter.AspNet.Core.IServiceProvider;

namespace Albumprinter.AspNet.Mvc
{
    public sealed class MvcDependencyResolverFeature : Feature
    {
        private IDependencyResolver Previous { get; set; }

        public override void Init(FeaturesBootstrapContext ctx)
        {
            base.Init(ctx);
            ctx.Pulse.On(FeaturesBootstrapState.RegisteredServices, OnRegisteredServices);
        }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            Previous = DependencyResolver.Current;
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
            var dependencyResolver = Previous;
            if (dependencyResolver != null)
            {
                DependencyResolver.SetResolver(dependencyResolver);
            }
        }

        private void OnRegisteredServices(FeaturesBootstrapContext ctx)
        {
            var serviceProvider = ctx.Get<IServiceProvider>();
            DependencyResolver.SetResolver(new MvcDependencyResolver(serviceProvider, Previous));
        }

        private sealed class MvcDependencyResolver : IDependencyResolver, IDisposable
        {
            private IServiceProvider Services { get; set; }
            private IDependencyResolver DependencyResolver { get; set; }

            public MvcDependencyResolver(IServiceProvider services, IDependencyResolver dependencyResolver)
            {
                Services = services;
                DependencyResolver = dependencyResolver;
            }

            public object GetService(Type serviceType)
            {
                object service;
                try
                {
                    service = Services.Get(serviceType);
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning(ex.Message);
                    service = DependencyResolver.GetService(serviceType);
                }
                return service;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                var services = new List<object>();
                try
                {
                    services.AddRange(Services.GetAll(serviceType));
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning(ex.Message);
                }
                finally
                {
                    services.AddRange(DependencyResolver.GetServices(serviceType));
                }
                return services;
            }

            public void Dispose()
            {
                Services.Dispose();
            }
        }
    }
}