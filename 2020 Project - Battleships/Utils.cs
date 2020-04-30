using System;
using System.Threading;
using static System.ConsoleColor;

namespace _2020_Project___Battleships
{
    public static class Utils
    {
        /* > Utilities Class for General Function < */



        /* ==== Random Generations ==== */

        /* - Generate Random Int - 
        ~ Description: Generates new random int between two chosen numbers
        ~ Just enter the range you want, NO NEED TO ADD 1 TO THE LAST ARGUMENT, the fn does it
        * Logic: The function creates a Random and returns a random number between the boundaries inputed (using random.Next).
        > Return: int. the random number created.
        * */
        public static int GenerateRandInt(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }
        // GenerateRandomInt END //


        /* - Generate Random Bool - 
        ~ Description: Generates random bool and return true or false
        ~ Just enter the range you want, NO NEED TO ADD 1 TO THE LAST ARGUMENT, the fn does it
        * Logic: The function calls the GenerateRandInt to import a random int between 0 to 1.
        * If GenerateRandBool returns 0, the function will return false. else, it will return true.
        > Return: bool. the random boolean created.
        * */
        public static bool GenerateRandBool()
        {
            if (GenerateRandInt(0, 1) == 0)
                return false;
            return true;
        }
        // GenerateRandBool END //


        /* - Letter To Number - 
        ~ Description: Converts a letter (small or capital) to a number.
        * Logic: The function identifies if the letter is capital or small letter,
        * and subtruct 'A' or 'a' according to that.
        > Return: int. the number after the conversion process.
        * */
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
        // LetterToNumber END //



        /* ==== Coloring & Design ==== */

        /* ---- Console Color ---- */

        /* - Set Forground Color - 
        ~ Description: Sets the forground color of the console.
        ~ Created as a short and easy way of changing the console color.
        > Return: void.
        * */
        public static void FGcolor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        // FGcolor END //

        /* - Set Background Color - 
        ~ Description: Sets the background color of the console.
        ~ Created as a short and easy way of changing the console color.
        > Return: void.
        * */
        public static void BGcolor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
        // BGcolor END //



        /* ---- Alignment & Text Design ---- */

        /* - Input Line - 
        ~ Description: A designed input line (colored in dark cyan).
        ~ Just input the length of the desired input line.
        > Return: void.
        * */
        public static void InputLine(int Length)
        {
            BGcolor(DarkCyan);
            for (int i = 0; i < Length - 1; Console.Write(" "), i++) ;
            Console.Write("|");
            Console.Write((char)13); Console.Write("|"); // print the "carriage return" character to return to the start of the line
        }
        // InputLine END //


        /* - Cross Line - 
        ~ Description: A designed line (colored in dark cyan) to seperate segments on the screen.
        ~ Just input the length of the desired line.
        > Return: void.
        * */
        public static void CrossLine(int Length)
        {
            BGcolor(DarkCyan);
            for (int i = 0; i < Length; Console.Write(" "), i++) ;
            BGcolor(Black);
            Console.WriteLine();
        }
        // CrossLine END //


        /* - Hyphen Underline - 
        ~ Description: A line made of "─"s. Used for titles.
        ~ Just input the length of the desired input line.
        > Return:
        * */
        public static void HyphenUnderline(int length)
        {
            for (int i = 0; i < length; Console.Write("─"), i++) ;
            Console.WriteLine();
        }
        // HyphenUnderline END //


        /* - Aligned Text - 
        ~ Description: Aligns a desired string in a given line length.
        * Logic: Subtructs the length of the string to get the length 
        * of the spaces, then print the spaces devided by 2, to align the text to the middle of the line length.
        > Return: void.
        * */
        public static void AlignedText(string txt, int LineLength)
        {
            for (int i = 0; i < (LineLength - txt.Length) / 2; Console.Write(" "), i++) ;
            Console.WriteLine(txt);
        }
        // AlignedText END //


        /* - Action Button - 
        ~ Description: A "button" used for asking the user to continue with the game.
        ~ You can input to the function what key the user has to "press" and for what purpose, 
        ~ and the function will print the message with all the given details and wait for respond.
        ! The function uses constants for common keyboard buttons that we've used:
        > Return: void.
        * */
        public const string Enter = "ENTER";
        public const string AnyKey = "ANY KEY";
        public const string Continue = "continue";
        public static void ActionButton(string keyToPress, string action)
        {
            FGcolor(DarkGray);
            Console.Write($"Press {keyToPress} to {action}");

            Console.ReadKey(true);
            //Thread.Sleep(100);
            Console.Clear();
        }
        // ActionButton END //


        /* - Error Symbol - 
        ~ Description: Prints an error symbol. 
        ~ Used for design the error messages displayed to the user.
        > Return: void.
        * */
        public static void ErrorSymbol()
        {
            FGcolor(DarkRed);
            Console.Write("[!] ");
            FGcolor(Red);
        }
        // ErrorSymbol END //


        /* - System Message - 
        ~ Description: Prints a system symbol. 
        ~ Used for design the system messages displayed to the user.
        > Return: void.
        * */
        public static void SystemMsg()
        {
            FGcolor(DarkGreen);
            Console.Write("[!] ");
            FGcolor(Green);
        }
        // SystemMsg END //


    }
}
