using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace TalleresWeb.Web.UI
{
    public static class Seguridad
    {

        #region Fields

        private static Byte[] key = { };
        private static Byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };
        private static String stringKey = "!5663a#KN";

        #endregion

        #region Methods

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
        public static String[] GetRolesUsuario()
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
        public static Boolean IsInRol(String NombreRol)
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

        public static String Encrypt(String text)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] byteArray = Encoding.UTF8.GetBytes(text);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                // Handle Exception Here
            }

            return string.Empty;
        }

        public static String Decrypt(String text)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] byteArray = Convert.FromBase64String(text);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                // Handle Exception Here
            }

            return string.Empty;
        }

        #endregion
    }
}