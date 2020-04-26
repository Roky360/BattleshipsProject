using System;
using System.Threading;
using static _2020_Project___Battleships.Player;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Game
    {
        public static Player[] Players { get; set; }                // The list of the players of the game (user, CPU)
        public static Position BoardSize = new Position(10, 10);    // The size of the players' game board (default value: 10 by 10)


        // constructor
        public Game(string userName)
        {
            // Run the Main Menu for the player and set the chosen board size for both players
            if (userName != CpuName)
                BoardSize = MainMenu();

            Players = new Player[2];
            Players[0] = new Player(userName);
            Players[1] = new Player(CpuName);
        }



        /* ==== General Functions ==== */

        /* - MainMenu - */
        public static Position MainMenu()
        {
            bool done = false;
            Position boardSize = new Position(10, 10);


            while (!done)
            {
                Console.Clear();
                MenuPrint();

                char ans = Console.ReadKey().KeyChar;


                switch (ans)
                {
                    // Start Game
                    case '1': 
                        done = true;
                        Console.Clear();
                        break;

                    // Board size
                    case '2':
                        boardSize = BoardSizeSet();
                        break;

                    // Read Instructions
                    case '3':
                        Console.Clear();
                        Program.Instructions();
                        Console.Clear();
                        break;

                    default:
                        break;
                }//switch
            }//while

            return boardSize;
        }
        // MainMenu END //


        private static void MenuPrint()
        {
            const int linesLength = 30;


            FGcolor(White);
            AlignedText("Main Menu", 31);
            AlignedText("---------", 31);

            CrossLine(linesLength);
            Console.WriteLine("1- Start Game");
            CrossLine(linesLength);
            Console.WriteLine("2- Change Board Size");
            CrossLine(linesLength);
            Console.WriteLine("3- Read Instructions");
            CrossLine(linesLength);
            
            Console.WriteLine();

            FGcolor(DarkGray);
            Console.Write("Please type the desired option ");
        }


        private static Position BoardSizeSet()
        {
            int size = 10;
            bool isNumber = false;
            Position boardSize = new Position(10, 10);


            while (!isNumber || size < 5 || size > 10)
            {
                Console.Clear();
                FGcolor(White);
                Console.WriteLine("Please enter the new size for row and column of the board, between 5-10 (10 is the recomended size):");
                InputLine(4);
                isNumber = int.TryParse(Console.ReadLine(), out size);
                BGcolor(Black);
            }
            boardSize.Row = boardSize.Col = size;

            Console.WriteLine();
            SystemMsg();
            Console.WriteLine("Board size changed successfuly!");
            Thread.Sleep(2000);
            Console.Clear();

            return boardSize;
        }


        /* - Start Game -
         ~ Description: The function that runs the whole game.
         ~ after setting the players, this func manages the turn system, the shooting system and the win condition
         ~ at the end of the game it asks the user if restart and return the answer to the main program. 
         > RETURNS: bool - whether or not to restart the game (asks the player at the end of the game).
         */
        public static bool StartGame()
        {

            while (true)
            {
                UserTurn();
                IsSankCheck();
                if (WinCondition())
                    break;

                CpuTurn();
                IsSankCheck();
                if (WinCondition())
                    break;                
            }

            return AskRestart();
        }
        // StartGame END //


        /* COMMENT NEEDED */
        public static void IsSankCheck()
        {
            // p runs on the Players array
            // i runs on the Ships array of each player
            foreach (Player p in Players)
            {
                for (int i = 0; i < p.Ships.Length; i++)
                {
                    p.Ships[i].IsSank = p.Ships[i].RemainParts == 0;
                }
            }
        }


        /* COMMENT NEEDED */
        public static bool WinCondition()
        {
            bool cpuWinCondition = true;
            bool playerWinCondition = true;


            for (int i = 0; i < Players[0].Ships.Length; i++)
            {
                if (Players[0].Ships[i].IsSank == false)
                {
                    cpuWinCondition = false;
                    break;
                }
            }
            if (cpuWinCondition)
            {
                FGcolor(DarkCyan);  Console.Write("The CPU has sunk all of your ships ! ");
                FGcolor(Red);       Console.Write("You lost");
                Console.WriteLine();
                return true;
            }

            for (int i = 0; i < Players[1].Ships.Length; i++)
            {
                if (Players[1].Ships[i].IsSank == false)
                {
                    playerWinCondition = false;
                    break;
                }
            }
            if (playerWinCondition)
            {
                FGcolor(DarkCyan);  Console.WriteLine("You have sunken all the CPU's ships. ");
                FGcolor(Green);     Console.WriteLine("Congratulations, you won !");
                Console.WriteLine();
                return true;
            }

            return false;
        }


        /* COMMENT NEEDED */
        public static bool AskRestart()
        {
            // asks the user if restart
            FGcolor(White);     Console.Write("Restart? ");
            FGcolor(DarkGray);  Console.WriteLine("[y/n]");
            InputLine(1);
            char restartAns = Console.ReadKey().KeyChar;
            BGcolor(Black);
            Console.WriteLine();

            // checking if the char is valid, if not tells the user to reenter
            while (restartAns != 'y' && restartAns != 'n')
            {                
                ErrorSymbol();
                Console.WriteLine("The possible inputs are 'y'(yes) or 'n'(no). Please reenter");
                FGcolor(DarkGray);
                Console.WriteLine("[y/n] ");
                InputLine(1);
                restartAns = Console.ReadKey().KeyChar;
                BGcolor(Black);
                Console.WriteLine();
            }

            // return user answer
            if (restartAns == 'y')
            {
                return true;
            }
            else return false;
        }
        // AskRestart END //


        /* - Hit Try -
         ~ Description: Used by the FreeShot and SearchShot functions as the actual attempt to hit a spot on the board, after they checked that the hitting spot is valid.
         * Logic: The function getting the position to hit as a parameter, and other needed things such as the board and the ships array.
         * The Fn checks if the hitted spot contains an opponent ship, then replaces the spot with the agreed mark for "hitted" - 'x'.
         * If  not, it marks the spot for "missed" - '/'.
         > RETURNS: Boolean. If succeed to hit a ship, return that the attempt was successful (true), if not return that the attempt was failed (false)
         */
        public static bool HitTry(Position hitCords, bool isPlayer)
        {
            Player plyr = isPlayer ? Players[1] : Players[0]; //get the player data
            char[,] hitBoard = plyr.GameBoard.ArrayBoard; //the player's board
            int row = hitCords.Row;
            int col = hitCords.Col;
            Position lastHitCords = isPlayer ? Players[0].LastHitCords : Players[1].LastHitCords;
            Ship[] shipsArray = plyr.Ships;


            /* checks if any ship has got hit
             * YES: reduces the Remain Parts attribute by 1 (-1)
             *      sets the slot in the array to the agreed mark of "hitted" - x
             *      sets the hitCondition variable to 4 (for the search shot to act next turn)
             * NO:  sets the slot in the array to the agreed mark of "missed" - /
             */
            if (hitBoard[row, col] >= '0' && hitBoard[row, col] <= '9')
            {
                if (isPlayer)
                {
                    shipsArray[hitBoard[row, col] - 53].RemainParts--;
                }
                else
                {
                    shipsArray[hitBoard[row, col] - 48].RemainParts--;
                }
                hitBoard[row, col] = 'x';
                lastHitCords.CopyAttributes(hitCords);
                return true;
            }
            // missed
            hitBoard[row, col] = '/';
            return false;
        }
        // HitTry END //



        /* ==== CPU ==== */

        /* - CPU Turn -  COMMENT NEEDED
         * 
         */
        public static void CpuTurn()
        {
            int hitCondition = Players[1].HitCondition;
            bool successfulHit;


            // Decides wich kind of shot to execute based on the hitCondition
            if (hitCondition > 0)
            { // Search Shot
                successfulHit = SearchShot();
                if (Players[1].HitCondition == 0)
                {
                    successfulHit = FreeShot();
                }
                else if (hitCondition == -1)
                {
                    hitCondition = 0;
                }
            }
            else // hitCondition > 0 => Free Shot
            {
                successfulHit = FreeShot();
            }

            // Display the results to the user
            FGcolor(White);
            Console.WriteLine("CPU's Turn");
            HyphenUnderline(length: 10);
            Console.WriteLine();

            if (successfulHit)
            {
                Players[0].GameBoard.PrintBoard();
                FGcolor(Gray);
                Console.WriteLine($"The CPU shot at {Players[1].LastHitColor.ToString()} and hit one of your ships!");
            }
            else
            {
                Players[0].GameBoard.PrintBoard();
                FGcolor(Gray);
                Console.WriteLine($"The CPU shot at {Players[1].LastHitColor.ToString()} and missed!");
            }
            Console.WriteLine();


            FGcolor(DarkGray);
            Console.WriteLine("Press ENTER to continue");
            Console.ReadKey();
            Console.Clear();
        }
        // CpuTurn END //


        /* - Free Shot - COMMENT NEEDED
         * 
         */
        public static bool FreeShot()
        {
            Player plyr = Players[0];
            char[,] hitBoard = plyr.GameBoard.ArrayBoard;
            // random hitting position
            Position hitCords = new Position(GenerateRandInt(0, hitBoard.GetLength(0) - 1), GenerateRandInt(0, hitBoard.GetLength(0) - 1));


            // if the spot is already been hit, regenerage spot.
            while (hitBoard[hitCords.Row, hitCords.Col] == 'x' || hitBoard[hitCords.Row, hitCords.Col] == '/')
            {
                hitCords.Col = GenerateRandInt(0, hitBoard.GetLength(0) - 1);
                hitCords.Row = GenerateRandInt(0, hitBoard.GetLength(0) - 1);
            }

            if (HitTry(hitCords, false))
            {
                Players[1].HitCondition = 4;
                Players[1].DestructionCount = 1;
                Players[1].LastHitColor.CopyAttributes(hitCords);
                return true;
            }
            Players[1].LastHitColor.CopyAttributes(hitCords);
            return false;
        }
        // FreeShot END //


        /* - Search Shot -
         ~ Description: This function will be used if the CPU hits a ship with the free shot. 
         ~ Instead of continuing the random attempts, the CPU searches the other parts of the ship he it hit with this function,
         ~ that searches the other ship parts in the 4 directions around the hit spot. 
         ~ That way it will quickly find the other parts and destroy the ship in minimum turns.
         * Logic: Operates with one switch that decides in which direction the CPU will try to hit, based on the hitCondition variable.
         * Each value of the hit condition (1-4) represents a direction (written inside the switch).
         * In each case the Fn 'moves' the hitCords around the hit spot according to the direction, and tries to strike there.
         * If succeeded, the next turn it will try in this in the same direction; 
         * If not, it will decrease the hitCondition property so the next turn it will try another direction.
         * SUCCESSFUL SHOT - Marks 'x' in the array board.
         * MISSED SHOT - Marks '/' in the array board.
         > RETURNS: Nothing.
         */
        public static bool SearchShot()
        {
            // Defining the needed variables
            int hitCondition = Players[1].HitCondition;
            Position lastHitCords = new Position(Players[1].LastHitCords.Row, Players[1].LastHitCords.Col); //create the object for the last hit position
            Position tempCords = new Position(lastHitCords.Row, lastHitCords.Col);
            int destructionCount = Players[1].DestructionCount;
            char[,] hitBoard = Players[0].GameBoard.ArrayBoard;
            bool successHit = false;
            bool isPlayed = false;
            

            /* This while loop makes sure that if the attempt to hit was failed because the cords were out of the boundaries or already hit,
             * it will decrease the hitCondition, wich means it will try another direction instead of skip and lose the turn. */
            while (!isPlayed)
            {
                switch (hitCondition) // check in which direction to move
                {
                    case 4:
                        lastHitCords.Row--;
                        break;
                    case 3:
                        lastHitCords.Col++;
                        break;
                    case 2:
                        lastHitCords.Row++;
                        break;
                    case 1:
                        lastHitCords.Col--;
                        break;       
                }
                if (hitCondition == 0) // if hitCondition is 0 - exit the search shot.
                    break;

                if (lastHitCords.Row >= 0 && lastHitCords.Row <= hitBoard.GetLength(0) - 1 && lastHitCords.Col >= 0 && lastHitCords.Col <= hitBoard.GetLength(1) - 1) // boundaries check
                {
                    switch (hitBoard[lastHitCords.Row, lastHitCords.Col]) // check if the spot is occupied
                    {   // if occupied, try another direction
                        case 'x':
                        case '/':
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords);
                            break;

                        default: // if not occupied, try to hit
                            if (HitTry(lastHitCords, false)) // if succeeded, wil save the cords for the next turn
                            {
                                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                                destructionCount++;
                                successHit = true;
                            }
                            else // if failed, will check if it in a middle of destruction of a ship, if it is- will exit it and return to free shot; if not, will decreace the hitCondition and will try different direction
                            {
                                if (destructionCount > 1)
                                {
                                    hitCondition = -1;
                                    destructionCount = 0;
                                }
                                else
                                {
                                    hitCondition--;
                                }
                            }
                            isPlayed = true; // mark as a played turn to exit the loop and pass the turn
                            break;
                    }
                }
                else //if out of boundaries
                {
                    hitCondition--;
                    lastHitCords.CopyAttributes(tempCords); // restore the position
                }                               
            }//while end   
            

            // Apply the values from this Fn to the CPU's attributes
            Players[1].HitCondition = hitCondition;
            Players[1].DestructionCount = destructionCount;
            Players[1].LastHitColor.CopyAttributes(lastHitCords);
            if (!successHit)
            { // if failed the shot, restore the hit cords from the tempCords and return false(- shot failed)
                Players[1].LastHitCords.CopyAttributes(tempCords);
                return false;
            }
            return true;
        }
        // SearchShot END //




        /* ==== User ==== */

        public static void UserTurn()
        {
            Position hitPos = new Position(0, 0);
            char[,] CPUboard = Players[1].GameBoard.ArrayBoard;


            // Turn title print
            string titleTxt = $"{Players[0].Name}'s Turn";
            FGcolor(White);
            Console.WriteLine(titleTxt);
            HyphenUnderline(titleTxt.Length);
            Console.WriteLine();

            Players[1].GameBoard.PrintBoard();
            FGcolor(DarkCyan); Console.WriteLine("Please enter hitting cords:");

            FGcolor(Red);
            hitPos.Row = Ship.GetRowFromUser(CPUboard);
            hitPos.Row = LetterToNumber(hitPos.Row);

            hitPos.Col = Ship.GetColumnFromUsers(CPUboard);

            // same hit spot alert
            while (CPUboard[hitPos.Row, hitPos.Col] == 'x' || CPUboard[hitPos.Row, hitPos.Col] == '/')
            {
                ErrorSymbol();
                Console.WriteLine("You already tried to hit in that spot. Please reenter");

                hitPos.Row = Ship.GetRowFromUser(CPUboard);
                hitPos.Row = LetterToNumber(hitPos.Row);

                hitPos.Col = Ship.GetColumnFromUsers(CPUboard);                
            }            

            Console.Clear();

            if (HitTry(hitPos, true))
            {
                Players[0].LastHitColor.CopyAttributes(hitPos);
                Players[1].GameBoard.PrintBoard();
                Console.WriteLine("You hit a CPU's ship!");
            }
            else
            {
                Players[0].LastHitColor.CopyAttributes(hitPos);
                Players[1].GameBoard.PrintBoard();
                Console.WriteLine("You missed!");
            }

            Console.WriteLine();
            FGcolor(DarkGray);
            Console.WriteLine("Press ENTER to end the turn");
            Console.ReadKey();
            Console.Clear();            
        }
        

    }
}
