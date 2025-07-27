using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class VendedoresDataAccess : EntityManagerDataAccess<VENDEDORES, VendedoresExtendedView, VendedoresParameters, DataModelContext>
    {
        public override List<VendedoresExtendedView> ReadExtendedView(VendedoresParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<VENDEDORES>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true)
                            .OrderBy(o => o.Descripcion)
                            select new VendedoresExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Email = t.Email,
                                Activo = t.Activo
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<VENDEDORES>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<VENDEDORES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<VENDEDORES>(this.EntityName)
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override VENDEDORES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<VENDEDORES>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}