using System;
using System.Threading;
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
        


        /* COMMENT NEEDED */
        static void Title()
        {
            FGcolor(White);
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────   ");
            FGcolor(Red);
            Console.WriteLine("╔════]     //\\    ═══╦═══ ═══╦═══ ╖     ╔═══  ╒═══╛ ╖     ╓ ╘═╦═╕ ╦═══╗ ╒═══╛  ");
            FGcolor(Magenta);
            Console.WriteLine("║____/    //  \\      ║       ║    ║     ║     \\     ║     ║   ║   ║   ║ \\    ");
            FGcolor(Blue);
            Console.WriteLine("║    \\   //--- \\     ║       ║    ║     ╠══    `═\\  ╠═════╣   ║   ╠═══╝  `═\\");
            FGcolor(Cyan);
            Console.WriteLine("║    |  //      \\    ║       ║    ║     ║         \\ ║     ║   ║   ║         \\");
            FGcolor(Green);
            Console.WriteLine("╚════┘ //        \\   ║       ║    ╚═══╛ ╚═══╛ ╘═══╛ ╜     ╙ ╘═╩═╕ ╜     ╘═══╛  ");
            FGcolor(White);
            Console.WriteLine("──────────────────────────────────────────── By Ron Berkhof & Eden Shaked ───   ");
            Console.WriteLine();
        }


        /* COMMENT NEEDED */
        public static void Instructions() // Explains the game to the user
        {
            FGcolor(White);
            Console.WriteLine("The Rules of the Game:");
            Console.WriteLine("----------------------");

            FGcolor(DarkCyan); Console.WriteLine("Pre Game");
            FGcolor(Gray);
            Console.WriteLine("Each player receives a game board and five ships of varying lengths.");
            Console.WriteLine("Before the game starts, each opponent secretly places their own five ships on the game board array.");
            Console.WriteLine("Each ship must be placed horizontally or vertically across array spaces—not diagonally—and the ships can't hang off the array.");
            Console.WriteLine("Ships can touch each other, but they can't occupy the same array space.");
            Console.WriteLine("You cannot change the position of the ships after the game begins.");

            FGcolor(DarkCyan); Console.WriteLine("Game Play");
            FGcolor(Gray);
            Console.WriteLine("Players take turns firing shots (by calling out an array coordinate) to attempt to hit the opponent's enemy ships.");
            Console.WriteLine("On your turn, input a letter and a number that identifies a row and column on your target array.");
            Console.WriteLine("Each shot will be marked on the grid with either 'x' which means your shot hit a target or a colored rectangle which means the shot missed your target.");

            FGcolor(DarkCyan); Console.WriteLine("Wining");
            FGcolor(Gray);
            Console.WriteLine("The first player to sink all five of their opponent's ships wins the game.");

            Console.WriteLine();

            FGcolor(DarkCyan); Console.WriteLine("Thats it, enjoy!");

            Console.WriteLine();

            FGcolor(DarkGray); Console.WriteLine("Press ENTER to return");
            Console.ReadKey();
        }


        



        static void Main(string[] args)
        {
            // Title
            FGcolor(White);
            Console.WriteLine("Welcome to...");
            Thread.Sleep(1000);
            Title();
            FGcolor(Gray);
            Console.WriteLine("We glad that you here playing our game!");
            Console.WriteLine();
            FGcolor(DarkGray);
            Console.Write("Press ANY KEY to start");
            Console.ReadKey();

            // Game Loop
            bool restart = true;


            while (restart)
            {
                Console.Clear();
                
                // Get Username
                FGcolor(White);
                string AskName = "What is your name?";
                Console.WriteLine(AskName);
                InputLine(AskName.Length);
                string usrName = Console.ReadLine();
                BGcolor(Black);

                // Game Play
                _ = new Game(usrName);
                restart = Game.StartGame();
            }

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
