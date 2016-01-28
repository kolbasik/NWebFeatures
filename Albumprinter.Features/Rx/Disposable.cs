using System;

namespace Albumprinter.Features.Rx
{
    public static class Disposable
    {
        public static readonly IDisposable Empty = new ActionDisposable(() => { });

        public static IDisposable Create(Action action)
        {
            return new ActionDisposable(action);
        }

        private class ActionDisposable : IDisposable
        {
            private readonly Action action;

            public ActionDisposable(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                action();
            }
        }
    }
}