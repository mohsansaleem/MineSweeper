using UnityEngine;
using Random = System.Random;

namespace PM.Minesweeper
{
    public partial class MinesweeperPresenter
    {
        public class MinesweeperStateSetup : MinesweeperState
        {
            public MinesweeperStateSetup(MinesweeperPresenter presenter) : base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                View.HideMessage();
                MinesweeperModel.Reset(Settings.SizeX, Settings.SizeY);
                
                PopulateGrid(Settings.SizeX, Settings.SizeY);

                View.ShowMessage("Start Game? ", ()=>
                {
                    MinesweeperModel.GameState.Value = MinesweeperModel.EGameState.Playing;
                });
            }
            
            private void PopulateGrid(uint x, uint y)
            {
                // Placing Mines
                Random random = new Random();

                uint placed = Settings.MinesCount;
                while (placed > 0)
                {
                    uint r = (uint)random.Next((int)Settings.SizeX);
                    uint c = (uint)random.Next((int)Settings.SizeY);
                    if (MinesweeperModel.MineFieldGrid[r, c].Data.Value != EMineFieldCellData.MINE &&
                        (r > x + 1 || r < x - 1) &&
                        (c > y + 1 || c < y - 1))
                    {
                        MinesweeperModel.MineFieldGrid[r, c].Data.Value = EMineFieldCellData.MINE;

                        UpdateSiblingsCount(r, c);

                        placed--;
                    }
                }
            }

            private void UpdateSiblingsCount(uint x, uint y)
            {
                for (uint r = x > 0 ? x - 1 : x; r < x + 2 && r < Settings.SizeX; r++)
                {
                    for (uint c = y > 0 ? y - 1 : y; c < y + 2 && c < Settings.SizeY; c++)
                    {
                        if (!(r == x && c == y) &&
                            MinesweeperModel.MineFieldGrid[r, c].Data.Value != EMineFieldCellData.MINE)
                        {
                            MinesweeperModel.MineFieldGrid[r, c].Data.Value++;
                        }
                    }
                }
            }
        }
    }
}