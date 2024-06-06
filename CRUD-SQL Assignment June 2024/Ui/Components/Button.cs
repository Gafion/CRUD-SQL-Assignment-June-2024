using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Button : Drawable, IHasPosition, IHasDimensions
    {
        private bool isFocused = false;
        private readonly string label;
        private readonly Alignment alignment;
        private readonly Actions.ButtonAction ButtonAction;

        public Button(Position pos, Dimensions dim, string label, Actions.ButtonAction action, Alignment align = Alignment.Center)
            : base(pos, dim)
        {
            this.ButtonAction = action;
            this.label = label;
            this.alignment = align;
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
    }
}
