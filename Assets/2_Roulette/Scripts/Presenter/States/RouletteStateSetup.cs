using UnityEngine;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateSetup : RouletteState
        {
            public RouletteStateSetup(RoulettePresenter presenter) : base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                // TODO: Command.
                GameplayApi.GetPlayerBalance()
                    .Done(balance =>
                        {
                            RouletteModel.SetBalance(balance);
                            RouletteModel.SetRouletteState(ERouletteState.Start);
                        },
                        exception =>
                        {
                            // TODO: Do something...
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });
                //(View as RouletteView).WheelTransform.Rotate(new Vector3(0,0,1), 45);
            }
        }
    }
}