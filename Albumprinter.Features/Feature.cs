namespace Albumprinter.Features
{
    public abstract class Feature
    {
        protected Feature()
        {
            Enabled = true;
        }

        public virtual bool Enabled { get; set; }

        public virtual void Init(FeaturesBootstrapContext ctx)
        {
            ctx.Pulse.On(FeaturesBootstrapState.ActivateFeatures, Activate);
            ctx.Pulse.On(FeaturesBootstrapState.DeactivateFeatures, Deactivate);
        }

        public abstract void Activate(FeaturesBootstrapContext ctx);

        public abstract void Deactivate(FeaturesBootstrapContext ctx);
    }
}