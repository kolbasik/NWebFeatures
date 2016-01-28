using System;
using System.Collections.Generic;

namespace Albumprinter.AspNet.Core
{
    public interface IServiceProvider : System.IServiceProvider, IDisposable
    {
        /// <summary>
        /// Gets one instance.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <returns></returns>
        TTarget Get<TTarget>();

        /// <summary>
        /// Gets one instance.
        /// </summary>
        /// <returns></returns>
        object Get(Type serviceType);

        /// <summary>
        /// Gets all instances.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <returns></returns>
        IEnumerable<TTarget> GetAll<TTarget>();

        /// <summary>
        /// Gets all instances.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        IEnumerable<object> GetAll(Type serviceType);

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        IServiceProvider BeginScope();
    }
}