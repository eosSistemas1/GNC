using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ReguladoresUnidadDataAccess : EntityManagerDataAccess<ReguladoresUnidad, ReguladoresUnidadExtendedView, ReguladoresUnidadParameters, TalleresWebEntities>
    {
        #region Methods

        public List<ReguladoresUnidad> ReadReguladorUnidad(Guid idRegulador, String nroSerie)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ReguladoresUnidad>(this.EntityName)
                    .Where(x => x.IdRegulador.Equals(idRegulador) && x.Descripcion.Equals(nroSerie))
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}