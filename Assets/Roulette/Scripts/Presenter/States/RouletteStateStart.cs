using UnityEngine;
using UnityEngine.UI;

namespace PG.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateStart : RouletteState
        {
            public RouletteStateStart(RoulettePresenter presenter) : base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                GetInitialWinSignal.Fire(SignalBus)
                    .Done(() =>
                        {
                            View.CanSpin = true;
                        },
                        exception =>
                        {
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });

                View.SubscribeOnSpinClick(OnSpinTriggered);
            }
            
            private void OnSpinTriggered()
            {
                View.CanSpin = false;
                Model.RouletteState = ERouletteState.Spinner;
            }

            public override void OnStateExit()
            {
                base.OnStateExit();

                View.UnSubscribeOnSpinClick(OnSpinTriggered);
            }
        }
    }
}