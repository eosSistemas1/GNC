using System;

namespace Genericos
{
    public static class Genericos 
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
    }   
}