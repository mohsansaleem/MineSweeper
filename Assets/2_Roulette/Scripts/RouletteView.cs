using UnityEngine;
using UnityEngine.UI;

namespace PM.Roulette
{
    public class RouletteView : MonoBehaviour, IRouletteView
    {
        [Header("References")]         
        public GameObject LogoImage;
        public Slider ProgressBar;

        public void SetProgress(float progress)
        {
            ProgressBar.value = progress;
        }

        public void Show()
        {
            gameObject?.SetActive(true);
        }

        public void Hide()
        {
            gameObject?.SetActive(false);
        }
    }

    public interface IRouletteView
    {
        void Show();
        void Hide();
    }
}

