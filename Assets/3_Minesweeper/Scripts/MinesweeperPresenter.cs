using System;
using PM.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace PM.Minesweeper
{
    public partial class MinesweeperPresenter : StateMachinePresenter
    {
        [Inject] private readonly IMinesweeperView _view;
        [Inject] private readonly MinesweeperModel _minesweeperModel;
        [Inject] private readonly MinesweeperSettingsInstaller.Settings _settings;

        // Actions.
        private Action<MineFieldCell> OnCellClicked;
        private Action<MineFieldCell> OnCellRightClicked;

        public override void Initialize()
        {
            base.Initialize();

            // Creating the Grid.
            CreateGrid();
            
            // Keeping a track of States.
            StateBehaviours.Add(typeof(MinesweeperStateSetup), new MinesweeperStateSetup(this));
            StateBehaviours.Add(typeof(MinesweeperStatePlaying), new MinesweeperStatePlaying(this));

            // Showing the View.
            _view.Show();
            
            _minesweeperModel.GameState.Subscribe(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void CreateGrid()
        {
            _minesweeperModel.CreateGrid(_settings.SizeX, _settings.SizeY);
            _view.CreateGridUI(_settings.SizeX, _settings.SizeY);
            
            for (uint indexX = 0; indexX < _settings.SizeX; indexX++)
            {
                for (uint indexY = 0; indexY < _settings.SizeY; indexY++)
                {
                    MineFieldCell mineFieldCell = _minesweeperModel.MineFieldGrid[indexX, indexY];
                    
                    // Binding Models and Views.
                    BindCell(mineFieldCell, indexX, indexY);
                    
                    // Binding Input.
                    _view.SubcribleOnCellClick(indexX, indexY, ()=> OnCellClicked?.Invoke(mineFieldCell));
                    _view.SubcribleOnCellRightClick(indexX, indexY, () => OnCellRightClicked?.Invoke(mineFieldCell));

                }
            }
        }

        private void BindCell(MineFieldCell cell, uint indexX, uint indexY)
        {
            cell.Data.Subscribe((e) =>
            {
                _view.SetCellData(indexX, indexY, e);
                
            }).AddTo(Disposables);
            
            cell.IsFlagged.Subscribe(isFlagged => _view.SetCellFlagged(indexX, indexY, isFlagged)).AddTo(Disposables);
            cell.IsOpened.Subscribe(isOpened => _view.SetCellContentVisible(indexX, indexY, isOpened)).AddTo(Disposables);
        }
        
        private void OnLoadingProgressChanged(MinesweeperModel.EGameState loadingProgress)
        {
            Type targetType = null;
            switch (loadingProgress)
            {
                case MinesweeperModel.EGameState.Setup:
                    targetType = typeof(MinesweeperStateSetup);
                    break;
                case MinesweeperModel.EGameState.Playing:
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