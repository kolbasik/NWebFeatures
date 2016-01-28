using System;
using Albumprinter.Features.Rx;

namespace Albumprinter.Features
{
    public sealed class FeaturesBootstrapPulse : PublishSubject<FeaturesBootstrapState>
    {
        private FeaturesBootstrapContext FeaturesBootstrapContext { get; set; }

        public FeaturesBootstrapPulse(FeaturesBootstrapContext context)
        {
            FeaturesBootstrapContext = context;
        }

        public IDisposable On(FeaturesBootstrapState state, Action<FeaturesBootstrapContext> action)
        {
            return Subscribe(
                Observer.Create<FeaturesBootstrapState>(
                    x =>
                    {
                        if (x == state)
                        {
                            action(FeaturesBootstrapContext);
                        }
                    }));
        }

        public IDisposable Catch(Action<FeaturesBootstrapContext, Exception> action)
        {
            return Subscribe(Observer.Catch<FeaturesBootstrapState>(ex => action(FeaturesBootstrapContext, ex)));
        }
    }
}