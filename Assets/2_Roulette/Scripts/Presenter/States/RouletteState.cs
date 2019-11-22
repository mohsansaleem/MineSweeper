using PM.Core;
using Server.API;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteState : StateBehaviour
        {
            protected readonly RoulettePresenter Presenter;
            protected readonly IRouletteModel Model;
            protected readonly IRouletteView View;

            protected readonly RouletteSettingsInstaller.Settings Settings;
            
            public RouletteState(RoulettePresenter presenter) : base(presenter)
            {
                Presenter = presenter;
                Model = presenter._iModel;
                View = presenter._iView;

                Settings = presenter._settings;
            }
        }
    }
}
