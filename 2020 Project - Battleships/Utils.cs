using System;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    public static class Utils
    {
        /* > Utilities Class for General Function < */


        /* ==== Random Generations ==== */

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


        public static int LetterToNumber(int num)
        {
            if (num > 'Z')
            {// small
                num -= 'a';
            }
            else
            {// capital
                num -= 'A';
            }

            return num;
        }



        /* ==== Coloring & Design ==== */

        public static void FGcolor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void BGcolor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }


        public static void InputLine(int Length)
        {
            BGcolor(DarkCyan);
            for (int i = 0; i < Length - 1; Console.Write(" "), i++) ;
            Console.Write("|");
            Console.Write((char)13); Console.Write("|"); // print the "carriage return" character to return to the start of the line
        }


        public static void CrossLine(int Length)
        {
            BGcolor(DarkCyan);
            for (int i = 0; i < Length; Console.Write(" "), i++) ;
            BGcolor(Black);
            Console.WriteLine();
        }


        public static void HyphenUnderline(int length)
        {
            for (int i = 0; i < length; Console.Write("-"), i++) ;
        }


        public static void AlignedText(string txt, int LineLength)
        {
            for (int i = 0; i < (LineLength - txt.Length) / 2; Console.Write(" "), i++) ;
            Console.WriteLine(txt);
        }


        public static void ErrorSymbol()
        {
            FGcolor(DarkRed);
            Console.Write("[!] ");
            FGcolor(Red);
        }


        public static void SystemMsg()
        {
            FGcolor(DarkGreen);
            Console.Write("[!] ");
            FGcolor(Green);
        }



    }
}
