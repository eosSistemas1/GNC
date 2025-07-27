using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnBuscar: ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Agregar";
            this.ImageUrl = "~/Imagenes/Iconos/buscar.png";
            this.ToolTip="Buscar"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "buscar";
        }
    }
}