using System;
using System.Net.Http;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class UsuarioLogic
    {
        #region Methods

        public UsuarioBasicView Login(UsuariosParameters up)
        {
            UsuarioBasicView usuario = null;
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Usuario/Login");
            HttpResponseMessage response = client.PostAsJsonAsync<UsuariosParameters>(queryString, up).Result;

            if (response.IsSuccessStatusCode)
                usuario = response.Content.ReadAsAsync<UsuarioBasicView>().Result;

            return usuario;
        }

        public Usuario ReadByUserName(String userName)
        {
            Usuario usuario = null;
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Usuario/ReadByUserName?userName={0}", userName);
            HttpResponseMessage response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
                usuario = response.Content.ReadAsAsync<Usuario>().Result;

            return usuario;
        }

        public Boolean Update(Usuario user)
        {
            HttpClient client = WebApi.ObtenerCliente();

            String queryString = String.Format("api/Usuario/Update");
            HttpResponseMessage response = client.PostAsJsonAsync<Usuario>(queryString, user).Result;
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        #endregion
    }
}
