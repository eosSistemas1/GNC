using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TalleresWeb.Web.Logic
{
    public class CilindrosLogic
    {
        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Cilindros/ReadListCodigosHomologacion?codigoHomologacion={0}", codigoHomologacion);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<String> resultado = response.Content.ReadAsAsync<List<String>>().Result;

                return resultado;
            }

            return default(List<String>);
        }


        public String ReadCilindroByCodigoHomologacion(String codigoHomologacion)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Cilindros/ReadCilindroByCodigoHomologacion?codigoHomologacion={0}", codigoHomologacion);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<String>().Result;
            }

            return "|";
        }

    }
}
