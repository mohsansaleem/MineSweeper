using UniRx;

namespace PM.Minesweeper
{
    public class MineFieldCell
    {
        public uint X, Y;
        public ReactiveProperty<EMineFieldCellData> Data;
            
        public ReactiveProperty<bool> IsOpened;
        public ReactiveProperty<bool> IsFlagged;

        public MineFieldCell(uint indexX, uint indexY)
        {
            X = indexX;
            Y = indexY;
            
            Data = new ReactiveProperty<EMineFieldCellData>(EMineFieldCellData.M0);
                
            IsOpened = new ReactiveProperty<bool>(false);
            IsFlagged = new ReactiveProperty<bool>(false);
        }

        public void ToggleFlagged()
        {
            IsFlagged.Value = !IsFlagged.Value;
        }
    }
        
    public enum EMineFieldCellData
    {
        // Surrounding Mines
        M0 = 0,
        M1,
        M2,
        M3,
        M4,
        M5,
        M6,
        M7,
        M8,
        M9,
            
        // It is a mine.
        MINE,
    }
}