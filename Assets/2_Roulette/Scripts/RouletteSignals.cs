using RSG;
using Zenject;

namespace PM.Roulette
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
    
    public abstract class ASignal
    {
        public RSG.Promise OnResult;

        protected ASignal()
        {
            OnResult = new RSG.Promise();
        }

        protected IPromise FireInternal(SignalBus signalBus)
        {
            signalBus.TryFire(this);

            return OnResult;
        }
    }
}