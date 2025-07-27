using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class MarcasDataAccess : EntityManagerDataAccess<MARCAS, MarcasExtendedView, MarcasParameters, DataModelContext>
    {
        public override List<MarcasExtendedView> ReadExtendedView(MarcasParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MARCAS>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true && x.ID != Guid.Empty)
                            .OrderBy(o => o.Descripcion)
                            select new MarcasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Activo = t.Activo
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MARCAS>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<MARCAS> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<MARCAS>(this.EntityName)
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override MARCAS Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<MARCAS>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}