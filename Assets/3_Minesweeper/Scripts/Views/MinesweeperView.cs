using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PM.Minesweeper
{
    public class MinesweeperView : MonoBehaviour, IMinesweeperView
    {
        [Header("View")]
        public GridLayoutGroup GridView;
        public RectTransform GridRectTransform;
        public MessageView MessageView;
        
        // Prefabs
        public MineSweeperCellView CellViewPrefab;
        
        // Locals
        public MineSweeperCellView[,] CellViews;

        public void CreateGridUI(uint sizeX, uint sizeY)
        {
            CellViews = new MineSweeperCellView[sizeX, sizeY];

            GridView.cellSize = new Vector2(GridRectTransform.sizeDelta.x / sizeY,
                                            GridRectTransform.sizeDelta.y / sizeX);

            GridView.constraintCount = (int)sizeY;
            
            for (int indexX = 0; indexX < sizeX; indexX++)
            {
                for (int indexY = 0; indexY < sizeY; indexY++)
                {
                    // No need for using a pool for Cells as they will be reused on replay.
                     CellViews[indexX, indexY] = (MineSweeperCellView)PrefabUtility.InstantiatePrefab(CellViewPrefab, GridRectTransform);
                     CellViews[indexX, indexY].name = $"Cell[{indexX}x{indexY}]";
                }
            }
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void ShowMessage(string message, Action action)
        {
            MessageView.Show(message, action);
        }

        public void HideMessage()
        {
            MessageView.Hide();
        }

        #region Setters

        public void SetCellData(uint cellX, uint cellY, EMineFieldCellData eMineFieldCellData)
        {
            MineSweeperCellView cellView = CellViews[cellX, cellY];
            
            cellView.IsMine = (eMineFieldCellData == EMineFieldCellData.MINE);
                
            if(eMineFieldCellData != EMineFieldCellData.M0 && 
               eMineFieldCellData != EMineFieldCellData.MINE)
            {
                cellView.CellValue.text = ((int)eMineFieldCellData).ToString();   
            }
            else
            {
                cellView.CellValue.text = "";
            }
        }

        public void SetCellFlagged(uint cellX, uint cellY, bool isFlagged)
        {
            CellViews[cellX, cellY].IsFlagged = isFlagged;
        }

        public void SetCellContentVisible(uint cellX, uint cellY, bool isOpened)
        {
            CellViews[cellX, cellY].IsContentVisible = isOpened;
        }
        
        #endregion


        #region Subscription
        public void SubcribleOnCellClick(uint indexX, uint indexY, Action action)
        {
            CellViews[indexX, indexY].CellButton.onClick.AddListener(()=> action?.Invoke());
        }

        public void SubcribleOnCellRightClick(uint indexX, uint indexY, Action action)
        {
            CellViews[indexX, indexY].OnRightClicked = action;
        }
        #endregion
    }
}

