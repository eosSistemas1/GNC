using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ClientesLogic : EntityManagerLogic<Clientes, ClientesExtendedView, ClientesParameters, ClientesDataAccess>
    {
        #region Methods

        public Clientes AddCliente(Clientes entity)
        {
            return this.EntityDataAccess.AddCliente(entity);
        }

        public List<Clientes> ReadClientesViewByTipoyNroDoc(Guid pTipoDoc, String pNroDoc)
        {
            return this.EntityDataAccess.ReadClientesViewByTipoyNroDoc(pTipoDoc, pNroDoc);
        }
        public List<Clientes> ReadByCliente(String cliente)
        {
            return this.EntityDataAccess.ReadByCliente(cliente);
        }        
        
        #endregion
    }
}