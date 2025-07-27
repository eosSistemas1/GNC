using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.Logic
{
    public class ObleaErrorDetalleLogic : EntityManagerLogic<ObleaErrorDetalle, ObleaErrorDetalleExtendedView, ObleaErrorDetalleParameters, ObleaErrorDetalleDataAccess>
    {
        #region Methods
        /// <summary>
        /// Devuelve los errores para una obblea
        /// </summary>        
        public List<ObleaErrorDetalle> ReadErroresByIDOblea(Guid obleaID)
        {
            return this.EntityDataAccess.ReadErroresByIDOblea(obleaID);
        }

        /// <summary>
        /// Devuelve la corrección del campo filtrando por oblea y tipo de error
        /// </summary>        
        public String ReadErrorByIDObleaIDTipo(Guid obleaID, Guid tipoErrorID)
        {
            return this.EntityDataAccess.ReadErrorByIDObleaIDTipo(obleaID, tipoErrorID, Guid.Empty);
        }

        /// <summary>
        /// Devuelve dominio corregido por id historico estado oblea
        /// </summary>        
        public String ReadDominioCorregidoByIDHistoricoEstado(Guid obleaHistoricoEstadoID)
        {            
            return this.EntityDataAccess.ReadByIDTipoErrorIDHistoricoEstado(ERRORTIPO.DOMINIO, obleaHistoricoEstadoID);
        }

        /// <summary>
        /// Devuelve la corrección del campo filtrando por oblea, tipo de error
        /// y por id de objeto corregido (para cilindros y valvulas)
        /// </summary>        
        public String ReadErrorByIDObleaIDTipo(Guid obleaID, Guid tipoErrorID, Guid objetoCorregidoID)
        {
            return this.EntityDataAccess.ReadErrorByIDObleaIDTipo(obleaID, tipoErrorID, objetoCorregidoID);
        }
        #endregion
    }
}
