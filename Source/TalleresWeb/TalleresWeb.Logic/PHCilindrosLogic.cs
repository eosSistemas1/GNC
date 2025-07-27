using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;
using CrossCutting.DatosDiscretos;
using System.Transactions;

namespace TalleresWeb.Logic
{
    public class PHCilindrosLogic : EntityManagerLogic<PHCilindros, PHCilindrosExtendedView, PHCilindrosParameters, PHCilindrosDataAccess>
    {
        #region Members
        private PHCilindroHistoricoEstadoLogic phCilindroHistoricoEstadoLogic;
        private PHCilindroHistoricoEstadoLogic PhCilindroHistoricoEstadoLogic
        {
            get
            {
                if (this.phCilindroHistoricoEstadoLogic == null) this.phCilindroHistoricoEstadoLogic = new PHCilindroHistoricoEstadoLogic();
                return this.phCilindroHistoricoEstadoLogic;
            }
        }

        private ObleasCilindrosLogic obleasCilindrosLogic;
        private ObleasCilindrosLogic ObleasCilindrosLogic
        {
            get
            {
                if (this.obleasCilindrosLogic == null) this.obleasCilindrosLogic = new ObleasCilindrosLogic();
                return this.obleasCilindrosLogic;
            }
        }

        private CilindrosLogic cilindrosLogic;
        private CilindrosLogic CilindrosLogic
        {
            get
            {
                if (this.cilindrosLogic == null) this.cilindrosLogic = new CilindrosLogic();
                return this.cilindrosLogic;
            }
        }

        private ValvulasLogic valvulaLogic;
        private ValvulasLogic ValvulaLogic
        {
            get
            {
                if (this.valvulaLogic == null) this.valvulaLogic = new ValvulasLogic();
                return this.valvulaLogic;
            }
        }

        private CilindrosUnidadLogic cilindrosUnidadLogic;
        private CilindrosUnidadLogic CilindrosUnidadLogic
        {
            get
            {
                if (this.cilindrosUnidadLogic == null) this.cilindrosUnidadLogic = new CilindrosUnidadLogic();
                return this.cilindrosUnidadLogic;
            }
        }

        private ValvulaUnidadLogic valvulaUnidadLogic;
        private ValvulaUnidadLogic ValvulaUnidadLogic
        {
            get
            {
                if (this.valvulaUnidadLogic == null) this.valvulaUnidadLogic = new ValvulaUnidadLogic();
                return this.valvulaUnidadLogic;
            }
        }
        #endregion

        #region Methods
        public PHCilindros ReadPhCilindroDetallado(Guid idPHCilindro)
        {
            return this.EntityDataAccess.ReadPhCilindroDetallado(idPHCilindro);
        }

