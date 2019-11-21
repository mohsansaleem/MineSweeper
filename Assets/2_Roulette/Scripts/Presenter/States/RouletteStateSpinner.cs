using System;
using UniRx;
using UnityEngine;

namespace PM.Roulette
{
    public partial class RoulettePresenter
    {
        public class RouletteStateSpinner: RouletteState
        {
            public RouletteStateSpinner(RoulettePresenter presenter):base(presenter)
            {
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();
                
                // TODO: Command.
                GameplayApi.GetMultiplier()
                    .Done(multiplier =>
                        {
                            RouletteModel.SetMultiplier(multiplier);
                        },
                        exception =>
                        {
                            // TODO: Do something...
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });

                // TODO: Constant from Settings.
                Observable.Timer(TimeSpan.FromSeconds(10))
                    .Subscribe(l =>
                    {
                        // TODO: Trigger the stop.
                        
                        // TODO: Set following on Stop.
                        RouletteModel.SetRouletteState(ERouletteState.Result);
                    })
                    .AddTo(Disposables);
            }
        }
    }
}