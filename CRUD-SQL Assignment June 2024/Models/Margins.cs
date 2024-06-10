using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    public class Margins
    {
        // Button dimensions
        public const int ButtonWidth = 16;
        public const int ButtonHeight = 3;

        // Margins for borders
        public const int BorderHorizontalMarginSingle = 1;
        public const int BorderHorizontalMarginDouble = 2;
        public const int BorderVerticalMarginSingle = 1;
        public const int BorderVerticalMarginDouble = 2;

        // Dialog box dimensions
        public const int DialogBoxWidth = 56;
        public const int DialogBoxHeight = 45;

        // ComboBox dimensions
        public const int ComboBoxHeight = 1;
        public const int ComboBoxDropMargin = 7;

        // Main box border margin
        public const int MainBoxBorderMargin = 4;

        // Table margins
        public const int TableTopMargin = 6;
        public const int TableBottomMargin = 12;

        // Rows reserved for non-data elements in a table
        public const int TopBorderRow = 1;
        public const int HeaderRow = 1;
        public const int HeaderBorderRow = 1;
        public const int BottomBorderRow = 1;

        // Total number of non-data rows in a table
        public static int NonDataRows => TopBorderRow + HeaderRow + HeaderBorderRow + BottomBorderRow;
    }
}
