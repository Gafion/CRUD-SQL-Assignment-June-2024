using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Table : Drawable, IHasPosition, IHasDimensions
    {
        private bool isFocused = false;
        private List<string> Headers { get; set; }
        private List<List<string>> Rows { get; set; }
        private List<int> ColumnWidths { get; set; }
        public Table(Position pos, Dimensions dim, List<string> headers, List<User> users, Dictionary<int, int>? columnAdjustments = null) : base(pos, dim)
        {
            this.Pos = pos;
            this.Dim = dim;
            this.Headers = headers;
            this.Rows = ConvertUsersToRows(users);
            this.ColumnWidths = CalculateColumnWidths(Headers, Rows, dim.Width, columnAdjustments ?? []);

            DrawTable();
        }

        private void DrawTable()
        {
            DrawBorderTop();
            DrawHeaders();
            DrawHeadersBottom();
            DrawRows();
            DrawBorderBottom();
        }

        private static List<int> CalculateColumnWidths(
            List<string> headers,
            List<List<string>> rows,
            int totalWidth,
            Dictionary<int, int> fixedAdjustments)
        {
            var initialWidths = headers.Select((header, index) =>
                Math.Max(header.Length, rows.Count != 0 ? rows.Max(row => row.ElementAtOrDefault(index)?.Length ?? 0) : 0))
                .ToList();

            int totalFixedWidth = initialWidths.Sum() + fixedAdjustments.Values.Sum();
            int availableWidth = totalWidth - headers.Count - 1 - totalFixedWidth;

            var dynamicColumns = Enumerable.Range(0, headers.Count).Where(i => !fixedAdjustments.ContainsKey(i)).ToList();

            if (dynamicColumns.Count != 0)
            {
                int extraSpacePerColumn = availableWidth / dynamicColumns.Count;
                dynamicColumns.ForEach(colIndex => initialWidths[colIndex] += extraSpacePerColumn);

                int remainingExtraSpace = availableWidth % dynamicColumns.Count;
                for (int i = 0; i < remainingExtraSpace; i++)
                {
                    initialWidths[dynamicColumns[i]] += 1;
                }
            }

            foreach (var adjustment in fixedAdjustments)
            {
                if (adjustment.Key < initialWidths.Count)
                {
                    initialWidths[adjustment.Key] += adjustment.Value;
                }
            }

            return initialWidths;
        }


        private static List<List<string>> ConvertUsersToRows(List<User> users)
        {
            return users.Select(user => new List<string>
            {
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.Address,
                user.City,
                user.PostCode,
                user.Education,
                user.EducationEnd,
                user.Company,
                user.Employed,
                user.EmployEnd,
                "Delete", // Placeholder for delete action
                "Edit"    // Placeholder for edit action
            }).ToList();
        }

        private void DrawBorderTop()
        {
            string topBorder =
                Borders.Get(BorderPart.TopLeft) + string.Join(Borders.Get(BorderPart.TopMiddle),
                ColumnWidths.Select(w => new string(Borders.Get(BorderPart.Horizontal), w))) + Borders.Get(BorderPart.TopRight);
            InsertAt(Pos, topBorder);
        }

        private void DrawHeaders()
        {
            Position headerPos = new(
                Pos.Left,
                Pos.Top + 1);
            string headerLine =
                Borders.Get(BorderPart.Vertical) + string.Join(Borders.Get(BorderPart.Vertical),
                Headers.Select((header, index) => header.PadRight(ColumnWidths[index]))) + Borders.Get(BorderPart.Vertical);
            InsertAt(headerPos, headerLine);
        }

        private void DrawHeadersBottom()
        {
            Position headerBottomPos = new(
                Pos.Left,
                Pos.Top + 2);
            string headerBottomBorder =
                Borders.Get(BorderPart.Left) + string.Join(Borders.Get(BorderPart.Cross),
                ColumnWidths.Select(w => new string(Borders.Get(BorderPart.Horizontal), w))) + Borders.Get(BorderPart.Right);
            InsertAt(headerBottomPos, headerBottomBorder);
        }

        private void DrawRows()
        {
            int maxDataRows = Dim.Height - Margins.NonDataRows;
            for (int rowIndex = 0; rowIndex < maxDataRows; rowIndex++)
            {
                DrawRow(rowIndex);
            }
        }

        private void DrawRow(int rowIndex)
        {
            var row = rowIndex < Rows.Count ? Rows[rowIndex] : new List<string>(new string[Headers.Count]);
            int cellLeftPosition = Pos.Left;
            int rowTopPosition = Pos.Top + Margins.TopBorderRow + Margins.HeaderRow + Margins.HeaderBorderRow + rowIndex;

            for (int colIndex = 0; colIndex < Headers.Count; colIndex++)
            {
                DrawCell(row, colIndex, cellLeftPosition, rowTopPosition);
                cellLeftPosition += ColumnWidths[colIndex] + 1;
            }
            InsertAt(new Position(cellLeftPosition, rowTopPosition), Borders.Get(BorderPart.Vertical).ToString());
        }

        private void DrawCell(List<string> row, int colIndex, int cellLeftPosition, int rowTopPosition)
        {
            ConsoleColor fgColor = isFocused ? ConsoleColor.Red : ConsoleColor.Gray;

            string cellContent = GetCellContent(row, colIndex).PadRight(ColumnWidths[colIndex]);
            Position cellContentPos = new(
                cellLeftPosition + Margins.BorderHorizontalMarginSingle,
                rowTopPosition);
            Position cellBorderPos = new(
                cellLeftPosition,
                rowTopPosition);
            InsertAt(cellContentPos, cellContent, fgColor);
            InsertAt(cellBorderPos, Borders.Get(BorderPart.Vertical).ToString());
        }

        private static string GetCellContent(List<string> row, int colIndex)
        {
            return row.ElementAtOrDefault(colIndex) ?? "";
        }

        private void DrawBorderBottom()
        {
            Position bottomBorderPos = new(
                Pos.Left,
                Pos.Top + Dim.Height - Margins.BorderVerticalMarginSingle);

            string bottomBorder =
                Borders.Get(BorderPart.BottomLeft) + string.Join(Borders.Get(BorderPart.BottomMiddle),
                ColumnWidths.Select(w => new string(Borders.Get(BorderPart.Horizontal), w))) + Borders.Get(BorderPart.BottomRight);
            InsertAt(bottomBorderPos, bottomBorder);
        }
    }
}
