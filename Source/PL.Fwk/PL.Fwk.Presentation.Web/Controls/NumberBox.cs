using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;


namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLNumberBox:ControlBase
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();
         

        #region Properties

        public Decimal Text
        {
            get { return Decimal.Parse(textbox.Text); }
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
            set { _isRequired = value ; }
        }

        public Boolean EnabledTxt {
            get { return this.textbox.Enabled; }
            set { this.textbox.Enabled = value; }
        }
        #endregion




        public PLNumberBox()
            : base()
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            textbox.ID = this.ID + "txt";
           

            Table table = new Table();
            TableRow row = new TableRow();

            table.Width = Unit.Percentage(100);

            TableCell cell = new TableCell();
            cell.Controls.Add(label);
            cell.Width = Unit.Percentage(50);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Width = Unit.Percentage(50);
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

        
        public override void InitializeControl()
        {


        

          
        }

    }
}
