using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Board
    {
        public string Name { get; set; }
        private Position BoardBoundaries { get; set; }
        public char[,] arrayBoard { get; set; }


        // constructor
        public Board(string name, int row, int column)
        {
            Name = name;
            BoardBoundaries = new Position(row, column);
            arrayBoard = new char[row, column];
            ResetBoard(arrayBoard);
        }


        /* Resetting the board (the array) to null */
        public static void ResetBoard(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '\0';
                }
            }
        }
        // ResetBoard END //


        /* Print the board to the screen */
        public void PrintBoard()
        {
            Console.WriteLine($"{Name}'s Board:");
            // print board index - numbers
            Console.Write("  ");
            for (int i = 0; i < arrayBoard.GetLength(0); i++)
            {
                Console.Write("| " + (i) + " ");
            }
            Console.WriteLine("|");

            // underline
            Console.Write("  ");
            for (int i = 0; i < arrayBoard.GetLength(0); i++)
            {
                Console.Write("----");
            }
            Console.WriteLine();

            for (int i = 0; i < arrayBoard.GetLength(0); i++) // print board index - characters
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < arrayBoard.GetLength(1); j++) // print the board
                {
                    Console.Write("| " + arrayBoard[i, j] + " ");
                }
                Console.WriteLine("|"); Console.WriteLine("-------------------------------------------");
            }
            Console.WriteLine();
            
        }
        // PrintBoard END //








    }
}