        public PHCilindros ReadByNroOperacionCRPC(int nroRevision, Guid? idEstadoPH)
        {
            return EntityDataAccess.ReadByNroOperacionCRPC(nroRevision, idEstadoPH);
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPendientesByPaso(string estacion)
        {
            var pendientesEstacion1 = this.EntityDataAccess.ReadCilindrosPendientesRegistroPeso();
            pendientesEstacion1.AddRange(this.EntityDataAccess.ReadCilindrosPendientesInspeccionRoscas());
            pendientesEstacion1.AddRange(this.EntityDataAccess.ReadCilindrosPendientesInspeccionExterior());
            pendientesEstacion1.AddRange(this.EntityDataAccess.ReadCilindrosPendientesMedicionEspesores());
            pendientesEstacion1 = pendientesEstacion1.GroupBy(ph => ph.ID)
                                                     .Select(grp => grp.First())
                                                     .ToList();
            var pendientesEstacion2 = this.EntityDataAccess.ReadCilindrosPendientesPruebaHidraulica();

            var pendientesEstacion3 = this.EntityDataAccess.ReadCilindrosPendientesInspeccionInterior();

            HashSet<Guid> estacion1Ids = new HashSet<Guid>(pendientesEstacion1.Select(s => s.ID));
            HashSet<Guid> estacion2Ids = new HashSet<Guid>(pendientesEstacion2.Select(s => s.ID));

            if (estacion == "1")
            {
                return pendientesEstacion1;
            }
            else if (estacion == "2")
            {
                return pendientesEstacion2.Where(m => !estacion1Ids.Contains(m.ID)).ToList();
            }
            else if (estacion == "3")
            {
                return pendientesEstacion3.Where(m => !estacion1Ids.Contains(m.ID)
                                                   && !estacion2Ids.Contains(m.ID)).ToList();
            }
            else if (estacion == "4")
            {
                return this.EntityDataAccess.ReadCilindrosPHPorEstado(EstadosPH.Observar);
            }

            return null;
        }


        public List<PHCilindrosPendientesView> ReadCilindrosPHPorEstado(Guid estadoPHID)
        {
            return this.EntityDataAccess.ReadCilindrosPHPorEstado(estadoPHID);
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaReimprimirHojaDeRuta()
        {
            return this.EntityDataAccess.ReadCilindrosPHParaReimprimirHojaDeRuta();
        }

        public void FinalizarProcesoPH(PHCilindros phCilindros, Guid usuarioID)
        {
            var pasosPendientes = new PHProcesoLogic().ObtenerPasosPendientesPH(phCilindros.ID);
            var noHayPasosPendientes = !pasosPendientes.Any();

            if (noHayPasosPendientes)
            {
                this.ActualizarResultadoPH(phCilindros.ID, !phCilindros.Rechazado.Value, phCilindros.NroCertificadoPH, usuarioID, phCilindros.ObservacionPH, false);
            }
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaVerificarCodigos()
        {
            return this.EntityDataAccess.ReadCilindrosPHParaVerificarCodigos();
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaEvaluarValvulas()
        {
            return this.EntityDataAccess.ReadCilindrosPHParaEvaluarValvulas();
        }

        public PHCilindros ReadEnProcesoByCilindroUnidadID(Guid? cilindroUnidadID)
        {
            return this.EntityDataAccess.ReadEnProcesoByCilindroUnidadID(cilindroUnidadID);
        }

        /// <summary>
        /// Devuelve las ph pendientes por zona, si zona es "" devuelve todas las zonas
        /// </summary>
        /// <param name="zona"></param>
        /// <returns></returns>
        public List<PHConsultaView> ReadPHPendientesByZona(string zona)
        {
            return this.EntityDataAccess.ReadPHPendientesByZona(zona);
        }

        /// <summary>
        /// valida no hay una ph finalizada , entregada o despachada para ese cilindro.
        /// </summary>
        /// <param name="idPHCilindro"></param>
        /// <returns></returns>
        public bool HayPHEnCurso(string codHomoCilindro, string serieCilindro)
        {
            return this.EntityDataAccess.HayPHEnCurso(codHomoCilindro, serieCilindro);
        }

        /// <summary>
        /// Actualiza el estado de la PH
        /// </summary>
        /// <param name="idPHCilindro"></param>
        /// <param name="aprobado"></param>
        /// <param name="numeroCertificado"></param>
        /// <param name="usuarioID"></param>
        public void ActualizarResultadoPH(Guid idPHCilindro, bool aprobado, string numeroCertificado, Guid usuarioID, string observaciones, bool finalizada)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    String numeroCertificadoCompleto = numeroCertificado.Length <= 6 ? $"{CrossCutting.DatosDiscretos.CRPC.CodigoCRPC}{numeroCertificado}" : numeroCertificado;
                    var ph = this.Read(idPHCilindro);
                    ph.Rechazado = !aprobado;
                    ph.NroCertificadoPH = numeroCertificadoCompleto;
                    if (finalizada) ph.IdEstadoPH = EstadosPH.Finalizada;

                    this.ObleasCilindrosLogic.ActualizarNumeroCertificadoPH(ph.PH.NroObleaHabilitante, ph.IdCilindroUnidad, numeroCertificadoCompleto);

                    this.Update(ph);

                    if (finalizada)
                    {
                        this.CambiarEstado(idPHCilindro, EstadosPH.Finalizada, observaciones, usuarioID);
                    }

                    ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        /// <summary>
        /// Devuelve los datos para imprimir la hoja de ruta del proceso de PH
        /// </summary>
        /// <param name="idPhCilindro"></param>
        /// <returns></returns>
        public PHCilindrosHojaRutaView ReadParaImprimirHojaRuta(Guid idPhCilindro)
        {
            return this.EntityDataAccess.ReadParaImprimirHojaRuta(idPhCilindro);
        }

        /// <summary>
        /// Cambia el estado de la ph a en proceso o a solicita verificación
        /// </summary>
        /// <param name="criteria"></param>
        public void SolicitarVerificarCodigos(PHCilindrosVerificarCodigosParameter criteria)
        {
            Guid estadoID = !criteria.SolicitarRevision ? EstadosPH.EnProceso : EstadosPH.SolicitaVerificacion;
            String descripcion = !criteria.SolicitarRevision ? "Ingresa a línea de proceso" :
                                $"Cód. Homologación Cil. leído: {criteria.CodigoHomologacionCilLeido} - " +
                                $"Número Serie Cil. leído: {criteria.NumeroSerieCilLeido} -" +
                                $"Código Homologación Val. leído: {criteria.CodigoHomologacionValLeido} - " +
                                $"Número Serie Val. leído: {criteria.NumeroSerieValLeido}";

            this.CambiarEstado(criteria.ID, estadoID, descripcion, criteria.UsuarioID);
        }

        /// <summary>
        /// Acepta la verificación de códigos
        /// </summary>
        /// <param name="iDPhCilindros"></param>
        /// <param name="serieCil"></param>
        /// <param name="homoCil"></param>
        /// <param name="serieVal"></param>
        /// <param name="homoVal"></param>
        /// <param name="iDUsuario"></param>
        public void AceptarVerificarCodigos(Guid iDPhCilindros, string serieCil, string homoCil, string serieVal, string homoVal, Guid iDUsuario)
        {
            var phCilindroDetallado = this.EntityDataAccess.ReadPhCilindroDetallado(iDPhCilindros);

            if (phCilindroDetallado != null)
            {
                Boolean hanCorregidoValores = !String.IsNullOrWhiteSpace(serieCil)
                                             || !String.IsNullOrWhiteSpace(homoCil)
                                             || !String.IsNullOrWhiteSpace(serieVal)
                                             || !String.IsNullOrWhiteSpace(homoVal);
                if (hanCorregidoValores)
                {
                    if (phCilindroDetallado.CilindrosUnidad != null)
                    {
                        ActualizarSerieCilindro(serieCil, phCilindroDetallado);

                        ActualizarCodHomologacionCilindro(homoCil, phCilindroDetallado);
                    }

                    if (phCilindroDetallado.Valvula_Unidad != null)
                    {
                        ActualizarSerieValvula(serieVal, phCilindroDetallado);

                        ActualizarCodHomologacionValvula(homoVal, phCilindroDetallado);
                    }
                }

                CambiarEstado(iDPhCilindros, EstadosPH.IngresarEnLinea, "Se verificaron los códigos.", iDUsuario);
            }
            else
            {
                throw new Exception("Ha ocurrido un error al actualizar los códigos. El registro no existe");
            }
        }

        private void ActualizarCodHomologacionValvula(string homoVal, PHCilindros phCilindroDetallado)
        {
            if (!String.IsNullOrWhiteSpace(homoVal) && phCilindroDetallado.Valvula_Unidad.Valvula != null)
            {
                phCilindroDetallado.Valvula_Unidad.Valvula.Descripcion = homoVal.Trim();
                this.ValvulaLogic.Update(phCilindroDetallado.Valvula_Unidad.Valvula);
            }
        }

        private void ActualizarSerieValvula(string serieVal, PHCilindros phCilindroDetallado)
        {
            if (!String.IsNullOrWhiteSpace(serieVal))
            {
                phCilindroDetallado.Valvula_Unidad.Descripcion = serieVal.Trim();
                this.ValvulaUnidadLogic.Update(phCilindroDetallado.Valvula_Unidad);
            }
        }

        private void ActualizarCodHomologacionCilindro(string homoCil, PHCilindros phCilindroDetallado)
        {
            if (!String.IsNullOrWhiteSpace(homoCil) && phCilindroDetallado.CilindrosUnidad.Cilindros != null)
            {
                phCilindroDetallado.CilindrosUnidad.Cilindros.Descripcion = homoCil.Trim();
                this.CilindrosLogic.Update(phCilindroDetallado.CilindrosUnidad.Cilindros);
            }
        }

        private void ActualizarSerieCilindro(string serieCil, PHCilindros phCilindroDetallado)
        {
            if (!String.IsNullOrWhiteSpace(serieCil))
            {
                phCilindroDetallado.CilindrosUnidad.Descripcion = serieCil.Trim();
                this.CilindrosUnidadLogic.Update(phCilindroDetallado.CilindrosUnidad);
            }
        }

        public void AceptarEvaluacionValvula(Guid iDPhCilindros, string func, string rosca, string observacion, Guid iDUsuario)
        {
            var phCilindro = this.Read(iDPhCilindros);

            if (phCilindro != null)
            {
                phCilindro.RoscaValvula = rosca;
                phCilindro.FuncValvula = func;
                phCilindro.ObservacionValvula = observacion;
                this.Update(phCilindro);
            }
        }

        /// <summary>
        /// Setea el estado nuevo de la ph cilindro y guarda el historial
        /// </summary>
        /// <param name="phCilindroID"></param>
        /// <param name="estadoID"></param>
        /// <param name="descripcion"></param>
        /// <param name="usuarioID"></param>
        public void CambiarEstado(Guid phCilindroID, Guid estadoID, string descripcion, Guid usuarioID)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    var ph = this.EntityDataAccess.Read(phCilindroID);
                    ph.IdEstadoPH = estadoID;

                    this.PhCilindroHistoricoEstadoLogic.Add(new PhCilindroHistoricoEstado()
                    {
                        ID = Guid.NewGuid(),
                        Descripcion = descripcion,
                        IDEstado = estadoID,
                        IDUsuario = usuarioID,
                        IDPHCilindro = ph.ID,
                        FechaHoraEstado = DateTime.Now
                    });

                    this.EntityDataAccess.Update(ph);
                    ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }

            }
        }

        /// <summary>
        /// devuelve la cantidad de ph en proceso , ingresadas y finalizadas 
        /// </summary>
        /// <returns>{en proceso}|{ingresadas}|{finalizadas}</returns>
        public String LeerPHActualmenteEnProceso()
        {
            return this.EntityDataAccess.LeerPHActualmenteEnProceso();
        }

        public int? SetearNumeroInternoOperacion(Guid id)
        {
            var ultimoNumeroOperacion = this.EntityDataAccess.LeerUltimoNumeroOperacion();

            var phCilindrosLogic = this.EntityDataAccess.Read(id);
            phCilindrosLogic.NroOperacionCRPC = ultimoNumeroOperacion + 1;
            Update(phCilindrosLogic);

            return phCilindrosLogic.NroOperacionCRPC;
        }

        public List<PHCilindrosInformarView> ReadPHParaInformar()
        {
            return this.EntityDataAccess.ReadPHParaInformar();
        }

        public List<PHCilindrosInformarView> ReadPHParaAsignar()
        {
            return this.EntityDataAccess.ReadPHParaAsignar();
        }
        #endregion
    }
}

