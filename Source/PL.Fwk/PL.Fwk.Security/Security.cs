using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Fwk.Security
{
    /// Esta clase contiene funciones para encriptar/desencriptar
    /// El ser estática no es necesario instanciar un objeto para 
    /// usar las funciones Encriptar y DesEncriptar

    public static class Security
    {

        /// Encripta una cadena
        public static string Encriptar(String cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        
        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }

}
