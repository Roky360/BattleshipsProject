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

            Ship.NextID = 0;

            Players = new Player[2];
            Players[0] = new Player(userName);
            Players[1] = new Player(CpuName);
        }



        /* ==== Pre Game ==== */

        /* ---- Title Sequence ---- */

        /* - Title Sequence - 
        ~ Description: Manages the titles at the begining of the game
        > Return: void.
        * */
        public static void TitleSequence()
        {
            FGcolor(White);
            Console.WriteLine("Welcome to...");
            Thread.Sleep(1000);
            
            Title();

            FGcolor(Gray);
            Console.WriteLine("We glad that you here playing our game!");
            Console.WriteLine();

            ActionButton(keyToPress: AnyKey, action: "start");
        }
        // TitleSequence END //

        /* - Title - 
        ~ Description: Displays a big title of the game at the begining of the game
        > Return: void.
        * */
        static void Title()
        {
            FGcolor(White);
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────   ");
            FGcolor(Red);
            Console.WriteLine("╔════]     //\\    ═══╦═══ ═══╦═══ ╖     ╔═══  ╒═══╛ ╖     ╓ ╘═╦═╕ ╦═══╗ ╒═══╛  ");
            FGcolor(Magenta);
            Console.WriteLine("║____/    //  \\      ║       ║    ║     ║     \\     ║     ║   ║   ║   ║ \\    ");
            FGcolor(Blue);
            Console.WriteLine("║    \\   //--- \\     ║       ║    ║     ╠══    `═\\  ╠═════╣   ║   ╠═══╝  `═\\");
            FGcolor(Cyan);
            Console.WriteLine("║    |  //      \\    ║       ║    ║     ║         \\ ║     ║   ║   ║         \\");
            FGcolor(Green);
            Console.WriteLine("╚════┘ //        \\   ║       ║    ╚═══╛ ╚═══╛ ╘═══╛ ╜     ╙ ╘═╩═╕ ╜     ╘═══╛  ");
            FGcolor(White);
            Console.WriteLine("──────────────────────────────────────────── By Ron Berkhof & Eden Shaked ───   ");
            Console.WriteLine();
        }
        // Title END //



        /* ---- Username ---- */

        /* - Get User Name - 
        ~ Description: Gets from the user it's user name and returns it.
        > Return: string. the user name.
        * */
        public static string GetUserName()
        {
            string AskName = "What is your name?";

            FGcolor(White);
            Console.WriteLine(AskName);
            InputLine(AskName.Length);

            string usrName = Console.ReadLine();
            BGcolor(Black);

            return usrName;
        }
        // GetUserName END //



        /* ---- Main Menu ---- */

        /* - MainMenu - 
        ~ Description: Controls the main menu.
        * Logic: Operates with a switch that controls the 3 options of the main menu:
        * 1- Start the game
        * 2- Change Board size
        * 3- Read Instructions
        * The switch is inside a while loop that continuing display the menu until the user decides to start.
        > Return: Position. the position object represents the board size for the current game.
        * */
        public static Position MainMenu()
        {
            bool done = false;
            Position boardSize = new Position(10, 10);


            while (!done)
            {
                Console.Clear();
                MenuPrint();

                //char ans = Console.ReadKey(true).KeyChar;
                char ans = MenuInputCheck();

                switch (ans)
                {
                    // Start Game
                    case '1':
                        done = true;
                        Console.Clear();
                        break;

                    // Change Board size
                    case '2':
                        boardSize = BoardSizeSet();
                        break;

                    // Read Instructions
                    case '3':
                        Instructions();
                        break;
                }//switch
            }//while

            return boardSize;
        }
        // MainMenu END //

        /* - Menu Print - 
        ~ Description: Prints the designed text of the main menu.
        > Return: void.
        * */
        private static void MenuPrint()
        {
            const int linesLength = 30;


            FGcolor(White);
            AlignedText("Main Menu", 31);
            AlignedText("─────────", 31);

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
        // MenuPrint END //

        /* - Menu Input Check - 
        ~ Description: Gets the selection for which option to choose fron the main menu.
        * Logic: A while loop that continues running until the user types a valid input (one of the options 1-3).
        > Return: char. the chosen option.
        * */
        private static char MenuInputCheck()
        {
            char ans = '\0';

            while (ans < '1' || ans > '3')
            {
                ans = Console.ReadKey(true).KeyChar;
            }

            return ans;
        }
        // MenuInputCheck END //

        /* - Board Size Set - 
        ~ Description: Asking the user for the new board size for the game.
        * Logic: A while loop that continues running until the user enters a valid board size (6-10).
        ! Disclaimer: We've decided to limit the board size only from 6X6 to 10X10 because we found
        ! those sizes optimal for the performance and the look of the game.
        > Return: Position. the new board size.
        * */
        private static Position BoardSizeSet()
        {
            int size = 10;
            bool isNumber = false;
            Position boardSize = new Position(10, 10);


            while (!isNumber || size < 7 || size > 10)
            {
                Console.Clear();

                FGcolor(White);
                Console.WriteLine("Please enter the new size for row and column of the board, between 7-10 (10 is the recomended size):");
                InputLine(4);

                isNumber = int.TryParse(Console.ReadLine(), out size);
                BGcolor(Black);
            }
            // apply the values
            boardSize.Row = boardSize.Col = size;

            Console.WriteLine();
            SystemMsg();
            Console.WriteLine("Board size changed successfuly!");
            Thread.Sleep(2000);
            Console.Clear();

            return boardSize;
        }
        // BoardSizeSet END //

        /* - Instructions - 
        ~ Description: Displays to the user the rules of the game.
        > Return: void.
        * */
        public static void Instructions() // Explains the game to the user
        {
            Console.Clear();

            FGcolor(White);
            Console.WriteLine("The Rules of the Game");
            HyphenUnderline(length: 21);

            FGcolor(DarkCyan); 
            Console.WriteLine("Pre Game");
            FGcolor(Gray);
            Console.WriteLine("Each player receives a game board and five ships of varying lengths.");
            Console.WriteLine("The ships are marked on the board with numbers from 0 to 4.");
            Console.WriteLine("Before the game starts, each opponent secretly places their own five ships on the game board array.");
            Console.WriteLine("Each ship must be placed horizontally or vertically across array spaces—not diagonally—and the ships can't hang off the array.");
            Console.WriteLine("Ships can touch each other, but they can't occupy the same array space.");
            Console.WriteLine("You cannot change the position of the ships after the game begins.");

            FGcolor(DarkCyan); 
            Console.WriteLine("Game Play");
            FGcolor(Gray);
            Console.WriteLine("Players take turns firing shots (by calling out an array coordinate) to attempt to hit the opponent's enemy ships.");
            Console.WriteLine("On your turn, input a letter and a number that identifies a row and column on your target array.");
            Console.WriteLine("Each shot will be marked on the grid with either 'x' which means your shot hit a target or a colored rectangle which means the shot missed your target.");

            FGcolor(DarkCyan); 
            Console.WriteLine("Winning");
            FGcolor(Gray);
            Console.WriteLine("The first player to sink all five of their opponent's ships wins the game.");

            Console.WriteLine();

            FGcolor(DarkCyan); 
            Console.WriteLine("Thats it, enjoy!");

            Console.WriteLine();

            ActionButton(keyToPress: Enter, action: "return");
        }
        // Instructions END //




        /* ==== Game Play ==== */

        /* - Start Game -
         ~ Description: The function that runs the whole game.
         ~ after setting the players, this func manages the turn system, the shooting system and the win condition
         ~ at the end of the game it asks the user if restart and return the answer to the main program. 
         > Return: bool - whether or not to restart the game (asks the player at the end of the game).
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


        /* ---- Game Management ---- */

        /* - Is Sank Check - 
        ~ Description: Scans the players' ships array and checks after each turn         
        ~ if any ship has sunken, and make the IsSank attribute true.
        > Return: void.
        * */
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
        // IsSankCheck END //


        /* - Win Condition - 
        ~ Description: Checks after each turn if any player has won the game.
        * Logic: Nested loop that runs over the Ships array checking 
        * if all the ships has IsSank - true, and marks the player as "lost" (IsLost = true).
        * The function does it reverse way - at the begining of the loop it declares the player
        * as "lost" and checks if any ship doesn't sank. If there is at least one ship "alive", the player hasn't lost.
        * if all the ship were sank, the IsLost remains true and the function calls the WinMessage function to announce
        * to the user who won.
        > Return: bool. whether or not anyone won. while no one has won the game, the while loop of the turns keeps running.
        * */
        public static bool WinCondition()
        {
            // Checking the IsSank property of each player
            for (int p = 0; p < Players.Length; p++)
            {
                Players[p].IsLost = true;
                for (int i = 0; i < Players[p].Ships.Length; i++)
                {
                    // If at least one ship did not sunk, the player didn't lose
                    if (!Players[p].Ships[i].IsSank)
                    {
                        Players[p].IsLost = false;
                        break;
                    }
                }

                // If anyone wins, print the correct message and return true
                if (Players[p].IsLost)
                {
                    // Pass the function the other player - who won
                    WinMessage(Players[p == 0 ? 1 : 0]);
                    return true;
                }
            }

            return false;
        }
        // WinCondition END //


        /* - Win Message - 
        ~ Description: In case of someone lost all of his ships, 
        ~ this function announces the user who won the game.
        > Return: void.
        * */
        private static void WinMessage(Player p)
        {
            FGcolor(DarkCyan);
            if (p.Name == CpuName)
            {
                Console.Write("The CPU has sunk all of your ships ! ");
                FGcolor(Red);
                Console.Write("You lost");
            }
            else
            {
                Console.WriteLine("You have sunken all the CPU's ships. ");
                FGcolor(Green);
                Console.WriteLine("Congratulations, you won !");
            }
            Console.WriteLine();
        }
        // WinMessage END //


        /* - Ask Restart - 
        ~ Description: After the game ends, 
        ~ this function asks the user if start another game, 
        ~ if not exit the program.
        > Return: bool. the answer - if restart.
        * */
        public static bool AskRestart()
        {
            // asks the user if restart
            FGcolor(White);
            Console.Write("Restart? ");
            FGcolor(DarkGray);
            Console.WriteLine("[y/n]");
            FGcolor(White);
            InputLine(3);

            char restartAns = Console.ReadKey().KeyChar;
            restartAns = RestartInput(restartAns);

            return restartAns == 'y';
        }
        // AskRestart END //

        /* - Restart Input - 
        ~ Description: Checks the input of the user, when the AskRestart function asks him if to restart.
        ~ The expected answers are 'y' (yes) or 'n' (no).
        * Logic: A while loop that continues running while the answer is invalid.
        * In case of invalid answer, the function erases the char the user typed by changing the cursor position.
        > Return: char. the answer.
        * */
        private static char RestartInput(char restartAns)
        {
            while (restartAns != 'y' && restartAns != 'n')
            {
                Console.SetCursorPosition(1, 2);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                restartAns = Console.ReadKey().KeyChar;
            }
            BGcolor(Black);
            Console.WriteLine();

            return restartAns;
        }
        // RestartInput END //


        /* - Credits - 
        ~ Description: The text that displays at the very end of the game, 
        ~ contains credits to the creators and technical information about the whole project.
        > Return: void.
        * */
        public static void Credits()
        {
            Console.WriteLine();
            CrossLine(Length: 54);
            Console.WriteLine();

            Console.WriteLine("Thanks for playing our game! We hope you enjoyed it :)");
            Console.WriteLine();
            FGcolor(DarkGray);
            Console.WriteLine("Made as a 10th grade computer science project");
            Console.WriteLine("Created by Ron Berkhof and Eden Shaked");
            Console.WriteLine("Ort Shapira high school, Kfar-Sava");
            Console.WriteLine("Timestamp: 03.05.2020");
        }
        // Credits END //




        /* ==== Players Functions ==== */

        /* - Turn Title Display - 
        ~ Description: Displays a custom title of the turn for a chosen player (the user or CPU)
        > Return: void.
        * */
        private static void TurnTitleDisplay(string userName)
        {
            string titleTxt = $"{userName}'s Turn";

            FGcolor(White);
            Console.WriteLine(titleTxt);

            HyphenUnderline(titleTxt.Length);
        }
        // TurnTitleDisplay END //


        /* ---- User ---- */

        /* - User Turn - 
        ~ Description: Manages the user's turn.
        * Logic: The function asks the user for hitting cords and validates them.
        * Then, it shots at the CPU's board and displays the results to the user.
        > Return: void.
        * */
        public static void UserTurn()
        {
            Position hitPos = new Position(0, 0);
            char[,] CPUboard = Players[1].GameBoard.ArrayBoard;


            // Turn Title
            TurnTitleDisplay(Players[0].Name);

            // Print the CPU's board to the user
            Players[1].GameBoard.PrintBoard();

            // Get hitting cords
            FGcolor(DarkCyan);
            Console.WriteLine("Please enter hitting cords:");
            GetHittingCords(CPUboard, hitPos);

            // same hit spot alert
            HitCordsInvalid(CPUboard, hitPos);

            Console.Clear();

            // The results of the turn            
            // Hit\Miss message
            if (HitTry(hitPos, true))
            {
                Players[0].LastShotColor.CopyAttributes(hitPos); // saves the last shot cords for the coloring system
                Players[1].GameBoard.PrintBoard();
                Console.WriteLine("You hit a CPU's ship!");
            }
            else
            {
                Players[0].LastShotColor.CopyAttributes(hitPos); // saves the last shot cords for the coloring system
                Players[1].GameBoard.PrintBoard();
                Console.WriteLine("You missed!");
            }

            Console.WriteLine();


            ActionButton(Enter, "end the turn");
        }
        // UserTurn END //

        /* - Get Hitting Cords - 
        ~ Description: Gets hitting cords from the user.
        * Logic: Calling existing functions from the Ship class, to get valid cords (row & column).
        > Return: void. (the Position object contains the hit position changed already because we passing it to the function).
        * */
        private static void GetHittingCords(char[,] CPUboard, Position hitPos)
        {
            FGcolor(Red);

            hitPos.Row = Ship.GetRowFromUser(CPUboard); // Get the Row selection from the user
            hitPos.Row = LetterToNumber(hitPos.Row); // convert the letter to a number in the boundaries of the array

            hitPos.Col = Ship.GetColumnFromUser(CPUboard); // Get the Column selection from the user
        }
        // GetHittingCords END //

        /* - Hit Cords Invalid - 
        ~ Description: If the marked spot for shot has already hit, 
        ~ it displays a message and asks cords again by calling to the GetHittingCords function.
        > Return: void.
        * */
        private static void HitCordsInvalid(char[,] CPUboard, Position hitPos)
        {
            while (CPUboard[hitPos.Row, hitPos.Col] == 'x' || CPUboard[hitPos.Row, hitPos.Col] == '/')
            {
                ErrorSymbol();
                Console.WriteLine("You already tried to hit in that spot. Please reenter");

                GetHittingCords(CPUboard, hitPos);
            }
        }
        // HitCordsInvalid END //



        /* ---- CPU ---- */

        /* - Cpu Turn - 
        ~ Description: Manages the turn of the CPU.
        * Logic: The function decides which shot to execute according to the HitCondition of the CPU.
        * The States of the Hit Condition: 
        *              | 4- up |                  | 0- free shot |         
        *  | 1- left |           | 3- right |
        *             | 2- down |                 | -1- missed in a middle of destruction |
        * At the end of the turn it displays the results to the user.
        > Return: void.
        * */
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

            CpuDisplayResults(successfulHit);
        }
        // CpuTurn END //


        /* - Free Shot - 
        ~ Description: An attempt to "guess" where are the user's ships (by random shot).
        * Logic: The Free Shot generates random hitting position and checks if it's not already
        * been hit ('x' or '/' in the array) and generates new position if needed.
        * Then, the Free Shot passes the hitting position to the HitTry function to attempt to hit a ship.
        * If succeeded to hit a ship, it will set the hitCondition to 4 (for the Search Shot in the next turn)
        * and the destruction count to 1 (the DestructionCount is how many slots the CPU managed to hit in a row,
        * and it resets every time the CPU misses).
        > Return: bool. If hit(true) or missed(false).
        * */
        public static bool FreeShot()
        {
            Player plyr = Players[0];
            char[,] hitBoard = plyr.GameBoard.ArrayBoard;
            // random hitting position
            Position hitCords = new Position(GenerateRandInt(0, hitBoard.GetLength(0) - 1), GenerateRandInt(0, hitBoard.GetLength(0) - 1));


            // if the spot has been already hit, regenerage spot.
            while (hitBoard[hitCords.Row, hitCords.Col] == 'x' || hitBoard[hitCords.Row, hitCords.Col] == '/')
            {
                hitCords.Col = GenerateRandInt(0, hitBoard.GetLength(0) - 1);
                hitCords.Row = GenerateRandInt(0, hitBoard.GetLength(0) - 1);
            }

            // the attempt to hit
            if (HitTry(hitCords, false))
            {
                Players[1].HitCondition = 4;
                Players[1].DestructionCount = 1;
                Players[1].LastShotColor.CopyAttributes(hitCords);
                return true;
            }
            Players[1].LastShotColor.CopyAttributes(hitCords);
            return false;
        }
        // FreeShot END //


        /* - Search Shot -
         ~ Description: This function will be used if the CPU hits a ship with the free shot. 
         ~ Instead of continuing the random attempts, the CPU searches the other parts of the ship with this function,
         ~ that searches the other ship parts in the 4 directions around the hit spot. 
         ~ That way it will quickly find the other parts and destroy the ship in minimum turns.
         * Logic: Operates with one switch that decides in which direction the CPU will try to hit, based on the hitCondition variable.
         * Each value of the hit condition (1-4) represents a direction (written inside the switch).
         * In each case the function 'moves' the hitCords around the hit spot according to the direction, and tries to strike there.
         * If succeeded, the next turn it will try in this same direction; 
         * If not, it will decrease the hitCondition property so the next turn it will try another direction.
         * SUCCESSFUL SHOT - Marks 'x' in the array board.
         * MISSED SHOT - Marks '/' in the array board.
         > Return: void.
         */
        public static bool SearchShot()
        {
            // Defining the needed variables
            int hitCondition = Players[1].HitCondition;
            Position lastHitCords = new Position(Players[1].LastHitCords.Row, Players[1].LastHitCords.Col); //create the object for the last hit position
            Position tempCords = new Position(lastHitCords.Row, lastHitCords.Col);
            int destructionCount = Players[1].DestructionCount;
            char[,] hitBoard = Players[0].GameBoard.ArrayBoard;
            bool successfulHit = false;
            bool isPlayed = false;
            

            /* This while loop makes sure that if the attempt to hit was failed because the cords were out of the boundaries or already hit,
             * it will decrease the hitCondition, wich means it will try another direction instead of skip and lose the turn. */
            while (!isPlayed)
            {
                // if hitCondition is 0 - exit the search shot.
                if (HitConditionMove(hitCondition, lastHitCords))
                    break;
                
                // boundaries check
                if (lastHitCords.Row >= 0 && lastHitCords.Row <= hitBoard.GetLength(0) - 1 && lastHitCords.Col >= 0 && lastHitCords.Col <= hitBoard.GetLength(1) - 1) 
                {
                    switch (hitBoard[lastHitCords.Row, lastHitCords.Col]) // check if the spot is occupied
                    {   // if occupied, try another direction
                        case 'x':
                        case '/':
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords); // restore the position
                            break;

                        // if not occupied, try to hit
                        default:
                            Tuple<int, int, bool> AttemptResults = SearchShotAttempt(lastHitCords, hitCondition, destructionCount, successfulHit);
                            hitCondition = AttemptResults.Item1;
                            destructionCount = AttemptResults.Item2;
                            successfulHit = AttemptResults.Item3;

                            isPlayed = true; // mark as a played turn to exit the loop and pass the turn
                            break;
                    }//switch
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
            Players[1].LastShotColor.CopyAttributes(lastHitCords);
            if (!successfulHit)
            { // if failed the shot, restore the hit cords from the tempCords and return false(- shot failed)
                Players[1].LastHitCords.CopyAttributes(tempCords);
                return false;
            }
            return true;
        }
        // SearchShot END //

        /* - Hit Condition Move - 
        ~ Description: Moves the current hit position in the direction according to the hit condition.
        > Return: bool. if the hit condition is 0.
        * */
        private static bool HitConditionMove(int hitCondition, Position lastHitCords)
        {
            // Moves in the correct direction according to the hitCondition
            switch (hitCondition) // check in which direction to move
            {
                case 4:
                    lastHitCords.Row--;
                    return false;
                case 3:
                    lastHitCords.Col++;
                    return false;
                case 2:
                    lastHitCords.Row++;
                    return false;
                case 1:
                    lastHitCords.Col--;
                    return false;

                default:
                    return true;
            }
        }
        // HitConditionMove END //

        /* - SearchShotAttempt - 
        ~ Description: Tries to hit for the Search Shot.
        * Logic: If succeeded, saves the position in the LastHitCords of the player, increase the destructionCount
        * and mark the hit as a "successful hit" (successfulHit = true)
        > Return: int, int, bool. hitCondition, destructionCount, successfulHit.
        * */
        private static Tuple<int, int, bool> SearchShotAttempt(Position lastHitCords, int hitCondition, int destructionCount, bool successfulHit)
        {
            if (HitTry(lastHitCords, false)) // if succeeded, wil save the cords for the next turn
            {
                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                destructionCount++;
                successfulHit = true;
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

            return Tuple.Create(hitCondition, destructionCount, successfulHit);
        }
        // SearchShotAttempt END //


        /* - Cpu Display Results - 
        ~ Description: Displays the results of the CPU's turn to the user.
        > Return: void.
        * */
        private static void CpuDisplayResults(bool successfulHit)
        {
            // Display the results to the user
            TurnTitleDisplay(CpuName);

            Players[0].GameBoard.PrintBoard();
            FGcolor(Gray);

            if (successfulHit)
            {
                Console.WriteLine($"The CPU shot at {Players[1].LastShotColor.ToString()} and hit one of your ships!");
            }
            else
            {
                Console.WriteLine($"The CPU shot at {Players[1].LastShotColor.ToString()} and missed!");
            }
            Console.WriteLine();

            ActionButton(keyToPress: Enter, action: Continue);
        }
        // CpuDisplayResults END //



        /* - Hit Try -
         ~ Description: Used by the FreeShot and SearchShot functions as the actual attempt to hit a spot on the board, after they checked that the hitting spot is valid.
         * Logic: The function getting the position to hit as a parameter, and other needed things such as the board and the ships array.
         * The Fn checks if the hitted spot contains an opponent ship, then replaces the spot with the agreed mark for "hitted" - 'x'.
         * If not, it marks the spot for "missed" - '/'.
         > Return: Boolean. If succeed to hit a ship, return that the attempt was successful (true), if not return that the attempt was failed (false)
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
                { // CPU
                    shipsArray[hitBoard[row, col] - '5'].RemainParts--; // subtructing 5 more because the ID of the CPU's ships are 5-9 and the array index is 0-4
                }
                else
                { // user
                    shipsArray[hitBoard[row, col] - '0'].RemainParts--;
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




    }
}
