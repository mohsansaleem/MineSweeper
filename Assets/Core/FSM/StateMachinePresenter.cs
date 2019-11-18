using System;
using System.Collections.Generic;
using Zenject;
using UniRx;
using UnityEngine;

namespace PM.Core
{
    public partial class StateMachinePresenter : IInitializable, ITickable, IDisposable
    {
        protected StateBehaviour CurrentStateBehaviour;
        protected Dictionary<Type, StateBehaviour> StateBehaviours = new Dictionary<Type, StateBehaviour>();
        
        protected CompositeDisposable Disposables;

        [Inject] protected readonly SignalBus SignalBus;

        public virtual void Initialize()
		{
			Disposables = new CompositeDisposable();
        }

        public virtual void GoToState(Type stateType)
        {
            if (StateBehaviours.ContainsKey(stateType))
            {
                if (CurrentStateBehaviour != null)
                {
                    CurrentStateBehaviour.OnStateExit();
                }
                CurrentStateBehaviour = StateBehaviours[stateType];
                
                CurrentStateBehaviour.OnStateEnter();
            }
            else
            {
                Debug.LogError($"State[{stateType.Name}] doesn't Exist in the Dictionary.");
            }
        }

        public virtual void Tick()
        {
            if (CurrentStateBehaviour != null)
            {
                CurrentStateBehaviour.Tick();
            }
        }

        public virtual void Dispose()
        {
            if (CurrentStateBehaviour != null)
            {
                CurrentStateBehaviour.OnStateExit();
            }

            Disposables.Dispose();

            StateBehaviours.Clear();
        }
    }
}
