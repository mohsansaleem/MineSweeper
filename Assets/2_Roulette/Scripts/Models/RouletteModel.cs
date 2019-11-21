using System;
using UniRx;

namespace PM.Roulette
{
    public class RouletteModel: IRouletteModel
    {
        private ReactiveProperty<ERouletteState> _rouletteState;
        private ReactiveProperty<long> _playerBalance;

        private ReactiveProperty<int> _initialWin;
        private ReactiveProperty<int> _multiplier;

        public RouletteModel()
        {
            _rouletteState = new ReactiveProperty<ERouletteState>(ERouletteState.Setup);
            _playerBalance = new ReactiveProperty<long>(0);
            _initialWin = new ReactiveProperty<int>(0);
        }

        public IDisposable SubscribeState(Action<ERouletteState> onChange)
        {
            return _rouletteState.Subscribe(onChange);
        }

        public void SetBalance(long balance)
        {
            _playerBalance.Value = balance;
        }
        
        public void SetInitialWin(int win)
        {
            _initialWin.Value = win;
        }

        public void SetMultiplier(int multiplier)
        {
            _multiplier.Value = multiplier;
        }

        public void SetRouletteState(ERouletteState start)
        {
            _rouletteState.Value = start;
        }

        public IDisposable SubscribeBalance(Action<long> action)
        {
            return _playerBalance.Subscribe(action);
        }

        public IDisposable SubscribeInitialWin(Action<int> setInitialWin)
        {
            return _initialWin.Subscribe(setInitialWin);
        }

        public IDisposable SubscribeMultiplier(Action<int> setMultiplier)
        {
            return _multiplier.Subscribe(setMultiplier);
        }

        public void ResetValues()
        {
            SetBalance(0);
            SetMultiplier(0);
            SetInitialWin(0);
        }
    }

    public interface IRouletteModel
    {
        IDisposable SubscribeState(Action<ERouletteState> onChange);
        void SetBalance(long balance);
        void SetMultiplier(int multiplier);
        void SetRouletteState(ERouletteState start);
        IDisposable SubscribeBalance(Action<long> action);
        void SetInitialWin(int win);
        IDisposable SubscribeInitialWin(Action<int> setInitialWin);
        IDisposable SubscribeMultiplier(Action<int> setMultiplier);
        void ResetValues();
    }
    
    public enum ERouletteState
    {
        Setup,
        Start,
        NormalReward,
        Spinner,
        Result
    }
}

