using System;
using System.ComponentModel;
using PM.Core;
using Server.API;
using UniRx;
using UnityEngine;
using Zenject;

namespace PM.Roulette
{
    public partial class RoulettePresenter : StateMachinePresenter
    {
        [Inject] private readonly IRouletteView _iView;
        [Inject] private readonly IRouletteModel _iModel;
        [Inject] private readonly GameplayApi _gameplayApi;
        
        public RoulettePresenter()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            _gameplayApi.Initialise();
            
            // Binding View and Model.
            BindView();
            
            StateBehaviours.Add(typeof(RouletteStateSetup), new RouletteStateSetup(this));
            StateBehaviours.Add(typeof(RouletteStateStart), new RouletteStateStart(this));
            StateBehaviours.Add(typeof(RouletteStateSpinner), new RouletteStateSpinner(this));
            StateBehaviours.Add(typeof(RouletteStateResult), new RouletteStateResult(this));

            _iModel.SubscribeState(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void BindView()
        {
            _iModel.SubscribeBalance(_iView.SetBalance).AddTo(Disposables);
            _iModel.SubscribeInitialWin(_iView.SetInitialWin).AddTo(Disposables);
            //_iModel.SubscribeMultiplier(_iView.SetMultiplier).AddTo(Disposables);
            _iModel.SubscribeResult(result => _iView.ShowResult(_iModel.Multiplier, result)).AddTo(Disposables);

        }

        private void ResetValues()
        {
            _iModel.ResetValues();
            _iView.Reset();
        }
        
        private void OnLoadingProgressChanged(ERouletteState rouletteState)
        {
            Type targetType = null;
            switch (rouletteState)
            {
                case ERouletteState.Setup:
                    targetType = typeof(RouletteStateSetup);
                    break;
                case ERouletteState.Start:
                    targetType = typeof(RouletteStateStart);
                    break;
                case ERouletteState.Spinner:
                    targetType = typeof(RouletteStateSpinner);
                    break;
                case ERouletteState.Result:
                    targetType = typeof(RouletteStateResult);
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
    }
}

