using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnCambiar: ImageButton
    {
        #region Propiedades
        public override string AlternateText
        {
            get
            {
                return base.AlternateText;
            }
            set
            {
                if (value != "") base.AlternateText = value; else base.AlternateText = "Modificar";
            }
        }

        public override string ToolTip
        {
            get
            {
                return base.ToolTip;
            }
            set
            {
                if (value != "") base.ToolTip = value; else base.ToolTip = "Modificar";
            }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {            
            this.ImageUrl = "~/Imagenes/Iconos/cambiar.png";            
            this.Width= Unit.Pixel(22);
            this.CommandName = "cambiar";
            this.OnClientClick = "return confirm ('Desea cambiar el item seleccionado?');";
            
            this.ToolTip = String.IsNullOrEmpty(this.ToolTip) ? "Cambiar" : this.ToolTip;
            this.AlternateText = String.IsNullOrEmpty(this.AlternateText) ? "Cambiar" : this.AlternateText;
        }
    }
}