using System;
using UniRx;
using UnityEngine;

namespace PG.Roulette
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
                            View.StartSpinning(Model.Multiplier);

                            Observable.Timer(TimeSpan.FromSeconds(Settings.SpinTime))
                                .Subscribe(l =>
                                {
                                    View.StopSpinning(() =>
                                    {
                                        Model.RouletteState = ERouletteState.Result;
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