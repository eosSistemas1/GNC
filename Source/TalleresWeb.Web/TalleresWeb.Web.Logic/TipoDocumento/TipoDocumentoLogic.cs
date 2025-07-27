using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TalleresWeb.Web.Logic
{
    public class TipoDocumentoLogic
    {
        public List<ViewEntity> ReadListTiposDocumentos()
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/TipoDocumento/ReadListTiposDocumentos");
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
