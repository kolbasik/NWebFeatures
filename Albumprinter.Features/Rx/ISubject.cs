using System;

namespace Albumprinter.Features.Rx
{
    public interface ISubject<TValue> : IObservable<TValue>, IObserver<TValue>
    {
    }
}