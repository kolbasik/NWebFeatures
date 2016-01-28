using System;
using System.Diagnostics;
using Albumprinter.AspNet.Core;
using Albumprinter.AspNet.Mvc;
using Albumprinter.AspNet.NInject;
using Albumprinter.AspNet.WebApi;
using Albumprinter.Features;
using Web.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NInjectFeature), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NInjectFeature), "Stop")]

namespace Web
{
    public sealed class WebAppBootstrap : FeaturesBootstrap
    {
        public WebAppBootstrap()
        {
            NInject = new NInjectFeature();
            Mvc = new MvcFeature();
            WebApi = new WebApiFeature();

            Root.Features.AddRange(new Feature[] { NInject, WebApi, Mvc });
        }

        public NInjectFeature NInject { get; private set; }
        public MvcFeature Mvc { get; private set; }
        public WebApiFeature WebApi { get; private set; }

        public override void OnStart(FeaturesBootstrapContext ctx)
        {
            ctx.Pulse.On(FeaturesBootstrapState.RegisterServices, x => OnRegisterServices(x.Get<IServiceCollection>()));
            ctx.Pulse.On(FeaturesBootstrapState.RegisteredServices, x => OnRegisteredServices(x.Get<IServiceCollection>(), x));
            ctx.Pulse.On(FeaturesBootstrapState.Started, OnStarted);
            ctx.Pulse.On(FeaturesBootstrapState.Stoped, OnStoped);
            ctx.Pulse.Catch(OnException);
        }

        public override void OnStop(FeaturesBootstrapContext ctx)
        {
        }

        private static void OnRegisterServices(IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddSingleton<LifetimeSnapshotProvider, LifetimeSnapshotProvider>();
            services.AddTransient<TransientLifetimeService, TransientLifetimeService>();
            services.AddSingleton<SingletonLifetimeService, SingletonLifetimeService>();
            services.AddInstance<InstanceLifetimeService>(new InstanceLifetimeService());
            services.AddScoped<ScopedLifetimeService, ScopedLifetimeService>();
        }

        private static void OnRegisteredServices(IServiceCollection services, FeaturesBootstrapContext ctx)
        {
            var serviceProvider = services.BuildServiceProvider();
            ctx.Set(serviceProvider);
        }

        private static void OnStarted(FeaturesBootstrapContext ctx)
        {
            Trace.WriteLine("Application is started.");
        }

        private static void OnStoped(FeaturesBootstrapContext ctx)
        {
            Trace.WriteLine("Application is stoped.");
        }

        private static void OnException(FeaturesBootstrapContext ctx, Exception exception)
        {
            Trace.TraceError("Application has failed: " + exception.Message);
        }
    }
}