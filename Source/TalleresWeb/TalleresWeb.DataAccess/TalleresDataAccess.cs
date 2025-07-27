using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

/*

using System.Text;

*/

namespace TalleresWeb.DataAccess
{

    public class TalleresDataAccess : EntityManagerDataAccess<Talleres, TalleresExtendedView, TalleresParameters, TalleresWebEntities>
    {
        #region Methods
        public override Talleres Read(Guid id)
         {
             using (var context = this.GetEntityContext())
             {
                 var entity = from t in context.CreateQuery<Talleres>(this.EntityName)
                              .Include("TalleresRT")
                              .Where(x => x.ID == id)
                              select t;

                 return entity.FirstOrDefault();
             }
         }
        
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                             .Where(x => x.ActivoTaller == true)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.RazonSocialTaller
                            };

                return query.OrderBy(t => t.Descripcion).ToList();
            }
        }

        public override List<TalleresExtendedView> ReadExtendedView(TalleresParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                             .Where(x => x.RazonSocialTaller.Contains(param.Descripcion) && x.ActivoTaller == true)

                            select new TalleresExtendedView
                            {
                                ID = t.ID,
                                Matricula = t.Descripcion,
                                Descripcion = t.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        public List<Talleres> ReadDetalladoByID(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                            .Where(x => x.ID.Equals(id))
                            select t;

                return query.ToList();
            }
        }

        public List<Talleres> ReadByTalleres(String taller)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                    .Where(x => x.RazonSocialTaller.Contains(taller))
                            select t;

                return query.ToList();
            }
        }

        public List<String> ReadListTalleres(String taller)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(taller))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }
        /*   public List<TalleresExtendedView> ReadExtendedViewByMatricula(TalleresParameters param)
           {
               using (var context = this.GetEntityContext())
               {
                   var query = from t in context.CreateQuery<Talleres>(this.EntityName)
                               .Where(x => x.Descripcion == param.Matricula && x.ActivoTaller == true)

                               select new TalleresExtendedView
                               {
                                   ID = t.ID,
                                   Matricula = t.Descripcion,
                                   Descripcion = t.RazonSocialTaller
                               };

                   return query.ToList();
               }
           }*/

        public ViewEntity ReadByDescripcion(String rt)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MarcasCilindros>(this.EntityName)
                            .Where(x => x.Descripcion == rt)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.FirstOrDefault();
            }
        }

        #endregion
    }
}