using System;
using PL.Fwk.Presentation.Web.Controls;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnAgregar: ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Agregar";
            this.ImageUrl = "~/Imagenes/Iconos/agregar.png";
            this.ToolTip="Agregar"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "agregar";
        }
    }
}