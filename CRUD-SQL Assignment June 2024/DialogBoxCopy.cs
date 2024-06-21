using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class DialogBoxCopy : Box, IHasDimensions, IHasPosition
    {
        private readonly InputFieldGroup inputFields;
        private readonly InputFieldGroup inputFieldsEducationEnd;
        private readonly InputFieldGroup inputFieldsEmploy;
        private readonly Table? table;
        private readonly string postCode;
        private readonly string education;
        private readonly string company;
        public DialogBoxCopy(Dimensions dim, Position pos, Alignment? align, string text, Table? table = null)
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
            LabelFieldGroup labelFields = new(
                pos: labelGroupStartPos,
                dim: labelFieldDim,
                spacing: Margins.BorderVerticalMarginDouble,
                inputFields: 9,
                labels: ["First Name", "Last Name", "Address", "Post code", "Education", "Education Ended", "Company", "Employed", "Ended"]);
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

            List<object> fields = []; 

            // -- Input Field for FirstName, LastName, Address
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
                labels: ["First Name", "Last Name", "Address"]);
            Position nextStartPosInputFields1 = inputFields.GetNextStartPosition();

            // -- ComboBox for PostCodes
            Position comboBoxPos = new(
                    nextStartPosInputFields1.Left + inputFieldDim.Width,
                    nextStartPosInputFields1.Top - Margins.BorderVerticalMarginSingle);
            Dimensions comboBoxDim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            ComboBox comboBoxPostCodes = new(
                pos: comboBoxPos,
                dim: comboBoxDim,
                options: Database.GetPostalCodes());
            comboBoxPostCodes.CaptureInput();
            postCode = comboBoxPostCodes.SelectedOption;

            // -- ComboBox for Education
            Position comboBoxPos2 = new(
                    nextStartPosInputFields1.Left + inputFieldDim.Width,
                    nextStartPosInputFields1.Top + Margins.ComboBoxHeight + Margins.BorderVerticalMarginDouble);
            Dimensions comboBoxDim2 = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            ComboBox comboBoxEducation = new(
                pos: comboBoxPos2,
                dim: comboBoxDim2,
                options: Database.GetPostalCodes());
            comboBoxEducation.CaptureInput();
            education = comboBoxEducation.SelectedOption;

            // -- Input Field for Education End
            Position inputGroup2StartPos = new(
                    nextStartPosInputFields1.Left,
                    nextStartPosInputFields1.Top + (Margins.ComboBoxHeight * 2) + Margins.BorderVerticalMarginDouble);
            Dimensions inputField2Dim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            inputFieldsEducationEnd = new(
               pos: inputGroup2StartPos,
               dim: inputField2Dim,
               spacing: Margins.BorderVerticalMarginDouble,
               labels: ["Education End"]);
            Position nextStartPosInputFields2 = inputFieldsEducationEnd.GetNextStartPosition();

            // -- ComboBox for Companies
            Position comboBoxCompanyPos = new(
                    nextStartPosInputFields2.Left + inputFieldDim.Width,
                    nextStartPosInputFields2.Top - Margins.BorderVerticalMarginSingle);
            Dimensions comboBoxCompanyDim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            ComboBox comboBoxCompanies = new(
                pos: comboBoxCompanyPos,
                dim: comboBoxCompanyDim,
                options: Database.GetCompanies());
            comboBoxCompanies.CaptureInput();
            company = comboBoxCompanies.SelectedOption;

            // -- Input Field Group
            Position inputGroup3StartPos = new(
                    nextStartPosInputFields2.Left,
                    nextStartPosInputFields2.Top + Margins.ComboBoxHeight + Margins.BorderVerticalMarginDouble);
            Dimensions inputField3Dim = new(
                    Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                    Margins.ComboBoxHeight);
            inputFieldsEmploy = new(
               pos: inputGroup3StartPos,
               dim: inputField3Dim,
               spacing: Margins.BorderVerticalMarginDouble,
               labels: ["Employed", "Employ Ended"]);

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

        public List<string> GetAllInputs()
        {
            List<string> allInputs = new();

            // Get inputs from the first input field group
            allInputs.AddRange(inputFields.GetAllInputs());
            // Insert the postCode string
            allInputs.Insert(allInputs.Count, postCode);
            // Insert the Education string
            allInputs.Insert(allInputs.Count, education);
            // Get inputs from the education end input field
            allInputs.AddRange(inputFieldsEducationEnd.GetAllInputs());
            // Insert the Company string
            allInputs.Insert(allInputs.Count, company);
            // Get inputs from the employment input fields
            allInputs.AddRange(inputFieldsEmploy.GetAllInputs());

            return allInputs;
        }

        public void AddUserFromInputs()
        {
            List<string> inputs = GetAllInputs();
            
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
