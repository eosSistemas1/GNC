using PetroleraManager.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;

namespace PetroleraManager.Logic
{
    public class ClientesLogic : EntityManagerLogic<CLIENTES, ClientesExtendedView, ClientesParameters, ClientesDataAccess>
    {
        #region Methods

        public List<ClientesExtendedView> ReadExtendedViewByCodigo(ClientesParameters param)
        {
            ClientesDataAccess oa = new ClientesDataAccess();
            return oa.ReadExtendedViewByCodigo(param);
        }

        public List<ClientesExtendedView> ReadNominaClientes(String param)
        {
            ClientesDataAccess oa = new ClientesDataAccess();
            return oa.ReadNominaClientes(param);
        }

        #endregion
    }
}