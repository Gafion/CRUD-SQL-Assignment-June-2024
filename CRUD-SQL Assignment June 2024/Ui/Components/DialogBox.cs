﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class DialogBox : Box, IHasDimensions, IHasPosition
    {
        private readonly InputFieldGroup inputFields;
        private readonly Table? table;
        public DialogBox(Dimensions dim, Position pos, Alignment? align, string text, List<string> labelsInput, Table? table = null)
            : base(dim, pos)
        {
            if(table != null) this.table = table;
            ConsoleKeyInfo keyInfo;

            // -- DialogBox Border
            _ = new Box(
                    dim,
                    pos);

            // -- DialogBox Title
            Position textFieldPosition = new(
                        pos.Left + Margins.BorderHorizontalMarginDouble,
                        pos.Top + Margins.BorderVerticalMarginSingle);
            Dimensions textFieldDimensions = new(dim);
            textFieldDimensions.Width -= Margins.BorderHorizontalMarginDouble;
            textFieldDimensions.Height -= Margins.BorderHorizontalMarginDouble;
            _ = new Textfield(

                    pos: textFieldPosition,
                    dim: textFieldDimensions,
                    text: text,
                    align: align);

            // -- Label Field Group
            Position labelGroupStartPos = new(
                    pos.Left + Margins.BorderHorizontalMarginDouble,
                    pos.Top + textFieldPosition.Top);
            Dimensions labelFieldDim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            List<string> combinedLabels = new(labelsInput);
            LabelFieldGroup labelFields = new(
                pos: labelGroupStartPos,
                dim: labelFieldDim,
                spacing: Margins.BorderVerticalMarginDouble,
                inputFields: labelsInput.Count,
                labels: combinedLabels);
            Position nextStartPosLabelFields = labelFields.GetNextStartPosition();

            // -- Accept and Cancel buttons
            int halfButtonWidth = Margins.ButtonWidth / 2;
            int centerLeftHalf = pos.Left + dim.Width / 4;
            int centerRightHalf = pos.Left + 3 * dim.Width / 4;

            Position cancelButtonPos = new(
                centerLeftHalf - halfButtonWidth,
                nextStartPosLabelFields.Top);
            Dimensions cancelButtonDim = new(
                Margins.ButtonWidth, Margins.ButtonHeight);
            Button cancelButton = new(
                pos: cancelButtonPos,
                dim: cancelButtonDim,
                label: "Cancel",
                align: Alignment.Center);

            Position acceptButtonPos = new(
                centerRightHalf - halfButtonWidth,
                nextStartPosLabelFields.Top);
            Dimensions acceptButtonDim = new(
                Margins.ButtonWidth, Margins.ButtonHeight);
            Button acceptButton = new(
                pos: acceptButtonPos,
                dim: acceptButtonDim,
                label: "Accept",
                align: Alignment.Center);

            // -- Input Field Group
            Position inputGroupStartPos = new(
                    pos.Left + Margins.BorderHorizontalMarginDouble,
                    pos.Top + textFieldPosition.Top);
            Dimensions inputFieldDim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);

             inputFields = new(
                pos: inputGroupStartPos,
                dim: inputFieldDim,
                spacing: Margins.BorderVerticalMarginDouble,
                labels: labelsInput);


            //Position nextStartPosInputFields = inputFields.GetNextStartPosition();

            // -- ComboBox Group
            /*Position comboBoxPos = new(
                    nextStartPosInputFields.Left,
                    nextStartPosInputFields.Top);
            Dimensions comboBoxDim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            ComboBoxGroup comboBoxes = new(
                pos: comboBoxPos,
                dim: comboBoxDim,
                labels: labelsComboBox,
                options: options,
                spacing: Margins.BorderVerticalMarginDouble);
            Position nextStartPosComboBoxes = comboBoxes.GetNextStartPosition();*/

            Console.CursorVisible = false;
            acceptButton.FocusToggle(true);
            acceptButton.DrawButton();

            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (acceptButton.isFocused)
                    {
                        AcceptPress();
                        return;
                    }
                    else if (cancelButton.isFocused)
                    {
                        CancelPress();
                        return;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    acceptButton.FocusToggle(false);
                    cancelButton.FocusToggle(true);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    cancelButton.FocusToggle(false);
                    acceptButton.FocusToggle(true);
                }
            } while (true);
        }

        void CancelPress()
        {
            ClearArea(Pos, Dim);
            table?.DrawTable();
        }

        public void AddUserFromInputs()
        {
            List<string> inputs = inputFields.GetAllInputs();
            
            List<List<string>> result = Database.Read1($"SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'person';", ["AUTO_INCREMENT"]);
            int personID = Int32.Parse(result[0][0] ?? "0");

            User user = new()
            {
                Id = personID,
                FirstName = inputs[0],
                LastName = inputs[1],
                Address = inputs[2],
                City = "",
                PostCode = inputs[3],
                Education = inputs[4],
                EducationEnd = inputs[5],
                Company = inputs[6],
                Employed = inputs[7],
                EmployEnd = inputs[8]
            };
            UserRepository.users.Add(user);
            Database.AddUser(user);
            table?.DrawTable();
            
        }

        void AcceptPress()
        {
            AddUserFromInputs(); // Add user from captured inputs
            table?.UpdateDataSource(UserRepository.GetAllUsers());
            ClearArea(Pos, Dim);
            table?.DrawTable();
        }
    }
}
