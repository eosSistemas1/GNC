using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TalleresWeb.Web.Logic
{
    public class TipoOperacionLogic
    {
        public List<ViewEntity> ReadListTiposOperaciones()
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/TipoOperacion/ReadListTiposOperaciones");
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<ViewEntity> resultado = response.Content.ReadAsAsync<List<ViewEntity>>().Result;

                resultado = resultado.Where(x => x.ID == TIPOOPERACION.Conversion ||
                                                 x.ID == TIPOOPERACION.RevisionAnual ||
                                                 x.ID == TIPOOPERACION.RevisionCRPC ||
                                                 x.ID == TIPOOPERACION.Modificacion ||
                                                 x.ID == TIPOOPERACION.Baja ||
                                                 x.ID == TIPOOPERACION.Reemplazo).ToList();

                return resultado;
            }

            return default(List<ViewEntity>);
        }
    }
}
