using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }


        public override string ToString()
        {
            return $"{Col},{Row}";
        }
    }
}
