using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Linq;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class VehiculosController : ApiController
    {

        #region Members

        private VehiculosLogic vehiculosLogic;

        public VehiculosLogic VehiculosLogic
        {
            get {
                if (vehiculosLogic == null) vehiculosLogic = new VehiculosLogic();
                return vehiculosLogic;
            }
            
        }
        #endregion

        #region Methods

        [ActionName("ReadByDominio")]
        [HttpGet]
        public VehiculosView ReadByDominio(String dominio)
        {
            Vehiculos vehiculos = this.VehiculosLogic.ReadVehiculoByDominio(dominio);

            if (vehiculos != null)
            {
                return VehiculosView.VehiculosToVehiculosView(vehiculos);
            }
            
            return default(VehiculosView);
        }
          
        #endregion

    }
}