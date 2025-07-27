using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnVer: ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Ver";
            this.ImageUrl="~/Imagenes/Iconos/ver.png";
            this.ToolTip="Ver"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "ver";
        }
    }
}