using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TalleresWeb.Web.Logic
{
    public class CRPCLogic
    {
        public List<ViewEntity> ReadListView()
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/CRPC/ReadListCRPC");
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<ViewEntity> resultado = response.Content.ReadAsAsync<List<ViewEntity>>().Result;

                return resultado;
            }

            return default(List<ViewEntity>);
        }
    }
}
