using System;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class BtnReEvaluar : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Re-evaluar";
            this.ImageUrl = "~/img/Iconos/reevaluar.png";
            this.ToolTip = "Re-evaluar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "reevaluar";
        }

        #endregion
    }
}