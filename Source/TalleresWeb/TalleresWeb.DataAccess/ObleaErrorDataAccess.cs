using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ObleaErrorDetalleDataAccess : EntityManagerDataAccess<ObleaErrorDetalle, ObleaErrorDetalleExtendedView, ObleaErrorDetalleParameters, TalleresWebEntities>
    {
        #region Methods
        /// <summary>
        /// Devuelve la corrección del campo filtrando por oblea, tipo de error
        /// y por id de objeto corregido (para cilindros y valvulas)
        /// </summary>
        public String ReadErrorByIDObleaIDTipo(Guid obleaID, Guid tipoErrorID, Guid objetoCorregidoID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleaErrorDetalle>(this.EntityName)
                             .Include("ObleaHistoricoEstado")
                             .Where(x => x.ObleaHistoricoEstado.IDOblea == obleaID
                                      && x.IDObleaErrorTipo == tipoErrorID
                                      && (objetoCorregidoID == Guid.Empty || x.IDObjetoCorregido == objetoCorregidoID ))

                            select t;

                return query != null? query.First().Correccion : String.Empty;
            }            
        }

        /// <summary>
        /// Devuelve error por tipo de error e id historico estado
        /// </summary>
        /// <param name="tipoErrorID"></param>
        /// <param name="obleaHistoricoEstadoID"></param>
        /// <returns></returns>
        public string ReadByIDTipoErrorIDHistoricoEstado(Guid tipoErrorID, Guid obleaHistoricoEstadoID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleaErrorDetalle>(this.EntityName)
                             .Where(x => x.IDObleaHistoricoEstado == obleaHistoricoEstadoID
                                        && x.IDObleaErrorTipo == tipoErrorID)

                            select t.Correccion;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Devuelve los errores para una obblea
        /// </summary>
        public List<ObleaErrorDetalle> ReadErroresByIDOblea(Guid obleaID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleaErrorDetalle>(this.EntityName)
                             .Include("ObleaHistoricoEstado")
                             .Where(x => x.ObleaHistoricoEstado.IDOblea == obleaID)

                            select t;

                return query.ToList();
            }
        }
        #endregion
    }
}