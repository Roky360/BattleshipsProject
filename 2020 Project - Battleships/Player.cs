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


        // constructor
        public Player(string name)
        {
            Name = name;
            GameBoard = new Board(Name, 10, 10);
            Ships = new Ship[5];
            // decide if create ships for CPU or for user
            if (Name == "CPU") // create ships for CPU
            {
                for (int i = 0; i < 5; Ships[i] = i != 4 ? Ships[i] = Ship.CpuCreate(GameBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.CpuCreate(GameBoard, 5), i++) ;
            }
            else // create ships for user
            {
                for (int i = 0; i < 5; i++)
                {
                    GameBoard.PrintBoard();
                    Ships[i] = i != 4 ? Ships[i] = Ship.UserCreate(GameBoard, (int)Math.Ceiling((i + 4) / 2.0)) : Ships[i] = Ship.UserCreate(GameBoard, 5);
                    Console.Write("Press ENTER to continue");
                    Console.ReadKey();
                    Thread.Sleep(100);
                    Console.Clear();
                }
            }
        }


    }
}
