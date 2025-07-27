using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace TalleresWeb.Web
{
    static class Genericos
    {
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

        public static String FormatearAnio(String Anio)
        {
            int xAnio = int.Parse(Anio);

            if (Anio.Length < 4)
            {
                if (Anio.Length == 2)
                {
                    if (xAnio > 80)
                    {
                        Anio = "19" + Anio;
                    }
                    else
                    {
                        Anio = "20" + Anio;
                    }
                }
                else
                {
                    Anio = "200" + Anio;
                }
            }

            return Anio;
        }

        public static String FormatearNroCuit(String cuit)
        {
            if (cuit != String.Empty)
            {
                cuit = cuit.Replace("-", "");
                cuit = cuit.Insert(2, "-");
                cuit = cuit.Insert((cuit.Length - 1), "-");
            }
            else
            {
                cuit = String.Empty;
            }
            return cuit;
        }
    }
}
