using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PM.Roulette
{
    public class RouletteView : MonoBehaviour, IRouletteView
    {
        [Header("References")] public RectTransform WheelTransform;
        public Text BalanceText;
        public Text BasicRewardText;
        public Text MultiplierText;
        public Text FinalRewardText;
        public Button SpinButton;
        public List<Text> WheelTexts;

        [Inject] private RouletteSettingsInstaller.Settings _settings;

        // Locals
        private Action onSpinButtonClicked;
        private Action onSpinningStopped;

        private float _speed;
        private bool _slowingDown;
        private float _targetAngle;

        private float _tmpAngle;

        private void Start()
        {
            SpinButton.onClick.AddListener(() => onSpinButtonClicked?.Invoke());

            for (int i = 0; i < WheelTexts.Count; i++)
            {
                WheelTexts[i].text = _settings.Multipliers[i] + "x";
            }
        }

        public void Show()
        {
            gameObject?.SetActive(true);
        }

        public void Hide()
        {
            gameObject?.SetActive(false);
        }

        public void Reset()
        {
            CanSpin = false;
        }

        public void StartSpinning(int multiplier)
        {
            _slowingDown = false;
            _speed = _settings.RouletteSpeed;

            _targetAngle = _settings.Multipliers.IndexOf(multiplier) * 20f;

            StartCoroutine(nameof(Rotate));
        }

        public void StopSpinning(Action onSpinningStop)
        {
            onSpinningStopped = onSpinningStop;
            _slowingDown = true;
        }

        IEnumerator Rotate()
        {
            while (true)
            {
                WheelTransform.localEulerAngles =
                    new Vector3(0, 0, WheelTransform.localEulerAngles.z + _speed * Time.deltaTime);

                if (_speed > _settings.MinSpeed)
                {
                    if (_slowingDown)
                        _speed -= _settings.Resistence;
                }
                else if (Mathf.Abs(WheelTransform.localEulerAngles.z - _targetAngle) < 5)
                {
                    // TODO: Make it more smoother.
                    onSpinningStopped?.Invoke();
                    break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
        
        public void ShowResult(int multiplier, long result)
        {
            SetMultiplier(multiplier);
            SetResult(result);
        }
        
        public void SetBalance(long balance)
        {
            BalanceText.text = balance.ToString();
        }

        public void SetInitialWin(int win)
        {
            BasicRewardText.text = win == 0 ? "---" : win.ToString();
        }

        private void SetMultiplier(int multiplier)
        {
            MultiplierText.text = multiplier == 0 ? "--" : multiplier.ToString();
        }

        private void SetResult(long result)
        {
            FinalRewardText.text = result == 0 ? "----" : result.ToString();
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
    }
}