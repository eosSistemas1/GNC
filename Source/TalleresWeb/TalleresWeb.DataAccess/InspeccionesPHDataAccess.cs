using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{
    public class InspeccionesDataAccess : EntityManagerDataAccess<Inspecciones, InspeccionesExtendedView, InspeccionesParameters, TalleresWebEntities>
    {
        #region Methods
        public List<ViewEntity> ReadInspeccionesPorTipo(Guid tipoInspeccionID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Inspecciones>(this.EntityName)
                    .Where(x => x.IdInspeccionTipo==tipoInspeccionID)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.OrderBy(c => c.Descripcion).ToList();
            }
        }
        #endregion

    }
}