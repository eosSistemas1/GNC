using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.UserControls
{
    public partial class MessageBoxCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Boolean MessageBox(String Titulo, List<String> Mensajes, UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            var mensaje = String.Join("</br>", Mensajes);
            this.MessageBox(Titulo, mensaje, TipoMensaje);
            return TipoMensaje == UserControls.MessageBoxCtrl.TipoWarning.Success;
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

        public enum TipoWarning
        {
            Info, Warning, Error, Success
        }

        private String ImagenTipoWarning(PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning tipoMensaje)
        {
            String url = String.Empty;
            switch (tipoMensaje)
            {
                case TipoWarning.Warning:
                    url = "~/Imagenes/Iconos/warning.png";
                    break;
                case TipoWarning.Error:
                    url = "~/Imagenes/Iconos/bloqueada.png";
                    break;
                case TipoWarning.Success:
                    url = "~/Imagenes/Iconos/correcta.png";
                    break;
                default:
                    url = "~/Imagenes/Iconos/info.png";
                    break;
            }

            return url;
        }
    }
}