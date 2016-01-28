using System;
using System.Linq;
using System.Threading;
using IServiceProvider = Albumprinter.AspNet.Core.IServiceProvider;

namespace Web.Services
{
    public interface ILifetimeService : IDisposable
    {
        LifetimeData GetLifetimeData();
    }

    public sealed class LifetimeData
    {
        public long Created;
        public long Alive;
        public long Disposed;

        public LifetimeData Copy()
        {
            return new LifetimeData { Created = Created, Alive = Alive, Disposed = Disposed };
        }
    }

    public sealed class LifetimeSnapshotProvider
    {
        private IServiceProvider ServiceProvider { get; set; }

        public LifetimeSnapshotProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public LifetimeSnapshot[] GetLifetimeSnapshot()
        {
            var model =
                Enumerable.Range(0, 3)
                    .Select(
                        x =>
                            new LifetimeSnapshot
                            {
                                Transient = ServiceProvider.Get<TransientLifetimeService>().GetLifetimeData(),
                                Singleton = ServiceProvider.Get<SingletonLifetimeService>().GetLifetimeData(),
                                Instance = ServiceProvider.Get<InstanceLifetimeService>().GetLifetimeData(),
                                Scoped = ServiceProvider.Get<ScopedLifetimeService>().GetLifetimeData()
                            })
                    .ToArray();
            return model;
        }
    }

    public class LifetimeSnapshot
    {
        public LifetimeData Transient { get; set; }
        public LifetimeData Singleton { get; set; }
        public LifetimeData Instance { get; set; }
        public LifetimeData Scoped { get; set; }
    }

    public abstract class LifetimeService : ILifetimeService
    {
        private readonly LifetimeData lifetime;

        protected LifetimeService(LifetimeData lifetime)
        {
            this.lifetime = lifetime;
            Interlocked.Increment(ref lifetime.Created);
            Interlocked.Increment(ref lifetime.Alive);
        }

        ~LifetimeService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Interlocked.Decrement(ref lifetime.Alive);
            Interlocked.Increment(ref lifetime.Disposed);
        }

        public LifetimeData GetLifetimeData()
        {
            return lifetime.Copy();
        }
    }

    public sealed class TransientLifetimeService : LifetimeService
    {
        private static readonly LifetimeData LifetimeData = new LifetimeData();

        public TransientLifetimeService() : base(LifetimeData)
        {
        }
    }

    public sealed class SingletonLifetimeService : LifetimeService
    {
        private static readonly LifetimeData LifetimeData = new LifetimeData();

        public SingletonLifetimeService() : base(LifetimeData)
        {
        }
    }

    public sealed class InstanceLifetimeService : LifetimeService
    {
        private static readonly LifetimeData LifetimeData = new LifetimeData();

        public InstanceLifetimeService() : base(LifetimeData)
        {
        }
    }

    public sealed class ScopedLifetimeService : LifetimeService
    {
        private static readonly LifetimeData LifetimeData = new LifetimeData();

        public ScopedLifetimeService() : base(LifetimeData)
        {
        }
    }
}