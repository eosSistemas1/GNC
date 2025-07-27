using TalleresWeb.Web.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TalleresWeb.Web.Logic;
using PL.Fwk.Entities;

namespace TalleresWeb.Web
{
    public partial class SiteMaster : MasterPage
    {
        #region Properties
        public static String UrlBase
        {
            get
            {
                HttpContext context = HttpContext.Current;
                String baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static String UserID
        {
            get { return HttpContext.Current.Session["USERID"].ToString(); }
        }

        public static ViewEntity Usuario
        {
            get { return (ViewEntity)HttpContext.Current.Session["USER"]; }
        }

        public static ViewEntity Taller
        {
            get {                
                if (HttpContext.Current.Session["TALLERID"] == null) RedirectLogin();
                return (ViewEntity)HttpContext.Current.Session["TALLERID"];                
            }
        }  
        #endregion

        #region Members

        private const String AntiXsrfTokenKey = "__AntiXsrfToken";
        private const String AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private String _antiXsrfTokenValue;

        private TalleresLogic tallerLogic;
        private TalleresLogic TallerLogic
        {
            get
            {
                if (tallerLogic == null) tallerLogic = new TalleresLogic();
                return tallerLogic;
            }
        }

        #endregion

        #region Methods

        protected void Page_Init(Object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    SiteMaster.RedirectLogin();
                    //throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            this.ValidarSeguridad();
            if (Context.User.Identity.IsAuthenticated)
            {
                try
                {
                    FormsIdentity identity = (FormsIdentity)Context.User.Identity;
                    var userData = identity.Ticket.UserData;
                    string[] data = userData.Split("|".ToCharArray());

                    Session["USERID"] = data[0];
                    Session["USER"] = new ViewEntity(new Guid(data[1]), data[2]);
                    Session["TALLERID"] = new ViewEntity(new Guid(data[3]), data[4]);
                    Session["MATRICULATALLERID"] = data[5];

                    this.CargarTaller();
                }
                catch
                {
                    SiteMaster.RedirectLogin();
                }
            }
            else
            {
                SiteMaster.RedirectLogin();
            }
        }

        /// <summary>
        /// Cargo la razón social del taller
        /// </summary>
        private void CargarTaller()
        {
            try
            {                    
                lblNombreTaller.Text = Taller.Descripcion;
                lblTaller.Text = String.Format("Bienvenido: {0}", Usuario.Descripcion);                    
            }
            catch
            {
                SiteMaster.RedirectLogin();
            }
        }

        /// <summary>
        /// Valida si el usuario esta logueado, sino redirige al login
        /// </summary>
        private void ValidarSeguridad()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
               SiteMaster.RedirectLogin();
            }
        }

        /// <summary>
        /// Redirige al login
        /// </summary>
        private static void RedirectLogin()
        {
            HttpContext.Current.Response.Redirect("/Account/login.aspx", true);
        }

        #endregion

        #region Message box

        public Boolean MessageBox(String Titulo, List<String> Mensajes, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            var mensaje = String.Join("</br>", Mensajes);
            this.MessageBoxCtrl.MessageBox(Titulo, mensaje, TipoMensaje);
            updMessage.Update();
            return TipoMensaje == TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Success;
        }

        /// <summary>
        /// muestra un msj box
        /// </summary>
        /// <param name="Titulo">Titulo del mensaje (String.Empty no muestra titulo)</param>
        /// <param name="Mensaje">Mensaje (String.Empty no muestra mensaje)</param>
        /// <param name="TipoMensaje">Info, Warning, Error, Success</param>
        public Boolean MessageBox(String Titulo, String Mensaje, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            this.MessageBoxCtrl.MessageBox(Titulo, Mensaje, TipoMensaje);
            updMessage.Update();
            return TipoMensaje == TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Success;
        }

        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            this.MessageBoxCtrl.MessageBox(Titulo, Mensaje, ResponseUrl, TipoMensaje);
            updMessage.Update();
        }

        public void MessageBox(String Titulo, String Mensaje, String ResponseUrl, MessageBoxCtrl.TipoWarning TipoMensaje, String URLOnOkButton, String TextOkButton)
        {
            this.MessageBoxCtrl.MessageBox(Titulo, Mensaje, ResponseUrl, TipoMensaje, URLOnOkButton, TextOkButton);
            updMessage.Update();
        }
        #endregion
    }
}