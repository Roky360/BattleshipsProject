using System;
using static _2020_Project___Battleships.Game;
using static _2020_Project___Battleships.Utils;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    class Program
    {
        // *******
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

            Console.WriteLine();
            CrossLine(Length: 54);
            Console.WriteLine();

            // Ending Message
            Console.WriteLine("Thanks for playing our game! We hope you enjoyed it :)");
            Console.WriteLine("credits... not to leizan!!");
            

            
            

            


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



            /* Key Chars for game board:
             * \0 (null) - empty slot
             * 0-9 - ships ID (0-4 : player | 5-9 : CPU)
             * x - hit on ship slot
             * / - hit on empty slot
             */



            /*
            TO DO LIST:
            - colors: Eden


            ---
            BUG FIXES:
            
            */




        }
    }
}
