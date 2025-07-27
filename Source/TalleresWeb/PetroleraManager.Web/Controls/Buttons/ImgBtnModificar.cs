using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnModificar: ImageButton
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

        #region Metodos
        protected override void OnInit(EventArgs e)
        {
            this.ImageUrl = "~/Imagenes/Iconos/modificar.png";
            this.Width= Unit.Pixel(22);
            this.CommandName = "modificar";

            this.ToolTip = String.IsNullOrEmpty(this.ToolTip) ? "Modificar" : this.ToolTip; 
        }
        #endregion

    }
}