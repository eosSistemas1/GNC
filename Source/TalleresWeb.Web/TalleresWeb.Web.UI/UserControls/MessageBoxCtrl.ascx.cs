using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.UserControls
{
    public partial class MessageBoxCtrl : System.Web.UI.UserControl
    {
        #region Methods

        protected void Page_Load(Object sender, EventArgs e)
        {

        }

        public Boolean MessageBox(String Titulo, List<String> Mensajes, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            var mensaje = String.Join("</br>", Mensajes);
            this.MessageBox(Titulo, mensaje, TipoMensaje);            
            return TipoMensaje == TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Success;
        }

        public void MessageBox(String Titulo, String Mensaje, MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();
        }

        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            MPE.OnCancelScript = "window.location.href='" + ResponseUrl + "';";
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();
        }

        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, MessageBoxCtrl.TipoWarning TipoMensaje, String URLOnOkButton, String TextOkButton)
        {
            btnOk.Text = TextOkButton;
            btnOk.Visible = true;
            MPE.OkControlID = "btnOk";
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            //URLOnOkButton = URLOnOkButton.Replace("~", SiteMaster.UrlBase);
            MPE.OnCancelScript = "window.location.href='" + ResponseUrl + "';";
            MPE.OnOkScript = "window.open('" + URLOnOkButton + "','','fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=no,directories=no,location=no,width=800,height=500,top=0,left=0'); window.location.href='" + ResponseUrl + "';";
            imgMsg.ImageUrl = ImagenTipoWarning(TipoMensaje);
            MPE.Show();
        }

        private String ImagenTipoWarning(MessageBoxCtrl.TipoWarning tipoMensaje)
        {
            String url = String.Empty;
            switch (tipoMensaje)
            {
                case MessageBoxCtrl.TipoWarning.Warning:
                    url = SiteMaster.UrlBase + "Images/Iconos/warning.png";
                    break;
                case MessageBoxCtrl.TipoWarning.Error:
                    url = SiteMaster.UrlBase + "Images/Iconos/bloqueada.png";
                    break;
                case MessageBoxCtrl.TipoWarning.Success:
                    url = SiteMaster.UrlBase + "Images/Iconos/correcta.png";
                    break;
                default:
                    url = SiteMaster.UrlBase + "Images/Iconos/info.png";
                    break;
            }

            return url;
        }

        #endregion

        public enum TipoWarning
        {
            Info, Warning, Error, Success
        }
    }
}