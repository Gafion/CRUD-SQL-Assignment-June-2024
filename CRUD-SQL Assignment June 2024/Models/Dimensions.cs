using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public static Dimensions Empty { get; } = new(0, 0);
        public Dimensions(int width, int heigth)
        {
            Width = Math.Max(0, width);
            Height = Math.Max(0, heigth);
        }
        public Dimensions(Dimensions dim)
        {
            this.Width = dim.Width;
            this.Height = dim.Height;
        }
    }
}
