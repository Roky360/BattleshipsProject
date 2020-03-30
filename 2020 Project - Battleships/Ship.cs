using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Ship
    {
        private static int NextID = 0;
        public int Id { get; set; }
        public Position StartingPos { get; set; }
        private bool Horizontal { get; set; }
        public int Length { get; set; }


        // constructor
        public Ship(Position startingPos, bool horizontal, int length)
        {
            Id = NextID++;
            StartingPos = startingPos;
            Horizontal = horizontal;
            Length = length;
        }


        /* Position Checking 
         * getting starting position (row,column) for the ship
         * return the values */
        private static Position GetPositionUser()
        {
            Console.WriteLine("Enter starting position (row and then column)");
            
            int row;
            Console.Write("Row = ");
            bool isNumberX = int.TryParse(Console.ReadLine(), out row);
            while (!isNumberX || row < 0 || row > 9)
            {
                Console.WriteLine("Input is not a number or not between 0-9. Please reenter");
                Console.Write("Row = ");
                isNumberX = int.TryParse(Console.ReadLine(), out row);
            }

            int col;
            Console.Write("Column = ");
            bool isNumberY = int.TryParse(Console.ReadLine(), out col);
            while (!isNumberY || col < 0 || col > 9)
            {
                Console.WriteLine("Input is not a number or not between 0-9. Please reenter");
                Console.Write("Column = ");
                isNumberY = int.TryParse(Console.ReadLine(), out col);
            }

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
            Position holderPos = new Position(posShip.Row, posShip.Col);

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
            posShip.Row = holderPos.Row;
            posShip.Col = holderPos.Col;
            return isOutOfBoard;
        }
        // IsOutOfBoard END //


        /* Board occupation checking
         * If is another ship occupying new ship's slots
         * return True or False */
        private static bool OccupationCheck(char[,] board, Position posShip, bool horizontal, int length)
        {
            bool isOccupied = true;
            Position holderPos = new Position(posShip.Row, posShip.Col);


            // occupation check
            if (horizontal) // for horizontal ship
            {
                for (int i = 0; i < length; i++)
                {
                    isOccupied = board[posShip.Row, posShip.Col] == 0;
                    if (!isOccupied) break;
                    posShip.Col++;
                }
            }
            else // for vertical ship
            {
                for (int i = 0; i < length; i++)
                {
                    isOccupied = board[posShip.Row, posShip.Col] == 0;
                    if (!isOccupied) break;
                    posShip.Row++;
                }
            }

            // restore position and return result
            posShip.Row = holderPos.Row;
            posShip.Col = holderPos.Col;
            return isOccupied;
        }
        // IsOccupied END //


        /* Creates a ship for the user */
        public static Ship UserCreate(Board board, int length)
        {
            // Declaring the "building permits"
            bool buildable = false;
            bool boundariesCheck;
            bool occupationCheck = false;


            // Recieve input - starting Position and Direction
            Console.WriteLine($"Let's create a new ship in the length of {length}");
            // Position Get
            Position usrPos = GetPositionUser();
            // Direction Get
            bool horizontal = GetDirectionUser();

            // run boundaries check first and if passed - run occupation check
            // because if out of boundaries the occupation check will blow the program cause it using the array.
            boundariesCheck = BoundariesCheck(board.ArrayBoard, usrPos, horizontal, length);
            if (boundariesCheck)
            {
                occupationCheck = OccupationCheck(board.ArrayBoard, usrPos, horizontal, length);
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

                usrPos = GetPositionUser();
                horizontal = GetDirectionUser();

                // building checks with new values
                boundariesCheck = BoundariesCheck(board.ArrayBoard, usrPos, horizontal, length);
                if (boundariesCheck)
                {
                    occupationCheck = OccupationCheck(board.ArrayBoard, usrPos, horizontal, length);
                    if (occupationCheck) buildable = true;
                }
            }

            // create the ship and mark it in the board
            Ship shipCreated = new Ship(usrPos, horizontal, length);

            if (horizontal)
            {
                for (int i = 0; i < length; i++)
                {
                    board.ArrayBoard[usrPos.Row, usrPos.Col] = (char)(shipCreated.Id + 48);
                    usrPos.Col++;
                }
            }
            else {
                for (int i = 0; i < length; i++)
                {
                    board.ArrayBoard[usrPos.Row, usrPos.Col] = (char)(shipCreated.Id + 48);
                    usrPos.Row++;
                }
            }

            // successful msg and return the created ship.
            Console.WriteLine("Ship created successfuly!");
            Console.WriteLine();
            return shipCreated;
        }
        // UserCreate END //
























    }
}
