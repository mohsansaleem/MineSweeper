using Zenject;

namespace PG.Core.Installer
{
    public class CoreContextInstaller : MonoInstaller<CoreContextInstaller>
    {
        public override void InstallBindings()
        { 
            // Installing Signal Bus Once and for all.
            SignalBusInstaller.Install(Container);
        }
    }
}