using System;

namespace DotnetHospitalManagementSystem
{
    internal class GeneralUtility
    {
        public static void DisplayMenuTable(string title, string menuName) // create a table for header
        {
            int tableWidth = 40;
            int titlePadding = (tableWidth - title.Length) / 2;
            int menuNamePadding = (tableWidth - menuName.Length) / 2;

            Console.WriteLine("┌" + new string('─', tableWidth) + "┐");

            Console.WriteLine("│" + new string(' ', titlePadding) + title + new string(' ', tableWidth - title.Length - titlePadding) + "│");

            Console.WriteLine("│" + new string('-', tableWidth) + "│");

            Console.WriteLine("│" + new string(' ', menuNamePadding) + menuName + new string(' ', tableWidth - menuName.Length - menuNamePadding) + "│");

            Console.WriteLine("└" + new string('─', tableWidth) + "┘");
        }
        /* I wrote this function after looking at the following page on StackOverFlow
         * https://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c */
    }
}
