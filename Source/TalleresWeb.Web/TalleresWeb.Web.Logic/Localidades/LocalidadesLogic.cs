using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class LocalidadesLogic
    {
        public List<ViewEntity> ReadListLocalidades()
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Localidades/ReadListLocalidades");
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<ViewEntity> resultado = response.Content.ReadAsAsync<List<ViewEntity>>().Result;

                return resultado;
            }

            return default(List<ViewEntity>);
        }

        public LocalidadesExtendedView ReadLocalidadByID(Guid localidadID)
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = $"api/Localidades/ReadLocalidadByID?localidadID={localidadID}";
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                LocalidadesExtendedView resultado = response.Content.ReadAsAsync<LocalidadesExtendedView>().Result;

                return resultado;
            }

            return default(LocalidadesExtendedView);
        }
    }
}
