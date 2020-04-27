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
            Row = row;
            Col = col;
        }


        /* - Copy Properties -
         ~ Description: Used to copy the properties of one Position object to another.
         * Logic: the function copies the Row & Col (Column) properties from the 'srcPos' (source position) object that is written as a parameter the object that the function is act on.
         # Syntax: dest.CopyProperties(source);
         > RETURNS: Nothing, just copying the values and has no need to return something.
         */
        public void CopyAttributes(Position srcPos)
        {
            Row = srcPos.Row;
            Col = srcPos.Col;
        }


        public override string ToString()
        {
            return $"{(char)(Row + 'A')},{Col}";
        }
    }
}
