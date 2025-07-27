using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls.Validators;
using System.Drawing;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLLabelLabel : ControlBase
    {
        private Label labelTitulo = new Label();
        private Label labelValor = new Label();

        #region Properties

        public String Text
        {
            get { return labelValor.Text; }
            set { labelValor.Text = value; }
        }

        public String LabelText
        {
            get { return this.labelTitulo.Text; }
            set { this.labelTitulo.Text = value; }
        }
        
        public Unit WidthValor
        {
            get { return this.labelValor.Width; }
            set { this.labelValor.Width = value; }
        }

        #endregion




        public PLLabelLabel()
            : base()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            labelValor.ID = this.ID + "txt";
            labelValor.Font.Bold  = true;

            Table table = new Table();
            TableRow row = new TableRow();

            table.Width = Unit.Percentage(100);
            
            TableCell cell = new TableCell();

            Unit u = Unit.Percentage(100);
            if ((labelTitulo.Text != String.Empty) && (labelTitulo.Text != null))
            {
                u = Unit.Percentage(50);
                cell.Controls.Add(labelTitulo);
                cell.Width = u;
                row.Cells.Add(cell);
            }
            cell = new TableCell();
            cell.Width = u;
            cell.Controls.Add(labelValor);
           

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
