using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Table : Drawable, IHasPosition, IHasDimensions
    {
        public bool isFocused = false;
        public List<string> Headers { get; set; }
        private List<List<string>> Rows { get; set; }
        private List<int> ColumnWidths { get; set; }
        public int activeRow;
        public int activeColumn;
        public Table(Position pos, Dimensions dim, List<string> headers, Dictionary<int, int>? columnAdjustments = null) : base(pos, dim)
        {
            this.Pos = pos;
            this.Dim = dim;
            this.Headers = headers;
            this.Rows = [];
            this.ColumnWidths = CalculateColumnWidths(Headers, Rows, dim.Width, columnAdjustments ?? []);
            activeRow = 0;
            activeColumn = headers.IndexOf("Delete");

            
            DrawTable();
        }

        private void LoadDataFromDatabase()
        {
            // Fetch data from the database
            GetUsersFromDatabase();

            // Convert users to rows
            Rows = ConvertUsersToRows(UserRepository.users);

            // Recalculate column widths
            ColumnWidths = CalculateColumnWidths(Headers, Rows, Dim.Width, []);
        }

        private static void GetUsersFromDatabase()
        {
            UserRepository.users.Clear();
            List<List<string>> result = Database.Read1("SELECT person.PersonId, person.FirstName, person.LastName, person.PostID, person.Address, city.City, " +
                "course.CourseName, education.EducationEndDate, " +
                "company.CompanyName, employment.Employed, employment.EmployEnd " +
                "FROM person, city, education, employment, course, company " +
                "WHERE city.PostID=person.PostID AND education.EducationID=person.PersonId " +
                "AND employment.EmploymentID=person.PersonId AND course.CourseID=education.CourseID " +
                "AND company.CompanyID=employment.CompanyID",
                ["PersonID", "FirstName", "LastName", "Address", "City", "PostID", "CourseName", "EducationEndDate", "CompanyName", "Employed", "EmployEnd"]);
            foreach (var u in result)
            {
                User user = new()
                {
                    Id = Int32.Parse(u[0]),
                    FirstName = u[1],
                    LastName = u[2],
                    Address = u[3],
                    City = u[4],
                    PostCode = u[5],
                    Education = u[6],
                    EducationEnd = DateTime.Parse(u[7]).ToString("dd MMM yyyy", CultureInfo.InvariantCulture),
                    Company = u[8],
                    Employed = DateTime.Parse(u[9]).ToString("dd MMM yyyy", CultureInfo.InvariantCulture),
                    EmployEnd = DateTime.Parse(u[10]).ToString("dd MMM yyyy", CultureInfo.InvariantCulture)
                };
                    
                UserRepository.users.Add(user);
            }
        }

        public void DrawTable()
        {
            LoadDataFromDatabase();
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
                DrawCell(row, colIndex, rowIndex, cellLeftPosition, rowTopPosition);
                cellLeftPosition += ColumnWidths[colIndex] + 1;
            }
            InsertAt(new Position(cellLeftPosition, rowTopPosition), Borders.Get(BorderPart.Vertical).ToString());
        }

        private void DrawCell(List<string> row, int colIndex, int rowIndex, int cellLeftPosition, int rowTopPosition)
        {
            ConsoleColor fgColor = GetCellColor(rowIndex, colIndex);

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

        private ConsoleColor GetCellColor(int rowIndex, int colIndex)
        {
            if (isFocused && rowIndex == activeRow && colIndex == activeColumn)
                return ConsoleColor.Red;

            if (isFocused && rowIndex == activeRow && colIndex == 0)
                return ConsoleColor.Red;

            if (colIndex == 0 || colIndex >= 11)
                return ConsoleColor.DarkGray;

            return ConsoleColor.Gray;
        }

        private void RedrawCell(int rowIndex, int colIndex, ConsoleColor color)
        {
            int cellLeft = Pos.Left + Enumerable.Range(0, colIndex).Sum(i => ColumnWidths[i] + 1) + 1;
            int cellTop = Pos.Top + 3 + rowIndex;
            string cellContent = Rows[rowIndex][colIndex].PadRight(ColumnWidths[colIndex]);

            Position cellPosition = new(cellLeft, cellTop);
            Dimensions cellDimensions = new(ColumnWidths[colIndex], 1);
            ClearArea(cellPosition, cellDimensions);
            InsertAt(cellPosition, cellContent, color);
        }

        public void ToggleActiveColumn()
        {
            int previousActiveColumn = activeColumn;
            activeColumn = (activeColumn == Headers.Count - 2) ? Headers.Count - 1 : Headers.Count - 2;
            RedrawCell(activeRow, previousActiveColumn, ConsoleColor.DarkGray);
            RedrawCell(activeRow, activeColumn, ConsoleColor.Red);
        }

        public void SetActiveRow(int index)
        {
            activeRow = index;
            activeRow = Math.Max(0, Math.Min(activeRow, Rows.Count - 1));
        }

        private void UpdateActiveRow(int newRow)
        {
            RedrawCell(activeRow, activeColumn, ConsoleColor.DarkGray);
            RedrawCell(activeRow, 0, ConsoleColor.DarkGray);
            activeRow = newRow;
            RedrawCell(activeRow, activeColumn, ConsoleColor.Red);
            RedrawCell(activeRow, 0, ConsoleColor.Red);
        }

        public void MoveActiveRowUp()
        {
            if (activeRow > 0) UpdateActiveRow(activeRow - 1);
        }

        public void MoveActiveRowDown()
        {
            if (activeRow < Rows.Count - 1) UpdateActiveRow(activeRow + 1);
        }

        public void AdjustActiveRowAfterDeletion()
        {
            if (activeRow >= Rows.Count) activeRow = Math.Max(0, Rows.Count - 1);
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

        public void UpdateDataSource(List<User> newUsers)
        {
            this.Rows = ConvertUsersToRows(newUsers);
        }

        public int GetActiveRowID()
        {
            if (activeRow >= 0 && activeRow < Rows.Count)
            {
                return int.Parse(Rows[activeRow][0]);
            }
            else
            {
                throw new InvalidOperationException("No active row is selected or the active row index is out of range.");
            }
        }

        public void RemoveUser(int id)
        {
            Database.RemoveUserWithID(id);
            DrawTable();
        }

    }
}
