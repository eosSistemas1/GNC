using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnSeleccionar : ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Seleccionar";
            this.ImageUrl = "~/Imagenes/Iconos/seleccionar.png";
            this.ToolTip="Seleccionar"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "seleccionar";
        }
    }
}