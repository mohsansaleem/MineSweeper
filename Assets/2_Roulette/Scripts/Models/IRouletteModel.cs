using System;

namespace PM.Roulette
{
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