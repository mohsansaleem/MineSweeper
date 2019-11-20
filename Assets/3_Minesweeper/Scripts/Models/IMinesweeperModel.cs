using System;
using UniRx;

namespace PM.Minesweeper
{
    public interface IMinesweeperModel
    {
        EGameState GameState { get; set; }
        
        void CreateGrid(uint sizeX, uint sizeY);
        void PopulateGrid(uint x, uint y, uint settingsMinesCount);
        void Reset(uint sizeX, uint sizeY);
        
        void ToggleFlagged(uint u, uint u1);
        
        EMineFieldCellData GetMineFieldGridCellData(uint x, uint y);
        void SetMineFieldGridCellData(uint x, uint y, EMineFieldCellData eMineFieldCellData);
        bool GetMineFieldCellOpenedStatus(uint x, uint y);
        void SetMineFieldCellOpenedStatus(uint x, uint y, bool value);
        
        IDisposable SubscribeGameState(Action<EGameState> onStateChanged);
        IDisposable SubscribeMineFieldGridCellData(uint x, uint y, Action<EMineFieldCellData> action);
        IDisposable SubscribeMineFieldGridCellOpened(uint x, uint y, Action<bool> action);
        IDisposable SubscribeMineFieldGridCellFlagged(uint x, uint y, Action<bool> action);
    }
}