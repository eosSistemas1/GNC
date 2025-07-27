using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class TipoDocumentoController : ApiController
    {

        #region Members


        private DocumentosLogic documentosLogic;

        public DocumentosLogic DocumentosLogic
        {
            get
            {
                if (documentosLogic == null) documentosLogic = new DocumentosLogic();
                    return documentosLogic;
            }
        }

        #endregion

        #region Methods

        [ActionName("ReadListTiposDocumentos")]
        [HttpGet]
        public List<ViewEntity> ReadListTiposDocumentos()
        {
            return DocumentosLogic.ReadListView();
        }
          
        #endregion

    }
}