using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class CaptureInputField : Drawable, IHasDimensions, IHasPosition
    {
        private bool isFocused = false;
        private readonly StringBuilder buffer = new();
        public Position FieldPos { get; private set; }
        public Dimensions FieldDim { get; private set; }
        public int? MaxLength { get; set; }
        public CaptureInputField(Position pos, Dimensions dim, int? maxLength = null)
            : base(pos, dim)
        {
            this.FieldPos = pos;
            this.FieldDim = dim;
            this.MaxLength = maxLength;

            CaptureInput();

        }

        public void CaptureInput()
        {
            if (!isFocused) return;

            Console.SetCursorPosition(FieldPos.Left, FieldPos.Top);
            Console.CursorVisible = true;
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && buffer.Length > 0)
                {
                    buffer.Remove(buffer.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    if (!MaxLength.HasValue || buffer.Length < MaxLength.Value)
                    {
                        buffer.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);
                    }
                }
            } while (isFocused);
        }

        public void SetFocus()
        {
            isFocused = true;
            Console.SetCursorPosition(FieldPos.Left, FieldPos.Top);
            Console.CursorVisible = true;
        }

        public void RemoveFocus()
        {
            isFocused = false;
            Console.CursorVisible = false;
        }

        public string GetInput()
        {
            return buffer.ToString();
        }

        public void ClearInput()
        {
            buffer.Clear();
        }
    }
}
