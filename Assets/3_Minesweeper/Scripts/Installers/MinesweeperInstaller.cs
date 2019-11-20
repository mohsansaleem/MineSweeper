using System;
using UnityEngine;
using Zenject;

namespace PM.Minesweeper
{
    public class MinesweeperInstaller : MonoInstaller
    {
        [SerializeField]
        private MinesweeperView _minesweeperView;
        
        public override void InstallBindings()
        {
            Container.Bind<MinesweeperModel>().AsSingle();

            Container.Bind<IMinesweeperView>().To<MinesweeperView>().FromInstance(_minesweeperView).AsSingle();
            
            Container.BindInterfacesTo<MinesweeperPresenter>().AsSingle();
        }
    }
}
