using System;
using UniRx;
using UnityEngine;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateResult: RouletteState
        {
            public RouletteStateResult(RoulettePresenter presenter):base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                RouletteModel.UpdateBalanceWithMultiplier();
                
                GameplayApi.SetPlayerBalance(RouletteModel.Balance)
                    .Done(() =>
                        {
                            // TODO: Decide what to Do. For Now just getting on the start again.
                            // TODO: Use Constants from Settings.
                            Observable.Timer(TimeSpan.FromSeconds(3))
                                .Subscribe((l => RouletteModel.RouletteState = ERouletteState.Setup));
                        },
                        exception =>
                        {
                            // TODO: Do something...
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });
                
            }
        }
    }
}