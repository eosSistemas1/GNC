using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class BtnModificar : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Modificar";
            this.ImageUrl = "~/img/Iconos/modificar.png";
            this.ToolTip = "Modificar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "modificar";            
        }

        #endregion
    }
}