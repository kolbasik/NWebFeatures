using System;
using System.Collections.Generic;

namespace Albumprinter.Features.Rx
{
    public class PublishSubject<TValue> : ISubject<TValue>
    {
        private List<IObserver<TValue>> observers;

        public PublishSubject()
        {
            observers = new List<IObserver<TValue>>();
        }

        public IDisposable Subscribe(IObserver<TValue> observer)
        {
            if (observers == null)
            {
                return Disposable.Empty;
            }
            observers.Add(observer);
            return Disposable.Create(() => observers.Remove(observer));
        }

        public void OnNext(TValue value)
        {
            ForEach(observer => observer.OnNext(value));
        }

        public void OnError(Exception error)
        {
            ForEach(observer => observer.OnError(error));
            observers = null;
        }

        public void OnCompleted()
        {
            ForEach(observer => observer.OnCompleted());
            observers = null;
        }

        private void ForEach(Action<IObserver<TValue>> action)
        {
            if (observers == null) return;
            foreach (var observer in observers)
            {
                action(observer);
            }
        }
    }
}