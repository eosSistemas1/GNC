using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class CRPCController : ApiController
    {

        #region Members
        private CRPCLogic crpcLogic;    
        public CRPCLogic CRPCLogic
        {
            get
            {
                if (crpcLogic == null) crpcLogic = new CRPCLogic();
                    return crpcLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadListCRPC")]
        [HttpGet]
        public List<ViewEntity> ReadListCRPC()
        {
            return this.CRPCLogic.ReadListView();
        }
          
        #endregion

    }
}