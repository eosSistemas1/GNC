using CrossCutting.DatosDiscretos;
using Newtonsoft.Json;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.Logic
{
    public class ObleasLogic
    {
        /// <summary>
        /// Devuelve una oblea por ID
        /// </summary>
        /// <param name="idOblea"></param>
        /// <returns></returns>
        public ObleasViewWebApi ReadObleaByID(Guid idOblea)
        {
            ObleasViewWebApi oblea = new ObleasViewWebApi();

            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Obleas/ReadObleaByID?idOblea={0}", idOblea.ToString());
            HttpResponseMessage response = client.GetAsync(queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                oblea = response.Content.ReadAsAsync<ObleasViewWebApi>().Result;
            }

            return oblea;
        }

        public List<ObleasConsultarView> ReadObleasConsulta(DateTime fechaDesde,
                                                            DateTime fechaHasta,
                                                            string dominio,
                                                            string numeroOblea,
                                                            Guid tallerID)
        {                                  
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = $"api/Obleas/ReadObleasConsulta?fechaDesde={fechaDesde}&fechaHasta={fechaHasta}&dominio={dominio}&numeroOblea={numeroOblea}&tallerID={tallerID}";
            var response = client.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<ObleasConsultarView>>().Result;
            }
                        
            return default;
        }

        /// <summary>
        /// Devuelve una oblea
        /// </summary>        
        public ObleasViewWebApi ReadOblea(ObleasParametersWebApi criteria)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Obleas/Read");
            HttpResponseMessage response = client.PostAsJsonAsync<ObleasParametersWebApi>(queryString, criteria).Result;
            ObleasViewWebApi resultado = response.Content.ReadAsAsync<ObleasViewWebApi>().Result;

            return resultado;
        }

        /// <summary>
        /// Devuelve las obleas pendientes para un taller
        /// </summary>
        /// <param name="idEstadoOblea"></param>
        /// <param name="idTaller"></param>
        /// <returns></returns>
        public List<EstadosTramitesView> ReadTramitesByTallerID(Guid idTaller)
        {            
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Obleas/ReadTramitesByTallerID?idTaller={0}", idTaller.ToString());
            HttpResponseMessage response = client.GetAsync(queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                var obleas = response.Content.ReadAsAsync<IEnumerable<EstadosTramitesView>>().Result;                
                return obleas.OrderByDescending(o => o.FechaTramite).ToList();
            }

            return default(List<EstadosTramitesView>);
        }

        /// <summary>
        /// Guarda una oblea
        /// </summary>        
        public ViewEntity Guardar(ObleasViewWebApi comprobante)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Obleas/Guardar");
            HttpResponseMessage response = client.PostAsJsonAsync<ObleasViewWebApi>(queryString, comprobante).Result;
            ViewEntity resultado = response.Content.ReadAsAsync<ViewEntity>().Result;

            return resultado;
        }

        /// <summary>
        /// Guarda una imagen
        /// </summary>        
        public Boolean GuardarImagen(ImagenModel imagen)
        {
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = String.Format("api/Obleas/GuardarImagen");
            HttpResponseMessage response = client.PostAsJsonAsync<ImagenModel>(queryString, imagen).Result;
            Boolean resultado = response.Content.ReadAsAsync<Boolean>().Result;

            return resultado;
        }

        public static Boolean ValidarDominio(String Dominio, int Anio, Guid IdUso)
        {
            String dominio = Dominio;
            int anio = Anio;
            Guid usoID = IdUso;

            if (usoID == TIPOVEHICULO.Particular ||
                usoID == TIPOVEHICULO.Taxi ||
                usoID == TIPOVEHICULO.PickUp ||
                usoID == TIPOVEHICULO.Bus ||
                usoID == TIPOVEHICULO.Oficial ||
                usoID == TIPOVEHICULO.Otros)
            {
                Regex auto = new Regex("^[A-Z][A-Z][A-Z][0-9-][0-9-][0-9-]$");
                Regex autoMercosur = new Regex("^[A-Z][A-Z][0-9-][0-9-][0-9-][A-Z][A-Z]$");

                if (anio == 2016 && (auto.IsMatch(dominio.ToUpper()) || autoMercosur.IsMatch(dominio.ToUpper()))) return true;

                if (anio < 2016 && auto.IsMatch(dominio.ToUpper())) return true;

                if (anio > 2016 && autoMercosur.IsMatch(dominio.ToUpper())) return true;
            }

            if (usoID == TIPOVEHICULO.Moto)
            {
                Regex moto = new Regex("^[0-9-][0-9-][0-9-][A-Z][A-Z][A-Z]$");
                Regex motoMercosur = new Regex("^[A-Z][0-9-][0-9-][0-9-][A-Z][A-Z][A-Z]$");

                if (anio == 2016 && (moto.IsMatch(dominio.ToUpper()) || motoMercosur.IsMatch(dominio.ToUpper()))) return true;

                if (anio < 2016 && moto.IsMatch(dominio.ToUpper())) return true;

                if (anio > 2016 && motoMercosur.IsMatch(dominio.ToUpper())) return true;
            }

            if (usoID == TIPOVEHICULO.Autoelevadores)
            {
                Regex autoelevador = new Regex("^[A-Z][A-Z][0-9-][0-9-][0-9-]$");

                if (autoelevador.IsMatch(dominio.ToUpper())) return true;
            }

            return false;
        }

        public Boolean ExisteTramitePendienteParaElDominio(string dominio)
        {           
            HttpClient client = WebApi.ObtenerCliente();
            String queryString = $"api/Obleas/ExisteTramitePendienteParaElDominio?dominio={dominio}";
            HttpResponseMessage response = client.GetAsync(queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Boolean>().Result;
            }

            return false;
        }
    }
}
