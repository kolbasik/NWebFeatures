using System;
using System.Collections.Generic;

namespace Albumprinter.Features
{
    public class CompositeFeature : Feature
    {
        public CompositeFeature()
        {
            Features = new List<Feature>();
        }

        public List<Feature> Features { get; private set; }

        public override void Init(FeaturesBootstrapContext ctx)
        {
            base.Init(ctx);
            ForEach(feature => feature.Init(ctx));
        }

        public override void Activate(FeaturesBootstrapContext ctx)
        {
        }

        public override void Deactivate(FeaturesBootstrapContext ctx)
        {
        }

        private void ForEach(Action<Feature> action)
        {
            if (Enabled)
            {
                foreach (var feature in Features)
                {
                    if (feature.Enabled)
                    {
                        action(feature);
                    }
                }
            }
        }
    }
}