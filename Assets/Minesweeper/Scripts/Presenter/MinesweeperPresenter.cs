using System;
using PG.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.Minesweeper
{
    public partial class MinesweeperPresenter : StateMachinePresenter
    {
        [Inject] private readonly IMinesweeperView _iView;
        [Inject] private readonly IMinesweeperModel _iModel;
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
            StateBehaviours.Add((int)EGameState.Setup, new MinesweeperStateSetup(this));
            StateBehaviours.Add((int)EGameState.Playing, new MinesweeperStatePlaying(this));

            // Showing the View.
            _iView.Show();

            _iModel.SubscribeGameState(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void CreateGrid()
        {
            _iModel.CreateGrid(_settings.SizeX, _settings.SizeY);
            _iView.CreateGridUI(_settings.SizeX, _settings.SizeY);

            for (uint indexX = 0; indexX < _settings.SizeX; indexX++)
            {
                for (uint indexY = 0; indexY < _settings.SizeY; indexY++)
                {
                    uint x = indexX;
                    uint y = indexY;

                    // Binding Models and Views.
                    BindCell(indexX, indexY);

                    // Binding Input.
                    _iView.SubcribleOnCellClick(indexX, indexY, () => OnCellClicked?.Invoke(x, y));
                    _iView.SubcribleOnCellRightClick(indexX, indexY, () => OnCellRightClicked?.Invoke(x, y));
                }
            }
        }

        private void BindCell(uint indexX, uint indexY)
        {
            //var cell = _minesweeperModel.MineFieldGrid[indexX, indexY];
            _iModel.SubscribeMineFieldGridCellData(indexX, indexY,
                (eMineFieldCellData) => { _iView.SetCellData(indexX, indexY, eMineFieldCellData); })
                .AddTo(Disposables);


            _iModel.SubscribeMineFieldGridCellFlagged(indexX, indexY,
                    isFlagged => { _iView.SetCellFlagged(indexX, indexY, isFlagged); })
                .AddTo(Disposables);

            _iModel.SubscribeMineFieldGridCellOpened(indexX, indexY,
                    isOpened =>
                    {
                        _iView.SetCellContentVisible(indexX,
                            indexY,
                            isOpened);
                    })
                .AddTo(Disposables);
        }

        private void OnLoadingProgressChanged(EGameState loadingProgress)
        {
            GoToState((int)loadingProgress);
        }

        private void OnReload()
        {
            _iView.Show();
        }

        private void OnLoadingStart()
        {
            _iView.Show();
        }
    }
}