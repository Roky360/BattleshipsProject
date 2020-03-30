using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Game
    {
        Player[] players;

        public Game(Player[] players)
        {
            players = new Player[2];
            players[0] = new Player("INPUT");
            players[1] = new Player("CPU");
        }


        


    }
}
