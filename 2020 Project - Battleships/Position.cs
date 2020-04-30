using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Position
    {
        /* This class used to save coordinates (row, column) together in an easy way. */

        public int Row { get; set; }
        public int Col { get; set; }


        // constructor
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }



        /* - Copy Attributes -
         ~ Description: Used to copy the properties of one Position object to another.
         * Logic: the function copies the Row & Col (Column) properties from the 'srcPos' (source position) object that is written as a parameter the object that the function is act on.
         # Syntax: dest.CopyProperties(source);
         > Return: void.
         */
        public void CopyAttributes(Position srcPos)
        {
            Row = srcPos.Row;
            Col = srcPos.Col;
        }
        // CopyAttributes END //


        /* - To String -
         ~ Description: Prints a position object in a string form.
         * Logic: Converts the Row attribute to capital letter to the UserTurn that uses this function
         * to print a pair of coordinates from the game board (that uses letters as the row index).
         */
        public override string ToString()
        {
            return $"{(char)(Row + 'A')},{Col}";
        }
        // ToString END //

    }
}
