using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _2020_Project___Battleships
{
    class Player
    {
        public string Name { get; set; }
        public Board GameBoard { get; set; }
        public Ship[] Ships { get; set; }
        // Used for CPU only \/
        public int HitCondition { get; set; }
        public Position LastHitCords { get; set; }
        public int DestructionCount { get; set; }
        // ----------------- /\


        // constructor
        public Player(string name)
        {
            Name = name;
            GameBoard = new Board(Name, 10, 10);
            Ships = new Ship[5];
            HitCondition = 0;
            LastHitCords = new Position(0, 0);
            DestructionCount = 0;

            // decide if create ships for CPU or for user
            if (Name == "CPU")
            {// create ships for CPU
                for (int i = 0; i < 5; Ships[i] = i != 4 ? Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.CpuCreate(GameBoard.ArrayBoard, 5), i++) GameBoard.PrintBoard();
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
