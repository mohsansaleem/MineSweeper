using Zenject;
using System;
using RSG;

namespace PG.Roulette
{
    public class RouletteSignalsInstaller : Installer<RouletteSignalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<GetPlayerBalanceSignal>();
            Container.DeclareSignal<GetMultiplierSignal>();
            Container.DeclareSignal<GetInitialWinSignal>();
            Container.DeclareSignal<UpdateBalanceSignal>();
            Container.DeclareSignal<SetBalanceSignal>();

            Container.BindSignal<GetPlayerBalanceSignal>()
                .ToMethod<GetPlayerBalanceCommand>(cmd => cmd.Execute)
                .FromNew();
            Container.BindSignal<GetMultiplierSignal>()
                .ToMethod<GetMultiplierCommand>((cmd) => cmd.Execute)
                .FromNew();
            Container.BindSignal<GetInitialWinSignal>()
                .ToMethod<GetInitialWinCommand>(cmd => cmd.Execute)
                .FromNew();
            Container.BindSignal<UpdateBalanceSignal>()
                .ToMethod<UpdateBalanceCommand>(cmd => cmd.Execute)
                .FromNew();
            Container.BindSignal<SetBalanceSignal>()
                .ToMethod<SetBalanceCommand>(cmd => cmd.Execute)
                .FromNew();
        }
    }
    
    public class GetPlayerBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetPlayerBalanceSignal signal = new GetPlayerBalanceSignal();
            signalBus.Fire(signal);
            return signal.FireInternal();
        }
    }

    public class GetMultiplierSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetMultiplierSignal signal = new GetMultiplierSignal();
            signalBus.Fire(signal);
            return signal.FireInternal();
        }
    }

    public class GetInitialWinSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            GetInitialWinSignal signal = new GetInitialWinSignal();
            signalBus.Fire(signal);
            return signal.FireInternal();
        }
    }
    
    public class UpdateBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            UpdateBalanceSignal signal = new UpdateBalanceSignal();
            signalBus.Fire(signal);
            return signal.FireInternal();
        }
    }
    
    public sealed class SetBalanceSignal : ASignal
    {
        public static IPromise Fire(SignalBus signalBus)
        {
            SetBalanceSignal signal = new SetBalanceSignal();
            
            signalBus.Fire(signal);
            return signal.FireInternal();
        }
    }
    
    public abstract class ASignal
    {
        private readonly RSG.Promise OnResult;

        protected ASignal()
        {
            OnResult = new RSG.Promise();
        }

        protected IPromise FireInternal()
        {
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