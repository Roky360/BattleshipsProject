using System;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Ship
    {
        public static int NextID = 0;                           // This responsable for set the Id property and increase by 1 every time
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

        /* - User Create - 
        ~ Description: The main function that creates a ship for teh user.
        ~ Manages the whole process and calls other functions to get the needed inputs and run the needed checks.
        > Return: Ship. the created ship as a Ship object.
        * */
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


        /* - Get Position User - 
         ~ Description: The main function for getting the starting position for the ships.
         ~ It calls the GetRow & GetColumn functions to get the row&column values.
         > Return: Position. the final position coordinates as a Position object.
         * */
        private static Position GetPositionUser(char[,] board)
        {
            FGcolor(Gray);
            Console.WriteLine($"Enter starting position: row- a letter (A-{(char)(board.GetLength(0) + 64)}), and column- a number (0-{board.GetLength(1) - 1}) like shown in your board.");

            // Get Row
            int row = GetRowFromUser(board);
            row = LetterToNumber(row);

            // Get Column
            int col = GetColumnFromUser(board);

            // Create the position object
            Position usrPos = new Position(row, col);
            return usrPos;
        }
        // GetPositionUser END //


        /* - Get Row From User - 
         ~ Description: Gets the row selection (a letter) from the user with an input check.
         > Return: char. the row selection.
         * */
        public static char GetRowFromUser(char[,] board)
        {
            // Get input
            char rowUser = RowInput();

            // while the input not valid (a letter in the boundaries of the board)
            while ((rowUser < 'A' || rowUser > board.GetLength(0) + 64) && (rowUser < 'a' || rowUser > board.GetLength(0) + 96))
            {
                ErrorSymbol();
                Console.WriteLine($"The input to the row selection must be a letter (small or capital) between A to {(char)(board.GetLength(0) + 64)}. Please reenter");

                rowUser = RowInput();
            }

            FGcolor(Gray);
            return rowUser;
        }
        // GetRowFromUser END //

        /* - Row Input - 
         ~ Description: Gets the actuall input and checks if it's valid.
         > Return: char. the row selection (the letter) as a char.
         * */
        private static char RowInput()
        {
            FGcolor(DarkGray);
            Console.Write("Row = ");
            FGcolor(White);

            char rowUser = Console.ReadKey().KeyChar;
            Console.WriteLine();

            return rowUser;
        }
        // RowInput END //


        /* - Get Column From User - 
         ~ Description: Gets the column selection (a number) from the user with an input check.
         > Return: int. the column selection.
         * */
        public static int GetColumnFromUser(char[,] board)
        {
            Tuple<bool, int> colInputResults = ColumnInput();
            bool isNumberCol = colInputResults.Item1;
            int col = colInputResults.Item2;

            // Wrong input
            while (!isNumberCol || col < 0 || col > board.GetLength(1) - 1)
            {
                ErrorSymbol();
                Console.WriteLine($"The input to the column selection must be a number between 0 to {board.GetLength(1) - 1}. Please reenter");

                colInputResults = ColumnInput();
                isNumberCol = colInputResults.Item1;
                col = colInputResults.Item2;
            }

            FGcolor(Gray);
            return col;
        }
        // GetColumnFromUser END //

        /* - Column Input - 
         ~ Description: Gets the actuall input and checks if it's valid.
         > Return: bool, int. if the input is int, the input.
         * */
        private static Tuple<bool, int> ColumnInput()
        {
            FGcolor(DarkGray);
            Console.Write("Column = ");
            FGcolor(White);

            bool isNumberCol = int.TryParse(Console.ReadLine(), out int col);

            return Tuple.Create(isNumberCol, col);
        }
        // ColumnInput END //


        /* - Get Direction User -
         ~ Description: Direction Checking (valid input)
         ~ getting the direction from the user for the ship placement
         ~ h- horizontal
         ~ v- vertival
         > Return true- horizontal , false- vertical */
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

        /* - Get Direction Input - 
        ~ Description: Gets the actuall input.
        > Return: char. the input.
        * */
        private static char GetDirectionInput()
        {
            FGcolor(DarkGray);
            Console.Write("Direction: ");

            FGcolor(White);
            char userDirectionInput = Console.ReadKey().KeyChar;
            Console.WriteLine();

            return userDirectionInput;
        }
        // GetDirectionInput END //


        /* - Boundaries Check - 
        ~ Description: Checks if the planned ship is devating the boundaries of the board
        > Return: Boolean. True or False - if passed the Boundaries Check.
        * */
        private static bool BoundariesCheck(char[,] board, Position posShip, bool horizontal, int length)
        {
            Position tempPos = new Position(posShip.Row, posShip.Col);

            // boundaries check
            if (horizontal) // for horizontal ship
            {
                if (board.GetLength(1) - posShip.Col < length)
                {
                    return false;
                }
            }
            else // for vertical ship
            {
                if (board.GetLength(0) - posShip.Row < length)
                {
                    return false;
                }
            }

            // restore position and return result
            posShip.CopyAttributes(tempPos);
            return true;
        }
        // BoundariesCheck END //


        /* - Occupation Check - 
        ~ Description: If is another ship occupying new ship's slots
        * Logic: Runs on the planned slots in the array and checks 
        * if there is another ship in those slots.
        > Return: Boolean. True or False - if passed the occupation check
        * */
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
        // OccupationCheck END //
        

        /* - Is Buildable - 
        ~ Description: Checks if a ship is buildable 
        ~ by running the boundaries and occupation checks.
        > Return: bool, bool, bool. the results of the boundariesCheck, occupationCheck, if the ship is buildable.
        > Item1: boundariesCheck, Item2: occupationCheck, Item3: buildable
        * */
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
        // IsBuildable END //

        /* - Creation Error Msgs - 
        ~ Description: Prints to the user the error messages 
        ~ explains why the values he entered can't build a ship 
        ~ (because of out of boundaries and/or another ship occupying slots).
        > Return: void.
        * */
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
        // CreationErrorMsgs END //


        /* - Create In Array - 
        ~ Description: Occupies the ship's slots in the game board array.
        > Return: void.
        * */
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
        // CreateInArray END //



        /* ==== CPU ==== */

        /* - Cpu Create - 
        ~ Description: Creates a ship for the CPU, with random generated values.
        ~ Runs validation checks on the values and then creates the Ship object.
        > Return: Ship. the new ship created as a Ship object.
        * */
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
