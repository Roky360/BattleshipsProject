using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _2020_Project___Battleships
{
    class Player
    {
        public string name { get; set; }
        public Board gameBoard { get; set; }
        public Ship[] ships { get; set; }


        // constructor
        public Player(string name)
        {
            this.name = name;
            gameBoard = new Board(this.name, 10, 10);
            ships = new Ship[5];
            for (int i = 0; i < 5; ships[i] = i != 4 ? ships[i] = Ship.UserCreate(gameBoard, (int)Math.Ceiling((i + 4) / 2.0)) : ships[i] = Ship.UserCreate(gameBoard, 5), i++)
            {
                if (i > 0)
                {
                    Console.Write("Press ENTER to continue");
                    Console.ReadKey();
                    Thread.Sleep(200);
                    Console.Clear();
                    gameBoard.PrintBoard();
                }
            }
        }



    }
}
