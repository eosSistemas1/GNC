using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{
    public class InspeccionesPHDataAccess : EntityManagerDataAccess<InspeccionesPH, InspeccionesPHExtendedView, InspeccionesPHParameters, TalleresWebEntities>
    {
        public void DeleteByPhCilindrosIDAndTipo(Guid phcilindrosID, Guid inspeccionID)
        {
            if (phcilindrosID == null || inspeccionID == null) return;

            using (var context = this.GetEntityContext())
            {
                var inspeccion = new InspeccionesDataAccess().Read(inspeccionID);
                    
                var query = from t in context.CreateQuery<InspeccionesPH>(this.EntityName)
                                             .Where(x => x.IdPHCilndro == phcilindrosID
                                                    && x.Inspecciones.IdInspeccionTipo == inspeccion.IdInspeccionTipo)
                            select t;

                if (inspeccion != null && query.Any())
                {
                    foreach (var item in query.ToList())
                    {
                        this.Delete(item.ID);
                    }
                }
            }
        }

        public List<InspeccionesPH> ReadAllInspeccionesByIDPhCil(Guid phcilindrosID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<InspeccionesPH>(this.EntityName)
                            .Include("Inspecciones")
                    .Where(x => x.IdPHCilndro == phcilindrosID)

                            select t;

                return query.ToList();
            }
        }
    }
}