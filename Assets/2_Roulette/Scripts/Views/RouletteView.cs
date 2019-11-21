using System;
using UnityEngine;
using UnityEngine.UI;

namespace PM.Roulette
{
    public class RouletteView : MonoBehaviour, IRouletteView
    {
        [Header("References")] 
        public RectTransform WheelTransform;
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
            Debug.LogError($"Balance: {balance}");
        }

        public void SetInitialWin(int win)
        {
            BasicRewardText.text = win == 0? "--" : win.ToString();
        }

        public void SubscribeOnSpinClick(Action onSpinTriggered)
        {
            onSpinButtonClicked += onSpinTriggered;
        }

        public void UnSubscribeOnSpinClick(Action onSpinTriggered)
        {
            onSpinButtonClicked -= onSpinTriggered;
        }
    }

    public interface IRouletteView
    {
        void Show();
        void Hide();
        void SetBalance(long balance);
        void SetInitialWin(int win);
        void SubscribeOnSpinClick(Action onSpinTriggered);
        void UnSubscribeOnSpinClick(Action onSpinTriggered);
    }
}

