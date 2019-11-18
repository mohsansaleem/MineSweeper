using Zenject;

namespace PM.Minesweeper
{
    public class MinesweeperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MinesweeperModel>().AsSingle();

            Container.BindInterfacesTo<MinesweeperPresenter>().AsSingle();
        }
    }
}
