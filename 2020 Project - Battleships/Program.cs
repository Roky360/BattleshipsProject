using System;
using System.Threading;

namespace _2020_Project___Battleships
{
    class Program
    {
        static int GetHashCode(string str)
        {
            int hash = 0;
            foreach (int i in str)
            {
                hash = i * 256 + (char)i;
            }
            return hash % 50;
        }

        static int mix(int a, int b, int c)
        {
            a = a - b; a = a - c; a = a ^ (c >> 13);
            b = b - c; b = b - a; b = b ^ (a << 8);
            c = c - a; c = c - b; c = c ^ (b >> 13);
            a = a - b; a = a - c; a = a ^ (c >> 12);
            b = b - c; b = b - a; b = b ^ (a << 16);
            c = c - a; c = c - b; c = c ^ (b >> 5);
            a = a - b; a = a - c; a = a ^ (c >> 3);
            b = b - c; b = b - a; b = b ^ (a << 10);
            c = c - a; c = c - b; c = c ^ (b >> 15);
            return c % 10;
        }
        // *******


        static void Main(string[] args)
        {
            
            bool restart = true;

            while (restart)
            {
                _ = new Game();
                restart = Game.StartGame();
            }

            Console.WriteLine("Thanks for playing our game! Hope you enjoyed it :)");
            Console.WriteLine("credits...");



            
            

            


            /*
             * input check
            int x;
            bool isNumber;
            isNumber = int.TryParse(Console.ReadLine(), out x);
            // bool isNumber = int.TryParse("1", out x);
            while (!isNumber)
            {
                Console.WriteLine("ERROR");
                isNumber = int.TryParse(Console.ReadLine(), out x);
            }
            Console.WriteLine(isNumber);
            */



            /*
            TO DO LIST:
            - instructions: Ron
            - ship create function for cpu:
            - destroying mechanism:
                * Player
                  - fire shot
                * CPU
                  - free shot
                  - search shot
            - win condition
            - colors:


            ---
            FIXES:
            - error msgs for ship creaion: sometimes tell the usr wrong explanation.
            - versions of the ship creation and etc.. to the CPU


            */




        }
    }
}
