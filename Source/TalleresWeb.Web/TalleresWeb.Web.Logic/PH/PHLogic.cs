using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class PHLogic
    {
        /// <summary>
        /// Devuelve una ph para imprimir
        /// </summary>        
        public PHPrintViewWebApi ReadForPrint(Guid idPH)
        {            
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/PH/ReadForPrint?phID={0}", idPH.ToString());
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            return response.Content.ReadAsAsync<PHPrintViewWebApi>().Result;
        }

        /// <summary>
        /// Guarda una ph
        /// </summary>        
        public ViewEntity SaveFromExtranet(PHViewWebApi ph)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/PH/Guardar");
            HttpResponseMessage response = client.PostAsJsonAsync<PHViewWebApi>(queryString, ph).Result;
            ViewEntity resultado = response.Content.ReadAsAsync<ViewEntity>().Result;

            return resultado;
        }
    }
}
