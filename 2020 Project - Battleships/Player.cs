using System;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Player
    {
        public string Name { get; set; }                        // The name of the player
        public Board GameBoard { get; set; }                    // The game board of the player, where all his ships are
        public Ship[] Ships { get; set; }                       // Array that stores the player's ships
        public Position LastShotColor { get; set; }             // The last hit position for the coloring system
        public bool IsLost { get; set; }                        // Whether or not the player lost the game
        // Used for CPU only \/
        public const string CpuName = "CPU";                    // constant variable storing the name of the player that the computer (CPU) plays
        public int HitCondition { get; set; }                   // hit condition of the CPU (direction where to hit next search shot: 
                                                                // 4- up | 3- right | 2- down | 1- left | 0- free shot | -1- missed in a middle of destruction)
        public Position LastHitCords { get; set; }              // Stores the position of the last successful shot of the CPU
        public int DestructionCount { get; set; }               // Stores how many spots in a row the CPU secceeded to hit
        // ----------------- /\

        
        // constructor
        public Player(string name)
        {
            Name = name;
            GameBoard = new Board(Name, Game.BoardSize);
            Ships = new Ship[5];
            LastShotColor = new Position(0, 0);
            IsLost = false;
            // CPU
            HitCondition = 0;
            LastHitCords = new Position(0, 0);
            DestructionCount = 0;

            // decide if create ships for CPU or for user
            if (Name == CpuName)
            {// create ships for CPU
                for (int i = 0; i < 5; Ships[i] = i != 4 ? Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, 5), i++) ;
            }
            else // create ships for user
            {
                for (int i = 0; i < 5; i++)
                {
                    FGcolor(White);
                    Console.WriteLine("Ship Creation");
                    HyphenUnderline(length: 13);

                    GameBoard.PrintBoard();
                    Ships[i] = i != 4 ? Ships[i] = Ship.UserCreate(GameBoard.ArrayBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.UserCreate(GameBoard.ArrayBoard, 5);

                    ActionButton(keyToPress: Enter, action: Continue);
                }
                GameBoard.PrintBoard();

                // end of creation message
                SystemMsg();
                Console.WriteLine("All of your ships created successfully !");
                Console.WriteLine();

                ActionButton(keyToPress: Enter, action: Continue);
            }
        }


    }
}
