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
                            // TODO: Start Spinning.
                        },
                        exception =>
                        {
                            // TODO: Do something...
                            Debug.LogError($"Error: Something went wrong. {exception}");
                        });

                // TODO: Constant from Settings.
                Observable.Timer(TimeSpan.FromSeconds(4))
                    .Subscribe(l =>
                    {
                        // TODO: Trigger the stop.
                        
                        // TODO: Set following on Stop.
                        RouletteModel.RouletteState = ERouletteState.Result;
                    })
                    .AddTo(Disposables);
            }

            private float speed = 100;
            
            public override void Tick()
            {
                base.Tick();
                
                (View as RouletteView).WheelTransform.Rotate(new Vector3(0,0,1), speed * Time.deltaTime);
            }
        }
    }
}