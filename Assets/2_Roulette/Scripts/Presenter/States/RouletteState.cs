using PM.Core;
using Server.API;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteState : StateBehaviour
        {
            protected readonly RoulettePresenter Presenter;
            protected readonly IRouletteModel RouletteModel;
            protected readonly IRouletteView View;
            
            protected readonly GameplayApi GameplayApi;

            protected readonly RouletteSettingsInstaller.Settings Settings;
            
            public RouletteState(RoulettePresenter presenter) : base(presenter)
            {
                Presenter = presenter;
                RouletteModel = presenter._iModel;
                View = presenter._iView;

                GameplayApi = presenter._gameplayApi;

                Settings = presenter._settings;
            }
        }
    }
}
