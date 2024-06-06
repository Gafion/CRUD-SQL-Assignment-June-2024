using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Drawable(Position? pos, Dimensions dim) : Master, IHasPosition, IHasDimensions
    {
        public Position Pos { get; set; } = pos ?? new Position(0, 0);
        public Dimensions Dim { get; set; } = dim;
    }
}
