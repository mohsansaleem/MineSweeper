using UnityEngine;
using UnityEngine.UI;

namespace PM.Roulette
{
    public class RouletteView : MonoBehaviour
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
}

