using Server.API;
using UnityEngine;
using Zenject;

namespace PM.Roulette
{
    public class RouletteInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RouletteModel>().AsSingle();
                
            Container.Bind<GameplayApi>().AsSingle();
            
            Container.BindInterfacesTo<RoulettePresenter>().AsSingle();
        }
    }
}