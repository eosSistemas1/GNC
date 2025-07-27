using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Common.Web.UserControls
{
    public partial class MessageBoxCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MessageBox(String Titulo, String Mensaje, TipoWarning TipoMensaje)
        {
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();
        }
        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, TipoWarning TipoMensaje)
        {
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            MPE.OnCancelScript = "window.location.href='" + ResponseUrl + "';";
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();

        }
        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, TipoWarning TipoMensaje, String URLOnOkButton, String TextOkButton)
        {
            btnOk.Text = TextOkButton;
            btnOk.Visible = true;
            MPE.OkControlID = "btnOk";
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            MPE.OnCancelScript = "window.location.href='" + ResponseUrl + "';";
            MPE.OnOkScript = "window.open('" + URLOnOkButton + "','','fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=no,directories=no,location=no,width=800,height=500,top=0,left=0'); window.location.href='" + ResponseUrl + "';";
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();
        }

        private String ImagenTipoWarning(TipoWarning tipoMensaje)
        {
            String url = String.Empty;
            switch (tipoMensaje)
            {
                case Common.Web.UserControls.MessageBoxCtrl.TipoWarning.Warning:
                    url = "~/Images/Iconos/warning.png";
                    break;
                case Common.Web.UserControls.MessageBoxCtrl.TipoWarning.Error:
                    url = "~/Images/Iconos/bloqueada.png";
                    break;
                case Common.Web.UserControls.MessageBoxCtrl.TipoWarning.Success:
                    url = "~/Images/Iconos/correcta.png";
                    break;
                default:
                    url = "~/Images/Iconos/info.png";
                    break;
            }

            return url;
        }
        public enum TipoWarning
        {
            Info, Warning, Error, Success
        }
    }
}