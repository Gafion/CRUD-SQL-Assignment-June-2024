using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class InputFieldGroup : Drawable
    {
        private Position StartPos { get; set; }
        private Dimensions FieldDim { get; set; }
        private int VerticalSpacing { get; set; }
        private List<CaptureInputField> InputFields { get; set; }
        private Position? NextStartPos { get; set; }

        public InputFieldGroup(Position pos, Dimensions dim, int spacing, List<string> labels) : base(pos, dim)
        {
            this.StartPos = pos;
            this.FieldDim = dim;
            this.VerticalSpacing = spacing;
            this.InputFields = [];

            CreateFields(labels);
        }
        public void CreateFields(List<string> labels)
        {
            if (labels == null || labels.Count == 0)
                throw new ArgumentException("Labels collection cannot be null or empty.", nameof(labels));

            Position currentPos = StartPos;

            foreach (var label in labels)
            {
                // -- Input Fields
                Position inputFieldPos = new(
                        currentPos.Left + FieldDim.Width + Margins.BorderHorizontalMarginSingle,
                        currentPos.Top);
                CaptureInputField inputField = new(
                    pos: inputFieldPos,
                    dim: FieldDim,
                    maxLength: FieldDim.Width - Margins.BorderHorizontalMarginSingle);
                InputFields.Add(inputField);

                currentPos = new Position(
                        currentPos.Left,
                        currentPos.Top + FieldDim.Height + VerticalSpacing);
            }
            NextStartPos = currentPos;
        }

        public Position GetNextStartPosition() => NextStartPos!;

        public void CaptureAllInputs()
        {
            foreach (var inputField in InputFields)
            {
                inputField.CaptureInput();
            }
        }

        public List<string> GetAllInputs()
        {
            List<string> inputs = [];
            foreach (var inputField in InputFields)
            {
                inputs.Add(inputField.GetInput());
            }
            return inputs;
        }
    }
}
