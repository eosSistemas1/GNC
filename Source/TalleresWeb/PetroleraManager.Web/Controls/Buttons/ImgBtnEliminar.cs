using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnEliminar: ImageButton
    {
        protected override void OnInit(EventArgs e)
        {
            this.AlternateText="Eliminar";
            this.ImageUrl = "~/Imagenes/Iconos/eliminar.png";
            this.ToolTip="Eliminar"; 
            this.Width= Unit.Pixel(22);
            this.CommandName = "eliminar";
            this.OnClientClick = "return confirm ('Desea eliminar el item seleccionado?');";
        }
    }
}