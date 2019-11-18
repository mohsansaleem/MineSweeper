using PM.Core;

namespace PM.Minesweeper
{
    public partial class MinesweeperPresenter
    {
        public class MinesweeperState : StateBehaviour
        {
            protected readonly MinesweeperPresenter Mediator;
            protected readonly MinesweeperView View;
            protected readonly MinesweeperModel MinesweeperModel;
            

            public MinesweeperState(MinesweeperPresenter presenter) : base(presenter) 
            {
                this.Mediator = presenter;
                this.View = presenter._view;
                
                this.MinesweeperModel = presenter._minesweeperModel;
            }
        }
    }
}
