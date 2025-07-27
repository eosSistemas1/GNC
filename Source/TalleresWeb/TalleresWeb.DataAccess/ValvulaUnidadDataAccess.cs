using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ValvulaUnidadDataAccess : EntityManagerDataAccess<Valvula_Unidad, ValvulaUnidadExtendedView, ValvulaUnidadParameters, TalleresWebEntities>
    {
        #region Methods

        public List<Valvula_Unidad> ReadValvulaUnidad(Guid idValvula, String nroSerie)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula_Unidad>(this.EntityName)
                    .Where(x => x.IdValvula.Value.Equals(idValvula) && x.Descripcion.Equals(nroSerie))
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}