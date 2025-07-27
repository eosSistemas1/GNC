using System;
using PL.Fwk.BusinessLogic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.Logic
{
    public class InformeLogic : EntityManagerLogic<INFORME, InformeExtendedView, InformeParameters, InformeDataAccess>
    {
        #region Propiedades
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }

        private InformeDetalleDataAccess informeDetalleDataAccess;
        public InformeDetalleDataAccess InformeDetalleDataAccess
        {
            get
            {
                if (informeDetalleDataAccess == null) informeDetalleDataAccess = new InformeDetalleDataAccess();
                return informeDetalleDataAccess;
            }
        }
        #endregion

        /// <summary>
        /// Actualiza cantidades y estado de obleas del informe
        /// </summary>
        /// <param name="informeID"></param>
        /// <param name="fichasOK"></param>
        /// <param name="fichasError"></param>
        public void ActualizarInforme(Guid informeID, int fichasOK, int fichasError)
        {
            INFORME informe = this.Read(informeID);

            informe.CantidadObleasAsignadas += fichasOK;
            informe.CantidadObleasRechazadas += fichasError;

            int fichasPendientes = informe.CantidadObleasEnviadas -
                                   informe.CantidadObleasAsignadas -
                                   informe.CantidadObleasRechazadas;

            if (fichasPendientes <= 0)
            {
                informe.Estado = true;
            }
            else
            {
                informe.Estado = false;
            }

            this.Update(informe);
        }

        /// <summary>
        /// Crea un informe y devuelve el nro de informe
        /// </summary>
        /// <param name="cantidadFichasEnviadas"></param>
        /// <returns></returns>
        public INFORME CrearInforme(int cantidadFichasEnviadas)
        {
            INFORME informe = new INFORME
            {
                ID = Guid.NewGuid(),
                CantidadObleasEnviadas = cantidadFichasEnviadas,
                FechaHora = DateTime.Now,
                Activo = true
            };

            this.Add(informe);

            return this.Read(informe.ID);
        }

        public List<InformesPendientesView> ReadAllInformePendiente()
        {
            return this.EntityDataAccess.ReadAllInformePendiente();
        }

        public void EliminarInforme(Guid informeSeleccionadoID, Guid idUsuario)
        {
            //using (TransactionScope ss = new TransactionScope())
            //{
                try
                {
                    INFORME informe = this.Read(informeSeleccionadoID);

                    if (informe == null) throw new Exception("El informe seleccionado es inexistente");

                    informe.Activo = false;
                    this.Update(informe);

                    ObleasLogic obleasLogic = new ObleasLogic();
                    foreach (var item in informe.INFORMEOBLEAS)
                    {
                        Obleas oblea = obleasLogic.Read(item.idOblea.Value);

                        var obleasEstadosAnteriores = oblea.ObleaHistoricoEstado.OrderByDescending(x => x.FechaHora)
                                                                                .FirstOrDefault(e => e.IDEstadoOblea != ESTADOSFICHAS.Informada
                                                                                            && e.IDEstadoOblea != ESTADOSFICHAS.InformadaConError);

                        Guid estadoNuevoObleaID = Guid.Empty;

                        if (obleasEstadosAnteriores != null)
                            estadoNuevoObleaID = obleasEstadosAnteriores.IDEstadoOblea;

                        if (estadoNuevoObleaID != Guid.Empty)
                        {
                            String observacion = $"Anulación Informe {informe.Numero} - Usuario: {idUsuario}";
                            obleasLogic.CambiarEstado(oblea.ID, estadoNuevoObleaID, observacion, idUsuario);
                        }
                    }

                  //  ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
            //}

        }

        public int CerrarInforme(Guid informeID, List<Guid> obleasBajasID, Guid idUsuario)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    var informe = this.EntityDataAccess.Read(informeID);

                    foreach (var item in obleasBajasID)
                    {
                        var obleaInforme = this.InformeDetalleDataAccess.Read(item);
                        obleaInforme.Descripcion = "Oblea operación baja";

                        //actulizo el detalle del informe para la oblea
                        this.InformeDetalleDataAccess.Update(obleaInforme);

                        //actualizo el estado de la oblea
                        this.ObleasLogic.CambiarEstado(obleaInforme.idOblea.Value, ESTADOSFICHAS.Finalizada, "Oblea operación baja", idUsuario);
                    }

                    int fichasOK = obleasBajasID.Count();
                    this.ActualizarInforme(informe.ID, fichasOK, 0);

                    ss.Complete();

                    return obleasBajasID.Count();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}