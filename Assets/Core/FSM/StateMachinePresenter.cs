using System;
using System.Collections.Generic;
using Zenject;
using UniRx;
using UnityEngine;

namespace PG.Core
{
    public partial class StateMachinePresenter : IInitializable, ITickable, IDisposable
    {
        protected StateBehaviour CurrentStateBehaviour;
        protected Dictionary<int, StateBehaviour> StateBehaviours = new Dictionary<int, StateBehaviour>();
        
        protected CompositeDisposable Disposables;

        [Inject] protected readonly SignalBus SignalBus;

        public virtual void Initialize()
		{
            Disposables = new CompositeDisposable();
        }

        public virtual void GoToState(int stateType)
        {
            if (!StateBehaviours.ContainsKey(stateType))
            {
                Debug.LogError("State Missing in Mediator.");
            }
            else if(CurrentStateBehaviour == null || StateBehaviours[stateType] != CurrentStateBehaviour)
            {
                GoToStateInternal(stateType);
            }
        }
        
        private void GoToStateInternal(int stateType)
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
                Debug.LogError($"State Id[{stateType}] doesn't Exist in the Dictionary.");
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
