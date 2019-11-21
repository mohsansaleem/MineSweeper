using PM.Roulette;
using Server.API;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GetPlayerBalanceCommand
    {
        [Inject] private GameplayApi _gameplayApi;
        [Inject] private IRouletteModel _rouletteModel;
        
        public void Execute(GetPlayerBalanceSignal signal)
        {
            _gameplayApi.GetPlayerBalance()
                .Done(balance =>
                    {
                        _rouletteModel.Balance = balance;
                        
                        signal.Resolve();
                    },
                    signal.Reject);
        }
    }
}