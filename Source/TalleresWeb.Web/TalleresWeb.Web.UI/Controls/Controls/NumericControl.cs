using System;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class NumericControl : WebControl
    {
        #region Members

        private Label label = new Label();

        private TextBox textbox = new TextBox();

        #endregion

        #region Events

        public event EventHandler OnTextChanged;

        #endregion

        #region Enums

        public enum Formatos
        {
            Enteros,
            DosDecimales,
            CuatroDecimales
        }

        #endregion

        #region Properties

        public Boolean AcceptNegatives { get; set; }

        public String CssTxtClass { get; set; }

        public Formatos Formato { get; set; }

        public String LabelText { get; set; }

        public Int32 MaxLenghtTxt
        {
            get { return this.textbox.MaxLength; }
            set { this.textbox.MaxLength = value; }
        }

        public String Text
        {
            get { return textbox.Text; }
            set { textbox.Text = value; }
        }

        public Unit WidthTxt
        {
            get { return this.textbox.Width; }
            set { this.textbox.Width = value; }
        }

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            textbox.ID = this.ID + "txt";

            textbox.CssClass = this.DeterminarClase();

            textbox.CssClass += " " + "text-right";

            if (!String.IsNullOrEmpty(CssTxtClass))
                textbox.CssClass = textbox.CssClass + " " + this.CssTxtClass;

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
            row.Cells.Add(cell);
            table.Rows.Add(row);
            this.Controls.Add(table);
            EnsureChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        private String DeterminarClase()
        {
            String clase = String.Empty;
            switch (this.Formato)
            {
                case Formatos.Enteros:
                    clase = "numerico";
                    break;

                case Formatos.DosDecimales:
                    clase = "dosDecimales";
                    break;

                default:
                    clase = "cuatroDecimales";
                    break;
            }

            return this.AcceptNegatives ? clase + "Negativo" : clase;
        }

        private void TextBox_TextChanged(Object sender, EventArgs e)
        {
            if (OnTextChanged != null)
            {
                OnTextChanged(sender, e);
            }
        }

        #endregion
    }
}