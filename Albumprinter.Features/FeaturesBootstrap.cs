using System;
using System.Diagnostics;

namespace Albumprinter.Features
{
    public abstract class FeaturesBootstrap
    {
        protected FeaturesBootstrap()
        {
            Root = new CompositeFeature();
        }

        public CompositeFeature Root { get; private set; }
        private FeaturesBootstrapContext FeaturesBootstrapContext { get; set; }

        public void Start(params object[] services)
        {
            var ctx = new FeaturesBootstrapContext();
            ctx.Set(services);
            OnStart(ctx);
            Activate(ctx);
            FeaturesBootstrapContext = ctx;
        }

        public void Stop()
        {
            var ctx = FeaturesBootstrapContext;
            OnStop(ctx);
            Deactivate(ctx);
            FeaturesBootstrapContext = null;
        }

        public abstract void OnStart(FeaturesBootstrapContext ctx);
        public abstract void OnStop(FeaturesBootstrapContext ctx);

        protected FeaturesBootstrapContext Activate(FeaturesBootstrapContext ctx)
        {
            var pulse = ctx.Pulse;
            try
            {
                Root.Init(ctx);
                pulse.OnNext(FeaturesBootstrapState.Starting);
                pulse.OnNext(FeaturesBootstrapState.ActivateFeatures);
                pulse.OnNext(FeaturesBootstrapState.ActivatedFeatures);
                pulse.OnNext(FeaturesBootstrapState.RegisterServices);
                pulse.OnNext(FeaturesBootstrapState.RegisteredServices);
                pulse.OnNext(FeaturesBootstrapState.RegisterMiddleware);
                pulse.OnNext(FeaturesBootstrapState.RegisteredMiddleware);
                pulse.OnNext(FeaturesBootstrapState.StartShell);
                pulse.OnNext(FeaturesBootstrapState.StartedShell);
                pulse.OnNext(FeaturesBootstrapState.Started);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                pulse.OnError(ex);
            }
            return ctx;
        }

        protected void Deactivate(FeaturesBootstrapContext ctx)
        {
            var pulse = ctx.Pulse;
            try
            {
                pulse.OnNext(FeaturesBootstrapState.Stoping);
                pulse.OnNext(FeaturesBootstrapState.DeactivateFeatures);
                pulse.OnNext(FeaturesBootstrapState.DeactivatedFeatures);
                pulse.OnNext(FeaturesBootstrapState.Stoped);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                pulse.OnError(ex);
            }
        }
    }
}
