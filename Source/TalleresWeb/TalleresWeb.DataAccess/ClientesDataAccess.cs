using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{
    public class ClientesDataAccess : EntityManagerDataAccess<Clientes, ClientesExtendedView, ClientesParameters, TalleresWebEntities>
    {
        #region Methods

        public Clientes AddCliente(Clientes entity)
        {
            var cliente = this.Read(entity.ID);

            if (cliente != null)
            {
                this.Update(entity);
            }
            else
            {
                this.Add(entity);
            }

            return entity;
        }

        public List<Clientes> ReadClientesViewByTipoyNroDoc(Guid pTipoDoc, String pNroDoc)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Clientes>(this.EntityName)
                                             .Include("DocumentosClientes")
                                             .Include("Localidades")
                                             .Include("Localidades.Provincias")
                                             .Where(x => x.IdTipoDniCliente.Equals(pTipoDoc) 
                                                       && x.NroDniCliente.Equals(pNroDoc))                    
                            select t;

                return query.ToList();
            }
        }
        public List<Clientes> ReadByCliente(String cliente)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Clientes>(this.EntityName)                
                    .Where(x => x.Descripcion.Contains(cliente))
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}