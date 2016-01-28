using Albumprinter.AspNet.Core;
using Ninject;
using Ninject.Web.Common;

namespace Albumprinter.AspNet.NInject
{
    /// <summary>
    /// Object Scopes
    /// <see cref="https://github.com/ninject/ninject/wiki/Object-Scopes"/>
    /// </summary>
    /// <seealso cref="IServiceCollection" />
    public sealed class NInjectServiceCollection : IServiceCollection
    {
        private IKernel Kernel { get; set; }

        public NInjectServiceCollection(IKernel kernel)
        {
            Kernel = kernel;
        }

        public void AddTransient<TSource, TTarget>() where TTarget : TSource
        {
            Kernel.Bind<TSource>().To<TTarget>();
        }

        public void AddSingleton<TSource, TTarget>() where TTarget : TSource
        {
            Kernel.Bind<TSource>().To<TTarget>().InSingletonScope();
        }

        public void AddInstance<TTarget>(TTarget target)
        {
            Kernel.Bind<TTarget>().ToConstant(target);
        }

        public void AddScoped<TSource, TTarget>() where TTarget : TSource
        {
            Kernel.Bind<TSource>().To<TTarget>().InRequestScope();
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new NInjectServiceProvider(Kernel, false);
        }
    }
}