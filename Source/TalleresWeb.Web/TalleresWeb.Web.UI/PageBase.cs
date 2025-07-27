using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TalleresWeb.Web.UI
{
    public class PageBase : Page
    {
        #region Propiedades

        public String UriActual
        {
            get
            {
                return Page.Request.Url.AbsoluteUri;
            }
        }

        public String TipoFoto
        {
            get
            {
                return Session["TIPOFOTO"] == null? String.Empty : Session["TIPOFOTO"].ToString();
            }
            set
            {                
                Session["TIPOFOTO"] = value;
            }
        }

        #endregion

        #region Methods

        public void Cancelar()
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, false);
        }

        protected override void OnLoad(EventArgs e)
        {

            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                //verifica que la pagina tenga permiso para el rol y sino la manda a la default
                if (!PaginaIsValid(UriActual)) Response.Redirect("~/Default.aspx");
            }

            base.OnLoad(e);

        }

        private Boolean PaginaIsValid(String pageName)
        {
            Boolean valor = true;
            // valida que el nombre de la pagina que se envia tiene permisos 
            //  en el rol(es) que tiene el usuario logueado.

            return valor;
        }               

        #endregion

    }
}