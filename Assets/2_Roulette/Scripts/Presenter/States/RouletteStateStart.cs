using UnityEngine;
using UnityEngine.UI;

namespace PM.Roulette
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

                RouletteModel.ResetValues();
                
                // TODO: Command.
                GameplayApi.GetInitialWin()
                    .Done(win =>
                        {
                            RouletteModel.SetInitialWin(win);
                        },
                        exception =>
                        {
                            // TODO: Do something...
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });

                View.SubscribeOnSpinClick(OnSpinTriggered);
            }
            
            private void OnSpinTriggered()
            {
                RouletteModel.SetRouletteState(ERouletteState.Spinner);
            }

            public override void OnStateExit()
            {
                base.OnStateExit();

                View.UnSubscribeOnSpinClick(OnSpinTriggered);
            }
        }
    }
}