using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class FleteDataAccess : EntityManagerDataAccess<FLETE, FleteExtendedView, FleteParameters, TalleresWebEntities>
    {
        #region Methods

        public override FLETE Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<FLETE>(this.EntityName)
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<FLETE> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<FLETE>(this.EntityName)                             
                             select t;

                return entity.ToList();
            }
        }

        public override List<FleteExtendedView> ReadExtendedView(FleteParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<FLETE>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new FleteExtendedView
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
                var query = from t in context.CreateQuery<FLETE>(this.EntityName)                                
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