using System.Text;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;


            // -- Main Window Border
            Dimensions mainBoxDim = new(Console.WindowWidth, Console.WindowHeight);
            Position mainBoxPos = new(0, 0);
            Box MainBox = new(
                    dim: mainBoxDim,
                    pos: mainBoxPos);

            // -- Main Window Title
            _ = new Textfield(
                    new Position(2, 1),
                    dim: mainBoxDim,
                    text: "CRUDapp",
                    align: Alignment.Left,
                    FG: ConsoleColor.Magenta);

            // -- Main Window Button for creating new user
            Position newUserButtonPos = new(
                        MainBox.Pos.Left + Console.WindowWidth - Margins.ButtonWidth - Margins.BorderHorizontalMarginDouble,
                        MainBox.Pos.Top + Margins.BorderVerticalMarginSingle);
            Dimensions newUserButtonDim = new(
                        Margins.ButtonWidth,
                        Margins.ButtonHeight);
            Button newUserButton = new(
                    pos: newUserButtonPos,
                    dim: newUserButtonDim,
                    label: "New User",
                    align: Alignment.Center,
                    isFocused: true);

            // -- Main Table
            Position tablePos = new(
                    mainBoxPos.Left + Margins.BorderHorizontalMarginDouble,
                    mainBoxPos.Top + Margins.TableTopMargin);
            Dimensions tableDim = new(
                    mainBoxDim.Width - Margins.MainBoxBorderMargin,
                    mainBoxDim.Height - Margins.TableBottomMargin);
            List<string> headers = ["ID", "First Name", "Last Name", "Address", "City", "Post code", "Education", "Education Ended", "Company", "Employed", "Ended", "Delete", "Edit"];
            Dictionary<int, int> columnAdjustments = new() { { 0, 4 }, { 11, 4 }, { 12, 4 } };
            Table table = new(
                    pos: tablePos,
                    dim: tableDim,
                    headers: headers,
                    columnAdjustments: columnAdjustments);

            // -- New User Window Popup
            /*void NewUser()
            {
                Dimensions newUserDialogBoxDim = new(
                        Margins.DialogBoxWidth,
                        Math.Min(Margins.DialogBoxHeight, Console.WindowHeight));
                Position newUserDialogBoxPos = new(
                            (Console.WindowWidth - Margins.DialogBoxWidth) / 2,
                            (Console.WindowHeight - Math.Min(Margins.DialogBoxHeight, Console.WindowHeight)) / 2);
                _ = new DialogBox(
                        dim: newUserDialogBoxDim,
                        pos: newUserDialogBoxPos,
                        align: Alignment.Center,
                        text: "Create New User",
                        labelsInput: ["First Name", "Last Name", "Address", "Post code", "Education", "Education Ended", "Company", "Employed", "Employ Ended"],
                        table: table);
            }*/

            void NewUser()
            {
                Dimensions newUserDialogBoxDim = new(
                        Margins.DialogBoxWidth,
                        Math.Min(Margins.DialogBoxHeight, Console.WindowHeight));
                Position newUserDialogBoxPos = new(
                            (Console.WindowWidth - Margins.DialogBoxWidth) / 2,
                            (Console.WindowHeight - Math.Min(Margins.DialogBoxHeight, Console.WindowHeight)) / 2);
                _ = new DialogBoxCopy(
                        dim: newUserDialogBoxDim,
                        pos: newUserDialogBoxPos,
                        align: Alignment.Center,
                        text: "Create New User",
                        table: table);
            }

            while (true)
            {
                switch(Console.ReadKey().Key) {
                    case ConsoleKey.Tab:
                        if(!newUserButton.isFocused && table.isFocused)
                        {
                            newUserButton.isFocused = true;
                            newUserButton.DrawButton();
                            table.isFocused = false;
                            table.DrawTable();
                        }
                        else if(newUserButton.isFocused && !table.isFocused)
                        {
                            newUserButton.isFocused = false;
                            newUserButton.DrawButton();
                            table.isFocused = true;
                            table.DrawTable();
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        if (table.isFocused)
                        {
                            table.ToggleActiveColumn();
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (table.isFocused)
                        {
                            table.MoveActiveRowUp();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (table.isFocused)
                        {
                            table.MoveActiveRowDown();
                        }
                        break;
                    case ConsoleKey.Enter:
                        if(newUserButton.isFocused)
                        {
                            NewUser();
                        }
                        else if (table.isFocused && table.activeColumn == table.Headers.IndexOf("Delete"))
                        {
                            table.RemoveUser(table.GetActiveRowID());
                            table.AdjustActiveRowAfterDeletion();
                            table.DrawTable();
                        }
                        break;
                }
            }



            // -- Edit Window Popup
            /*static void EditUser()
            {
                Dimensions EditUserDialogBoxDim = new(
                        Margins.DialogBoxWidth,
                        Math.Min(Margins.DialogBoxHeight, Console.WindowHeight));
                Position EditUserDialogBoxPos = new(
                            (Console.WindowWidth - Margins.DialogBoxWidth) / 2,
                            (Console.WindowHeight - Math.Min(Margins.DialogBoxHeight, Console.WindowHeight)) / 2);
                _ = new DialogBox(
                        dim: EditUserDialogBoxDim,
                        pos: EditUserDialogBoxPos,
                        align: Alignment.Center,
                        text: "Edit User",
                        labelsInput: ["First Name", "Last Name", "Email", "Phone", "Address"],
                        labelsComboBox: ["Title"],
                        options: ["Dev", "DevOps", "Support", "UX", "CEO"]);
            }*/
        }
    }
}
