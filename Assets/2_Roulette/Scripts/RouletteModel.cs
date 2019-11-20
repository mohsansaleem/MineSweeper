using System;
using UniRx;

namespace PM.Roulette
{
    public class RouletteModel: IRouletteModel
    {
        private ReactiveProperty<ERouletteState> _rouletteState;

        public RouletteModel()
        {
            _rouletteState = new ReactiveProperty<ERouletteState>(ERouletteState.Setup);
        }

        public IDisposable SubscribeState(Action<ERouletteState> onChange)
        {
            return _rouletteState.Subscribe(onChange);
        }
    }

    public interface IRouletteModel
    {
        IDisposable SubscribeState(Action<ERouletteState> onChange);
    }
    
    public enum ERouletteState
    {
        Setup,
        Rest,
        NormalReward,
        Spinner
    }
}

