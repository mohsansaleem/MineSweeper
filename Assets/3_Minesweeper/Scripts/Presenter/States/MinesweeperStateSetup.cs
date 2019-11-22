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
                Model.Reset(Settings.SizeX, Settings.SizeY);
                
                Model.PopulateGrid(Settings.SizeX, Settings.SizeY, Settings.MinesCount);

                View.ShowMessage("Start Game? ", ()=>
                {
                    Model.GameState = EGameState.Playing;
                });
            }
        }
    }
}