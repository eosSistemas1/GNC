using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class ProveedoresDataAccess : EntityManagerDataAccess<PROVEEDORES, ProveedoresExtendedView, ProveedoresParameters, DataModelContext>
    {
        public override List<ProveedoresExtendedView> ReadExtendedView(ProveedoresParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                             .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true)

                            select new ProveedoresExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Domicilio  = t.Domicilio,
                                CUIT = t.Cuit 
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<PROVEEDORES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override PROVEEDORES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public List<ProveedoresExtendedView> ReadExtendedViewByCodigo(ProveedoresParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                             .Where(x => x.Codigo == param.Codigo && x.Activo == true)

                            select new ProveedoresExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Domicilio = t.Domicilio,
                                CUIT = t.Cuit
                            };

                return query.ToList();
            }
        }

        public List<ProveedoresExtendedView> ReadNominaProveedores(String razonSocial)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PROVEEDORES>(this.EntityName)
                            .Include("LOCALIDADES")
                            .Include("LOCALIDADES.PROVINCIAS")
                            .Where(x => x.Descripcion.Contains(razonSocial) || razonSocial == String.Empty)
                            .OrderBy(x => x.Descripcion)
                            select new ProveedoresExtendedView
                            {
                                ID = t.ID,
                                Codigo = t.Codigo,
                                RazonSocial = t.Descripcion,
                                Domicilio = t.Domicilio,
                                Telefono = t.Telefono1,
                                Localidad = t.LOCALIDADES.Descripcion,
                                Provincia = t.LOCALIDADES.PROVINCIAS.Descripcion,
                                CUIT = t.Cuit
                            };

                return query.ToList();
            }
        }
    }
}