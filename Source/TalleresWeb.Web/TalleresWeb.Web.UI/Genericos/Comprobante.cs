using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalleresWeb.Web.UI.Genericos
{
    public static class Comprobante
    {
        #region Methods

        public static Decimal CalcularCoeficiente(Decimal precio, Decimal coef)
        {
            Decimal valor = 0;

            coef = (coef / 100);

            valor = precio * coef;

            return valor;
        }

        public static String FormatearNroComprobante(String nroComprobante)
        {
            if (!nroComprobante.Replace("&nbsp;", String.Empty).Equals(String.Empty))
            {
                String numeros = "0123456789";
                nroComprobante = nroComprobante.Replace("-", String.Empty);

                if (numeros.Contains(nroComprobante[0].ToString()))
                {
                    return nroComprobante.Insert(4, "-");
                }
                else
                {
                    return nroComprobante.Insert(5, "-");
                }
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}