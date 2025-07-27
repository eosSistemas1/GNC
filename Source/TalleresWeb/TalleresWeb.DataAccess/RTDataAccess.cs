using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class RTDataAccess : EntityManagerDataAccess<RT, RTExtendedView, RTParameters, TalleresWebEntities>
    {

        #region Methods
       public override RT Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<RT>(this.EntityName)                             
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<RT>(this.EntityName)
                             .Where(x => x.ActivoRT == true)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.NombreApellidoRT
                            };

                return query.ToList();
            }
        }

        public override List<RTExtendedView> ReadExtendedView(RTParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<RT>(this.EntityName)
                             .Where(x => x.NombreApellidoRT.Contains(param.Descripcion) && x.ActivoRT)

                            select new RTExtendedView
                            {
                                ID = t.ID,
                                Matricula = t.MatriculaRT,
                                Descripcion = t.NombreApellidoRT
                            };

                return query.ToList();
            }
        }

        public List<RT> ReadDetalladoByID(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<RT>(this.EntityName)                            
                            .Where(x => x.ID.Equals(id))
                            select t;

                return query.ToList();
            }
        }
        public List<RT> ReadByRT(String rt)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<RT>(this.EntityName)
                    .Where(x => x.NombreApellidoRT.Contains(rt))
                            select t;

                return query.ToList();
            }
        }

        public List<String> ReadListRT(String rt)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<RT>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(rt))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }
        public override List<RT> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<RT>(this.EntityName)                            
                             select t;

                return entity.ToList();
            }
        }

        #endregion
    }
}