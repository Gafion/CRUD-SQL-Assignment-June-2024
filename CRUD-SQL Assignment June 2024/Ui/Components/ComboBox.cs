using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class ComboBox : Drawable, IHasPosition, IHasDimensions
    {
        private bool isFocused = false;
        public ComboBox(Position pos, Dimensions dim, List<string> options)
            : base(pos, dim)
        {
            Console.CursorVisible = false;
            int activeIndex = 0;

            ClearArea(pos, dim);
            Dimensions dropdownBorder = new(dim.Width, dim.Height * options.Count + Margins.BorderVerticalMarginDouble);
            _ = new Box(dropdownBorder, pos);

            for (int i = 0; i < options.Count; i++)
            {
                string text = options[i].PadRight(dim.Width - Margins.BorderHorizontalMarginDouble);
                if (i == activeIndex)
                {
                    _ = new Textfield(new Position(pos.Left + Margins.BorderHorizontalMarginSingle, pos.Top + Margins.BorderVerticalMarginSingle + i),
                        dim,
                        text,
                        Alignment.Left,
                        ConsoleColor.Black,
                        ConsoleColor.White);
                }
                else
                {
                    _ = new Textfield(new Position(pos.Left + Margins.BorderHorizontalMarginSingle, pos.Top + Margins.BorderVerticalMarginSingle + i),
                        dim,
                        text,
                        Alignment.Left);
                }
            }
        }
    }
}
