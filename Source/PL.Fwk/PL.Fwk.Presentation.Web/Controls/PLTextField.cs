using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;
using System.Web.UI;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLTextField : ControlBase
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();


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

        public Boolean EnabledTxt
        {
            get { return this.textbox.Enabled; }
            set { this.textbox.Enabled = value; }
        }

        public Boolean ReadOnly
        {
            get { return this.textbox.ReadOnly; }
            set { this.textbox.ReadOnly = value; }
        }

        public Unit WidthTxt
        {
            get { return this.textbox.Width; }
            set { this.textbox.Width = value; }
        }

        //private int _MaxLength;
        //public int MaxLength
        //{
        //    get { return _MaxLength; }
        //    set { _MaxLength = value; }
        //}

        private int _filas;
        public int Rows
        {
            get { return _filas; }
            set { _filas = value; }
        }
        #endregion




        public PLTextField()
            : base()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            //ClientScriptManager cs = Page.ClientScript;
            //String script = "<script type=text/javascript>function textboxMultilineMaxNumber(txt, maxLen) "+
            //                    "{ if (txt.value.length > (maxLen – 1)) return false; else {return true;}}</script>";
            //cs.RegisterStartupScript(this.GetType(), "fecha", script);


            textbox.ID = this.ID + "txt";
            textbox.Rows = _filas;
            textbox.TextMode = TextBoxMode.MultiLine;
            //textbox.Attributes.Add("onkeypress", "return textboxMultilineMaxNumber(this," + _MaxLength.ToString() + ");");

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


            if (Required)
            {
                PLRequiredFieldValidator rfv = new PLRequiredFieldValidator();
                rfv.ID = this.ID + "rfv";
                //rfv.Text = "Campo Requerido";
                //rfv.Enabled = true;

                //rfv.Display = ValidatorDisplay.Static;
                //rfv.ErrorMessage = "$nbsp; $nbsp; * Campo Requerido";
                rfv.ControlToValidate = textbox.ID;
                //rfv.Visible = true;

                cell.Controls.Add(rfv);
            }

            row.Cells.Add(cell);
            table.Rows.Add(row);
            this.Controls.Add(table);
            EnsureChildControls();

        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    if (this.TextMode == TextBoxMode.MultiLine
        //        && this.MaxLength > 0)
        //    {
                
        //    }

        //    base.Render(writer);
        //}

        public override void InitializeControl()
        {





        }

    }
}
