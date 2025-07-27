using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;
using System.Drawing;
using AjaxControlToolkit;
using System.Web.UI;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLTextBoxMasked : ControlBase
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();
        private MaskedEditExtender mee = new MaskedEditExtender();

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

        public Boolean AutoPostBack
        {
            get { return this.textbox.AutoPostBack; }
            set { this.textbox.AutoPostBack = value; }
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

        private String _mask;
        public String Mask
        {
            get { return _mask; }
            set { _mask = value; }
        }
        private MaskedEditType _maskType;
        public MaskedEditType MaskType
        {
            get { return _maskType; }
            set { _maskType = value; }
        }

        public Boolean ClearMaskOnLostFocus
        {
            get { return mee.ClearMaskOnLostFocus; }
            set { mee.ClearMaskOnLostFocus = value; }
        }

        private MaskedEditShowSymbol _AcceptNegative;
        public MaskedEditShowSymbol? AcceptNegative
        {
            get { return _AcceptNegative; }
            set {
                if (value.HasValue)
                    _AcceptNegative = value.Value;
                else
                    _AcceptNegative = MaskedEditShowSymbol.None;
                }
        }

        private MaskedEditInputDirection _inputDirection;
        public MaskedEditInputDirection InputDirection
        {
            get { return _inputDirection; }
            set { _inputDirection = value; }
        }

        private String _CssClass;
        public String CssTxtClass 
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }

        private ClientIDMode _ClientIDMode;
        public ClientIDMode ClientIDMode
        {
            get { return this._ClientIDMode; }
            set { this._ClientIDMode = value; }
        }

        #endregion

        public PLTextBoxMasked() : base()
        {

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (OnTextChanged!= null)
            {
                OnTextChanged(sender, e);
            }
        }

        public override void InitializeControl()
        {
            textbox.ID = this.ID + "txt";
            if (!String.IsNullOrEmpty(CssTxtClass)) textbox.CssClass = CssTxtClass;
            if (_mask != null)
            {
                mee.UserDateFormat = MaskedEditUserDateFormat.DayMonthYear;
                mee.TargetControlID = textbox.ClientID;
                mee.InputDirection = _inputDirection != null? _inputDirection : MaskedEditInputDirection.LeftToRight;
                mee.MaskType = _maskType;
                mee.ID = this.ID + "mee";
                mee.Mask = _mask;
                if ((_maskType == MaskedEditType.Date) || (_maskType == MaskedEditType.DateTime)) mee.UserDateFormat = MaskedEditUserDateFormat.DayMonthYear;
                mee.AcceptNegative = _AcceptNegative != null ? _AcceptNegative : MaskedEditShowSymbol.None;
            }

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
            if (_mask != String.Empty) cell.Controls.Add(mee);

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
