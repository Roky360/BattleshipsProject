using System;
using static _2020_Project___Battleships.Player;
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
         ~ Description: The main function that displays the game board, customized for each player.
         * Logic: Calls the different functions to print out all the parts of the game board.
         # Syntax: boardName.PrintBoard();
         */
        public void PrintBoard()
        {
            char[,] board = ArrayBoard;
            bool isPlayer = Name != CpuName;


            // Title
            FGcolor(isPlayer ? Cyan : Red); // Decide in which color to print the title
            Console.WriteLine($"{Name}'s Board:");

            // Column Index - Numbers
            IndexColumnPrint();
            // underline
            Console.Write("  ");
            Underline(board.GetLength(0) * 4);

            // Board Body
            MainBoardPrint(isPlayer);

            Console.WriteLine();
        }
        // PrintBoard END //

        /* - Index Column Print - 
        ~ Description: Prints the index for the columns (numbers).
        * */
        private void IndexColumnPrint()
        {
            char[,] board = ArrayBoard;

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
        }
        // IndexColumnPrint END //

        /* - Underline - 
        ~ Description: Used to print an underline below each line of teh board.
        * */
        private void Underline(int length)
        {
            char[,] board = ArrayBoard;

            FGcolor(DarkGray);
            HyphenUnderline(length);
            FGcolor(Gray);
        }
        // Underline END //


        /* - Main Board Print - 
        ~ Description: prints the main part of the board (the actuall array and it's contents).
        * Logic: Uses a nested 'for' loop to print the index letters (for the rows),
        * and then calls the ColorCharDecider to print the content of the array in the correct colors.
        * */
        private void MainBoardPrint(bool isPlayer)
        {
            char[,] board = ArrayBoard;
            Position cpuHitColor = new Position(-1, -1); // starting values, in case that the LastHitColor is null
            // check if the LastHitColor of the player is empty so the program will not crush from the Player object being empty
            if (Players != null && Players[isPlayer ? 1 : 0] != null)
            {
                cpuHitColor.CopyAttributes(Players[isPlayer ? 1 : 0].LastShotColor);
            }


            for (int i = 0; i < board.GetLength(0); i++)
            {
                // Row Index - Letters
                Console.Write((char)('A' + i) + " |");

                for (int j = 0; j < board.GetLength(1); j++) // print the board
                {
                    char currentSpot = board[i, j];
                    bool lastHitColorSpot = (i == cpuHitColor.Row) && (j == cpuHitColor.Col);

                    ColorCharDecider(currentSpot, lastHitColorSpot, isPlayer);
                }//for j (rows)
                Console.WriteLine();

                // Print underline between the lines
                Underline(board.GetLength(0) * 4 + 2);
            }//for i (columns)
        }
        // MainBoardPrint END //

        /* - Color Char Decider - 
        ~ Description: Uses a switch to decide what to print based on the content of the current spot of the array.
        * Logic: The Color Char Decider uses dedicated functions to print the differrent kinds of slots.
        * In addition, it uses ternary operators to decide which colors to pass to those functions,
        * based on the lastHitColorSpot (contains the last shot position) and the isPlayer (cause there are different colors for each player) variables.
        * */
        private void ColorCharDecider(char currentSpot, bool lastHitColorSpot, bool isPlayer)
        {
            switch (currentSpot)
            {
                // Missed '/' | Gray & Dark Gray BG : Red & Dark Red
                case '/':
                    MissedSpot(isPlayer ? (lastHitColorSpot ? Gray : DarkGray) : (lastHitColorSpot ? Red : DarkRed));
                    break;

                // Hit 'x' | Red & Dark Red FG : Green & DarkGreen
                case 'x':
                    PrintContent(isPlayer ? (lastHitColorSpot ? Red : DarkRed) : (lastHitColorSpot ? Green : DarkGreen), currentSpot);
                    break;

                // Player's Ship | Dark Cyan FG
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                    PrintContent(DarkCyan, currentSpot);
                    break;

                // null slot | null
                default:
                    EmptySpot();
                    break;
            }//switch
        }
        // ColorCharDecider END //


        /* - Missed Spot - 
        ~ Description: Used by the MainBoardPrint function to print "missed spot" ('/' in the array).
        * */
        private static void MissedSpot(ConsoleColor color)
        {
            FGcolor(DarkGray);
            Console.Write(" ");
            BGcolor(color);
            Console.Write(" ");
            BGcolor(Black);
            Console.Write(" |");
        }
        // MissedSpot END //

        /* - Print Content - 
        ~ Description:
        * Logic: The PrintContent function gets the desired color and the character to print and prints it in the correct format design.
        * */
        private void PrintContent(ConsoleColor color, char currentSpot)
        {
            FGcolor(DarkGray);
            Console.Write(" ");
            FGcolor(color);
            Console.Write(currentSpot + " ");
            FGcolor(DarkGray);
            Console.Write("|");
        }
        // PrintContent END //

        /* - Empty Spot - 
        ~ Description: Used by the MainBoardPrint function to print an empty spot.
        * */
        private static void EmptySpot()
        {
            FGcolor(DarkGray);
            Console.Write("   |");
        }
        // EmptySpot END //


    }
}
