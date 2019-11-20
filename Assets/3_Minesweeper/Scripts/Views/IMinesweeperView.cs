using System;

namespace PM.Minesweeper
{
    public interface IMinesweeperView
    {
        void CreateGridUI(uint settingsSizeX, uint settingsSizeY);
        
        void Show();
        void Hide();
        
        void ShowMessage(string message, Action action);
        void HideMessage();

        void SetCellData(uint cellX, uint cellY, EMineFieldCellData eMineFieldCellData);
        void SetCellFlagged(uint cellX, uint cellY, bool isFlagged);
        void SetCellContentVisible(uint cellX, uint cellY, bool isOpened);
        
        void SubcribleOnCellClick(uint indexX, uint indexY, Action action);
        void SubcribleOnCellRightClick(uint indexX, uint indexY, Action action);
    }
}