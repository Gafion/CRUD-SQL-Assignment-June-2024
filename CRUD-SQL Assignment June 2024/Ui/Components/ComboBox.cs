using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Mysqlx.Resultset;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class ComboBox : Drawable, IHasPosition, IHasDimensions
    {
        private readonly Position pos;
        private readonly Dimensions dim;
        private readonly List<string> options;
        private int activeIndex = 0;
        public string SelectedOption => options[activeIndex];
        public ComboBox(Position pos, Dimensions dim, List<string> options)
            : base(pos, dim)
        {
            Console.CursorVisible = false;
            this.pos = pos;
            this.dim = dim;
            this.options = options;

            Draw();
        }

        public void Draw()
        {
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

        public void CaptureInput()
        {
            ConsoleKey key;
            do
            {
                Draw();
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        activeIndex = (activeIndex - 1 + options.Count) % options.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        activeIndex = (activeIndex + 1) % options.Count;
                        break;
                }
            } while (key != ConsoleKey.Enter);
        }
    }
}
