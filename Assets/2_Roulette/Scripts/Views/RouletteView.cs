using System;
using UnityEngine;
using UnityEngine.UI;

namespace PM.Roulette
{
    public class RouletteView : MonoBehaviour, IRouletteView
    {
        [Header("References")] 
        public RectTransform WheelTransform;
        public Text BalanceText;
        public Text BasicRewardText;
        public Text MultiplierText;
        public Text FinalRewardText;
        public Button SpinButton;

        // Locals
        private Action onSpinButtonClicked;

        private void Start()
        {
            SpinButton.onClick.AddListener(() => onSpinButtonClicked?.Invoke());
        }

        public void Show()
        {
            gameObject?.SetActive(true);
        }

        public void Hide()
        {
            gameObject?.SetActive(false);
        }

        public void SetBalance(long balance)
        {
            BalanceText.text = balance.ToString();
        }

        public void SetInitialWin(int win)
        {
            BasicRewardText.text = win == 0? "---" : win.ToString();
        }

        private void SetMultiplier(int multiplier)
        {
            MultiplierText.text = multiplier == 0? "--" : multiplier.ToString();
        }

        private void SetResult(long result)
        {
            FinalRewardText.text = result == 0? "----" : result.ToString();
        }
        
        public void ShowResult(int multiplier, long result)
        {
            SetMultiplier(multiplier);
            SetResult(result);
        }
        
        public bool CanSpin
        {
            set => SpinButton.interactable = value;
        }
        
        public void SubscribeOnSpinClick(Action onSpinTriggered)
        {
            onSpinButtonClicked += onSpinTriggered;
        }

        public void UnSubscribeOnSpinClick(Action onSpinTriggered)
        {
            onSpinButtonClicked -= onSpinTriggered;
        }

        public void Reset()
        {
            CanSpin = false;
        }
    }

    public interface IRouletteView
    {
        void Show();
        void Hide();
        
        void SetBalance(long balance);
        void SetInitialWin(int win);
        void ShowResult(int multiplier, long result);
        
        bool CanSpin { set; }
        
        void SubscribeOnSpinClick(Action onSpinTriggered);
        void UnSubscribeOnSpinClick(Action onSpinTriggered);
        void Reset();
    }
}

