using System;

namespace Albumprinter.Features.Rx
{
    public static class Observer
    {
        private static readonly Action<Exception> OnErrorNop = delegate { };
        private static readonly Action OnCompletedNop = delegate { };

        public static IObserver<TValue> Create<TValue>(Action<TValue> onNext)
        {
            return new ActionObserver<TValue>(onNext, OnErrorNop, OnCompletedNop);
        }

        public static IObserver<TValue> Create<TValue>(Action<TValue> onNext, Action onCompleted)
        {
            return new ActionObserver<TValue>(onNext, OnErrorNop, onCompleted);
        }

        public static IObserver<TValue> Catch<TValue>(Action<Exception> onError)
        {
            return new ActionObserver<TValue>(delegate { }, onError, OnCompletedNop);
        }

        private sealed class ActionObserver<TValue> : IObserver<TValue>
        {
            private readonly Action<TValue> onNext;
            private readonly Action<Exception> onError;
            private readonly Action onCompleted;

            public ActionObserver(Action<TValue> onNext, Action<Exception> onError, Action onCompleted)
            {
                this.onNext = onNext;
                this.onError = onError;
                this.onCompleted = onCompleted;
            }

            public void OnNext(TValue value)
            {
                if (this.onNext != null)
                {
                    this.onNext(value);
                }
            }

            public void OnError(Exception error)
            {
                if (this.onError != null)
                {
                    this.onError(error);
                }
            }

            public void OnCompleted()
            {
                if (this.onCompleted != null)
                {
                    this.onCompleted();
                }
            }
        }
    }
}