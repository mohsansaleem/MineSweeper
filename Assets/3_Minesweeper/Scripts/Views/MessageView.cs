using System;
using UnityEngine;
using UnityEngine.UI;

namespace PM.Minesweeper
{
    public class MessageView : MonoBehaviour
    {
        public Text MessageText;
        public Button Button;

        public void Show(string message, Action onClick)
        {
            MessageText.text = message;
            Button.onClick.AddListener(() =>
            {
                Hide();
                onClick?.Invoke();
            });

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}