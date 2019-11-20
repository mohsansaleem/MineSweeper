using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PM.Minesweeper
{
    public class MineSweeperCellView : MonoBehaviour, IPointerClickHandler
    {
        public RectTransform ContentPanel;
        public Button CellButton;
        public Text CellValue;
        public Image FlagImage;
        public Image BombImage;

        public Action OnRightClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClicked?.Invoke();
            }
        }
        
        public bool IsContentVisible
        {
            set
            {
                ContentPanel.gameObject.SetActive(value);
                CellButton.interactable = !value;
            }
        }
        
        public bool IsFlagged
        {
            set
            {
                FlagImage.gameObject.SetActive(value);
            }
        }
        
        public bool IsMine
        {
            set
            {
                BombImage.gameObject.SetActive(value);
                CellValue.gameObject.SetActive(!value);
            }
        }
    }
}