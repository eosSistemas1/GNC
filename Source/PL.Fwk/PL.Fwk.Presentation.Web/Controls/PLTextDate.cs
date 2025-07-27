using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;
using System.Drawing;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLTextDate : ControlBase
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();


        #region Properties

        public DateTime Text
        {
            get { return DateTime.Parse(textbox.Text); }
            set { textbox.Text = value.ToString(); }
        }

        public String LabelText
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }


        private Boolean _isRequired;
        public Boolean Required
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public Boolean EnabledTxt
        {
            get { return this.textbox.Enabled; }
            set { this.textbox.Enabled = value; }
        }

        public Boolean ReadOnlyTxt
        {
            get { return this.textbox.ReadOnly; }
            set { this.textbox.ReadOnly = value; }
        }

        public Unit WidthTxt
        {
            get { return this.textbox.Width; }
            set { this.textbox.Width = value; }
        }

        public int MaxLenghtTxt
        {
            get { return this.textbox.MaxLength; }
            set { this.textbox.MaxLength = value; }
        }

        public Color BorderColor
        {
            get { return this.textbox.BorderColor; }
            set { this.textbox.BorderColor = value; }
        }

        private String _validationGroup;
        public String ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }
        #endregion

        public PLTextDate()
            : base()
        {

        }

        public override void InitializeControl()
        {
            textbox.ID = this.ID + "txt";

            if (_validationGroup != null) textbox.ValidationGroup = _validationGroup;

            Table table = new Table();
            TableRow row = new TableRow();

            table.Width = Unit.Percentage(100);

            TableCell cell = new TableCell();

            Unit u = Unit.Percentage(100);
            if ((label.Text != String.Empty) && (label.Text != null))
            {
                u = Unit.Percentage(50);
                cell.Controls.Add(label);
                cell.Width = u;
                row.Cells.Add(cell);
            }
            cell = new TableCell();
            cell.Width = u;
            cell.Controls.Add(textbox);

            PLCompareValidator cv = new PLCompareValidator();
            cv.ID = this.ID + "cv";
            cv.ControlToValidate = textbox.ID;
            cv.Display = ValidatorDisplay.Dynamic;
            cv.ErrorMessage = "*";
            cv.Operator = ValidationCompareOperator.DataTypeCheck;
            cv.Type = ValidationDataType.Date;
            cell.Controls.Add(cv);

            if (Required)
            {
                PLRequiredFieldValidator rfv = new PLRequiredFieldValidator();
                rfv.ID = this.ID + "rfv";

                rfv.ControlToValidate = textbox.ID;
                rfv.Display = ValidatorDisplay.Dynamic;
                rfv.ErrorMessage = " * ";

                cell.Controls.Add(rfv);
            }

            row.Cells.Add(cell);
            table.Rows.Add(row);
            this.Controls.Add(table);
            EnsureChildControls();

        }

    }

}

