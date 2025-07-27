using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class ReguladoresController : ApiController
    {

        #region Members
        private ReguladoresLogic reguladoresLogic;
        public ReguladoresLogic ReguladoresLogic
        {
            get
            {
                if (reguladoresLogic == null) reguladoresLogic = new ReguladoresLogic();
                return reguladoresLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadListCodigosHomologacion")]
        [HttpGet]
        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.ReguladoresLogic.ReadListCodigosHomologacion(codigoHomologacion);
        }

        #endregion

    }
}