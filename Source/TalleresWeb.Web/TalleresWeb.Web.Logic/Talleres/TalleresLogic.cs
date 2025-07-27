using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TalleresWeb.Web.Logic
{
    public class TalleresLogic
    {
        public ViewEntity ReadTaller(Guid idTaller)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Talleres/ReadTaller?idTaller={0}", idTaller);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewEntity resultado = response.Content.ReadAsAsync<ViewEntity>().Result;

                return resultado;
            }

            return default(ViewEntity);                  
        }
    }
}
