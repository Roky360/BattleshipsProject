using System;
using static _2020_Project___Battleships.Game;

namespace _2020_Project___Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            // Title
            TitleSequence();
            
            // Game Loop
            bool restart = true;

            while (restart)
            {
                Console.Clear();

                // Get Username
                string usrName = GetUserName();
                
                // Game Play
                _ = new Game(usrName);
                restart = StartGame();
            }
            
            Credits();



            /* Key Chars for game board:
             * \0 (null) - empty slot
             * 0-9 - ships ID (0-4 : player | 5-9 : CPU)
             * x - hit ship slot
             * / - hit empty slot
             */



            /* ───────────────────────────────────────────── *\
            |  Made as a 10th grade computer science project  |
            |  Created by Ron Berkhof and Eden Shaked         |
            |  Ort Shapira high school, Kfar-Sava             |
            |  Timestamp: 03.05.2020                          |
            \* ───────────────────────────────────────────── */


        }
    }
}
