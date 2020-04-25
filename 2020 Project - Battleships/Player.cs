using System;
using System.Threading;

namespace _2020_Project___Battleships
{
    class Player
    {
        public string Name { get; set; }                        // The name of the player
        public Board GameBoard { get; set; }                    // The game board of the player, where all his ships are
        public Ship[] Ships { get; set; }                       // Array that stores the player's ships
        public Position LastHitColor { get; set; }              // The last hit position for the coloring system
        // Used for CPU only \/
        public int HitCondition { get; set; }                   // hit condition of the CPU (direction where to hit next search shot)
        public Position LastHitCords { get; set; }              // Stores the position of the last successful shot of the CPU
        public int DestructionCount { get; set; }               // Stores how many spots in a row the CPU secceeded to hit
        // ----------------- /\


        // constructor
        public Player(string name)
        {
            Name = name;
            Position boardSize = new Position(10, 10); // default values
            if (Name != "CPU")
                boardSize = Game.MainMenu();
            GameBoard = new Board(Name, boardSize);
            Ships = new Ship[5];
            LastHitColor = new Position(0, 0);
            HitCondition = 0;
            LastHitCords = new Position(0, 0);
            DestructionCount = 0;

            // decide if create ships for CPU or for user
            if (Name == "CPU")
            {// create ships for CPU
                for (int i = 0; i < 5; Ships[i] = i != 4 ? Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, 5), i++) ;
                ; /* << REMOVE */
            }
            // create ships for user
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    GameBoard.PrintBoard();
                    Ships[i] = i != 4 ? Ships[i] = Ship.UserCreate(GameBoard.ArrayBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.UserCreate(GameBoard.ArrayBoard, 5);
                    Console.Write("Press ENTER to continue");
                    Console.ReadKey();
                    Thread.Sleep(100);
                    Console.Clear();
                }
                GameBoard.PrintBoard();
                Console.WriteLine("That's how your board looks like!");
                Console.Write("Press ENTER to continue");
                Console.ReadKey();
                Thread.Sleep(100);
                Console.Clear();
            }
        }


    }
}
