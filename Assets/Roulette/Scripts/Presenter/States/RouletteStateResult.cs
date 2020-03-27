using System;
using UniRx;
using UnityEngine;

namespace PG.Roulette
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

                UpdateBalanceSignal.Fire(SignalBus).Done(() =>
                    {
                        SetBalanceSignal.Fire(SignalBus)
                            .Done(() =>
                                {
                                    // TODO: Decide what to Do. For Now just getting on the start again.
                                    
                                    Observable.Timer(TimeSpan.FromSeconds(Settings.ResultVisibilityTime))
                                        .Subscribe(l => Model.RouletteState = ERouletteState.Setup)
                                        .AddTo(Disposables);
                                },
                                exception =>
                                {
                                    // TODO: Do something...
                                    Debug.LogError($"Error: Something went wrong. {exception}");
                                });
                    },
                    exception =>
                    {
                        Debug.LogError($"Error: Something went wrong. {exception}");
                    });

            }
        }
    }
}