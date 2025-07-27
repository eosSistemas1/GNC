using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class CilindrosUnidadDataAccess : EntityManagerDataAccess<CilindrosUnidad, CilindrosUnidadExtendedView, CilindrosUnidadParameters, TalleresWebEntities>
    {
        #region Methods

        public List<CilindrosUnidad> ReadCilindroUnidad(Guid idCilindro, String nroSerie)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CilindrosUnidad>(this.EntityName)
                    .Where(x => x.IdCilindro.Value.Equals(idCilindro) && x.Descripcion == nroSerie.Trim())
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}