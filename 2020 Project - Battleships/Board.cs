using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Board
    {
        public string Name { get; set; }                        // The name of the owner of the board
        public char[,] ArrayBoard { get; set; }                 // The array that contains the data of the board


        // constructor
        public Board(string name, int row, int column)
        {
            Name = name;
            ArrayBoard = new char[row, column];
            ResetBoard(ArrayBoard);
        }


        /* - Reset Board -
         ~ Description: Resets the array of the board to null.
         * Logic: Moves over the whole 2D array and sets the values to '\0' (null)
         > RETURNS: Nothing.
         */
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


        /* - Print Board -
         ~ Description: Prints the board to the screen
         * Logic: Prints the array and it's values via nested for loop, and the instructors - numbers (0-9) and letters (A-J)
         # Syntax: boardName.PrintBoard();
         > RETURNS: Nothing, just prints the board to the screen.
         */
        public void PrintBoard()
        {
            Console.WriteLine($"{Name}'s Board:");
            // print board index - numbers
            Console.Write("  ");
            for (int i = 0; i < ArrayBoard.GetLength(0); i++)
            {
                Console.Write("| " + (i) + " ");
            }
            Console.WriteLine("|");

            // underline
            Console.Write("  ");
            for (int i = 0; i < ArrayBoard.GetLength(0); i++)
            {
                Console.Write("----");
            }
            Console.WriteLine();

            for (int i = 0; i < ArrayBoard.GetLength(0); i++) // print board index - characters
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < ArrayBoard.GetLength(1); j++) // print the board
                {
                    if (ArrayBoard[i, j] == '/')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("| " + ArrayBoard[i, j] + " ");                        
                    }
                    else
                    if (ArrayBoard[i, j] == 'x')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("| " + ArrayBoard[i, j] + " ");                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("| " + ArrayBoard[i, j] + " ");
                    }                        
                }
                Console.WriteLine("|"); Console.WriteLine("-------------------------------------------");
            }
            Console.WriteLine();            
        }
        // PrintBoard END //


    }
}
