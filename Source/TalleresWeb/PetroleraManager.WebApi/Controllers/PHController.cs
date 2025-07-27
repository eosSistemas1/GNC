using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class PHController : ApiController
    {
        private PHLogic phLogic;
        public PHLogic PHLogic
        {
            get
            {
                if (phLogic == null) phLogic = new PHLogic();
                return phLogic;
            }
        }

        [ActionName("ReadForPrint")]
        [HttpGet]
        public PHPrintViewWebApi ReadForPrint(Guid phID)
        {
            try
            {
                return this.PHLogic.ReadForPrint(phID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("Guardar")]
        [HttpPost]
        public ViewEntity Guardar(PHViewWebApi oblea)
        {
            try
            {                
                ViewEntity o = this.PHLogic.SaveFromExtranet(oblea, oblea.UsuarioID.Value, oblea.PECID.Value);
                return o;
            }
            catch (Exception ex)
            {
                return new ViewEntity(Guid.Empty, ex.InnerException.Message);
            }
        }
    }
}