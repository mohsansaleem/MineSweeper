using PM.Core;

namespace PM.Minesweeper
{
    public partial class MinesweeperPresenter
    {
        public class MinesweeperState : StateBehaviour
        {
            protected readonly MinesweeperPresenter Presenter;
            protected readonly IMinesweeperView View;
            protected readonly IMinesweeperModel MinesweeperModel;
            
            protected readonly MinesweeperSettingsInstaller.Settings Settings;
            

            public MinesweeperState(MinesweeperPresenter presenter) : base(presenter) 
            {
                this.Presenter = presenter;
                this.View = presenter._iView;
                
                this.MinesweeperModel = presenter._iModel;

                this.Settings = presenter._settings;
            }
        }
    }
}
