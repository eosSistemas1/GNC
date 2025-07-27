using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{ 
    public class BtnDefault : Button
    {
        public Boolean DisableOnClick {
            set
            {
                if ((value == null) || (value))
                {
                    this.OnClientClick = "this.disabled=true";
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.CssClass = "btn btn-default";
            this.UseSubmitBehavior = false;
            this.Text = !String.IsNullOrEmpty(this.Text) ? this.Text : "Default";
        }
    }
}