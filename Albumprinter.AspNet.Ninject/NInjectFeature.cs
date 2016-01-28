using System;
using System.Web;
using Albumprinter.AspNet.Core;
using Albumprinter.Features;
using JetBrains.Annotations;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

namespace Albumprinter.AspNet.NInject
{
    public sealed class NInjectFeature : Feature
    {
        private static readonly NInjectFeature Instance = new NInjectFeature();
        private readonly IBootstrapper Bootstrapper = new Bootstrapper();

        [PublicAPI]
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Instance.Activate(null);
        }

        [PublicAPI]
        public static void Stop()
        {
            Instance.Deactivate(null);
        }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
            if (Bootstrapper.Kernel == null)
            {
                Bootstrapper.Initialize(CreateKernel);
            }
            if (ctx != null)
            {
                var kernel = Bootstrapper.Kernel;
                ctx.Set<IKernel>(kernel);
                ctx.Set<IServiceCollection>(new NInjectServiceCollection(kernel));
            }
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                //kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectWebApiHttpApplicationPlugin>();
                //kernel.Components.Add<IWebApiRequestScopeProvider, DefaultWebApiRequestScopeProvider>();

                //kernel.Bind<IKernel>().ToMethod(ctx => ctx.Kernel);
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => ctx.Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}