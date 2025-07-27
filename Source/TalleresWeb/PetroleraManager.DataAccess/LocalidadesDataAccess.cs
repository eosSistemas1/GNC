using PetroleraManager.Entities;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetroleraManager.DataAccess
{
    public class LocalidadesDataAccess : EntityManagerDataAccess<LOCALIDADES, LocalidadesExtendedView, LocalidadesParameters, DataModelContext>
    {
        #region Methods

        public override LOCALIDADES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<LOCALIDADES>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<LOCALIDADES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<LOCALIDADES>(this.EntityName)
                             .Include("PROVINCIAS")
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override List<LocalidadesExtendedView> ReadExtendedView(LocalidadesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<LOCALIDADES>(this.EntityName)
                            .Where(x => x.ID != Guid.Empty
                                        && x.Descripcion.Contains(paramentersEntity.Descripcion) 
                                        && x.Activo == true)
                            .OrderBy(o => o.Descripcion)
                            select new LocalidadesExtendedView
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
                var query = from t in context.CreateQuery<LOCALIDADES>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        #endregion
    }
}