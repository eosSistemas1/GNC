using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class VehiculosLogic
    {        
        public VehiculosView ReadByDominio(String dominio)
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Vehiculos/ReadByDominio?dominio={0}", dominio);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                VehiculosView resultado = response.Content.ReadAsAsync<VehiculosView>().Result;

                return resultado;
            }

            return default(VehiculosView);
        }
    }
}
