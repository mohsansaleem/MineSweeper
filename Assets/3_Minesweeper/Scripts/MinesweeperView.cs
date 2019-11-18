using UnityEngine;
using UnityEngine.UI;

namespace PM.Minesweeper
{
    public class MinesweeperView : MonoBehaviour
    {
        [Header("View")]
        public RectTransform Container;
        public Transform EntitiesRoot;
        public Image Image;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

