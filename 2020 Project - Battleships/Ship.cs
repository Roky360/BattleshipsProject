using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Ship
    {
        private static int NextID = 0; // This responsable for set the Id property and increase by 1 every time
        public int Id { get; set; } // The identity number of the ships. Each ship has a different ID number
        public Position StartingPos { get; set; } // Contains the starting position of the ship
        private bool Horizontal { get; set; } // Decides whether or not the ship will be horizontally (true) or vertically (false)
        public int Length { get; set; } // Contains the length of the ship - how many slots it will occupy from the starting position
        public int RemainParts { get; set; } // Contains how many parts remain to the ship
        public bool isSank { get; set; } // Contains if the ship is steel "alive" - if it has at least 1 remaining slot


        // constructor
        public Ship(Position startingPos, bool horizontal, int length)
        {
            Id = NextID++;
            StartingPos = startingPos;
            Horizontal = horizontal;
            Length = length;
            RemainParts = length;
        }


        private static char GetRowFromUser(char[,] board)
        {
            Console.Write("Row = ");
            char rowUser = Console.ReadKey().KeyChar;
            Console.WriteLine();

            while ((rowUser < 'A' || rowUser > 'Z') && (rowUser < 'a' || rowUser > 'z'))
            {
                Console.WriteLine($"The input to the row selection must be a letter (small or capital) between A-{(char)(board.GetLength(0) + 64)}. Please reenter");
                Console.Write("Row = ");
                rowUser = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            
            return rowUser;
        }


        private static int GetColFromUsers(char[,] board)
        {
            int col;
            
            Console.Write("Column = ");
            bool isNumberY = int.TryParse(Console.ReadLine(), out col);

            while (!isNumberY || col < 0 || col > board.GetLength(1))
            {
                Console.WriteLine($"The input to the column selection must be a number between 0-{board.GetLength(1) - 1}. Please reenter");
                Console.Write("Column = ");
                isNumberY = int.TryParse(Console.ReadLine(), out col);
            }

            return col;
        }


        /* Position Checking 
         * getting starting position (row,column) for the ship
         * return the values */
        private static Position GetPositionUser(char[,] board)
        {
            Console.WriteLine($"Enter starting position: row- a letter (A-{(char)(board.GetLength(0) + 64)}), and column- a number (0-{board.GetLength(1) - 1}) like shown in your board.");
            
            int row = GetRowFromUser(board);
            // convert the char from the Fn to a number in the bounds of the array
            if (row > 'Z')
            {// small
                row -= 97;
            }
            else
            {// capital
                row -= 65;
            }

            int col = GetColFromUsers(board);

            Position usrPos = new Position(row, col);
            return usrPos;
        }
        // GetPositionUser END //


        /* Direction Checking (valid input)
         * getting the direction from the user for the ship placement
         * h- horizontal
         * v- vertival
         * return true- horizontal , false- vertical */
        private static bool GetDirectionUser()
        {
            Console.WriteLine("Do you want the ship to be horizontally or vertically?");
            Console.WriteLine("h - Horizontally");
            Console.WriteLine("v - Vertically");

            // input one character only
            Console.Write("Direction: ");
            char usrDirection = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // checking if the char is valid, if not tells the user to reenter
            while (usrDirection != 'h' && usrDirection != 'v')
            {
                Console.WriteLine("Input is not the letter 'h' or 'v'. Please reenter");
                Console.Write("Direction: ");
                usrDirection = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }

            // applying the chosen direction (char) to a variable and return it.
            bool horizontal = usrDirection == 'h';
            return horizontal;
        }
        // GetDirectionUser END //
        

        /* Out of board boundaries checking
         * If the planned ship is devating the boundaries of the board
         * return True or False */
        private static bool BoundariesCheck(char[,] board, Position posShip, bool horizontal, int length)
        {
            bool isOutOfBoard = true;
            Position tempPos = new Position(posShip.Row, posShip.Col);

            // boundaries check
            if (horizontal) // for horizontal ship
            {
                if (board.GetLength(1) - posShip.Col < length)
                {
                    return isOutOfBoard = false;
                }
            }
            else // for vertical ship
            {
                if (board.GetLength(0) - posShip.Row < length)
                {
                    return isOutOfBoard = false;
                }
            }

            // restore position and return result
            posShip.CopyAttributes(tempPos);
            return isOutOfBoard;
        }
        // IsOutOfBoard END //


        /* Board occupation checking
         * If is another ship occupying new ship's slots
         * return True or False - if passed the occupation check */
        private static bool OccupationCheck(char[,] board, Position posShip, bool horizontal, int length)
        {
            bool isOccupied = true;
            Position tempPos = new Position(posShip.Row, posShip.Col);


            // occupation check
            for (int i = 0; i < length; i++)
            {
                isOccupied = board[posShip.Row, posShip.Col] == 0;
                if (isOccupied)
                {
                    if (horizontal) // for horizontal ship
                    {
                        posShip.Col++;
                    } // for vertical ship
                    else posShip.Row++;
                }
                else
                    break;
            }


            // restore position and return result
            posShip.CopyAttributes(tempPos);
            return isOccupied;
        }
        // IsOccupied END //


        /* Creates a ship for the user */
        public static Ship UserCreate(char[,] board, int length)
        {
            // Declaring the "building permits"
            bool buildable = false;
            bool boundariesCheck;
            bool occupationCheck = true;


            // Recieve input - starting Position and Direction
            Console.WriteLine($"Let's create a new ship in the length of {length}");
            // Position Get
            Position usrPos = GetPositionUser(board);
            // Direction Get
            bool horizontal = GetDirectionUser();

            // run boundaries check first and if passed - run occupation check
            // because if out of boundaries the occupation check will blow the program cause it using the array.
            boundariesCheck = BoundariesCheck(board, usrPos, horizontal, length);
            if (boundariesCheck)
            {
                occupationCheck = OccupationCheck(board, usrPos, horizontal, length);
                if (occupationCheck) buildable = true;
            }
            
            // If not buildable (the ship doesn't pass the boundaries or occupation check) - ask to rebuild
            while (!buildable)
            {
                // explanation messages why the ship can not be built.
                if (!boundariesCheck) // because of out of boundaries
                {
                    Console.WriteLine("The values you entered cannot build a ship because it out of board boundaries. Please enter new values for the ship.");
                }
                if (!occupationCheck) // because of other ship occupying the place(s)
                {
                    Console.WriteLine("The values you entered cannot build a ship because another ship is occupying some slots. Please enter new values for the ship.");
                }

                usrPos = GetPositionUser(board);
                horizontal = GetDirectionUser();

                // building checks with new values
                boundariesCheck = BoundariesCheck(board, usrPos, horizontal, length);
                if (boundariesCheck)
                {
                    occupationCheck = OccupationCheck(board, usrPos, horizontal, length);
                    if (occupationCheck) buildable = true;
                }
            }

            // create the ship and mark it in the board
            Ship shipCreated = new Ship(usrPos, horizontal, length);

            if (horizontal)
            {
                for (int i = 0; i < length; i++)
                {
                    board[usrPos.Row, usrPos.Col] = (char)(shipCreated.Id + 48);
                    usrPos.Col++;
                }
            }
            else {
                for (int i = 0; i < length; i++)
                {
                    board[usrPos.Row, usrPos.Col] = (char)(shipCreated.Id + 48);
                    usrPos.Row++;
                }
            }


            // successful msg and return the created ship.
            Console.WriteLine("Ship created successfuly!");
            Console.WriteLine();
            return shipCreated;
        }
        // UserCreate END //




        /* ==== CPU ==== */

        /* Creates a ship for the CPU 
         * returns the new ship that created */
        public static Ship CpuCreate(char[,] board, int length)
        {
            // Declaring the "building permits"
            bool buildable = false;
            bool boundariesCheck;
            bool occupationCheck = true;


            // Position Set
            Position cpuPos = new Position(Program.GenerateRandInt(0, board.GetLength(0) - 1), Program.GenerateRandInt(0, board.GetLength(1) - 1));
            
            // Direction Set
            bool horizontal = Program.GenerateRandBool();
            Console.WriteLine(cpuPos.ToString() + " " + horizontal);

            // run boundaries check first and if passed - run occupation check
            // because if out of boundaries the occupation check will blow the program cause it using the array.
            boundariesCheck = BoundariesCheck(board, cpuPos, horizontal, length);
            if (boundariesCheck)
            {
                occupationCheck = OccupationCheck(board, cpuPos, horizontal, length);
                if (occupationCheck) 
                    buildable = true;
            }

            // If not buildable (the ship doesn't pass the boundaries or occupation check) - rebuild
            while (!buildable)
            {
                cpuPos = new Position(Program.GenerateRandInt(0, board.GetLength(0)), Program.GenerateRandInt(0, board.GetLength(1)));                
                horizontal = Program.GenerateRandBool();

                // building checks with new values
                boundariesCheck = BoundariesCheck(board, cpuPos, horizontal, length);
                if (boundariesCheck)
                {
                    occupationCheck = OccupationCheck(board, cpuPos, horizontal, length);
                    if (occupationCheck) buildable = true;
                }
            }

            // create the ship and mark it in the board
            Ship shipCreated = new Ship(cpuPos, horizontal, length);

            if (horizontal)
            {
                for (int i = 0; i < length; i++)
                {
                    board[cpuPos.Row, cpuPos.Col] = (char)(shipCreated.Id + 48);
                    cpuPos.Col++;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    board[cpuPos.Row, cpuPos.Col] = (char)(shipCreated.Id + 48);
                    cpuPos.Row++;
                }
            }

            return shipCreated;
        }
        // CpuCreate END //





















    }
}
