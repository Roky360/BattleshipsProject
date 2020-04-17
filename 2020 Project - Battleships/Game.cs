using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Game
    {
        public static Player[] Players { get; set; }            // The list of the players of the game (user, CPU)


        // constructor
        public Game(string usrName)
        {
            Players = new Player[2];
            Players[0] = new Player(usrName);
            Players[1] = new Player("CPU");
        }



        /* ==== General Functions ==== */

        /* - Start Game -
         ~ Description: The function that runs the whole game.
         ~ after setting the players, this func manages the turn system, the shooting system and the win condition
         ~ at the end of the game it asks the user if restart and return the answer to the main program. 
         > RETURNS: bool - whether or not to restart the game (asks the player at the end of the game).
         */
        public static bool StartGame()
        {
            /* \/ for testing \/ */
            for (int i = 0; i < 30; i++)
            {
                CpuTurn();
                Players[0].GameBoard.PrintBoard();
                Console.ReadKey();
                Console.Clear();
            }

            while (true)
            {
                CpuTurn();
                IsSankCheck();
                if (WinCondition())
                    break;

                // userTurn();
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
                Console.WriteLine("The CPU has sunk all of your ships ! You lost");
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
                Console.WriteLine("You have sunken all the CPU's ships. Congratulations, you won !");
                return true;
            }

            return false;
        }


        /* COMMENT NEEDED */
        public static bool AskRestart()
        {
            // asks the user if restart
            Console.Write("Restart? [y/n] ");
            char restartAns = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // checking if the char is valid, if not tells the user to reenter
            while (restartAns != 'y' && restartAns != 'n')
            {
                Console.WriteLine("The possible inputs are 'y'(yes) or 'n'(no). Please reenter");
                Console.Write("[y/n] ");
                restartAns = Console.ReadKey().KeyChar;
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
        public static bool HitTry(Position hitCords)
        {
            // defining variables to the player's attributes for shorter calling
            Player plyr = Players[0]; //get the player data
            char[,] hitBoard = plyr.GameBoard.ArrayBoard; //the player's board
            int row = hitCords.Row;
            int col = hitCords.Col;
            Position lastHitCords = Players[1].LastHitCords;
            Ship[] plyrShips = plyr.Ships;


            /* checks if any ship has got hit
             * YES: reduces the Remain Parts attribute by 1 (-1)
             *      sets the slot in the array to the agreed mark of "hitted" - x
             *      sets the hitCondition variable to 4 (for the search shot to act next turn)
             * NO:  sets the slot in the array to the agreed mark of "missed" - /
             */
            switch (hitBoard[row, col])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                    plyrShips[hitBoard[row, col] - 48].RemainParts--;
                    hitBoard[row, col] = 'x';
                    lastHitCords.CopyAttributes(hitCords);
                    return true;

                default:
                    hitBoard[row, col] = '/';
                    return false;
            }
        }
        // HitTry END //




        /* ==== CPU ==== */

        /* - CPU Turn -  COMMENT NEEDED
         * 
         */
        public static void CpuTurn()
        {
            int hitCondition = Players[1].HitCondition;


            if (hitCondition > 0)
            {
                SearchShot();
                if (Players[1].HitCondition == 0)
                {
                    FreeShot();
                }
                else if (hitCondition == -1)
                {
                    hitCondition = 0;
                }
            }
            else
            {
                FreeShot();
            }
        }
        // CpuTurn END //


        /* - Free Shot - COMMENT NEEDED
         * 
         */
        public static void FreeShot()
        {
            Console.WriteLine("free Shot");
            Player plyr = Players[0];
            char[,] hitBoard = plyr.GameBoard.ArrayBoard;
            // random hitting position
            Position hitCords = new Position(Program.GenerateRandInt(0, hitBoard.GetLength(0) - 1), Program.GenerateRandInt(0, hitBoard.GetLength(0) - 1));


            // if the spot is already been hit, regenerage spot.
            while (hitBoard[hitCords.Row, hitCords.Col] == 'x' || hitBoard[hitCords.Row, hitCords.Col] == '/')
            {
                hitCords.Col = Program.GenerateRandInt(0, hitBoard.GetLength(0) - 1);
                hitCords.Row = Program.GenerateRandInt(0, hitBoard.GetLength(0) - 1);
            }

            if (HitTry(hitCords))
            {
                Players[1].HitCondition = 4;
                Players[1].DestructionCount = 1;
            }
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
        public static void SearchShot()
        {
            Console.WriteLine("free Shot");
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
                {
                    Console.WriteLine("ALERT: HIT CONDITION < 0"); /* << DEBUGGUNG ALERT, REMOVE WHEN NOT NEEDED */
                    break;
                }

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
                            if (HitTry(lastHitCords)) // if succeeded, wil save the cords for the next turn
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
                                //hitCondition = lastHitCords.Row - 2 < 0 || lastHitCords.Col - 2 < 0 ? hitCondition-- : hitBoard[lastHitCords.Row - 2, lastHitCords.Col - 2] == 'x' ? 0 : hitCondition--;
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
            

            // apply the values from this Fn to the main variable in the outside.
            if (!successHit)
            {
                Players[1].LastHitCords.CopyAttributes(tempCords);
            }                        
            Players[1].HitCondition = hitCondition;
            Players[1].DestructionCount = destructionCount;
        }
        // SearchShot END //




        /* ==== User ==== */



    }
}
