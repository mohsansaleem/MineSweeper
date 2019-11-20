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
        [Inject] private readonly IMinesweeperModel _minesweeperModel;
        [Inject] private readonly MinesweeperSettingsInstaller.Settings _settings;

        // Actions.
        private Action<uint, uint> OnCellClicked;
        private Action<uint, uint> OnCellRightClicked;

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
            
            _minesweeperModel.SubscribeGameState(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void CreateGrid()
        {
            _minesweeperModel.CreateGrid(_settings.SizeX, _settings.SizeY);
            _view.CreateGridUI(_settings.SizeX, _settings.SizeY);
            
            for (uint indexX = 0; indexX < _settings.SizeX; indexX++)
            {
                for (uint indexY = 0; indexY < _settings.SizeY; indexY++)
                {
                    uint x = indexX;
                    uint y = indexY;
                    
                    // Binding Models and Views.
                    BindCell(indexX, indexY);
                    
                    // Binding Input.
                    _view.SubcribleOnCellClick(indexX, indexY, ()=> OnCellClicked?.Invoke(x, y));
                    _view.SubcribleOnCellRightClick(indexX, indexY, () => OnCellRightClicked?.Invoke(x, y));

                }
            }
        }

        private void BindCell(uint indexX, uint indexY)
        {
            //var cell = _minesweeperModel.MineFieldGrid[indexX, indexY];
            _minesweeperModel.SubscribeMineFieldGridCellData(indexX, indexY, (eMineFieldCellData) =>
            {
                _view.SetCellData(indexX, indexY, eMineFieldCellData);
                
            }).AddTo(Disposables);
            
            
            _minesweeperModel.SubscribeMineFieldGridCellFlagged(indexX,
                                                                indexY,
                                                                isFlagged =>
                                                                {
                                                                    _view.SetCellFlagged(indexX, indexY, isFlagged);
                                                                })
                .AddTo(Disposables);
            
            _minesweeperModel.SubscribeMineFieldGridCellOpened(indexX,
                                                               indexY,
                                                               isOpened =>
                                                               {
                                                                   _view.SetCellContentVisible(indexX, 
                                                                                               indexY,
                                                                                               isOpened);
                                                               })
                .AddTo(Disposables);
        }
        
        private void OnLoadingProgressChanged(EGameState loadingProgress)
        {
            Type targetType = null;
            switch (loadingProgress)
            {
                case EGameState.Setup:
                    targetType = typeof(MinesweeperStateSetup);
                    break;
                case EGameState.Playing:
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