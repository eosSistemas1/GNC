using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class BtnEliminar : ImageButton
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.AlternateText = "Eliminar";
            this.ImageUrl = "~/img/Iconos/eliminar.png";
            this.ToolTip = "Eliminar";
            this.Width = Unit.Pixel(22);
            this.CommandName = "eliminar";
            this.OnClientClick = "return confirm ('¿Desea eliminar el item seleccionado?');";
        }

        #endregion
    }
}