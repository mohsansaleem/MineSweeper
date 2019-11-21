using DefaultNamespace;
using Server.API;
using UnityEngine;
using Zenject;

namespace PM.Roulette
{
    public class RouletteInstaller : MonoInstaller
    {
        [SerializeField] private RouletteView _view;

        public override void InstallBindings()
        {
            Container.Bind<GameplayApi>().AsSingle();

            Container.Bind<IRouletteModel>().To<RouletteModel>().AsSingle();
            Container.Bind<IRouletteView>().To<RouletteView>().FromInstance(_view).AsSingle();

            Container.DeclareSignal<GetPlayerBalanceSignal>().RunAsync();
            Container.BindSignal<GetPlayerBalanceSignal>()
                .ToMethod<GetPlayerBalanceCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();
            
            Container.DeclareSignal<GetMultiplierSignal>().RunAsync();
            Container.BindSignal<GetMultiplierSignal>()
                .ToMethod<GetMultiplierCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();

            Container.DeclareSignal<GetInitialWinSignal>().RunAsync();
            Container.BindSignal<GetInitialWinSignal>()
                .ToMethod<GetInitialWinCommand>((cmd, signal) => { cmd.Execute(signal); })
                .FromNew();

            
            Container.BindInterfacesTo<RoulettePresenter>().AsSingle();
        }
    }
}