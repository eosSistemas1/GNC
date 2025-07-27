using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Logic;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class ValvulasController : ApiController
    {

        #region Members
        private ValvulasLogic valvulaLogic;
        public ValvulasLogic ValvulaLogic
        {
            get
            {
                if (valvulaLogic == null) valvulaLogic = new ValvulasLogic();
                return valvulaLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadListCodigosHomologacion")]
        [HttpGet]
        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.ValvulaLogic.ReadListCodigosHomologacion(codigoHomologacion);
        }

        #endregion

    }
}