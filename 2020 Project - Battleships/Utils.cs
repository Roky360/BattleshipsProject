using System;
using System.Collections.Generic;
using System.Text;

namespace _2020_Project___Battleships
{
    public static class Utils
    {
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


        public static void FGcolor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void BGcolor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }







    }
}
