using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TalleresWeb.Web.Logic
{
    public class ValvulasLogic
    {
        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Valvulas/ReadListCodigosHomologacion?codigoHomologacion={0}", codigoHomologacion);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<String> resultado = response.Content.ReadAsAsync<List<String>>().Result;

                return resultado;
            }

            return default(List<String>);
        }
    }
}
