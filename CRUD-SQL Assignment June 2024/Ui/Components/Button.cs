using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Button : Drawable, IHasPosition, IHasDimensions
    {
        public bool isFocused = false;
        private readonly string label;
        private readonly Alignment alignment;

        public Button(Position pos, Dimensions dim, string label, Alignment align = Alignment.Center, bool isFocused = false)
            : base(pos, dim)
        {
            this.label = label;
            this.alignment = align;
            this.isFocused = isFocused;
            DrawButton();
        }

        public void DrawButton()
        {
            ConsoleColor fgColor = isFocused ? ConsoleColor.Red : ConsoleColor.White;

            _ = new Box(this.Dim, this.Pos);

            Dimensions TextfieldDimensions = new(this.Dim);
            TextfieldDimensions.Width -= Margins.BorderHorizontalMarginDouble;
            TextfieldDimensions.Height -= Margins.BorderHorizontalMarginDouble;
            _ = new Textfield(
                new Position(this.Pos.Left + Margins.BorderHorizontalMarginSingle,
                                this.Pos.Top + Margins.BorderVerticalMarginSingle),
                TextfieldDimensions, label, alignment, fgColor);
        }



        public void FocusToggle(bool g)
        {
            isFocused = g;
            DrawButton();
        }
        
    }
}
