using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class BtnCancel : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Cancelar";
            this.ImageUrl = "~/Images/Iconos/cancelar.png";
            this.ToolTip = "Cancelar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "cancelar";
        }

        #endregion
    }
}