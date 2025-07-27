using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.UserControls
{
    public partial class MessageBoxCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MessageBox(String Titulo, String Mensaje, MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            List<String> mensaje = new List<String>();
            mensaje.Add(Mensaje);
            this.MessageBox(Titulo, mensaje, TipoMensaje); 
        }

        public void MessageBox(String Titulo, List<String> Mensajes, MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            var mensaje = String.Join("</br>", Mensajes);
            
            this.Show(Titulo, mensaje, TipoMensaje);
        }

        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, TipoWarning TipoMensaje)
        {
            this.Show(Titulo, Mensaje, TipoMensaje, ResponseUrl);
        }

        private void Show(String titulo, String mensaje, MessageBoxCtrl.TipoWarning TipoMensaje, string returnURL=null)
        {
            if (!String.IsNullOrWhiteSpace(titulo)) this.lblTituloMsj.Text = titulo;            
            String imagen = ImagenTipoWarning(TipoMensaje);
            if (returnURL == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"openModal('{mensaje}', '{imagen}');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", $"openModal('{mensaje}', '{imagen}', '{returnURL}');", true);
            }            
        }
                     

        public enum TipoWarning
        {
            Info, Warning, Error, Success
        }

        private String ImagenTipoWarning(MessageBoxCtrl.TipoWarning tipoMensaje)
        {
            String path = "/img/Iconos/";
            String imagen = String.Empty;

            switch (tipoMensaje)
            {
                case TipoWarning.Warning:
                    imagen = "warning.png";
                    break;
                case TipoWarning.Error:
                    imagen = "bloqueada.png";
                    break;
                case TipoWarning.Success:
                    imagen = "correcta.png";
                    break;
                default:
                    imagen = "info.png";
                    break;
            }

            return String.Format("{0}{1}", path, imagen);
        }
    }
}