using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class ClientesLogic
    {        
        public ClientesView ReadByDocumento(Guid tipoDocumentoID, String numeroDocumento)
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Clientes/ReadByDocumento?tipoDocumentoID={0}&nroDocumento={1}", tipoDocumentoID, numeroDocumento);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                ClientesView resultado = response.Content.ReadAsAsync<ClientesView>().Result;

                return resultado;
            }

            return default(ClientesView);
        }
    }
}
