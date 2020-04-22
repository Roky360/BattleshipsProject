using System;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Board
    {
        public string Name { get; set; }                        // The name of the owner of the board
        public char[,] ArrayBoard { get; set; }                 // The array that contains the data of the board


        // constructor
        public Board(string name, Position boardSize)
        {
            Name = name;
            ArrayBoard = new char[boardSize.Row,boardSize.Col];
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
            char[,] board = ArrayBoard;            


            Console.WriteLine($"{Name}'s Board:");

            // print board index - numbers
            Console.Write("  ");

            if (Name == "CPU")
            {
                CpuMainBoardPrint();
            }
            else UserMainBoardPrint();

            FGcolor(DarkGray);
            Console.WriteLine("|");
            FGcolor(DarkGray);

            // underline
            Console.Write("  ");
            FGcolor(DarkGray);
            for (int j = 0; j < board.GetLength(0) * 4; Console.Write("-"), j++) ;
            FGcolor(Gray);
            Console.WriteLine();


            for (int i = 0; i < board.GetLength(0); i++) // print board index - characters
            {
                Console.Write((char)('A' + i) + " ");
                
                FGcolor(DarkGray);
                Console.WriteLine("|");
                for (int j = 0; j < board.GetLength(0) * 4 + 2; Console.Write("-"), j++) ;
                Console.WriteLine();
                FGcolor(Gray);
            }
            Console.WriteLine();            
        }
        // PrintBoard END //


        private void CpuMainBoardPrint()
        {
            char[,] board = ArrayBoard;
            //Game.Players[0].LastHitColor = new Position(-1, -1); // starting values
            Position userHitColor = new Position(0, 0);
            userHitColor.CopyAttributes(Game.Players[0].LastHitColor);


            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++) // print the board
                {
                    // Missed - '/' | Red & Dark Red
                    if (i == userHitColor.Row && j == userHitColor.Col)
                    {
                        Console.Write("|");
                        BGcolor(DarkRed);
                        Console.Write("   ");
                        BGcolor(Black);
                    }
                    else
                    if (board[i, j] == '/')
                    {
                        Console.Write("|");
                        BGcolor(Red);
                        Console.Write("   ");
                        BGcolor(Black);
                    }

                    // Hit - 'x' | Green & Dark Green
                    else
                    if (i == userHitColor.Row && j == userHitColor.Col)
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(DarkGreen);
                        Console.Write(board[i, j] + " ");
                        FGcolor(Gray);
                    }
                    else
                    if (board[i, j] == 'x')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(Green);
                        Console.Write(board[i, j] + " ");
                        FGcolor(Gray);
                    }

                    // Player's Ship - Dark Cyan
                    else
                    if (board[i, j] >= '0' && board[i, j] <= '4')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(DarkCyan);
                        Console.Write(board[i, j] + " ");
                    }
                    else if (board[i, j] >= '5' && board[i, j] <= '9')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        Console.Write("  ");
                    }
                    else // null
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(Gray);
                        Console.Write(board[i, j] + " ");
                    }
                }

                FGcolor(DarkGray);
                Console.Write("| ");
                FGcolor(Gray);
                Console.Write(i + " ");
            }            
        }
        // MainBoardPrint END //


        private void UserMainBoardPrint()
        {
            char[,] board = ArrayBoard;
            //Game.Players[1].LastHitColor = new Position(-1, -1); // starting values
            Position cpuHitColor = new Position(0, 0);
            cpuHitColor.CopyAttributes(Game.Players[1].LastHitColor);


            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++) // print the board
                {
                    // Missed - '/' | Red & Dark Red
                    if (i == cpuHitColor.Row && j == cpuHitColor.Col)
                    {
                        Console.Write("|");
                        BGcolor(DarkRed);
                        Console.Write("   ");
                        BGcolor(Black);
                    }
                    else
                    if (board[i, j] == '/')
                    {
                        Console.Write("|");
                        BGcolor(Red);
                        Console.Write("   ");
                        BGcolor(Black);
                    }

                    // Hit - 'x' | Green & Dark Green
                    else
                    if (i == cpuHitColor.Row && j == cpuHitColor.Col)
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(DarkGreen);
                        Console.Write(board[i, j] + " ");
                        FGcolor(Gray);
                    }
                    else
                    if (board[i, j] == 'x')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(Green);
                        Console.Write(board[i, j] + " ");
                        FGcolor(Gray);
                    }

                    // Player's Ship - Dark Cyan
                    else
                    if (board[i, j] >= '0' && board[i, j] <= '4')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(DarkCyan);
                        Console.Write(board[i, j] + " ");
                    }
                    else if (board[i, j] >= '5' && board[i, j] <= '9')
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        Console.Write("  ");
                    }
                    else // null
                    {
                        FGcolor(DarkGray);
                        Console.Write("| ");
                        FGcolor(Gray);
                        Console.Write(board[i, j] + " ");
                    }
                }

                FGcolor(DarkGray);
                Console.Write("| ");
                FGcolor(Gray);
                Console.Write(i + " ");
            }
        }
        // MainBoardPrint END //


    }
}
