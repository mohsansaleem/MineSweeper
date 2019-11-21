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
                
                MinesweeperModel.PopulateGrid(Settings.SizeX, Settings.SizeY, Settings.MinesCount);

                View.ShowMessage("Start Game? ", ()=>
                {
                    MinesweeperModel.GameState = EGameState.Playing;
                });
            }
        }
    }
}