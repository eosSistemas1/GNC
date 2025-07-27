using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace TalleresWeb.Web.Logic
{
    public static class WebApi
    {
        public static HttpClient ObtenerCliente()
        {         
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(3);
            String uri = WebConfigurationManager.AppSettings["DireccionWebApi"].ToString();
            client.BaseAddress = new Uri(uri, UriKind.Absolute);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            String user = WebConfigurationManager.AppSettings["UsuarioWebApi"].ToString();
            String password = WebConfigurationManager.AppSettings["PasswordWebApi"].ToString();
            String credentials = String.Format("{0}:{1}", user, password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials)));
            return client;
        }
    }
}
