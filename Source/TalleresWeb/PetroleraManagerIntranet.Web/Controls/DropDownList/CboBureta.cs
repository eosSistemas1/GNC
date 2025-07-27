using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboBureta : DropDownList
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
            dt.Add(new ListItem("1", "1"));
            dt.Add(new ListItem("2", "2"));

            this.DataSource = dt;
            this.DataBind();
        }
    }
}