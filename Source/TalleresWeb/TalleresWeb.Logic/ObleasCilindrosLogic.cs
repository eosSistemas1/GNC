using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Transactions;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ObleasCilindrosLogic : EntityManagerLogic<ObleasCilindros, ObleasCilindrosExtendedView, ObleasCilindrosParameters, ObleasCilindrosDataAccess>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            return EntityDataAccess.ReadAllConsultaEnBase(param);
        }

        public List<ObleasCilindros> ReadByIDOblea(Guid idOblea)
        {
            ObleasCilindrosDataAccess oa = new ObleasCilindrosDataAccess();

            return oa.ReadByIDOblea(idOblea);
        }

        /// <summary>
        /// Actualiza el nro de certificado ph en el cilindro correspondiente de la tabla oblea cilindros
        /// </summary>
        /// <param name="nroObleaHabilitante"></param>
        /// <param name="idCilindroUnidad"></param>
        /// <param name="numeroCertificadoCompleto"></param>
        public void ActualizarNumeroCertificadoPH(string nroObleaHabilitante, Guid idCilindroUnidad, string numeroCertificadoCompleto)
        {
            ObleasCilindros obleaCilindro = this.EntityDataAccess.ReadByIDCilindroUnidadNroOblea(nroObleaHabilitante, idCilindroUnidad);
            
            if (obleaCilindro != null)
            {
                obleaCilindro.NroCertificadoPH = numeroCertificadoCompleto;
                this.EntityDataAccess.Update(obleaCilindro);
            }            
        }

        public ObleasCilindros ReadUltimaRevisionCilndro(Guid idCilindroUnidad)
        {
            return EntityDataAccess.ReadUltimaRevisionCilindro(idCilindroUnidad);
        }

        #endregion
    }
}