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
        private ReactiveProperty<long> _resultantBalance;

        public RouletteModel()
        {
            _rouletteState = new ReactiveProperty<ERouletteState>(ERouletteState.Setup);
            _playerBalance = new ReactiveProperty<long>(0);
            _initialWin = new ReactiveProperty<int>(0);
            _multiplier = new ReactiveProperty<int>(0);
            _resultantBalance = new ReactiveProperty<long>(0);
        }

        public IDisposable SubscribeState(Action<ERouletteState> onChange)
        {
            return _rouletteState.Subscribe(onChange);
        }

        public long Balance
        {
            get => _playerBalance.Value;
            set => _playerBalance.Value = value;
        }
        
        public int InitialWin
        {
            get => _initialWin.Value;
            set => _initialWin.Value = value;
        }

        public int Multiplier
        {
            get => _multiplier.Value;
            set => _multiplier.Value = value;
        }

        public long Result
        {
            get => _resultantBalance.Value;
            private set => _resultantBalance.Value = value;
        } 

        public ERouletteState RouletteState
        {
            get => _rouletteState.Value;
            set => _rouletteState.Value = value;
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

        public IDisposable SubscribeResult(Action<long> setResult)
        {
            return _resultantBalance.Subscribe(setResult);
        }

        public void ResetValues()
        {
            Balance = 0;
            Multiplier = 0;
            InitialWin = 0;
            Multiplier = 0;
            Result = 0;
        }

        public bool UpdateBalanceWithMultiplier()
        {
            Result = InitialWin * Multiplier;
            Balance += Result;

            return true;
        }
    }

    public interface IRouletteModel
    {
        long Balance { get; set; }
        int InitialWin{ get; set; }
        int Multiplier{ get; set; }
        long Result{ get; }
        
        void ResetValues();
        bool UpdateBalanceWithMultiplier();
        
        ERouletteState RouletteState { get; set; }
        IDisposable SubscribeBalance(Action<long> action);
        IDisposable SubscribeInitialWin(Action<int> setInitialWin);
        IDisposable SubscribeMultiplier(Action<int> setMultiplier);
        IDisposable SubscribeResult(Action<long> setResult);
        IDisposable SubscribeState(Action<ERouletteState> onChange);
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

