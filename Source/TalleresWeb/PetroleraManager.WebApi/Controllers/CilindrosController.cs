using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;
using System.Linq;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class CilindrosController : ApiController
    {

        #region Members
        private CilindrosLogic cilindrosLogic;
        public CilindrosLogic CilindrosLogic
        {
            get
            {
                if (cilindrosLogic == null) cilindrosLogic = new CilindrosLogic();
                return cilindrosLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadListCodigosHomologacion")]
        [HttpGet]
        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.CilindrosLogic.ReadListCodigosHomologacion(codigoHomologacion);
        }

        [ActionName("ReadCilindroByCodigoHomologacion")]
        [HttpGet]
        public String ReadCilindroByCodigoHomologacion(String codigoHomologacion)
        {
            var cilindro = this.CilindrosLogic.ReadByCodigoHomologacion(codigoHomologacion).FirstOrDefault();

            if (cilindro == null) return "|";

            String marca = cilindro.MarcasCilindros != null ? cilindro.MarcasCilindros.Descripcion : String.Empty;
            String capacidad = cilindro.CapacidadCil.HasValue ? cilindro.CapacidadCil.Value.ToString() : String.Empty;
            return String.Format("{0}|{1}", marca, capacidad);
        }
        
        #endregion

    }
}