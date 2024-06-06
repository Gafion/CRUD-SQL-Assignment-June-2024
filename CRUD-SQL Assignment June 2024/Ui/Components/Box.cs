using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Box : Drawable
    {
        private readonly int RightWindowMargin = 2;
        private readonly int MarginRight = 1;
        public Box(Dimensions dim, Position? pos = null, ConsoleColor color = ConsoleColor.White)
            : base(pos ?? Position.Empty, dim)
        {
            ClearArea(this.Pos, dim);
            InsertAt(
                this.Pos,
                $"{Borders.Get(BorderPart.TopLeft)}" +
                    $"{new string(Borders.Get(BorderPart.Horizontal), dim.Width - RightWindowMargin)}" +
                    $"{Borders.Get(BorderPart.TopRight)}",
                color);

            Position newPosition = new(this.Pos);
            for (int i = 1; i < dim.Height - MarginRight; i++)
            {
                newPosition.Top = this.Pos.Top + i;
                newPosition.Left = this.Pos.Left;
                InsertAt(
                    newPosition,
                    Borders.Get(BorderPart.Vertical).ToString(),
                    color);
                newPosition.Left = this.Pos.Left + dim.Width - MarginRight;
                InsertAt(
                    newPosition,
                    Borders.Get(BorderPart.Vertical).ToString(),
                    color);
            }

            newPosition.Top = this.Pos.Top + dim.Height - MarginRight;
            newPosition.Left = this.Pos.Left;
            InsertAt(
                newPosition,
                $"{Borders.Get(BorderPart.BottomLeft)}" +
                    $"{new string(Borders.Get(BorderPart.Horizontal), dim.Width - RightWindowMargin)}" +
                    $"{Borders.Get(BorderPart.BottomRight)}",
                color);

        }
    }
}
