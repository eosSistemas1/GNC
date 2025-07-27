using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    //[Authorize]
    public class ProvinciasController : ApiController
    {

        #region Members


        private ProvinciasLogic provinciasLogic;

        public ProvinciasLogic ProvinciasLogic
        {
            get
            {
                if (provinciasLogic == null) provinciasLogic = new ProvinciasLogic();
                    return provinciasLogic;
            }
        }

        #endregion

        #region Methods

        [ActionName("ReadListProvincias")]
        [HttpGet]
        public List<ViewEntity> ReadListProvincias()
        {
            return ProvinciasLogic.ReadListView();
        }
          
        #endregion

    }
}