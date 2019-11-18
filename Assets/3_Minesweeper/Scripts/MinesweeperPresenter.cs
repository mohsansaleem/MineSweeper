using System;
using PM.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace PM.Minesweeper
{
    public partial class MinesweeperPresenter : StateMachinePresenter
    {
        [Inject] private readonly MinesweeperView _view;
        [Inject] private readonly MinesweeperModel _minesweeperModel;

        public MinesweeperPresenter()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            _view.Show();

            StateBehaviours.Add(typeof(MinesweeperStateLoad), new MinesweeperStateLoad(this));
            StateBehaviours.Add(typeof(MinesweeperStatePlaying), new MinesweeperStatePlaying(this));

            _minesweeperModel.GamePlayState.Subscribe(OnLoadingProgressChanged).AddTo(Disposables);
        }
        
        private void OnLoadingProgressChanged(MinesweeperModel.EGamePlayState loadingProgress)
        {
            Type targetType = null;
            switch (loadingProgress)
            {
                case MinesweeperModel.EGamePlayState.Load:
                    targetType = typeof(MinesweeperStateLoad);
                    break;
                case MinesweeperModel.EGamePlayState.Playing:
                    targetType = typeof(MinesweeperStatePlaying);
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
    }
}