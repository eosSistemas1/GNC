using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{

    public class S_CasosDeUsoDataAccess : EntityManagerDataAccess<S_CASOSDEUSO, S_CasosDeUsoExtendedView, S_CasosDeUsoParameters, TalleresWebEntities>
    {
        public override List<S_CasosDeUsoExtendedView> ReadExtendedView(S_CasosDeUsoParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<S_CASOSDEUSO>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new S_CasosDeUsoExtendedView
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
                var query = from t in context.CreateQuery<S_CASOSDEUSO>(this.EntityName)
                             //.Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<S_CASOSDEUSO> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_CASOSDEUSO>(this.EntityName)
                             //.Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override S_CASOSDEUSO Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_CASOSDEUSO>(this.EntityName)
                             .Where(x => x.ID.Equals(id) )//&& x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

    }
}