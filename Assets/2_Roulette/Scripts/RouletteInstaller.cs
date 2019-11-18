using Server.API;
using UnityEngine;
using Zenject;

namespace PM.Roulette
{
    public class RouletteInstaller : MonoInstaller
    {
        [SerializeField]
        private RouletteView _view;

        public override void InstallBindings()
        {
            Container.Bind<RouletteModel>().AsSingle();
                
            Container.Bind<GameplayApi>().AsSingle();
            
            Container.BindInstances(_view);
            Container.BindInterfacesTo<RoulettePresenter>().AsSingle();
        }
    }
}