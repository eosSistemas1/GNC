using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.PH
{
    /// <summary>
    /// Summary description for PHService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PHService : System.Web.Services.WebService
    {
        #region Properties
        private CilindrosLogic cilindrosLogic;
        private CilindrosLogic CilindrosLogic
        {
            get
            {
                if (this.cilindrosLogic == null) this.cilindrosLogic = new CilindrosLogic();
                return this.cilindrosLogic;
            }
        }

        private ValvulasLogic valvulasLogic;
        private ValvulasLogic ValvulasLogic
        {
            get
            {
                if (this.valvulasLogic == null) this.valvulasLogic = new ValvulasLogic();
                return this.valvulasLogic;
            }
        }
        #endregion


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public VehiculosView GetVehiculoByDominio(string dominio)
        {
            Vehiculos result = new VehiculosLogic().ReadVehiculoByDominio(dominio);
            return VehiculosView.VehiculosToVehiculosView(result);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ClientesView GetClienteByDocumento(string tipoDocumento, string numeroDocumento)
        {
            Clientes result = new ClientesLogic().ReadClientesViewByTipoyNroDoc(new Guid(tipoDocumento), numeroDocumento).FirstOrDefault();
            return ClientesView.ClientesToClientesView(result);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCilindrosCodHomAutoCompleteData(string txt)
        {
            List<string> result = this.CilindrosLogic.ReadListCodigosHomologacion(txt);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCilindroMarcaYCapacidad(string codigo)
        {
            var result = this.CilindrosLogic.ReadCilindroMarcaYCapacidad(codigo);
            return result.Any() ? result.First() : "Sin Marca|0";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetValvulasCodHomAutoCompleteData(string txt)
        {
            List<string> result = this.ValvulasLogic.ReadListCodigosHomologacion(txt);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCP(string idLocalidad)
        {
            Guid id = new Guid(idLocalidad);
            var result = new LocalidadesLogic().Read(id);
            return result != null? result.CodigoPostal.Trim() : String.Empty;
        }       
    }
}
