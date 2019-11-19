using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            MineField mf = new MineField(10, 10, 10);

            mf.StartGame(4, 4);

            mf.PrintGameGrid();

            MineField.Cell cell = MineField.Cell.EMPTY;
            do
            {
                mf.PrintUserGrid();

                string str = Console.ReadLine();

                uint r = uint.Parse(str);

                str = Console.ReadLine();

                uint c = uint.Parse(str);

                cell = mf.Open(--r, --c);
            } while (cell != MineField.Cell.MINE);




            Console.ReadKey();
        }
    }
}
