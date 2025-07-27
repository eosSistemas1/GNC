using System;
using System.Collections.Generic;
using PL.Fwk.BusinessLogic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class InformeDetalleLogic : EntityManagerLogic<INFORMEOBLEAS, InformeDetalleExtendedView, InformeDetalleParameters, InformeDetalleDataAccess>
    {
        public void ActualizarDetalleObleaInforme(Guid informeID, Guid? obleaID, string descripcionError, Guid? estado)
        {
            this.EntityDataAccess.ActualizarDetalleObleaInforme(informeID, obleaID, descripcionError, estado);
        }

        /// <summary>
        /// inserto en el detalle una oblea para un informe
        /// </summary>
        /// <param name="informeID"></param>
        /// <param name="obleaID"></param>
        public void InsertarDetalleObleaInforme(Guid informeID, Guid obleaID, Guid estadoActualID)
        {
            INFORMEOBLEAS item = new INFORMEOBLEAS();
            item.ID = Guid.NewGuid();
            item.IdInforme = informeID;
            item.idOblea = obleaID;
            item.IdEstadoOblea = estadoActualID;
            item.Descripcion = String.Empty;

            this.Add(item);
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasEnviadas(Guid informeID)
        {
            return this.EntityDataAccess.ReadDetalleNumerosObleasEnviadas(informeID);
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasAsignadas(Guid informeID)
        {
            return this.EntityDataAccess.ReadDetalleNumerosObleasAsignadas(informeID);
        }

        public List<InformeDetalleBasicView> ReadDetalleNumerosObleasRechazadas(Guid informeID)
        {
            return this.EntityDataAccess.ReadDetalleNumerosObleasRechazadas(informeID);
        }

        /// <summary>
        /// Devuelve los trámites bajas de un informe excluyendo los que fueron rechazados por el ente
        /// </summary>
        public List<InformeDetalleBasicView> ReadBajasByInformeID(Guid informeID)
        {
            return this.EntityDataAccess.ReadBajasByInformeID(informeID);
        }
    }
}