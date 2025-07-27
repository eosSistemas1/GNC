using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class LocalidadesDataAccess : EntityManagerDataAccess<Localidades, LocalidadesExtendedView, LocalidadesParameters, TalleresWebEntities>
    {
        #region Methods

        public override Localidades Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<Localidades>(this.EntityName)
                             .Include("Provincias")
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<Localidades> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<Localidades>(this.EntityName)
                             .Include("PROVINCIAS")
                             select t;

                return entity.ToList();
            }
        }

        public override List<LocalidadesExtendedView> ReadExtendedView(LocalidadesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Localidades>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new LocalidadesExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Localidades>(this.EntityName)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.OrderBy(x => x.Descripcion).ToList();
            }
        }


        public List<Localidades> ReadByLocalidad(String localidad)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Localidades>(this.EntityName)
                    .Where(x => x.Descripcion.Contains(localidad))
                            select t;

                return query.ToList();
            }
        }

        public List<String> ReadListLocalidades(String localidad)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Localidades>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(localidad))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }

        #endregion
    }
}
