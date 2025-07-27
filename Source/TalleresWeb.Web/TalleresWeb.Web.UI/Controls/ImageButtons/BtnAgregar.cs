using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class BtnAgregar : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Agregar";
            this.ImageUrl = "~/Images/Iconos/agregar.png";
            this.ToolTip = "Agregar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "agregar";
        }

        #endregion
    }
}