using PM.Core;
using Server.API;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteState : StateMachinePresenter.StateBehaviour
        {
            protected readonly RoulettePresenter Presenter;
            protected readonly IRouletteModel RouletteModel;
            protected readonly IRouletteView View;
            
            protected readonly GameplayApi GameplayApi;
            
            public RouletteState(RoulettePresenter presenter) : base(presenter)
            {
                this.Presenter = presenter;
                this.RouletteModel = presenter._iModel;
                this.View = presenter._iView;

                GameplayApi = presenter._gameplayApi;
            }
        }
    }
}
