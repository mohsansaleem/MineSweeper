using PM.Core;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteState : StateMachinePresenter.StateBehaviour
        {
            protected readonly RoulettePresenter Presenter;
            protected readonly IRouletteModel RouletteModel;
            protected readonly IRouletteView View;
            
            public RouletteState(RoulettePresenter presenter) : base(presenter)
            {
                this.Presenter = presenter;
                this.RouletteModel = presenter._rouletteModel;
                this.View = presenter._view;
            }
        }
    }
}
