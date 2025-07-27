using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class InformeDataAccess : EntityManagerDataAccess<INFORME, InformeExtendedView, InformeParameters, TalleresWebEntities>
    {
        #region Methods

        public override INFORME Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INFORME>(this.EntityName)
                             .Include("INFORMEOBLEAS")
                             .Include("INFORMEOBLEAS.OBLEAS")
                             .Where(x => x.ID == id && x.Activo)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<InformesPendientesView> ReadAllInformePendiente()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INFORME>(this.EntityName)
                             .Where(x => !x.Estado
                                       && x.CantidadObleasEnviadas > (x.CantidadObleasAsignadas + x.CantidadObleasRechazadas)
                                       && x.Activo)


                            select new InformesPendientesView()
                            {
                                ID = t.ID,
                                Numero = t.Numero,
                                FechaHora = t.FechaHora,
                                CantidadObleasEnviadas = t.INFORMEOBLEAS.Count(),
                                CantidadObleasAsignadas = t.INFORMEOBLEAS.Count(o => o.Obleas.IdEstadoFicha == ESTADOSFICHAS.Asignada
                                                                                     || o.Obleas.IdEstadoFicha == ESTADOSFICHAS.AsignadaConError),
                                CantidadObleasRechazadas = t.INFORMEOBLEAS.Count(o => o.Obleas.IdEstadoFicha == ESTADOSFICHAS.RechazadaPorEnte),
                                CantidadObleasBaja = t.INFORMEOBLEAS.Count( o => o.Obleas.IdOperacion == TIPOOPERACION.Baja),
                            };
              
                return query.OrderBy(t => t.Numero).ToList();
            }
        }
        #endregion
    }
}