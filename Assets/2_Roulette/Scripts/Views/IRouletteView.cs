using System;

namespace PM.Roulette
{
    public interface IRouletteView
    {
        void Show();
        void Hide();

        void Reset();
        void StartSpinning(int targetAngle);
        void StopSpinning(Action onSpinningStop);
        void ShowResult(int multiplier, long result);
        
        void SetBalance(long balance);
        void SetInitialWin(int win);

        bool CanSpin { set; }

        void SubscribeOnSpinClick(Action onSpinTriggered);
        void UnSubscribeOnSpinClick(Action onSpinTriggered);
    }
}