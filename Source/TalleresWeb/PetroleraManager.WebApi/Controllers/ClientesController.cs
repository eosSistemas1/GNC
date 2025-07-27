using System;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Linq;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class ClientesController : ApiController
    {

        #region Members

        private ClientesLogic clientesLogic;

        public ClientesLogic ClientesLogic
        {
            get {
                if (clientesLogic == null) clientesLogic = new ClientesLogic();
                return clientesLogic;
            }
            
        }
        #endregion

        #region Methods

        [ActionName("ReadByDocumento")]
        [HttpGet]
        public ClientesView ReadByDocumento(Guid tipoDocumentoID, String nroDocumento)
        {
            List<Clientes> clientes = this.ClientesLogic.ReadClientesViewByTipoyNroDoc(tipoDocumentoID, nroDocumento);

            if (clientes.Any())
            {
                return ClientesView.ClientesToClientesView(clientes.First());
            }
            
            return default(ClientesView);
        }
          
        #endregion

    }
}