using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class ValoresDataAccess : EntityManagerDataAccess<VALORES, ValoresExtendedView, ValoresParameters, DataModelContext>
    {
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<VALORES>(this.EntityName)
                            // .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<VALORES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<VALORES>(this.EntityName)
                             //.Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override VALORES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<VALORES>(this.EntityName)
                             //.Where(x => x.ID == id && x.Activo == true)
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}