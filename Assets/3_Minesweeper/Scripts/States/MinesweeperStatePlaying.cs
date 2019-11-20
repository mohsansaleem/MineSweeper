using UnityEngine;

namespace PM.Minesweeper
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

                if (MinesweeperModel.MineFieldGrid[x, y].Data.Value == EMineFieldCellData.MINE)
                {
                    View.ShowMessage("<Color=Red>You Hit a Mine.</Color>\n\n <b>Restart?</b>", 
                        (() => MinesweeperModel.GameState.Value = MinesweeperModel.EGameState.Setup));
                }
                else if(OpenedCells + Settings.MinesCount == Settings.SizeX * Settings.SizeY)
                {
                    View.ShowMessage("<Color=Green>You won.</Color> \n\n <b>Replay?</b>", 
                        (() => MinesweeperModel.GameState.Value = MinesweeperModel.EGameState.Setup));
                }
            }

            private void UnveilEmptyCells(uint x, uint y)
            {
                if (MinesweeperModel.MineFieldGrid[x, y].IsOpened.Value)
                    return;

                MinesweeperModel.MineFieldGrid[x, y].IsOpened.Value = true;
                OpenedCells++;
                
                if (MinesweeperModel.MineFieldGrid[x, y].Data.Value != EMineFieldCellData.M0)
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
            
            private void OnCellClicked(MineFieldCell cell)
            {
                Open(cell.X, cell.Y);
            }

            private void OnCellRightClicked(MineFieldCell cell)
            {
                cell.ToggleFlagged();
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