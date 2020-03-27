using UnityEngine;

namespace PG.Minesweeper
{
    public partial class MinesweeperPresenter
    {
        public class MinesweeperStatePlaying : MinesweeperState
        {
            private uint OpenedCells = 0;
            
            public MinesweeperStatePlaying(MinesweeperPresenter presenter) : base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                OpenedCells = 0;
                
                Presenter.OnCellClicked += OnCellClicked;
                Presenter.OnCellRightClicked += OnCellRightClicked;
            }

            public void Open(uint x, uint y)
            {
                UnveilEmptyCells(x, y);

                if (Model.GetMineFieldGridCellData(x, y) == EMineFieldCellData.MINE)
                {
                    View.ShowMessage("<Color=Red>You Hit a Mine.</Color>\n\n <b>Restart?</b>", 
                        (() => Model.GameState = EGameState.Setup));
                }
                else if(OpenedCells + Settings.MinesCount == Settings.SizeX * Settings.SizeY)
                {
                    View.ShowMessage("<Color=Green>You won.</Color> \n\n <b>Replay?</b>", 
                        (() => Model.GameState = EGameState.Setup));
                }
            }

            private void UnveilEmptyCells(uint x, uint y)
            {
                if (Model.GetMineFieldCellOpenedStatus(x, y))
                    return;

                Model.SetMineFieldCellOpenedStatus(x, y, true);
                OpenedCells++;
                
                if (Model.GetMineFieldGridCellData(x, y) != EMineFieldCellData.M0)
                    return;

                for (uint r = x > 0 ? x - 1 : x; r < x + 2 && r < Settings.SizeX; r++)
                {
                    for (uint c = y > 0 ? y - 1 : y; c < y + 2 && c < Settings.SizeY; c++)
                    {
                        if (!(r == x && c == y))
                            UnveilEmptyCells(r, c);
                    }
                }
            }
            
            private void OnCellClicked(uint x, uint y)
            {
                Open(x, y);
            }

            private void OnCellRightClicked(uint x, uint y)
            {
                Model.ToggleFlagged(x, y);
            }
            
            public override void OnStateExit()
            {
                Presenter.OnCellClicked -= OnCellClicked;
                Presenter.OnCellRightClicked -= OnCellRightClicked;
                
                base.OnStateExit();
            }
        }
    }
}