using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;
using System.Drawing;
using System.Web.UI;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLTextBox : ControlBase
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();

        #region Members
        public event EventHandler OnTextChanged;
        #endregion

        #region Properties

        public String Text
        {
            get { return textbox.Text; }
            set { textbox.Text = value; }
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

        public Boolean AutoPostBack
        {
            get { return this.textbox.AutoPostBack; }
            set { this.textbox.AutoPostBack = value; }
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

        public Unit BorderWidth
        {
            get { return this.textbox.BorderWidth; }
            set { this.textbox.BorderWidth = value; }
        }

        private String _validationGroup;
        public String ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }

        private ClientIDMode _ClientIDMode;
        public ClientIDMode ClientIDMode
        {
            get { return this._ClientIDMode; }
            set { this._ClientIDMode = value; }
        }
        #endregion


        public PLTextBox()
            : base()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            textbox.ID = this.ID + "txt";

            if (this.ClientIDMode != null) this.textbox.ClientIDMode = this.ClientIDMode;

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
            textbox.TextChanged += new EventHandler(TextBox_TextChanged);
            cell.Controls.Add(textbox);
           
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

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (OnTextChanged != null)
            {
                OnTextChanged(sender, e);
            }
        }


        public override void InitializeControl()
        {
        }

    }
}
