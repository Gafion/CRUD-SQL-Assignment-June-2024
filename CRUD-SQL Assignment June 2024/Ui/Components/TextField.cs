using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Textfield : Drawable
    {
        private readonly string text = "";

        public string Text { get { return text; } }
        public Textfield(Position pos, Dimensions dim, string text, Alignment? align, ConsoleColor FG = ConsoleColor.White, ConsoleColor BG = ConsoleColor.Black)
            : base(pos, dim)
        {
            this.text = text;

            int aligned = Aligner.Align(this.text, align, dim.Width);
            InsertAt(new Position(pos.Left + aligned, pos.Top), this.text, FG, BG);
        }
    }
}
