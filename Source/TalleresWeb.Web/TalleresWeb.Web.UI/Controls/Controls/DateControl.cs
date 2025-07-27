using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class DateControl : WebControl
    {

        private TextBox textbox = new TextBox();
        private Label label = new Label();
        private ImageButton img = new ImageButton();
        private CalendarExtender calExt = new CalendarExtender();
        private MaskedEditExtender mee = new MaskedEditExtender();
        private RequiredFieldValidator reqField = new RequiredFieldValidator();

        #region Properties

        public String Text
        {
            get
            {
                String valor = String.Empty;
                String tmp = textbox.Text.Replace("/", String.Empty).Replace("_", String.Empty).Trim();
                if (tmp != String.Empty)
                {
                    valor = textbox.Text;
                }

                return valor;
            }
            set { textbox.Text = value; }
        }

        public String LabelText
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        public Boolean EnabledTxt
        {
            get { return this.textbox.Enabled; }
            set { this.textbox.Enabled = value; }
        }

        private Boolean _byYear;
        public Boolean SelectByYear
        {
            get { return _byYear; }
            set { _byYear = value; }
        }

        public Unit WidthTxt
        {
            get
            {
                if (this.textbox.Width != default(Unit))
                    return this.textbox.Width;
                else
                    return new Unit(100);
            }
            set
            {
                this.textbox.Width = value;
            }
        }

        private String _validationGroup;
        public String ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }

        public String cssClass
        {
            get { return this.textbox.CssClass; }
            set { this.textbox.CssClass = value; }
        }

        public String buttonImageURL
        {
            get
            {
                if (!String.IsNullOrEmpty(this.img.ImageUrl))
                    return this.img.ImageUrl;
                else
                    return "/Images/Iconos/calendario.png";
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    this.img.ImageUrl = "/Images/Iconos/calendario.png";
                else
                    this.img.ImageUrl = value;
            }
        }
        #endregion


        public DateControl()
            : base()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            if (_byYear)
            {
                ClientScriptManager cs = Page.ClientScript;
                String cstext1 = @"<script type=text/javascript> function ChangeCalendarView(sender,args){sender._switchMode('years', true);}</script>";
                cs.RegisterStartupScript(this.GetType(), "fecha", cstext1);
                calExt.OnClientShown = "ChangeCalendarView";
            }

            textbox.ReadOnly = false;
            textbox.ID = this.ID + "txt";
            textbox.Width = WidthTxt;
            img.ID = this.ID + "img";
            img.CausesValidation = false;
            img.Height = textbox.Height;
            img.Width = img.Height;
            img.ImageUrl = buttonImageURL;
            calExt.ID = this.ID + "calXtd";
            calExt.PopupButtonID = img.ClientID;
            calExt.TargetControlID = textbox.ClientID;
            calExt.Format = "dd/MM/yyyy";
            calExt.TodaysDateFormat = "dd/MM/yyyy";


            mee.ID = this.ID + "mee";
            mee.TargetControlID = textbox.ClientID;
            mee.MaskType = MaskedEditType.Date;
            mee.Mask = "99/99/9999";
            mee.UserDateFormat = MaskedEditUserDateFormat.DayMonthYear;
            mee.CultureName = "es-ar";
            mee.MessageValidatorTip = false;


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
            cell.VerticalAlign = VerticalAlign.Middle;
            cell.Width = u;
            cell.Controls.Add(textbox);
            cell.Controls.Add(img);
            cell.Controls.Add(calExt);
            cell.Controls.Add(mee);

            row.Cells.Add(cell);
            table.Rows.Add(row);
            this.Controls.Add(table);
            EnsureChildControls();

        }       

    }
}