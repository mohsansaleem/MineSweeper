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
                
                GetMultiplierSignal.Fire(SignalBus)
                    .Done(() =>
                        {
                            Debug.LogError("Requesting.");
                            View.StartSpinning(RouletteModel.Multiplier);

                            Observable.Timer(TimeSpan.FromSeconds(Settings.SpinTime))
                                .Subscribe(l =>
                                {
                                    // TODO: Trigger the stop.
                                    View.StopSpinning(() =>
                                    {
                                        // TODO: Set following on Stop.
                                        RouletteModel.RouletteState = ERouletteState.Result;
                                    });
                                })
                                .AddTo(Disposables);
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