using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class PECController : ApiController
    {

        #region Members
        private PECLogic pecLogic;    
        public PECLogic PECLogic
        {
            get
            {
                if (pecLogic == null) pecLogic = new PECLogic();
                    return pecLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadListPEC")]
        [HttpGet]
        public List<ViewEntity> ReadListPEC()
        {
            return this.PECLogic.ReadListView();
        }  
        #endregion

    }
}