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
        [Inject] private readonly RouletteView _view;
        [Inject] private readonly RouletteModel _rouletteModel;
        [Inject] private readonly GameplayApi _gameplayApi;
        
        public RoulettePresenter()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            _gameplayApi.Initialise();

            StateBehaviours.Add(typeof(RouletteStateLoadStaticData), new RouletteStateLoadStaticData(this));
            StateBehaviours.Add(typeof(RouletteStateLoadUserData), new RouletteStateLoadUserData(this));
            StateBehaviours.Add(typeof(RouletteStateCreateUserData), new RouletteStateCreateUserData(this));
            StateBehaviours.Add(typeof(RouletteStateGamePlay), new RouletteStateGamePlay(this));

            _rouletteModel.LoadingProgress.Subscribe(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void OnLoadingProgressChanged(RouletteModel.ELoadingProgress loadingProgress)
        {
            _view.ProgressBar.value = (float)loadingProgress / 100;
            _view.LogoImage.SetActive(loadingProgress != RouletteModel.ELoadingProgress.GamePlay);
            
            
            Type targetType = null;
            switch (loadingProgress)
            {
                case RouletteModel.ELoadingProgress.Zero:
                    targetType = typeof(RouletteStateLoadStaticData);
                    break;
                case RouletteModel.ELoadingProgress.StaticDataLoaded:
                    targetType = typeof(RouletteStateLoadUserData);
                    break;
                case RouletteModel.ELoadingProgress.UserNotFound:
                    targetType = typeof(RouletteStateCreateUserData);
                    break;
                case RouletteModel.ELoadingProgress.GamePlay:
                    targetType = typeof(RouletteStateGamePlay);
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

