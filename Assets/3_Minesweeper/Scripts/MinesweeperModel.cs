using System.Collections.Generic;
using UniRx;
using Zenject;

namespace PM.Minesweeper
{
    public class MinesweeperModel
    {
        [Inject] private SignalBus _signalBus;

        public enum EGamePlayState
        {
            Load,
            Playing
        }

        public ReactiveProperty<EGamePlayState> GamePlayState;
        public ReactiveCollection<EGamePlayState> Collection;
        public SortedList<int,string> SortedList = new SortedList<int,string>();
        
        public MinesweeperModel()
        {
            GamePlayState = new ReactiveProperty<EGamePlayState>(EGamePlayState.Load);
            Collection = new ReactiveCollection<EGamePlayState>();
            
        }

    }
}