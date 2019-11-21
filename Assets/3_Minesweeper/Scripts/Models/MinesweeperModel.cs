using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace PM.Minesweeper
{
    public class MinesweeperModel : IMinesweeperModel
    {
        private readonly ReactiveProperty<EGameState> _gameState;
        private MineFieldCell[,] MineFieldGrid;

        public MinesweeperModel()
        {
            _gameState = new ReactiveProperty<EGameState>(EGameState.Setup);
        }

        public EGameState GameState
        {
            get => _gameState.Value;
            set => _gameState.Value = value;
        }
        

        #region Grid
        
        public void CreateGrid(uint sizeX, uint sizeY)
        {
            MineFieldGrid = new MineFieldCell[sizeX, sizeY];

            for (uint indexX = 0; indexX < sizeX; indexX++)
            {
                for (uint indexY = 0; indexY < sizeY; indexY++)
                {
                    MineFieldGrid[indexX, indexY] = new MineFieldCell(indexX, indexY);
                }
            }
        }

        public void PopulateGrid(uint x, uint y, uint settingsMinesCount)
        {
            // Placing Mines
            Random random = new Random();

            uint placed = settingsMinesCount;
            while (placed > 0)
            {
                uint r = (uint) random.Next((int) x);
                uint c = (uint) random.Next((int) y);
                if (MineFieldGrid[r, c].Data.Value != EMineFieldCellData.MINE &&
                    (r > x + 1 || r < x - 1) &&
                    (c > y + 1 || c < y - 1))
                {
                    MineFieldGrid[r, c].Data.Value = EMineFieldCellData.MINE;

                    UpdateSiblingsCount(r, c, x, y);

                    placed--;
                }
            }
        }

        private void UpdateSiblingsCount(uint x, uint y, uint sizeX, uint sizeY)
        {
            for (uint r = x > 0 ? x - 1 : x; r < x + 2 && r < sizeX; r++)
            {
                for (uint c = y > 0 ? y - 1 : y; c < y + 2 && c < sizeY; c++)
                {
                    if (!(r == x && c == y) && MineFieldGrid[r, c].Data.Value != EMineFieldCellData.MINE)
                    {
                        MineFieldGrid[r, c].Data.Value++;
                    }
                }
            }
        }

        public void Reset(uint sizeX, uint sizeY)
        {
            // Clearing out the User Grid
            for (uint r = 0; r < sizeX; ++r)
            {
                for (uint c = 0; c < sizeY; ++c)
                {
                    MineFieldGrid[r, c].IsFlagged.Value = false;
                    MineFieldGrid[r, c].IsOpened.Value = false;
                    MineFieldGrid[r, c].Data.Value = EMineFieldCellData.M0;
                }
            }
        }
        
        #endregion


        #region Cells
        
        public void ToggleFlagged(uint x, uint y)
        {
            MineFieldGrid[x, y].ToggleFlagged();
        }

        public EMineFieldCellData GetMineFieldGridCellData(uint x, uint y)
        {
            return MineFieldGrid[x, y].Data.Value;
        }

        public void SetMineFieldGridCellData(uint x, uint y, EMineFieldCellData eMineFieldCellData)
        {
            MineFieldGrid[x, y].Data.Value = eMineFieldCellData;
        }

        public bool GetMineFieldCellOpenedStatus(uint x, uint y)
        {
            return MineFieldGrid[x, y].IsOpened.Value;
        }

        public void SetMineFieldCellOpenedStatus(uint x, uint y, bool value)
        {
            MineFieldGrid[x, y].IsOpened.Value = value;
        }
        #endregion

        #region Subscribers
        // Subscribes.
        public IDisposable SubscribeGameState(Action<EGameState> onStateChanged)
        {
            return _gameState.Subscribe(onStateChanged);
        }

        public IDisposable SubscribeMineFieldGridCellData(uint x, uint y, Action<EMineFieldCellData> action)
        {
            return MineFieldGrid[x, y].Data.Subscribe(action);
        }

        public IDisposable SubscribeMineFieldGridCellOpened(uint x, uint y, Action<bool> action)
        {
            return MineFieldGrid[x, y].IsOpened.Subscribe(action);
        }

        public IDisposable SubscribeMineFieldGridCellFlagged(uint x, uint y, Action<bool> action)
        {
            return MineFieldGrid[x, y].IsFlagged.Subscribe(action);
        }
        #endregion
    }
}