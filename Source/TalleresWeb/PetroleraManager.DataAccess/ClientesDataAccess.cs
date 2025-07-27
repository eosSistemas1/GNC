using PetroleraManager.Entities;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetroleraManager.DataAccess
{
    public class ClientesDataAccess : EntityManagerDataAccess<CLIENTES, ClientesExtendedView, ClientesParameters, DataModelContext>
    {
        #region Methods

        public override CLIENTES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<CLIENTES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override List<ClientesExtendedView> ReadExtendedView(ClientesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                             .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true)

                            select new ClientesExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Domicilio = t.Domicilio,
                                CUIT = t.CUIT
                            };

                return query.ToList();
            }
        }

        public List<ClientesExtendedView> ReadExtendedViewByCodigo(ClientesParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                            .Where(x => x.Codigo == param.Codigo && x.Activo == true)

                            select new ClientesExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Domicilio = t.Domicilio,
                                CUIT = t.CUIT
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public List<ClientesExtendedView> ReadNominaClientes(String razonSocial)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CLIENTES>(this.EntityName)
                            .Include("LOCALIDADES")
                            .Include("LOCALIDADES.PROVINCIAS")
                            .Where(x => x.Descripcion.Contains(razonSocial) || razonSocial == String.Empty)
                            .OrderBy(x => x.Descripcion)
                            select new ClientesExtendedView
                            {
                                ID = t.ID,
                                Codigo = t.Codigo,
                                Descripcion = t.Descripcion,
                                Domicilio = t.Domicilio,
                                Telefono = t.Telefono1,
                                Localidad = t.LOCALIDADES.Descripcion,
                                Provincia = t.LOCALIDADES.PROVINCIAS.Descripcion,
                                CUIT = t.CUIT
                            };

                return query.ToList();
            }
        }

        #endregion
    }
}