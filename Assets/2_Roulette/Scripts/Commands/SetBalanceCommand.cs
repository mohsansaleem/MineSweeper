using System;
using PM.Roulette;
using Server.API;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SetBalanceCommand
    {
        [Inject] private GameplayApi _gameplayApi;
        [Inject] private IRouletteModel _rouletteModel;

        public void Execute(SetBalanceSignal signal)
        {
            _gameplayApi.SetPlayerBalance(_rouletteModel.Balance).Done(signal.Resolve, signal.Reject);
        }
    }
}