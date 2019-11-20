using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using Zenject;

namespace PM.Minesweeper
{
    public class MinesweeperModel
    {
        public readonly ReactiveProperty<EGameState> GameState;
        public MineFieldCell[,] MineFieldGrid;

        public MinesweeperModel()
        {
            GameState = new ReactiveProperty<EGameState>(EGameState.Setup);
        }

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


        public enum EGameState
        {
            Setup,
            Playing
        }
    }
}