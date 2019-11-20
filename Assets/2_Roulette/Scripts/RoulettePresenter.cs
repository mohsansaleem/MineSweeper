using System;
using PM.Core;
using Server.API;
using UniRx;
using UnityEngine;
using Zenject;

namespace PM.Roulette
{
    public partial class RoulettePresenter : StateMachinePresenter
    {
        [Inject] private readonly IRouletteView _view;
        [Inject] private readonly IRouletteModel _rouletteModel;
        [Inject] private readonly GameplayApi _gameplayApi;
        
        public RoulettePresenter()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            _gameplayApi.Initialise();

            StateBehaviours.Add(typeof(RouletteStateSetup), new RouletteStateSetup(this));
            StateBehaviours.Add(typeof(RouletteStateStart), new RouletteStateStart(this));
            StateBehaviours.Add(typeof(RouletteStateInitialWin), new RouletteStateInitialWin(this));
            StateBehaviours.Add(typeof(RouletteStateSpinner), new RouletteStateSpinner(this));

            _rouletteModel.SubscribeState(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void OnLoadingProgressChanged(ERouletteState rouletteState)
        {
            Type targetType = null;
            switch (rouletteState)
            {
                case ERouletteState.Setup:
                    targetType = typeof(RouletteStateSetup);
                    break;
                case ERouletteState.Rest:
                    targetType = typeof(RouletteStateStart);
                    break;
                case ERouletteState.NormalReward:
                    targetType = typeof(RouletteStateInitialWin);
                    break;
                case ERouletteState.Spinner:
                    targetType = typeof(RouletteStateSpinner);
                    break;
                default:
                    Debug.LogError("State Missing in Mediator.");
                    break;
            }

            if (targetType != null &&
                (CurrentStateBehaviour == null ||
                 targetType != CurrentStateBehaviour.GetType()))
            {
                GoToState(targetType);
            }
        }

        private void OnReload()
        {
            _view.Show();
        }

        private void OnLoadingStart()
        {
            _view.Show();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

