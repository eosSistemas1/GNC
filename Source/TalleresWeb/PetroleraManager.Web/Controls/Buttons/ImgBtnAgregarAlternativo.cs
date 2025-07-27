using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnReEvaluar : ImageButton
    {

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Re-evaluar";
            this.ImageUrl = "~/Imagenes/Iconos/reevaluar.png";
            this.ToolTip = "Re-evaluar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "reevaluar";
        }
    }
}