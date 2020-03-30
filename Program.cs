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
            //Board PlayerHitBoard = new Board("Player", 10, 10); // create the player's board
            //Board CpuHitBoard = new Board("CPU", 10, 10); // create the CPU's board

            //PlayerHitBoard.PrintBoard();
            //CpuHitBoard.PrintBoard();

            Console.WriteLine("What is your name?");
            string usrName = Console.ReadLine();
            Player player1 = new Player(usrName);
            // Player player2 = new Player("CPU");
            player1.gameBoard.PrintBoard();
            // Console.WriteLine($"{a.Id}, {b.Id}");



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

            - colors:


            ---
            FIXES:
            


            */




        }
    }
}
