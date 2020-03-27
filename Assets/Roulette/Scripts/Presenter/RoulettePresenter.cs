using System;
using System.ComponentModel;
using PG.Core;
using Server.API;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.Roulette
{
    public partial class RoulettePresenter : StateMachinePresenter
    {
        [Inject] private readonly IRouletteView _iView;
        [Inject] private readonly IRouletteModel _iModel;
        [Inject] private readonly GameplayApi _gameplayApi;

        [Inject] private readonly RouletteSettingsInstaller.Settings _settings;
        
        public RoulettePresenter()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            // TODO: Perform this action with Command.
            _gameplayApi.Initialise();
            
            // Binding View and Model.
            BindView();
            
            StateBehaviours.Add((int)ERouletteState.Setup, new RouletteStateSetup(this));
            StateBehaviours.Add((int)ERouletteState.Start, new RouletteStateStart(this));
            StateBehaviours.Add((int)ERouletteState.Spinner, new RouletteStateSpinner(this));
            StateBehaviours.Add((int)ERouletteState.Result, new RouletteStateResult(this));

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
            GoToState((int)rouletteState);
        }
    }
}

