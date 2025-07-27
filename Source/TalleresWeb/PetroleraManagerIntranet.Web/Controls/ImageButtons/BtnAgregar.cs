using System;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class BtnAgregar : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Agregar";
            this.ImageUrl = "~/img/Iconos/agregar.png";
            this.ToolTip = "Agregar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "agregar";
        }

        #endregion
    }
}