using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Master
    {
        public static void ClearArea(Position position, Dimensions dimensions)
        {
            Position newPosition = new(position);
            for (int row = 0; row < dimensions.Height; row++)
            {
                newPosition.Top = position.Top + row;
                InsertAt(newPosition, new string(' ', dimensions.Width), ConsoleColor.White, ConsoleColor.Black);
            }
        }

        public static void InsertAt(Position position, string text, ConsoleColor FG = ConsoleColor.White, ConsoleColor BG = ConsoleColor.Black)
        {
            try
            {
                Console.SetCursorPosition(position.Left, position.Top);
                Console.ForegroundColor = FG;
                Console.BackgroundColor = BG;
                Console.Write(text);
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e}");
            }
        }
    }
}
