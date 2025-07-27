using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboPresionDePrueba : DropDownList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control nn");

            LoadData();
        }

        public void LoadData()
        {
            List<ListItem> dt = new List<ListItem>();
            dt.Add(new ListItem("300 Bar", "300"));
            this.DataTextField = "Text";
            this.DataValueField = "Value";
            this.DataSource = dt;
            this.DataBind();
        }
    }
}