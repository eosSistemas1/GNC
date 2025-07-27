using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Configuration;

namespace GestionDrogueria.Web.Account
{
    public static class Seguridad
    {
        /// <summary>
        /// Captura la cookie de autentificacion y genera realiza todo los procesos
        /// internos para que se valide el ingreso del usuario.
        /// </summary>
        /// <param name="HttpContext">Se debe enviar el contexto</param>
        public static void Authenticate(System.Web.HttpContext HttpContext)
        {
            FormsAuthentication.Initialize();

            String cookieName = FormsAuthentication.FormsCookieName;
            HttpCookieCollection authCookieCol = HttpContext.Current.Request.Cookies;
            HttpCookie authCookie = authCookieCol.Get(cookieName);

            if (authCookie == null)
            {
                // -- No existe la cookie de autentificacion
                return;
            }

            FormsAuthenticationTicket authTicket = null;

            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                // -- Log exception details (omitted for simplicity)
                return;
            }

            if (authTicket == null)
            {
                // -- No se pudo desencriptar la cookie.
                return;
            }

            // -- Cuando el ticket fue creado, La propiedad UserData se le asignada un caracter delimitador en los roles.
            String[] roles = authTicket.UserData.Split(new Char[] { ',' });

            // -- Creando el objeto Identity.
            FormsIdentity id = new FormsIdentity(authTicket);

            // -- This principal will flow throughout the request.
            GenericPrincipal principal = new GenericPrincipal(id, roles);

            // -- Agrega el nuevo principal object al objeto HttpContext actual
            HttpContext.Current.User = principal;

            try
            {
                if (authCookieCol.Get("CookieDomain").Value != ConfigurationManager.AppSettings["CookieDomain"])
                {
                    // -- Si la cookie pertenece a otro sistema.
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                }
            }
            catch (Exception ex)
            {
                authCookie = null;
                FormsAuthentication.SignOut();
            }

        }

        /// <summary>
        /// Busca y devuelve todos los roles del usuario que se encuentra
        /// ya firmado en la aplicacion.
        /// </summary>
        /// <returns>Conjunto de roles del usuario</returns>
        public static string[] GetRolesUsuario()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        return roles;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Determina si el usuario actual tiene ese rol.
        /// </summary>
        /// <param name="NombreRol">Nombre del rol que se esta buscando</param>
        /// <returns></returns>
        public static bool IsInRol(string NombreRol)
        {
            String[] roles = GetRolesUsuario();
            foreach (string rol in roles)
            {
                if (rol == NombreRol)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Encripta contrasenia.
        /// </summary>
        /// <param name="contrasenia">String de contrasenia a encriptar</param>
        /// <returns></returns>
        public static String EncriptaContrasenia(string contrasenia)
        {
            String pw = FormsAuthentication.HashPasswordForStoringInConfigFile(contrasenia.Trim(), "sha1");
            return pw;
        }
    }
}
