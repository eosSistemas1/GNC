using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{

    public class ProvinciasDataAccess : EntityManagerDataAccess<Provincias, ProvinciasExtendedView, ProvinciasParameters, TalleresWebEntities>
    {
        public override List<ProvinciasExtendedView> ReadExtendedView(ProvinciasParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Provincias>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new ProvinciasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Provincias>(this.EntityName)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<Provincias> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<Provincias>(this.EntityName)
                             select t;

                return entity.ToList();
            }
        }

        public override Provincias Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<Provincias>(this.EntityName)
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}