using System;
using static _2020_Project___Battleships.Game;
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
            for (int i = 0; i < board.GetLength(0); i++)
            {
                FGcolor(DarkGray);
                Console.Write("| ");
                FGcolor(Gray);
                Console.Write(i + " ");
            }
            FGcolor(DarkGray);
            Console.WriteLine("|");
            FGcolor(Gray);

            // underline
            Console.Write("  ");
            FGcolor(DarkGray);
            for (int j = 0; j < board.GetLength(0) * 4; Console.Write("-"), j++) ;
            FGcolor(Gray);
            Console.WriteLine();

            if (Name == "CPU")
            {
                CpuMainBoardPrint();
            }
            else UserMainBoardPrint();
        }
        // PrintBoard END //


        private void CpuMainBoardPrint()
        {
            char[,] board = ArrayBoard;
            Position userHitColor = new Position(-1, -1); // starting values, in case that the LastHitColor is null
            // check if the LastHitColor of the player is empty so the program will not crush from the Player object being empty
            if (Players != null && Players[0] != null)
            {
                userHitColor.CopyAttributes(Players[0].LastHitColor);
            }


            for (int i = 0; i < board.GetLength(0); i++)
            {
                // print board index - letters
                Console.Write((char)('A' + i) + " ");

                for (int j = 0; j < board.GetLength(1); j++) // print the board
                {
                    if (i == userHitColor.Row && j == userHitColor.Col)
                    {
                        switch (board[i, j])
                        {
                            case '/':
                                FGcolor(DarkGray);
                                Console.Write("| ");
                                BGcolor(Red);
                                Console.Write(" ");
                                BGcolor(Black);
                                Console.Write(" ");
                                break;

                            case 'x':
                                FGcolor(DarkGray);
                                Console.Write("| ");
                                FGcolor(Green);
                                Console.Write(board[i, j] + " ");
                                break;
                        }//switch
                    }//if hitColor = board Position
                    else
                    {
                        // Missed '/' | Red BG
                        if (board[i, j] == '/')
                        {
                            FGcolor(DarkGray);
                            Console.Write("| ");
                            BGcolor(DarkRed);
                            Console.Write(" ");
                            BGcolor(Black);
                            Console.Write(" ");
                        }
                        else // Hit 'x' | Dark Green FG
                        if (board[i, j] == 'x')
                        {
                            FGcolor(DarkGray);
                            Console.Write("| ");
                            FGcolor(DarkGreen);
                            Console.Write(board[i, j] + " ");
                        }/*
                        else // CPU's Ships '5-9' | Hidden (null)
                        if (board[i, j] >= '5' && board[i, j] <= '9')
                        {
                            FGcolor(DarkGray);
                            Console.Write("|   ");
                        }*/
                        else // null slot | null
                        {
                            FGcolor(DarkGray);
                            Console.Write("|   ");
                        }
                    }
                }//for j (rows)

                // Print underline between the lines
                FGcolor(DarkGray);
                Console.WriteLine("|");
                for (int j = 0; j < board.GetLength(0) * 4 + 2; Console.Write("-"), j++) ;
                Console.WriteLine();
                FGcolor(Gray);
            }//for i (columns)
        }
        // CpuMainBoardPrint END //


        private void UserMainBoardPrint()
        {
            char[,] board = ArrayBoard;
            Position cpuHitColor = new Position(-1, -1); // starting values, in case that the LastHitColor is null
            // check if the LastHitColor of the player is empty so the program will not crush from the Player object being empty
            if (Players != null && Players[1] != null)
            {
                cpuHitColor.CopyAttributes(Players[1].LastHitColor);
            }


            for (int i = 0; i < board.GetLength(0); i++)
            {
                // print board index - letters
                Console.Write((char)('A' + i) + " ");

                for (int j = 0; j < board.GetLength(1); j++) // print the board
                {
                    if (i == cpuHitColor.Row && j == cpuHitColor.Col)
                    {
                        switch (board[i, j])
                        {
                            // Missed Last Turn | Gray BG
                            case '/':
                                FGcolor(DarkGray);
                                Console.Write("| ");
                                BGcolor(Gray);
                                Console.Write(" ");
                                BGcolor(Black);
                                Console.Write(" ");
                                break;

                            // Hit Last Turn | Red FG
                            case 'x':
                                FGcolor(DarkGray);
                                Console.Write("| ");
                                FGcolor(Red);
                                Console.Write(board[i, j] + " ");                                
                                break;
                        }//switch
                    }//if hitColor = board Position
                    else
                    {
                        // Missed '/' | Dark Gray BG
                        if (board[i, j] == '/')
                        {
                            FGcolor(DarkGray);
                            Console.Write("| ");
                            BGcolor(DarkGray);
                            Console.Write(" ");
                            BGcolor(Black);
                            Console.Write(" ");
                        }
                        else // Hit 'x' | Dark Red FG
                        if (board[i, j] == 'x')
                        {
                            FGcolor(DarkGray);
                            Console.Write("| ");
                            FGcolor(DarkRed);
                            Console.Write(board[i, j] + " ");
                        }
                        else // Player's Ship | Dark Cyan FG
                        if (board[i, j] >= '0' && board[i, j] <= '4')
                        {
                            FGcolor(DarkGray);
                            Console.Write("| ");
                            FGcolor(DarkCyan);
                            Console.Write(board[i, j] + " ");
                        }
                        else // null slot | null
                        {
                            FGcolor(DarkGray);
                            Console.Write("|   ");
                        }
                    }
                    
                }//for j (rows)

                // Print underline between the lines
                FGcolor(DarkGray);
                Console.WriteLine("|");
                for (int j = 0; j < board.GetLength(0) * 4 + 2; Console.Write("-"), j++) ;
                Console.WriteLine();
                FGcolor(Gray);
            }//for i (columns)            
        }
        // UserMainBoardPrint END //


    }
}
