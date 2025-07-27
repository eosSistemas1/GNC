using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class InformeDetalleDataAccess : EntityManagerDataAccess<INFORMEOBLEAS, InformeDetalleExtendedView, InformeDetalleParameters, TalleresWebEntities>
    {
        #region Methods
        public void ActualizarDetalleObleaInforme(Guid informeID, Guid? obleaID, String descripcionError, Guid? estado)
        {
            using (var context = this.GetEntityContext())
            {
                //var entity = (from t in context.CreateQuery<INFORMEOBLEAS>(this.EntityName)
                //             .Where(x =>  x.IdInforme == informeID 
                //                       && x.idOblea == obleaID)
                //             select t).FirstOrDefault();

                //entity.Descripcion = descripcionError;

                context.INFORMEOBLEAS.Where(x => x.IdInforme == informeID && x.idOblea == obleaID).First().Descripcion = descripcionError;
                if(estado.HasValue)context.INFORMEOBLEAS.Where(x => x.IdInforme == informeID && x.idOblea == obleaID).First().IdEstadoOblea = estado.Value;

                context.SaveChanges();
                
            }
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasEnviadas(Guid informeID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = (from t in context.CreateQuery<INFORMEOBLEAS>(this.EntityName)
                             .Where(x => x.IdInforme == informeID)
                              select new InformeDetalleBasicView
                                    {
                                        Taller = t.Obleas.Talleres.RazonSocialTaller,
                                        Dominio = t.Obleas.Vehiculos.Descripcion,
                                        NumeroObleaAnterior = t.Obleas.Descripcion,
                                        Operacion = t.Obleas.Operaciones.Descripcion
                                    });

                return entity.ToList();
            }
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasRechazadas(Guid informeID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = (from t in context.CreateQuery<INFORMEOBLEAS>(this.EntityName)
                             .Where(x => x.IdInforme == informeID 
                                        && x.IdEstadoOblea == ESTADOSFICHAS.RechazadaPorEnte)
                              select new InformeDetalleBasicView
                              {
                                  Taller = t.Obleas.Talleres.RazonSocialTaller,
                                  Dominio = t.Obleas.Vehiculos.Descripcion,
                                  NumeroObleaAnterior = t.Obleas.Descripcion,
                                  Operacion = t.Obleas.Operaciones.Descripcion
                              });

                return entity.ToList();
            }
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasAsignadas(Guid informeID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = (from t in context.CreateQuery<INFORMEOBLEAS>(this.EntityName)
                             .Where(x => x.IdInforme == informeID
                                        && (x.IdEstadoOblea == ESTADOSFICHAS.Asignada
                                              || x.IdEstadoOblea == ESTADOSFICHAS.AsignadaConError
                                              || (x.Obleas.IdEstadoFicha == ESTADOSFICHAS.Finalizada
                                                    && x.Obleas.IdOperacion == TIPOOPERACION.Baja))
                                    )
                              select new InformeDetalleBasicView
                              {
                                  Taller = t.Obleas.Talleres.RazonSocialTaller,
                                  Dominio = t.Obleas.Vehiculos.Descripcion,
                                  NumeroObleaAnterior = t.Obleas.Descripcion,
                                  Operacion = t.Obleas.Operaciones.Descripcion
                              });

                return entity.ToList();
            }
        }

        /// <summary>
        /// Devuelve los trámites bajas de un informe excluyendo los que fueron rechazados por el ente
        /// </summary>
        public List<InformeDetalleBasicView> ReadBajasByInformeID(Guid informeID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = (from t in context.CreateQuery<INFORMEOBLEAS>(this.EntityName)
                             .Where(x => x.IdInforme == informeID
                                        && x.Obleas.IdOperacion == TIPOOPERACION.Baja
                                        && (x.Obleas.IdEstadoFicha == ESTADOSFICHAS.Informada
                                           || x.Obleas.IdEstadoFicha == ESTADOSFICHAS.InformadaConError))

                              select new InformeDetalleBasicView
                              {
                                  ID = t.ID,
                                  Taller = t.Obleas.Talleres.RazonSocialTaller,
                                  Dominio = t.Obleas.Vehiculos.Descripcion,
                                  NumeroObleaAnterior = t.Obleas.Descripcion,
                                  Operacion = t.Obleas.Operaciones.Descripcion
                              });

                return entity.ToList();
            }
        }
        #endregion
    }
}