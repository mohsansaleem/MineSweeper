using UniRx;
using UnityEngine;
using Zenject;

namespace PG.Core
{
    partial class StateMachinePresenter
    {
        public class StateBehaviour
        {
            protected CompositeDisposable Disposables;

            protected readonly SignalBus SignalBus;
        
            public StateBehaviour(StateMachinePresenter stateMachinePresenter)
            {
                this.SignalBus = stateMachinePresenter.SignalBus;
            }
        
            public virtual void OnStateEnter()
            {
                Debug.Log(string.Format("{0} , OnStateEnter()", this));

                Disposables = new CompositeDisposable();
            }

            public virtual void OnStateExit()
            {
                Debug.Log(string.Format("{0} , OnStateExit()", this));

                Disposables.Dispose();
            }

            public virtual bool IsValidOpenState()
            {
                return false;
            }

            public virtual void Tick()
            {

            }
        }   
    }
}
