using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{

    public class PHCilindroHistoricoEstadoDataAccess : EntityManagerDataAccess<PhCilindroHistoricoEstado, PHCilindroHistoricoEstadoExtendedView, PHCilindroHistoricoEstadoParameters, TalleresWebEntities>
    {
        public List<PhCilindroHistoricoEstado> ReadEstadoByIDPhCilindro(Guid idPHCilindro)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PhCilindroHistoricoEstado>(this.EntityName)                             
                             .Where(x => x.IDPHCilindro == idPHCilindro)

                            select t;

                return query.ToList();
            }
        }
    }
}