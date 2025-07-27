using CrossCutting.DatosDiscretos;
using System;
using System.Text.RegularExpressions;

namespace TalleresWeb.Web.Cross
{
    public static class Funciones
    {
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

        public static bool EmailValido(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ValidarCuit(string cuit)
        {
            try
            {
                cuit = cuit.Replace("-", string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(cuit) || cuit.Length != 11) return false;

                var factores = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                var acumulado = 0;

                for (int i = 0; i < factores.Length; i++)
                    acumulado += int.Parse(cuit[i].ToString()) * factores[i];

                acumulado = 11 - (acumulado % 11);
                if (acumulado == 11)
                    acumulado = 0;
                if (int.Parse(cuit[10].ToString()) != acumulado)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }

        }

        public static DateTime? ObtenerValorFecha(string valor)
        {
            try
            {
                DateTime fecha = DateTime.Parse(valor);

                if (fecha >= CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime &&
                    fecha <= CrossCutting.DatosDiscretos.GetDinamyc.MaxDatetime)
                    return fecha;
            }
            catch
            {
            }

            return default(DateTime?);
        }
    }

    public static class Archivos
    {
        public static string GenerarPathArchivos(long numeroInforme)
        {

            var path = GenerarPathArchivos(DateTime.Now);

            path = $"{path}\\{numeroInforme}\\emitidos";

            return path;
        }

        public static string GenerarPathArchivos(DateTime fecha)
        {
            String dia = fecha.Date.Day.ToString("00");
            String mes = fecha.Month.ToString("00");
            String anio = fecha.Year.ToString("0000");

            String path = $"{GetDinamyc.UrlArchivosEnte}{anio}\\{mes}\\{dia}";

            return path;
        }
    }
}
