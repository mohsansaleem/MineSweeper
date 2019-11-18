using System;
using Zenject;
using UniRx;

namespace PM.Core.View
{
    public abstract class Presenter : IInitializable, IDisposable
    {
        [Inject] protected readonly SignalBus SignalBus;

        protected readonly CompositeDisposable _disposables;

        protected Presenter()
        {
            _disposables = new CompositeDisposable();
        }

        public virtual void Initialize()
        {
            // Initialize stuff here.
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}

