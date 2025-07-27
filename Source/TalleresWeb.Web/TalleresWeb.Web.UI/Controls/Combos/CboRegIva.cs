using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboRegIva : ComboBase
    {
        public override void LoadData()
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = "api/Maestros/ReadRegIvaListView";
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                List<ViewEntity> resultado = new List<ViewEntity>();
                resultado.AddRange(response.Content.ReadAsAsync<IEnumerable<ViewEntity>>().Result);

                this.DataSource = resultado;
            }
        }
    }

}