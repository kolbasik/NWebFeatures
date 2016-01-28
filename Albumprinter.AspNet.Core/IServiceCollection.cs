namespace Albumprinter.AspNet.Core
{
    public interface IServiceCollection
    {
        /// <summary>
        /// A new instance is created every time.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        void AddTransient<TSource, TTarget>() where TTarget : TSource;

        /// <summary>
        /// A single instance is created and it acts like a singleton.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        void AddSingleton<TSource, TTarget>() where TTarget : TSource;

        /// <summary>
        /// A specific instance is given all the time. You are responsible for its initial creation.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="target">The source.</param>
        void AddInstance<TSource>(TSource target);

        /// <summary>
        /// A single instance is created inside the current scope. It is equivalent to Singleton in the current scope.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        void AddScoped<TSource, TTarget>() where TTarget : TSource;

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <returns></returns>
        IServiceProvider BuildServiceProvider();
    }
}
