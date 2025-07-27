using System;
using System.Collections.Generic;
using System.Linq;
using PL.Fwk.BusinessLogic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class PHCilindroHistoricoEstadoLogic : EntityManagerLogic<PhCilindroHistoricoEstado, PHCilindroHistoricoEstadoExtendedView, PHCilindroHistoricoEstadoParameters, PHCilindroHistoricoEstadoDataAccess>
    {
        /// <summary>
        /// Devuelve el historico de estados para un ph cilindro
        /// </summary>
        /// <param name="idPHCilindro"></param>
        /// <returns></returns>
        public List<PhCilindroHistoricoEstado> ReadEstadoByIDPhCilindro(Guid idPHCilindro)
        {
            return this.EntityDataAccess.ReadEstadoByIDPhCilindro(idPHCilindro);
        }

        /// <summary>
        /// Devuelve el ultimo de los estados para un ph cilindro
        /// </summary>
        /// <param name="idPHCilindro"></param>
        /// <returns></returns>
        public PhCilindroHistoricoEstado ReadUltimoEstadoByIDPhCilindro(Guid idPHCilindro)
        {
             var estados = this.EntityDataAccess.ReadEstadoByIDPhCilindro(idPHCilindro);

            return estados.OrderByDescending(x => x.FechaHoraEstado).FirstOrDefault();
        }
    }
}