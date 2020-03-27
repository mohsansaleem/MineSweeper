using PG.Core;

namespace PG.Minesweeper
{
    public partial class MinesweeperPresenter
    {
        public class MinesweeperState : StateMachinePresenter.StateBehaviour
        {
            protected readonly MinesweeperPresenter Presenter;
            protected readonly IMinesweeperView View;
            protected readonly IMinesweeperModel Model;
            
            protected readonly MinesweeperSettingsInstaller.Settings Settings;
            

            public MinesweeperState(MinesweeperPresenter presenter) : base(presenter) 
            {
                this.Presenter = presenter;
                this.View = presenter._iView;
                
                this.Model = presenter._iModel;

                this.Settings = presenter._settings;
            }
        }
    }
}
