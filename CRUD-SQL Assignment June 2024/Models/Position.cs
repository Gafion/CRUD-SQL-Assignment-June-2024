using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Position
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public static Position Empty { get; } = new(0, 0);
        public Position(int left, int top)
        {
            Left = Math.Max(0, left);
            Top = Math.Max(0, top);
        }
        public Position(Position position)
        {
            this.Left = position.Left;
            this.Top = position.Top;
        }
    }
}
