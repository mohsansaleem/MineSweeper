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

            Container.BindInterfacesTo<RoulettePresenter>().AsSingle();
            
            RouletteSignalsInstaller.Install(Container);
        }
    }
}