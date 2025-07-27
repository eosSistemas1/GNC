using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnImprimir: ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Imprimir";
            this.ImageUrl = "~/Imagenes/Iconos/imprimir.png";
            this.ToolTip="Imprimir"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "imprimir";
        }
    }
}