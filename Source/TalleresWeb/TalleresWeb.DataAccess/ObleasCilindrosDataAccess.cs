using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ObleasCilindrosDataAccess : EntityManagerDataAccess<ObleasCilindros, ObleasCilindrosExtendedView, ObleasCilindrosParameters, TalleresWebEntities>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasCilindros>(this.EntityName)
                            .Where(x => (x.CilindrosUnidad.Cilindros.IdMarcaCilindro.Value.Equals(param.MarcaCilID) || param.MarcaCilID.Equals(Guid.Empty))
                                     && (x.CilindrosUnidad.Descripcion.Equals(param.SerieCil) || param.SerieCil.Equals(String.Empty))
                                     )
                            select new ObleasExtendedView
                            {
                                ID = t.Obleas.ID,
                                FechaHabilitacion = t.Obleas.FechaHabilitacion.HasValue ? t.Obleas.FechaHabilitacion.Value : CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime,
                                Dominio = t.Obleas.Vehiculos.Descripcion,
                                NombreyApellido = t.Obleas.Clientes.Descripcion,
                                NroObleaAnterior = t.Obleas.Descripcion,
                                NroObleaNueva = t.Obleas.NroObleaNueva,
                            };

                return query.ToList();
            }
        }

        public List<ObleasCilindros> ReadByIDOblea(Guid idOblea)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasCilindros>(this.EntityName)
                            .Include("ObleasValvulas")
                    .Where(x => x.IdOblea.Equals(idOblea))
                            select t;

                return query.ToList();
            }
        }

        public ObleasCilindros ReadUltimaRevisionCilindro(Guid idCilindroUnidad)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasCilindros>(this.EntityName)                            
                    .Where(x => x.IdCilindroUnidad == idCilindroUnidad)
                    .OrderByDescending(o => o.Obleas.FechaHabilitacion)
                            select t;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// lee oblea cilindro por numero de oblea habilitante y cilindro unidad
        /// </summary>
        /// <param name="nroObleaHabilitante"></param>
        /// <param name="idCilindroUnidad"></param>
        /// <returns></returns>
        public ObleasCilindros ReadByIDCilindroUnidadNroOblea(string nroObleaHabilitante, Guid idCilindroUnidad)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasCilindros>(this.EntityName)
                    .Where(x => x.Obleas.Descripcion==nroObleaHabilitante
                             && x.IdCilindroUnidad == idCilindroUnidad)
                            select t;

                return query.FirstOrDefault();
            }
        }

        #endregion
    }
}