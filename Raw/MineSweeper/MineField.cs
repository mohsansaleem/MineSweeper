using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class MineField
    {
        private uint _width, _height, _minesCount;
        private EMineFieldState _mineFieldState;
        private Cell[,] _mineFieldGrid;
        private Cell[,] _currentUserGrid;

        public MineField(uint width, uint height, uint mines)
        {
            this._width = width;
            this._height = height;
            _mineFieldGrid = new Cell[height, width];
            _currentUserGrid = new Cell[height, width];
            _minesCount = mines;

            Reset();
        }

        public bool StartGame(uint x, uint y)
        {
            if (_mineFieldState == EMineFieldState.Initilized)
            {
                PopulateGrid(x, y);

                _mineFieldState = EMineFieldState.GameIsOn;

                Open(x, y);

                return true;
            }
            return false;
        }

        private void PopulateGrid(uint x, uint y)
        {
            // Placing Mines
            Random random = new Random();

            uint placed = _minesCount;
            while (placed > 0)
            {
                uint r = (uint)random.Next((int)_height);
                uint c = (uint)random.Next((int)_width);
                if (_mineFieldGrid[r, c] != Cell.MINE &&
                    (r > x + 1 || r < x - 1) &&
                    (c > y + 1 || c < y - 1))
                {
                    _mineFieldGrid[r, c] = Cell.MINE;

                    UpdateSiblingsCount(r, c);

                    placed--;
                }
            }

            _mineFieldState = EMineFieldState.Populated;
        }

        private void UpdateSiblingsCount(uint x, uint y)
        {
            for (uint r = x > 0 ? x - 1 : x; r < x + 2 && r < _height; r++)
            {
                for (uint c = y > 0 ? y - 1 : y; c < y + 2 && c < _width; c++)
                {
                    if (!(r == x && c == y) && _mineFieldGrid[r, c] != Cell.MINE)
                        _mineFieldGrid[r, c]++;
                }
            }
        }

        private void Reset()
        {
            // Clearing out the User Grid
            for (uint r = 0; r < _height; ++r)
            {
                for (uint c = 0; c < _width; ++c)
                {
                    _currentUserGrid[r, c] = Cell.CLOSED;
                    _mineFieldGrid[r, c] = Cell.EMPTY;
                }
            }

            _mineFieldState = EMineFieldState.Initilized;
        }

        public Cell Open(uint x, uint y)
        {
            if (_mineFieldState != EMineFieldState.GameIsOn)
                throw new Exception("Grid not initiated.");

            UnveilEmptyCells(x, y);

            if (_mineFieldGrid[x, y] == Cell.MINE)
                _mineFieldState = EMineFieldState.Finished;

            return _currentUserGrid[x, y];
        }

        private void UnveilEmptyCells(uint x, uint y)
        {
            if (_currentUserGrid[x, y] != Cell.CLOSED &&
                _currentUserGrid[x, y] != Cell.Flagged)
                return;

            _currentUserGrid[x, y] = _mineFieldGrid[x, y];

            if (_mineFieldGrid[x, y] != Cell.EMPTY)
                return;

            for (uint r = x > 0 ? x - 1 : x; r < x + 2 && r < _height; r++)
            {
                for (uint c = y > 0 ? y - 1 : y; c < y + 2 && c < _width; c++)
                {
                    if (!(r == x && c == y))
                        UnveilEmptyCells(r, c);
                }
            }
        }

        public Cell GetCellAt(uint x, uint y)
        {
            return Cell.EMPTY;
        }

        public void PrintGameGrid()
        {
#if DEBUG
            Console.WriteLine("\n***********Game Grid***********");
            for (uint r = 0; r < _height; r++)
            {
                for (uint c = 0; c < _width; c++)
                    Console.Write("----");
                Console.WriteLine("-");
                for (uint c = 0; c < _width; c++)
                {
                    if (_mineFieldGrid[r, c] == Cell.EMPTY)
                        Console.Write("| _ ");
                    else if (_mineFieldGrid[r, c] == Cell.MINE)
                        Console.Write("| M ");
                    else
                        Console.Write("| " + (int)_mineFieldGrid[r, c] + " ");
                }
                Console.WriteLine("|");
            }
            for (uint c = 0; c < _width; c++)
                Console.Write("----");
            Console.WriteLine("-");
#endif
        }

        public void PrintUserGrid()
        {
            Console.WriteLine("\n***********User Grid***********");
            for (uint r = 0; r < _height; r++)
            {
                for (uint c = 0; c < _width; c++)
                    Console.Write("----");
                Console.WriteLine("-");
                for (uint c = 0; c < _width; c++)
                {
                    if (_currentUserGrid[r, c] == Cell.EMPTY)
                        Console.Write("| _ ");
                    else if (_currentUserGrid[r, c] == Cell.MINE)
                        Console.Write("| M ");
                    else if (_currentUserGrid[r, c] == Cell.CLOSED)
                        Console.Write("|   ");
                    else if (_currentUserGrid[r, c] == Cell.Flagged)
                        Console.Write("| F ");
                    else
                        Console.Write("| " + (int)_mineFieldGrid[r, c] + " ");
                }
                Console.WriteLine("|");
            }
            for (uint c = 0; c < _width; c++)
                Console.Write("----");
            Console.WriteLine("-");
        }

        public enum Cell
        {
            EMPTY = 0,
            M1,
            M2,
            M3,
            M4,
            M5,
            M6,
            M7,
            M8,
            M9,
            MINE,
            Flagged,
            CLOSED
        };

        private enum EMineFieldState
        {
            Garbage,
            Initilized,
            Populated,
            GameIsOn,
            Finished
        }
    }
}