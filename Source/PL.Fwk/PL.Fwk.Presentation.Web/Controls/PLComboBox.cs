using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PL.Fwk.Entities;
using System.Drawing;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLComboBox:ControlBase
    {
        #region Members
        public event EventHandler OnSelectedIndexChange;
        #endregion

        #region Properties
        private Boolean _automaticLoad;
        public Boolean AutomaticLoad
        {
            get { return _automaticLoad; }
            set { _automaticLoad = value; }
        }

        public Boolean AutoPostback
        {
            get { return this.cbo.AutoPostBack; }
            set { this.cbo.AutoPostBack = value; }
        }

        private String _valueMember;

        public String ValueMember
        {
            get { return _valueMember; }
            set { _valueMember = value; }
        }

        private String _displayMember;
        public String DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        private Label label;
        private DropDownList cbo;

        public String LabelText 
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        public PLComboBox()
        {
            DisplayMember = ViewEntity.DescripcionPropertieName;
            ValueMember = ViewEntity.IDPropertieName;
            label = new Label();
            cbo = new DropDownList(); 
        }

        public List<ViewEntity> DataSource {
            get
            {


                return (List<ViewEntity>)this.cbo.DataSource;
            }
            set
            {
                this.cbo.DataValueField = this.ValueMember;
                this.cbo.DataTextField = this.DisplayMember;
                this.cbo.DataSource = value;
                this.cbo.DataBind();
            }
        }

        public Guid SelectedValue
        {
            get
            {
                return new Guid(this.cbo.SelectedValue);
            }
            set
            {
                try
                {
                    this.cbo.SelectedValue = value.ToString();
                }
                catch
                {
                    this.cbo.SelectedIndex = -1;
                }
            }
        }

        public String SelectedValueString
        {
            get
            {
                return this.cbo.SelectedValue.ToString();
            }
            set
            {
                this.cbo.SelectedValue = value.ToString();
            }
        }

        public String SelectedText
        {
            get
            {
                return this.cbo.SelectedItem.Text;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.cbo.SelectedIndex;
            }
            set
            {
                this.cbo.SelectedIndex = value;
            }
        }

        public Color BorderColor
        {
            get { return this.cbo.BorderColor; }
            set { this.cbo.BorderColor = value; }
        }

        public Unit WidthCbo
        {
            get
            {
                return this.cbo.Width;
            }
            set 
            {
                if (value == null)
                {
                    cbo.Width = Unit.Pixel(230);
                }
                else 
                { 
                    cbo.Width = value;
                }
            }
        }

        #endregion
         
        public override void InitializeControl()
        {
            this.cbo.DataValueField = this.ValueMember;
            this.cbo.DataTextField = this.DisplayMember;

            //cbo.Style.Add("max-width", "230px");

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
            this.cbo.SelectedIndexChanged += new EventHandler(cbo_SelectedIndexChanged);
            cell.Controls.Add(cbo);
            row.Cells.Add(cell);
            table.Rows.Add(row);

            this.Controls.Add(table);

            if (AutomaticLoad)
            {
                LoadData();
            }
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnSelectedIndexChange != null)
            {
                OnSelectedIndexChange(sender, e);
            }
        }

        public virtual void LoadData()
        {
            throw new NotImplementedException();
        }

    }
}
