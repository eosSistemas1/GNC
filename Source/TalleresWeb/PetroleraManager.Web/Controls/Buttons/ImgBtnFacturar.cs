using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{
    public class ImgBtnFacturar: ImageButton
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
                if (value != "") base.AlternateText = value; else base.AlternateText = "Facturar"; 
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
                if (value != "") base.ToolTip = value; else base.ToolTip = "Facturar"; 
            }
        }
        #endregion

        #region Metodos
        protected override void OnInit(EventArgs e)
        {
            this.ImageUrl = "~/Imagenes/Iconos/facturar.png";
            this.Width= Unit.Pixel(22);
            this.CommandName = "facturar";
        }
        #endregion

    }
}