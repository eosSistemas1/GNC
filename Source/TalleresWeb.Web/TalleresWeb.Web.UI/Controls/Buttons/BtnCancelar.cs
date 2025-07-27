using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{ 
    public class BtnCancelar : Button
    {
        protected override void OnInit(EventArgs e)
        {
            this.CssClass = "btn btn-danger";
            this.UseSubmitBehavior = false;
            this.Text = !String.IsNullOrEmpty(this.Text) ? this.Text : "Cancelar";                       
        }
    }
}