using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    class Game
    {
        public Player[] Players { get; set; }

        public Game()
        {
            Players = new Player[2];
            Console.WriteLine("What is your name?");
            string usrName = Console.ReadLine();
            Players[0] = new Player(usrName);
            Players[1] = new Player("CPU");
        }


        /* The function that runs the whole game.
         * after setting the players, this func manages the turn system, the shooting system and the win condition
         * at the end of the game it asks the user if restart and return the answer to the main program. */
        public static bool StartGame()
        {
            // If restart the game at the end.
            bool restart = false;








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
                return restart = true;
            }
            else return restart = false;
        }
        // StartGame END //



    }
}
