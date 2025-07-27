using System;
using System.Web;
using System.Web.UI;

namespace TalleresWeb.Web.Cross.Configuracion
{
    public class PageBase : Page
    {
        public static String UrlBase
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public Guid UsuarioID
        {
            get { return new Guid("9E1051FA-B7BC-4AB5-AEE4-2E3E311B3706"); }
        }

        public Guid RTPEC
        {
            get
            {
                Guid valor = CrossCutting.DatosDiscretos.PEC_RT.PEC_RT_Principal;
                return valor;
            }
        }

        public Guid PEC
        {
            get
            {
                Guid valor = CrossCutting.DatosDiscretos.PEC.PEAR;
                return valor;
            }
        }
        

        protected override void OnLoad(EventArgs e)
        {
            this.ValidarAutenticacion();

            base.OnLoad(e);
        }

        private void ValidarAutenticacion()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                //HttpContext context = HttpContext.Current;
                //string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                //Response.Redirect(baseUrl + "Account/login.aspx");
            }
        }
    }
}
