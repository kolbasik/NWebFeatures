using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Syntax;
using IServiceProvider = Albumprinter.AspNet.Core.IServiceProvider;

namespace Albumprinter.AspNet.NInject
{
    public sealed class NInjectServiceProvider : Core.IServiceProvider
    {
        private IKernel Kernel { get; set; }
        private IResolutionRoot ResolutionRoot { get; set; }

        public NInjectServiceProvider(IKernel kernel, bool useActivationBlock)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            Kernel = kernel;
            ResolutionRoot = useActivationBlock ? (IResolutionRoot)kernel.BeginBlock() : kernel;
        }

        public TTarget Get<TTarget>()
        {
            return ResolutionRoot.Get<TTarget>();
        }

        public object Get(Type serviceType)
        {
            return ResolutionRoot.Get(serviceType);
        }

        public IEnumerable<TTarget> GetAll<TTarget>()
        {
            return ResolutionRoot.GetAll<TTarget>();
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return ResolutionRoot.GetAll(serviceType);
        }
        public IServiceProvider BeginScope()
        {
            return new NInjectServiceProvider(Kernel, true);
        }

        public object GetService(Type serviceType)
        {
            return ResolutionRoot.Get(serviceType);
        }

        public void Dispose()
        {
            var disposable = ResolutionRoot as IDisposable;
            disposable?.Dispose();
        }
    }
}