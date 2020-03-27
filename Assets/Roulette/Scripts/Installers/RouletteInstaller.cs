using DefaultNamespace;
using Server.API;
using UnityEngine;
using Zenject;

namespace PG.Roulette
{
    public class RouletteInstaller : MonoInstaller
    {
        [SerializeField] private RouletteView _view;

        public override void InstallBindings()
        {
            Container.Bind<GameplayApi>().AsSingle();

            Container.Bind<IRouletteModel>().To<RouletteModel>().AsSingle();
            Container.Bind<IRouletteView>().To<RouletteView>().FromInstance(_view).AsSingle();

            Container.DeclareSignal<GetPlayerBalanceSignal>();
            Container.DeclareSignal<GetMultiplierSignal>();
            Container.DeclareSignal<GetInitialWinSignal>();
            Container.DeclareSignal<UpdateBalanceSignal>();
            Container.DeclareSignal<SetBalanceSignal>();
            
            Container.BindSignal<GetPlayerBalanceSignal>()
                .ToMethod<GetPlayerBalanceCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            Container.BindSignal<GetMultiplierSignal>()
                .ToMethod<GetMultiplierCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            Container.BindSignal<GetInitialWinSignal>()
                .ToMethod<GetInitialWinCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            Container.BindSignal<UpdateBalanceSignal>()
                .ToMethod<UpdateBalanceCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            Container.BindSignal<SetBalanceSignal>()
                .ToMethod<SetBalanceCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            
            Container.BindInterfacesTo<RoulettePresenter>().AsSingle();
        }
    }
}