using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class BtnModificar : ImageButton
    {

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.CausesValidation = false;
            this.ImageUrl = "~/Images/Iconos/modificar.png";
            this.Width = Unit.Pixel(22);
            this.CommandName = "modificar";
        }

        #endregion
    }
}