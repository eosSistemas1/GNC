using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class DocumentosDataAccess : EntityManagerDataAccess<DocumentosClientes, DocumentosExtendedView, DocumentosParameters, TalleresWebEntities>
    {
        #region Methods

        public override DocumentosClientes Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<DocumentosClientes>(this.EntityName)
                             .Where(x => x.ID == id)//&& x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<DocumentosClientes> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<DocumentosClientes>(this.EntityName)
                             //.Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override List<DocumentosExtendedView> ReadExtendedView(DocumentosParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<DocumentosClientes>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new DocumentosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }

        #endregion
    }
}