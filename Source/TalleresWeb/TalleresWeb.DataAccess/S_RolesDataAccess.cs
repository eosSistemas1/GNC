using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class S_RolesDataAccess : EntityManagerDataAccess<S_ROLES, S_RolesExtendedView, S_RolesParameters, TalleresWebEntities>
    {
        #region Methods

        public override S_ROLES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ROLES>(this.EntityName)
                             .Where(x => x.ID.Equals(id))//&& x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<S_ROLES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ROLES>(this.EntityName)
                             //.Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public S_ROLES ReadByIdRol(int idRol)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ROLES>(this.EntityName)
                             .Where(x => x.IdRol.Equals(idRol))//&& x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<S_RolesExtendedView> ReadExtendedView(S_RolesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<S_ROLES>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new S_RolesExtendedView
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
                var query = from t in context.CreateQuery<S_ROLES>(this.EntityName)
                            //.Where(x => x.Activo == true)
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