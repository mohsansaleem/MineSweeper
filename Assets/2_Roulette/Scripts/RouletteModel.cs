using UniRx;

namespace PM.Roulette
{
    public class RouletteModel
    {
        public enum ELoadingProgress
        {
            NotLoaded = -1,
            Zero = 0,
            StaticDataLoaded = 30,
            UserNotFound = 80,
            GamePlay = 100
        }

        public ReactiveProperty<ELoadingProgress> LoadingProgress;

        public RouletteModel()
        {
            LoadingProgress = new ReactiveProperty<ELoadingProgress>(ELoadingProgress.Zero);
        }
    }
}

