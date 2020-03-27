using System;
using RSG;
using Zenject;

namespace PG.Roulette
{
    public class GetPlayerBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetPlayerBalanceSignal signal = new GetPlayerBalanceSignal();

            return signal.FireInternal(signalBus);
        }
    }

    public class GetMultiplierSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetMultiplierSignal signal = new GetMultiplierSignal();

            return signal.FireInternal(signalBus);
        }
    }

    public class GetInitialWinSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetInitialWinSignal signal = new GetInitialWinSignal();
            
            return signal.FireInternal(signalBus);
        }
    }
    
    public class UpdateBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            UpdateBalanceSignal signal = new UpdateBalanceSignal();
            
            return signal.FireInternal(signalBus);
        }
    }
    
    public class SetBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            SetBalanceSignal signal = new SetBalanceSignal();
            
            return signal.FireInternal(signalBus);
        }
    }
    
    public abstract class ASignal
    {
        private readonly RSG.Promise OnResult;

        protected ASignal()
        {
            OnResult = new RSG.Promise();
        }

        protected IPromise FireInternal(SignalBus signalBus)
        {
            signalBus.TryFire(this);

            return OnResult;
        }

        public void Resolve()
        {
            OnResult.Resolve();
        }

        public void Reject(Exception ex)
        {
            OnResult.Reject(ex);
        }
    }
}