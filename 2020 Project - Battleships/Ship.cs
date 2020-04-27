using System;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Ship
    {
        private static int NextID = 0;                          // This responsable for set the Id property and increase by 1 every time
        public int Id { get; set; }                             // The identity number of the ships. Each ship has a different ID number
        public Position StartingPos { get; set; }               // Contains the starting position of the ship
        private bool Horizontal { get; set; }                   // Decides whether or not the ship will be horizontally (true) or vertically (false)
        public int Length { get; set; }                         // Contains the length of the ship - how many slots it will occupy from the starting position
        public int RemainParts { get; set; }                    // Contains how many parts remain to the ship
        public bool IsSank { get; set; }                        // Contains if the ship is steel "alive" - if it has at least 1 remaining slot


        // constructor
        public Ship(Position startingPos, bool horizontal, int length)
        {
            Id = NextID++;
            StartingPos = startingPos;
            Horizontal = horizontal;
            Length = length;
            RemainParts = length;
            IsSank = false;
        }



        /* ==== User ==== */

        /* COMMENT NEEDED */
        public static char GetRowFromUser(char[,] board)
        {
            // Get input
            FGcolor(DarkGray);
            Console.Write("Row = ");
            FGcolor(White);
            char rowUser = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // while the input not valid (a letter in the boundaries of the board)
            while ((rowUser < 'A' || rowUser > board.GetLength(0) + 64) && (rowUser < 'a' || rowUser > board.GetLength(0) + 96))
            {
                ErrorSymbol();
                Console.WriteLine($"The input to the row selection must be a letter (small or capital) between A to {(char)(board.GetLength(0) + 64)}. Please reenter");

                FGcolor(DarkGray);
                Console.Write("Row = ");
                FGcolor(White);
                rowUser = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }

            FGcolor(Gray);
            return rowUser;
        }


        /* COMMENT NEEDED */
        public static int GetColumnFromUsers(char[,] board)
        {
            int col;

            FGcolor(DarkGray);
            Console.Write("Column = ");
            FGcolor(White);
            bool isNumberCol = int.TryParse(Console.ReadLine(), out col);

            // Wrong input
            while (!isNumberCol || col < 0 || col > board.GetLength(1) - 1)
            {
                ErrorSymbol();
                Console.WriteLine($"The input to the column selection must be a number between 0 to {board.GetLength(1) - 1}. Please reenter");
                FGcolor(DarkGray);
                Console.Write("Column = ");
                FGcolor(White);
                isNumberCol = int.TryParse(Console.ReadLine(), out col);
            }

            FGcolor(Gray);
            return col;
        }


        /* Position Checking 
         * getting starting position (row,column) for the ship
         * return the values */
        private static Position GetPositionUser(char[,] board)
        {
            FGcolor(Gray);
            Console.WriteLine($"Enter starting position: row- a letter (A-{(char)(board.GetLength(0) + 64)}), and column- a number (0-{board.GetLength(1) - 1}) like shown in your board.");
            
            // Get Row
            int row = GetRowFromUser(board);
            row = LetterToNumber(row);

            // Get Column
            int col = GetColumnFromUsers(board);

            // Create the position object
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
            // Message the user
            FGcolor(DarkCyan);
            Console.WriteLine("Do you want the ship to be horizontally or vertically?");
            FGcolor(Gray);
            Console.WriteLine("h - Horizontally");
            Console.WriteLine("v - Vertically");

            // input one character only
            char userDirection = GetDirectionInput();

            // Input Validation
            while (userDirection != 'h' && userDirection != 'v')
            {
                ErrorSymbol();
                Console.WriteLine("Input is not the letter 'h' or 'v'. Please reenter");
                userDirection = GetDirectionInput();
            }

            // applying the chosen direction (char) to a variable and return it.
            bool horizontal = userDirection == 'h';
            return horizontal;
        }
        // GetDirectionUser END //

        private static char GetDirectionInput()
        {
            FGcolor(DarkGray);
            Console.Write("Direction: ");

            FGcolor(White);
            char userDirectionInput = Console.ReadKey().KeyChar;
            Console.WriteLine();

            return userDirectionInput;
        }


        /* Out of board boundaries checking
         * If the planned ship is devating the boundaries of the board
         * return True or False - if passed the Boundaries Check 
         */
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
                isOccupied = board[posShip.Row, posShip.Col] == 0; // check if the slot contains null
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
            // Title
            FGcolor(DarkCyan);
            Console.WriteLine($"Let's create a new ship in the length of {length}");

            // Get Position & Direction
            Position shipStartingPos = GetPositionUser(board);
            Console.WriteLine();
            bool horizontal = GetDirectionUser();

            // Validation for the values from the user
            Tuple<bool, bool, bool> buildingResults = IsBuildable(board, shipStartingPos, horizontal, length);
            bool boundariesCheck = buildingResults.Item1;
            bool occupationCheck = buildingResults.Item2;
            bool buildable = buildingResults.Item3;

            // If not buildable (the ship doesn't pass the boundaries or occupation check) - ask to rebuild
            while (!buildable)
            {
                CreationErrorMsgs(boundariesCheck, occupationCheck);

                // Get new position and direction values
                shipStartingPos = GetPositionUser(board);
                horizontal = GetDirectionUser();

                // building checks with new values
                Tuple<bool, bool, bool> newBuildingResults = IsBuildable(board, shipStartingPos, horizontal, length);
                boundariesCheck = newBuildingResults.Item1;
                occupationCheck = newBuildingResults.Item2;
                buildable = newBuildingResults.Item3;
            }

            // create the ship and mark it in the board
            Ship shipCreated = new Ship(shipStartingPos, horizontal, length);
            CreateInArray(board, shipCreated, shipStartingPos, horizontal, length);
            
            Console.WriteLine();

            // successful msg and return the created ship.
            SystemMsg();
            Console.WriteLine("Ship created successfuly!");
            Console.WriteLine();
            return shipCreated;
        }
        // UserCreate END //


        /* Item1: boundariesCheck, Item2: occupationCheck, Item3: buildable */
        private static Tuple<bool, bool, bool> IsBuildable(char[,] board, Position shipStartingPos, bool horizontal, int length)
        {
            bool occupationCheck = true;
            bool boundariesCheck = BoundariesCheck(board, shipStartingPos, horizontal, length);


            // run boundaries check first and if passed - run occupation check
            // because if out of boundaries the occupation check will crush the program cause it using the array.
            if (boundariesCheck)
            {
                occupationCheck = OccupationCheck(board, shipStartingPos, horizontal, length);
                if (occupationCheck)
                    return Tuple.Create(boundariesCheck, occupationCheck, true);
            }

            return Tuple.Create(boundariesCheck, occupationCheck, false);
        }

        private static void CreationErrorMsgs(bool boundariesCheck, bool occupationCheck)
        {
            // explanation messages why the ship can not be built.
            if (!boundariesCheck) // out of boundaries
            {
                ErrorSymbol();
                Console.WriteLine("The values you entered cannot build a ship because it out of board boundaries. Please enter new values for the ship.");
                Console.WriteLine();
            }
            if (!occupationCheck) // other ship occupying slot(s)
            {
                ErrorSymbol();
                Console.WriteLine("The values you entered cannot build a ship because another ship is occupying some slots. Please enter new values for the ship.");
                Console.WriteLine();
            }
        }


        private static void CreateInArray(char[,] board, Ship shipCreated, Position startingPos, bool horizontal, int length)
        {
            if (horizontal)
            {
                for (int i = 0; i < length; i++)
                {
                    board[startingPos.Row, startingPos.Col] = (char)(shipCreated.Id + '0');
                    startingPos.Col++;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    board[startingPos.Row, startingPos.Col] = (char)(shipCreated.Id + '0');
                    startingPos.Row++;
                }
            }
        }




        /* ==== CPU ==== */

        /* Creates a ship for the CPU 
         * returns the new ship that created */
        public static Ship CpuCreate(char[,] board, int length)
        {
            // Declaring the "building permits"
            bool buildable = false;
            bool horizontal = false; // Direction Set (temp value)
            Position startingPos = new Position(0, 0); // Position Set (temp values)
                       

            while (!buildable)
            {
                startingPos = new Position(GenerateRandInt(0, board.GetLength(0) - 1), GenerateRandInt(0, board.GetLength(1) - 1));
                horizontal = GenerateRandBool();

                Tuple<bool, bool, bool> buildingResults = IsBuildable(board, startingPos, horizontal, length);
                buildable = buildingResults.Item3;
            }

            // create the ship and mark it in the board
            Ship shipCreated = new Ship(startingPos, horizontal, length);
            CreateInArray(board, shipCreated, startingPos, horizontal, length);

            return shipCreated;
        }
        // CpuCreate END //







    }
}
