using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class TipoOperacionController : ApiController
    {

        #region Members


        private TiposOperacionesLogic tiposOperacionesLogic;

        public TiposOperacionesLogic TiposOperacionesLogic
        {
            get
            {
                if (tiposOperacionesLogic == null) tiposOperacionesLogic = new TiposOperacionesLogic();
                    return tiposOperacionesLogic;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Devuelve las operaciones de la oblea
        /// </summary>        
        [ActionName("ReadListTiposOperaciones")]
        [HttpGet]
        public List<ViewEntity> ReadListTiposOperaciones()
        {
            return TiposOperacionesLogic.ReadEVOperaciones();
        }

        /// <summary>
        /// Devuelve los estados MSDB
        /// </summary>        
        [ActionName("MSDB")]
        [HttpGet]
        public List<ViewEntity> ReadListMSDB()
        {
            return TiposOperacionesLogic.ReadEVMSDB();
        }

        #endregion

    }
}