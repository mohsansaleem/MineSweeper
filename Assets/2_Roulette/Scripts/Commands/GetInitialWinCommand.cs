using PM.Roulette;
using Server.API;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GetInitialWinCommand
    {
        [Inject] private GameplayApi _gameplayApi;
        [Inject] private IRouletteModel _rouletteModel;
        
        public void Execute(GetInitialWinSignal signal)
        {
            _gameplayApi.GetInitialWin()
                .Done(win =>
                    {
                        _rouletteModel.InitialWin = win;

                        signal.Resolve();
                    },
                    signal.Reject);
        }
    }
}