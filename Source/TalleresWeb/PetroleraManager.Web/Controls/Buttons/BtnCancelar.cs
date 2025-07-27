using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{ 
    public class BtnCancelar : Button
    {
        protected override void OnInit(EventArgs e)
        { 
            this.UseSubmitBehavior = false;
            this.Text = !String.IsNullOrEmpty(this.Text) ? "       " + this.Text : "       Cancelar";
            this.Height = Unit.Pixel(35);
            this.Style.Add("background", "transparent url(/Imagenes/Iconos/bloqueada.png) center left no-repeat;");
        }
    }
}