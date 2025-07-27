using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnCancelar : ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Cancelar";
            this.ImageUrl = "~/Imagenes/Iconos/cancelar.png";
            this.ToolTip= "Cancelar"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "cancelar";
            this.OnClientClick = "return confirm ('Desea cancelar?');";
        }
    }
}