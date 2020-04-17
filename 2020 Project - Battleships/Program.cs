using System;
using System.Threading;

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





        /* Generates new random int between two chosen numbers
         * Just enter the range you want, NO NEED TO ADD 1 TO THE LAST ARGUMENT, the fn does it */
        public static int GenerateRandInt(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }
        // GenerateRandomInt END //


        /* Generates random bool and return true or false */
        public static bool GenerateRandBool()
        {
            if (GenerateRandInt(0, 1) == 0)
                return false;
            else
                return true;
        }
        // GenerateRandBool END //


        static void Title()
        {
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────   ");
            Console.WriteLine("╔════]     //\\    ═══╦═══ ═══╦═══ ╖     ╔═══  ╒═══╛ ╖     ╓ ╘═╦═╕ ╦═══╗ ╒═══╛  ");
            Console.WriteLine("║____/    //  \\      ║       ║    ║     ║     \\     ║     ║   ║   ║   ║ \\    ");
            Console.WriteLine("║    \\   //--- \\     ║       ║    ║     ╠══    `═\\  ╠═════╣   ║   ╠═══╝  `═\\");
            Console.WriteLine("║    |  //      \\    ║       ║    ║     ║         \\ ║     ║   ║   ║         \\");
            Console.WriteLine("╚════┘ //        \\   ║       ║    ╚═══╛ ╚═══╛ ╘═══╛ ╜     ╙ ╘═╩═╕ ╜     ╘═══╛  ");
            Console.WriteLine("──────────────────────────────────────────── By Ron Berkhof & Eden Shaked ───   ");
        }

        


        static void Main(string[] args)
        {
            Position pos = new Position(2, 3);
            Board b = new Board("aaa", pos.Row, pos.Col);
            b.PrintBoard();
            Console.ReadKey();

            for (int i = 0; i < 20; i++)
            {
                Player a = new Player("CPU");
                Console.ReadKey();
                Console.Clear();
            }



            //Title();
            bool restart = true;

            while (restart)
            {
                Console.Clear();
                Console.WriteLine("What is your name?");
                string usrName = Console.ReadLine();
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
             * null - empty slot
             * 0-9 - ships ID
             * x - hit on ship slot
             * / - hit on empty slot
             */



            /*
            TO DO LIST:
            - instructions: Ron
            - destroying mechanism:
                * Player : Ron
                  - fire shot
                * CPU : Eden
                  - free shot V
                  - search shot V*
            - win condition: Ron
            - colors: Eden


            ---
            BUG FIXES:
            - random int & boolean: every time we want to call this Fns we need to write "Program.~~" before because it stored in the main program.
            - search shot: hitCords check values! if out of board...
            - change the selection of the row at the ship creation to letters.

            */






            /*
                // if secceeded to hit this direction, set the hit condition to this direction again so will try to hit this direction again next turn
                switch (hitCondition)
                {
                    // try ^UP^
                    case 4:
                        lastHitCords.Row--;
                        if (lastHitCords.Row < 0)
                        {
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords);
                        }
                        else if (hitBoard[lastHitCords.Row, lastHitCords.Col] == 'x' || hitBoard[lastHitCords.Row, lastHitCords.Col] == '/')
                        {
                            hitCondition--;
                        }
                        else
                        {
                            if (HitTry(lastHitCords))
                            {
                                hitCondition = 4;
                                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                                successHit = true;
                            }
                            else hitCondition--;
                            isPlayed = true;
                        }
                        break;

                    // try >RIGHT>
                    case 3:
                        lastHitCords.Col++;
                        if (lastHitCords.Col > 9)
                        {
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords);
                        }
                        else if (hitBoard[lastHitCords.Row, lastHitCords.Col] == 'x' || hitBoard[lastHitCords.Row, lastHitCords.Col] == '/')
                        {
                            hitCondition--;
                        }
                        else
                        {
                            if (HitTry(lastHitCords))
                            {
                                hitCondition = 3;
                                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                                successHit = true;
                            }
                            else hitCondition--;
                            isPlayed = true;
                        }
                        break;

                    // try \DOWN/
                    case 2:
                        lastHitCords.Row++;
                        if (lastHitCords.Row > 9)
                        {
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords);
                        }
                        else if (hitBoard[lastHitCords.Row, lastHitCords.Col] == 'x' || hitBoard[lastHitCords.Row, lastHitCords.Col] == '/')
                        {
                            hitCondition--;
                        }
                        else
                        {
                            if (HitTry(lastHitCords))
                            {
                                hitCondition = 2;
                                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                                successHit = true;
                            }
                            else hitCondition--;
                            isPlayed = true;
                        }
                        break;

                    // try <LEFT<
                    case 1:
                        lastHitCords.Col--;
                        if (lastHitCords.Col < 0)
                        {
                            hitCondition--;
                            lastHitCords.CopyAttributes(tempCords);
                        }
                        else if (hitBoard[lastHitCords.Row, lastHitCords.Col] == 'x' || hitBoard[lastHitCords.Row, lastHitCords.Col] == '/')
                        {
                            hitCondition--;
                        }
                        else
                        {
                            if (HitTry(lastHitCords))
                            {
                                hitCondition = 1;
                                Players[1].LastHitCords.CopyAttributes(lastHitCords);
                                successHit = true;
                            }
                            else hitCondition--;
                            isPlayed = true;
                        }
                        break;
                }//switch end
                */




        }
    }
}
