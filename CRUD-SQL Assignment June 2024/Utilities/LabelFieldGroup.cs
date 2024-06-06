using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    internal class LabelFieldGroup : Drawable
    {
        private Position StartPos { get; }
        private Dimensions FieldDim { get; }
        private int VerticalSpacing { get; }
        private int InputFields { get; }
        private int ComboBoxes { get; }
        private Position? NextStartPos { get; set; }

        public LabelFieldGroup(Position pos, Dimensions dim, int spacing, int inputFields, List<string>? labels = null) : base(pos, dim)
        {
            this.StartPos = pos;
            this.FieldDim = dim;
            this.VerticalSpacing = spacing;
            this.InputFields = inputFields;

            if (labels != null)
            {
                CreateFields(labels);
            }
        }
        public void CreateFields(List<string> labels)
        {
            if (labels == null || labels.Count == 0)
                throw new ArgumentException("Labels collection cannot be null or empty.", nameof(labels));

            Position currentPos = StartPos;

            for (int i = 0; i < labels.Count; i++)
            {
                var label = labels[i];

                // -- Labels
                _ = new Textfield(
                    pos: currentPos,
                    dim: FieldDim,
                    text: label,
                    align: Alignment.Center);

                bool isComboBox = i >= InputFields && i < InputFields + ComboBoxes;

                // -- Borders
                if (!isComboBox)
                {
                    // -- Input Field Border
                    Position borderPos = new(
                        currentPos.Left + FieldDim.Width,
                        currentPos.Top - Margins.BorderVerticalMarginSingle);
                    Dimensions borderDim = new(
                            Margins.DialogBoxWidth / 2 - Margins.BorderHorizontalMarginDouble,
                            Margins.ButtonHeight);
                    _ = new Box(
                        pos: borderPos,
                        dim: borderDim);

                    currentPos = new Position(
                            currentPos.Left,
                            currentPos.Top + FieldDim.Height + VerticalSpacing);
                }
                else
                {
                    // -- ComboBox Border
                    Position borderPos = new(
                        currentPos.Left + FieldDim.Width,
                        currentPos.Top - Margins.BorderVerticalMarginSingle);
                    Dimensions borderDim = new(
                            Margins.DialogBoxWidth / 2 - Margins.ButtonHeight - Margins.MainBoxBorderMargin,
                            Margins.ButtonHeight);
                    _ = new Box(
                        pos: borderPos,
                        dim: borderDim);

                    Position borderDropPos = new(
                        currentPos.Left + FieldDim.Width + Margins.DialogBoxWidth / 2 - Margins.ComboBoxDropMargin,
                        currentPos.Top - Margins.BorderVerticalMarginSingle);
                    Dimensions borderDropDim = new(
                            Margins.ButtonHeight + Margins.BorderHorizontalMarginDouble,
                            Margins.ButtonHeight);
                    _ = new Box(
                        pos: borderDropPos,
                        dim: borderDropDim);

                    Position dropDownIcon = new(
                        borderDropPos.Left + (borderDropDim.Width / 2),
                        currentPos.Top);
                    Dimensions dropDownDim = new(
                            Margins.ButtonHeight - Margins.BorderHorizontalMarginDouble,
                            Margins.ButtonHeight - Margins.BorderVerticalMarginDouble);

                    _ = new Textfield(
                        pos: dropDownIcon,
                        dim: dropDownDim,
                        text: Borders.Get(BorderPart.DownArrow).ToString(),
                        align: Alignment.Center);
                    currentPos = new Position(
                            currentPos.Left,
                            currentPos.Top + FieldDim.Height + VerticalSpacing);
                }
                NextStartPos = currentPos;
            }
        }
        public Position GetNextStartPosition() => NextStartPos!;
    }
}
