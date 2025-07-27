using CrossCutting.DatosDiscretos;
using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ObleasLogic : EntityManagerLogic<Obleas, ObleasExtendedView, ObleasParameters, ObleasDataAccess>
    {
        #region Methods

        public Obleas ReadAllByNroObleaNueva(String nroObleaNueva)
        {
            return EntityDataAccess.ReadAllByNroObleaNueva(nroObleaNueva);
        }

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            return EntityDataAccess.ReadAllConsultaEnBase(param);
        }

        public List<ObleasExtendedView> ReadAllInformeEstadoObleas(ObleasParameters paramentersEntity)
        {
            return EntityDataAccess.ReadAllInformeEstadoObleas(paramentersEntity);
        }

        public Obleas ReadDetalladoByID(Guid idOblea)
        {
            return EntityDataAccess.ReadDetalladoByID(idOblea);
        }

        public Obleas ReadDetalladoByObleaAnterior(String nroObleaAnt)
        {
            return EntityDataAccess.ReadDetalladoByObleaAnterior(nroObleaAnt);
        }

        public List<ObleasConsultarView> ReadObleasConsulta(DateTime fechaDesde,
                                                            DateTime fechaHasta,
                                                            string dominio,
                                                            string numeroOblea,
                                                            Guid tallerID)
        {
            return EntityDataAccess.ReadObleasConsulta(fechaDesde, fechaHasta, dominio, numeroOblea, tallerID);
        }

        public List<ObleasParaCorregirDominioView> ReadFichaParaCorregirDominio(DateTime fechaDesde, DateTime fechaHasta, string numeroOblea, string dominioConError)
        {
            var obleas = EntityDataAccess.ReadFichaParaCorregirDominio(fechaDesde, fechaHasta, numeroOblea, dominioConError);

            foreach (var oblea in obleas)
            {
                var obleaErrorDetalle = new ObleaErrorDetalleLogic().ReadDominioCorregidoByIDHistoricoEstado(oblea.IdObleaHistoricoEstado);
                oblea.DominioOK = obleaErrorDetalle;
            }

            return obleas.Where(o => o.DominioConError != o.DominioOK).ToList();
        }

        public List<ObleasExtendedView> ReadObleasAInformar(ObleasParameters param)
        {
            return EntityDataAccess.ReadObleasAInformar(param);
        }

        public List<ObleasExtendedView> ReadObleasPorEstado(Guid idEstadoFicha, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return EntityDataAccess.ReadObleasPorEstado(idEstadoFicha, fechaDesde, fechaHasta);
        }        

        public List<ObleasExtendedView> ReadObleasRealizadas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return EntityDataAccess.ReadObleasRealizadas(fechaDesde, fechaHasta);
        }

        public List<ObleasExtendedView> ReadObleasAVencer(DateTime fechaDesde, DateTime fechaHasta)
        {
            return EntityDataAccess.ReadObleasAVencer(fechaDesde, fechaHasta);
        }

        public int ActualizarObleaErrorAsignada(ObleaCargaResultadosView oblea, Guid informeID, Guid idUsuario)
        {
            Obleas o = this.ReadObleaByObleaCargaResultadosView(oblea);            

            if (o != null)
            {
                if (o.IdEstadoFicha != ESTADOSFICHAS.Informada && o.IdEstadoFicha != ESTADOSFICHAS.InformadaConError)
                    return 0;

                String descripcion = String.Format(oblea.DescripcionError);
                this.CambiarEstado(o.ID, ESTADOSFICHAS.RechazadaPorEnte, descripcion, idUsuario);

                this.ActualizarDetalleObleaInforme(informeID, o.ID, descripcion, ESTADOSFICHAS.RechazadaPorEnte);

                return 1;
            }
            else
            {
                String descripcionError = String.Format("La oblea del taller matrícula {0} con número interno {1} no existe.", oblea.CodigoTaller, oblea.NroInternoObleaTaller);
                this.ActualizarDetalleObleaInforme(informeID, default(Guid?), descripcionError);
            }

            return 0;
        }

        public int ActualizarObleaAsignada(ObleaCargaResultadosView oblea, Guid informeID, Guid idUsuario)
        {
            Obleas o = this.ReadObleaByObleaCargaResultadosView(oblea);

            if (o != null)
            {
                // si no esta en estado para asignar salgo
                if (o.IdEstadoFicha != ESTADOSFICHAS.Informada 
                    && o.IdEstadoFicha != ESTADOSFICHAS.InformadaConError) return 0;

                String descripcion = String.Format("Oblea asignada por Enargas Nro. {0}", oblea.NroObleaAsignada);
                Guid estado = ESTADOSFICHAS.Asignada;

                if (o.IdEstadoFicha == ESTADOSFICHAS.InformadaConError)
                {
                    descripcion = String.Format("Oblea asignada con error por Enargas Nro. {0}", oblea.NroObleaAsignada);
                    estado = ESTADOSFICHAS.AsignadaConError;
                }

                o.NroObleaNueva = oblea.NroObleaAsignada;
                o.FechaVencimiento = this.GetFechaVencimientoOblea(o);
                this.Update(o);

                this.CambiarEstado(o.ID, estado, descripcion, idUsuario);

                this.ActualizarDetalleObleaInforme(informeID, o.ID, descripcion, estado);

                return 1;
            }
            else
            {
                String descripcionError = String.Format("La oblea del taller matrícula {0} con número interno {1} no existe.", oblea.CodigoTaller, oblea.NroInternoObleaTaller);
                this.ActualizarDetalleObleaInforme(informeID, default(Guid?), descripcionError);
            }

            return 0;
        }

        /// <summary>
        /// Valida si existe un trámite pendiente para un dominio
        /// </summary>
        /// <param name="dominio"></param>
        /// <returns></returns>
        public bool ExisteTramitePendienteParaElDominio(string dominio)
        {
            return this.EntityDataAccess.ExisteTramitePendienteParaElDominio(dominio);
        }

        /// <summary>
        /// Valida si existe una oblea con el nro oblea enviado por parámetro
        /// </summary>
        public string ExisteObleaConNroObleaAnterior(String nroObleaAnterior, Guid? obleaID)
        {
            var oblea = this.EntityDataAccess.ReadObleaByNumeroAnterior(nroObleaAnterior);

            string estado = string.Empty;

            if (oblea.Count > 1) estado = oblea.First().EstadosFichas.Descripcion;

            if (obleaID.HasValue && obleaID != Guid.Empty)
            {
                if (oblea.Count == 1 && oblea.Any(x => x.ID != obleaID)) estado = oblea.First().EstadosFichas.Descripcion;
            }
            else
            {
                if (oblea.Count == 1) estado = oblea.First().EstadosFichas.Descripcion;
            }

            return estado;
        }

        /// <summary>
        /// Valida si existe una oblea con el nro oblea enviado por parámetro
        /// </summary>
        public bool ExisteObleaConNroObleaAnterior(String nroObleaAnterior)
        {
            return EntityDataAccess.ExisteObleaConNroObleaAnterior(nroObleaAnterior);
        }

        /// <summary>
        /// la fecha de vencimiento es un año mas de la anterior,
        /// en el caso que vencen cilindros antes del año, la fecha es la del vencimiento del cilindro
        /// </summary>        
        public DateTime? GetFechaVencimientoOblea(Obleas o)
        {
            DateTime fechaVencimientoNormal = o.FechaHabilitacion.Value.AddYears(1);

            var cilindroConVencimientoMasViejo = o.ObleasCilindros.OrderBy(x1 => new DateTime(x1.AnioUltimaRevisionCil, x1.MesUltimaRevisionCil, 1))
                                                                  .First(x => x.IdOperacion == MSDB.Montaje || x.IdOperacion == MSDB.Sigue);

            var anio = cilindroConVencimientoMasViejo.AnioUltimaRevisionCil + 2000;
            var mes = cilindroConVencimientoMasViejo.MesUltimaRevisionCil;

            DateTime fechaCilindroConVencimientoMasViejo = new DateTime(anio, mes, DateTime.DaysInMonth(anio, mes)).AddYears(5);

            if (fechaCilindroConVencimientoMasViejo < fechaVencimientoNormal) return fechaCilindroConVencimientoMasViejo;

            return fechaVencimientoNormal;
        }

        /// <summary>
        /// Método usado para cambiar estado a una oblea y guardar su histórico de estados
        /// </summary>        
        public void CambiarEstado(Guid obleaID, Guid idEstadoFicha, String observacion, Guid idUsuario)
        {
            this.CambiarEstado(obleaID, idEstadoFicha, observacion, new List<ObleaErrorDetalle>(), idUsuario);
        }

        /// <summary>
        /// Método usado para cambiar estado a una oblea y guardar su histórico de estados
        /// </summary>        
        public void CambiarEstado(Guid obleaID, Guid idEstadoFicha, String observacion, List<ObleaErrorDetalle> correcciones, Guid idUsuario)
        {

            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    Obleas oblea = this.Read(obleaID);

                    if (oblea == null)
                    {
                        throw new Exception("No se puede cambiar estado. La oblea es inexistente.");
                    }

                    oblea.IdEstadoFicha = idEstadoFicha;
                    oblea.ObservacionesFicha = observacion;
                    this.Update(oblea);

                    //Inserta en el historico
                    Guid ID_ObleaHistoricoEstado = this.EntityDataAccess.ActualizarHistoricoEstadoOblea(obleaID, idEstadoFicha, observacion, idUsuario);

                    if (correcciones.Any())
                    {
                        ObleaErrorDetalleDataAccess obleaErrorDetalleDataAccess = new ObleaErrorDetalleDataAccess();

                        foreach (var c in correcciones)
                        {
                            c.IDObleaHistoricoEstado = ID_ObleaHistoricoEstado;

                            obleaErrorDetalleDataAccess.Add(c);
                        };
                    }

                    ss.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ss.Dispose();
                }
            }

        }

        private Obleas ReadObleaByObleaCargaResultadosView(ObleaCargaResultadosView oblea)
        {
            return this.EntityDataAccess.ReadObleaByObleaCargaResultadosView(oblea);
        }

        private void ActualizarDetalleObleaInforme(Guid informeID, Guid? obleaID, String descripcionError)
        {
            this.ActualizarDetalleObleaInforme(informeID, obleaID, descripcionError, default(Guid?));
        }

        private void ActualizarDetalleObleaInforme(Guid informeID, Guid? obleaID, string descripcionError, Guid? estado)
        {
            InformeDetalleLogic informeDetalleLogic = new InformeDetalleLogic();
            informeDetalleLogic.ActualizarDetalleObleaInforme(informeID, obleaID, descripcionError, estado);
        }

        /// <summary>
        /// Devuelve las obleas para despachar
        /// </summary>        
        public List<ObleasDespachoView> ReadAllObleasByZonaIDParaDespacho(string zona)
        {
            return this.EntityDataAccess.ReadAllObleasByZonaIDParaDespacho(zona);
        }

        public List<ObleasExtendedView> ReadFichaParaReInformarFichaEntregada(string numeroObleaAsignada, string dominio)
        {
            return this.EntityDataAccess.ReadFichaParaReInformarFichaEntregada(numeroObleaAsignada, dominio);
        }
        #endregion

        #region Methods Web Api

        /// <summary>
        /// Devuelve tramites pendientes para un taller
        /// </summary>
        /// <param name="idEstadoOblea"></param>
        /// <param name="idTaller"></param>
        /// <returns></returns>
        public List<EstadosTramitesView> ReadTramitesByTallerID(Guid idTaller)
        {
            return this.EntityDataAccess.ReadTramitesByTallerID(idTaller);
        }

        /// <summary>
        /// Devuelve un oblea aplanada en base a filtros
        /// </summary>        
        public ObleasViewWebApi ReadForIngesoWebApi(ObleasParametersWebApi criteria)
        {
            var oblea = this.EntityDataAccess.ReadForIngesoWebApi(criteria);
            if (criteria.ID != null && criteria.ID != Guid.Empty)
            {
                return oblea;
            }
            else if (oblea != null && !String.IsNullOrWhiteSpace(criteria.NumeroOblea))
            {

                Boolean existeObleaPendiente = this.EntityDataAccess.ExisteTramitePendienteParaElNroOblea(criteria.NumeroOblea);
                Boolean existeTramitePendienteParaElDominio = this.EntityDataAccess.ExisteTramitePendienteParaElDominio(oblea.VehiculoDominio);
                oblea.ExisteObleaPendiente = existeObleaPendiente;
                oblea.ExisteTramitePendienteParaElDominio = existeTramitePendienteParaElDominio;

                return oblea;
            }

            return null;

        }

        public ObleasViewWebApi ReadByIDWebApi(Guid idOblea)
        {
            return this.EntityDataAccess.ReadByIDWebApi(idOblea);
        }

        public ViewEntity SaveFromExtranet(ObleasViewWebApi oblea)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                Guid idOblea = Guid.NewGuid();

                try
                {

                    Guid idTallerRT = this.ObtenerTallerRT(oblea.TallerID);

                    var cliente = this.GuardarCliente(oblea);

                    var vehiculo = this.GuardarVehiculo(oblea);

                    this.GuardarFicha(idOblea, vehiculo, cliente, oblea, idTallerRT);

                    this.GrabarRegulador(idOblea, oblea);

                    this.GrabarCilindros(idOblea, oblea, vehiculo.ID, cliente.ID);

                    ss.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ss.Dispose();
                }

                return new ViewEntity(idOblea);
            }
        }

        /// <summary>
        /// Obtiene el RT de un taller
        /// </summary>        
        private Guid ObtenerTallerRT(Guid tallerID)
        {
            List<TalleresRTExtendedView> talleresRT = (new TalleresRTLogic()).ReadByTallerID(tallerID);

            if (talleresRT != null && talleresRT.Any())
            {
                if (talleresRT.Count == 1)
                {
                    return talleresRT.First().ID;
                }
                else
                {
                    return talleresRT.First(x => x.EsRTPrincipal).ID;
                }
            }
            else
            {
                throw new Exception("- El taller seleccionado no posee RT asociado.");
            }
        }

        /// <summary>
        /// Guarda el cliente
        /// </summary>      
        private Clientes GuardarCliente(ObleasViewWebApi obleaEnviada)
        {
            ClientesLogic clientesLogic = new ClientesLogic();

            Clientes cliente = clientesLogic.ReadClientesViewByTipoyNroDoc(obleaEnviada.ClienteTipoDocumentoID, obleaEnviada.ClienteNumeroDocumento).FirstOrDefault();
            if (cliente != null)
            {
                cliente.Descripcion = obleaEnviada.ClienteNombreApellido;
                cliente.CalleCliente = obleaEnviada.ClienteDomicilio;
                cliente.TelefonoCliente = obleaEnviada.ClienteTelefono;
                cliente.CelularCliente = obleaEnviada.ClienteCelular;
                cliente.IdLocalidad = obleaEnviada.ClienteLocalidadID;
                cliente.MailCliente = obleaEnviada.ClienteEmail;
                clientesLogic.Update(cliente);
            }
            else
            {
                cliente = new Clientes();
                cliente.ID = Guid.NewGuid();
                cliente.Descripcion = obleaEnviada.ClienteNombreApellido;
                cliente.CalleCliente = obleaEnviada.ClienteDomicilio;
                cliente.IdTipoDniCliente = obleaEnviada.ClienteTipoDocumentoID;
                cliente.NroDniCliente = obleaEnviada.ClienteNumeroDocumento;
                cliente.TelefonoCliente = obleaEnviada.ClienteTelefono;
                cliente.CelularCliente = obleaEnviada.ClienteCelular;
                cliente.IdLocalidad = obleaEnviada.ClienteLocalidadID;
                cliente.MailCliente = obleaEnviada.ClienteEmail;
                clientesLogic.AddCliente(cliente);
            }
            return cliente;
        }

        /// <summary>
        /// Guardo el vehiculo
        /// </summary>        
        private Vehiculos GuardarVehiculo(ObleasViewWebApi obleaEnviada)
        {
            VehiculosLogic vehiculosLogic = new VehiculosLogic();

            Vehiculos vehiculo = vehiculosLogic.ReadVehiculoByDominio(obleaEnviada.VehiculoDominio);
            if (vehiculo != null)
            {
                vehiculo.MarcaVehiculo = obleaEnviada.VehiculoMarca;
                vehiculo.ModeloVehiculo = obleaEnviada.VehiculoModelo;
                vehiculo.AnioVehiculo = obleaEnviada.VehiculoAnio;
                //vehiculo.EsInyeccionVehiculo = obleaEnviada.VehiculoEsInyeccion;
                //vehiculo.RA = obleaEnviada.VehiculoEsRA;
                //vehiculo.NumeroRA = obleaEnviada.VehiculoNumeroRA;
                vehiculosLogic.Update(vehiculo);
            }
            else
            {
                vehiculo = new Vehiculos();
                vehiculo.ID = Guid.NewGuid();
                vehiculo.MarcaVehiculo = obleaEnviada.VehiculoMarca;
                vehiculo.ModeloVehiculo = obleaEnviada.VehiculoModelo;
                vehiculo.Descripcion = obleaEnviada.VehiculoDominio;
                vehiculo.AnioVehiculo = obleaEnviada.VehiculoAnio;
                vehiculo.EsInyeccionVehiculo = obleaEnviada.VehiculoEsInyeccion;
                vehiculo.RA = obleaEnviada.VehiculoEsRA;
                vehiculo.NumeroRA = obleaEnviada.VehiculoNumeroRA;
                vehiculosLogic.Add(vehiculo);
            }
            return vehiculo;
        }

        /// <summary>
        /// Guardo cabecera ficha
        /// </summary>        
        private void GuardarFicha(Guid idOblea,
                                  Vehiculos vehiculo,
                                  Clientes cliente,
                                  ObleasViewWebApi obleaEnviada,
                                  Guid idTallerRT)
        {
            ObleasLogic obleasLogic = new ObleasLogic();

            Guid idOperacion = obleaEnviada.OperacionID;

            TalleresLogic tallerLogic = new TalleresLogic();
            var taller = tallerLogic.Read(obleaEnviada.TallerID);
            int ultNroOP = taller.UltimoNroIntOperacion + 1;

            Obleas oblea = new Obleas();
            oblea.ID = idOblea;
            oblea.FechaHabilitacion = obleaEnviada.FechaHabilitacion;
            oblea.Descripcion = obleaEnviada.ObleaNumeroAnterior;
            oblea.IdVehiculo = vehiculo.ID;
            oblea.IdUso = obleaEnviada.IdUso;
            oblea.IdOperacion = idOperacion;
            oblea.IdEstadoFicha = ESTADOSFICHAS.PendienteRevision;
            oblea.IdPEC = CrossCutting.DatosDiscretos.PEC.PEAR;
            oblea.IdTaller = taller.ID;
            oblea.NroIntOperacTP = ultNroOP;
            oblea.IdCliente = cliente.ID;
            oblea.IdTitular = cliente.ID;
            oblea.ObservacionesFicha = obleaEnviada.Observacion;
            oblea.IdUsuarioAlta = obleaEnviada.UsuarioID;
            oblea.FechaRealAlta = DateTime.Now;

            oblea.IdRTPEC = PEC_RT.PEC_RT_Principal;
            oblea.IdTallerRT = idTallerRT;
            obleasLogic.Add(oblea);

            //actualizo el puntero del último nro de operación del taller
            taller.UltimoNroIntOperacion = ultNroOP;
            tallerLogic.Update(taller);
        }

        /// <summary>
        /// Grabo el regulador
        /// </summary>        
        private void GrabarRegulador(Guid idOblea, ObleasViewWebApi obleaEnviada)
        {
            var reguladores = obleaEnviada.Reguladores;

            foreach (ObleasReguladoresExtendedView gr in reguladores)
            {
                ObleasReguladores obleaReg = new ObleasReguladores();

                String CodigoREG = gr.CodigoReg.ToUpper().Trim();
                String SerieREG = gr.NroSerieReg.ToUpper().Trim();
                Guid MSDBREG = gr.MSDBRegID;

                ReguladoresLogic reguladoresLogic = new ReguladoresLogic();
                Reguladores reg = reguladoresLogic.ReadByCodigoHomologacion(CodigoREG).FirstOrDefault();
                if (reg == null)
                {
                    //Si el IdRegulador es vacio es porque no existe
                    //creo uno y guado el id en idReg                    
                    reg = new Reguladores();
                    reg.ID = Guid.NewGuid();
                    reg.IdMarcaRegulador = MARCASINEXISTENTES.Reguladores;
                    reg.Descripcion = CodigoREG;
                    reguladoresLogic.Add(reg);
                }


                ReguladoresUnidadLogic reguladoresUnidadLogic = new ReguladoresUnidadLogic();
                ReguladoresUnidad regUni = reguladoresUnidadLogic.ReadReguladorUnidad(reg.ID, SerieREG).FirstOrDefault();
                if (regUni == null)
                {
                    //Si el id de unidad es vacio es porque no existe
                    //creo la unidad y guardo el id para usarlo mas abajo                    
                    regUni = new ReguladoresUnidad();
                    regUni.ID = Guid.NewGuid();
                    regUni.IdRegulador = reg.ID;
                    regUni.Descripcion = SerieREG;
                    reguladoresUnidadLogic.Add(regUni);
                }

                ObleasReguladoresLogic objObleaREG = new ObleasReguladoresLogic();
                ObleasReguladores oR = new ObleasReguladores();
                oR.ID = Guid.NewGuid();
                oR.IdOblea = idOblea;
                oR.IdReguladorUnidad = regUni.ID;
                oR.IdOperacion = MSDBREG;
                objObleaREG.Add(oR);
            }
        }

        /// <summary>
        /// Grabo cilindros y las valvulas
        /// </summary>        
        private void GrabarCilindros(Guid idOblea, ObleasViewWebApi obleaEnviada, Guid vehiculoID, Guid clienteID)
        {
            var cilindros = obleaEnviada.Cilindros;
            var cilindrosPH = obleaEnviada.Cilindros.Where(c => c.RealizaPH);
            var valvulas = obleaEnviada.Valvulas;
            var phID = obleaEnviada.PH != null ? obleaEnviada.PH.ID : default(Guid?);

            GrabarCilindros(idOblea, phID, cilindros, cilindrosPH, valvulas);
        }

        public void GrabarCilindros(Guid idOblea,
                                    Guid? phID,
                                    List<ObleasCilindrosExtendedView> cilindros,
                                    IEnumerable<ObleasCilindrosExtendedView> cilindrosPH,
                                    List<ObleasValvulasExtendedView> valvulas)
        {
            foreach (ObleasCilindrosExtendedView gr in cilindros)
            {
                #region Grabo el Cilindro

                ObleasCilindros obleaReg = new ObleasCilindros();

                Guid idObleaCil = gr.ID;
                String CodigoCIL = gr.CodigoCil;
                String SerieCIL = gr.NroSerieCil;
                int? FabMes = String.IsNullOrWhiteSpace(gr.CilFabMes) ? default(int?) : int.Parse(gr.CilFabMes);
                int? FabAnio = String.IsNullOrWhiteSpace(gr.CilFabAnio) ? default(int?) : int.Parse(gr.CilFabAnio);
                int RevMes = String.IsNullOrWhiteSpace(gr.CilRevMes) ? default(int) : int.Parse(gr.CilRevMes);
                int RevAnio = String.IsNullOrWhiteSpace(gr.CilRevAnio) ? default(int) : int.Parse(gr.CilRevAnio);
                Guid CRPC = gr.CRPCCilID;
                Guid MSDB = gr.MSDBCilID;
                String NroCertifPH = gr.NroCertificadoPH;

                CilindrosLogic cilindrosLogic = new CilindrosLogic();
                Cilindros cil = cilindrosLogic.ReadByCodigoHomologacion(CodigoCIL).FirstOrDefault();
                if (cil == null)
                {
                    //si viene el ID Cilindro = guid.empty es porque no existe ,
                    //lo creo y guardo el valor del ID en idCil                    
                    cil = new Cilindros();
                    cil.ID = Guid.NewGuid();
                    cil.IdMarcaCilindro = MARCASINEXISTENTES.Cilindros;
                    cil.Descripcion = CodigoCIL;
                    cilindrosLogic.Add(cil);
                }

                CilindrosUnidadLogic cilindrosUnidadLogic = new CilindrosUnidadLogic();
                CilindrosUnidad cilUni = cilindrosUnidadLogic.ReadCilindroUnidad(cil.ID, SerieCIL).FirstOrDefault();
                if (cilUni == null)
                {
                    //si viene el ID Cil unidad = guid.empty es porque no existe ,
                    //creo la unidad y guardo el valor del ID en idCilUni                    
                    cilUni = new CilindrosUnidad();
                    cilUni.ID = Guid.NewGuid();
                    cilUni.IdCilindro = cil.ID;
                    cilUni.Descripcion = SerieCIL;
                    cilUni.MesFabCilindro = FabMes;
                    cilUni.AnioFabCilindro = FabAnio;
                    cilindrosUnidadLogic.Add(cilUni);
                }
                else
                {
                    cilUni.MesFabCilindro = FabMes;
                    cilUni.AnioFabCilindro = FabAnio;
                    cilindrosUnidadLogic.Update(cilUni);
                }

                ObleasCilindrosLogic objObleaCIL = new ObleasCilindrosLogic();
                ObleasCilindros oc = new ObleasCilindros();
                oc.ID = idObleaCil;
                oc.IdOblea = idOblea;
                oc.IdCilindroUnidad = cilUni.ID;
                oc.MesUltimaRevisionCil = RevMes;
                oc.AnioUltimaRevisionCil = RevAnio;
                oc.IdCRPC = CRPC;
                oc.IdOperacion = MSDB;
                oc.NroCertificadoPH = NroCertifPH;
                objObleaCIL.Add(oc);
                #endregion                
            }

            foreach (ObleasValvulasExtendedView valvula in valvulas)
            {
                #region Grabo la Valvula

                ObleasValvulas obleaVal = new ObleasValvulas();
                String CodigoVAL = valvula.CodigoVal.ToUpper().Trim();
                String SerieVAL = valvula.NroSerieVal.ToUpper().Trim();
                Guid MSDBVAL = valvula.MSDBValID;

                ValvulasLogic valvulaLogic = new ValvulasLogic();
                Valvula val = valvulaLogic.ReadByCodigoHomologacion(CodigoVAL).FirstOrDefault();
                if (val == null)
                {
                    //Si el id viene vacio es porque no existe la valvula
                    //entonces creo una y guardo el Id
                    val = new Valvula();
                    val.ID = Guid.NewGuid();
                    val.IdMarcaValvula = MARCASINEXISTENTES.Valvulas;
                    val.Descripcion = CodigoVAL;
                    valvulaLogic.Add(val);
                }

                ValvulaUnidadLogic valvulaUnidadLogic = new ValvulaUnidadLogic();
                Valvula_Unidad valUni = valvulaUnidadLogic.ReadValvulaUnidad(val.ID, SerieVAL).FirstOrDefault();
                if (valUni == null)
                {
                    //Si el id de la unidad es vacio entonces creo la unidad
                    // y guardo el id para su uso posterior                    
                    valUni = new Valvula_Unidad();
                    valUni.ID = Guid.NewGuid();
                    valUni.IdValvula = val.ID;
                    valUni.Descripcion = SerieVAL;
                    valvulaUnidadLogic.Add(valUni);
                }

                ObleasValvulasLogic objObleaVAL = new ObleasValvulasLogic();
                ObleasValvulas oV = new ObleasValvulas();
                oV.ID = Guid.NewGuid();
                oV.IdObleaCilindro = valvula.IdObleaCil;
                oV.IdValvulaUnidad = valUni.ID;
                oV.IdOperacion = MSDBVAL;
                objObleaVAL.Add(oV);

                #endregion
            }

            if (cilindrosPH.Any())
            {
                PHLogic phLogic = new PHLogic();
                phLogic.GuardarPHExtranet(idOblea, phID.Value, cilindrosPH.Select(c => c.ID).ToList());
            }
        }

        /// <summary>
        ///Valida que todos los elementos que componen el trámite estén adecuadamente declarados.               
        /// </summary>
        /// <param name="obleaID"></param>
        /// <returns>true si faltan elementos (hay error)</returns>
        public bool ValidarFaltanElementos(Obleas oblea)
        {
            if (oblea.IdOperacion == TIPOOPERACION.Baja)
            {
                // si  es baja todos los componentes deben ser baja o desmontaje
                var regConError = oblea.ObleasReguladores.Any(r => r.IdOperacion == MSDB.Montaje ||
                                                                   r.IdOperacion == MSDB.Sigue);
                if (regConError) return true;

                var cilConError = oblea.ObleasCilindros.Any(r => r.IdOperacion == MSDB.Montaje ||
                                                                 r.IdOperacion == MSDB.Sigue);
                if (cilConError) return true;

                foreach (var obleaCilindro in oblea.ObleasCilindros)
                {
                    var valConError = obleaCilindro.ObleasValvulas.Any(r => r.IdOperacion == MSDB.Montaje ||
                                                                            r.IdOperacion == MSDB.Sigue);
                    if (valConError) return true;
                }
            }
            else
            {
                //si no es baja debe tener al menos un componente de cada tipo en Sigue o Montaje
                var regConError = oblea.ObleasReguladores.Any(r => r.IdOperacion == MSDB.Sigue ||
                                                                    r.IdOperacion == MSDB.Montaje);
                if (!regConError) return true;

                var cilConError = oblea.ObleasCilindros.Any(r => r.IdOperacion == MSDB.Sigue ||
                                                                 r.IdOperacion == MSDB.Montaje);
                if (!cilConError) return true;

                var cantValvMantiene = 0;
                foreach (var obleaCilindro in oblea.ObleasCilindros)
                {
                    var mantieneValvula = obleaCilindro.ObleasValvulas.Any(r => r.IdOperacion == MSDB.Sigue ||
                                                                            r.IdOperacion == MSDB.Montaje);
                    if (mantieneValvula) cantValvMantiene++;
                }

                if (cantValvMantiene == 0) return true;
            }

            return false;
        }

        public bool ValidarFaltaPH(Obleas oblea)
        {
            //Se debe verificar para cada cilindro interviniente en el trámite que tenga el “Nro.de Certificado de
            //PH”, esto se verifica obviamente para cilindros en los cuales, en primera instancia el CRPC
            //declarado es distinto de “FAB” (que significa que es de fabricación) y luego las Fechas de
            //Fabricación(Mes y año) y la fecha de Revisión(mes y año) son distintas, al ser distintas la Fecha de
            //Revisión(última revisión) tiene que ser 5 años inferior. (los meses se toman hábiles hasta el
            //último día del mes)           
            foreach (var cilindro in oblea.ObleasCilindros)
            {
                if (string.IsNullOrWhiteSpace(cilindro.NroCertificadoPH))
                {
                    var anioFabricacion = cilindro.CilindrosUnidad.AnioFabCilindro.Value > 70 ?
                                                cilindro.CilindrosUnidad.AnioFabCilindro.Value + 1900 :
                                                cilindro.CilindrosUnidad.AnioFabCilindro.Value + 2000;

                    var anioRevision = cilindro.AnioUltimaRevisionCil > 70 ?
                                                cilindro.AnioUltimaRevisionCil + 1900 :
                                                cilindro.AnioUltimaRevisionCil + 2000;

                    DateTime fechaFabricacion = new DateTime(anioFabricacion, cilindro.CilindrosUnidad.MesFabCilindro.Value, 1);
                    DateTime fechaRevision = new DateTime(anioRevision, cilindro.MesUltimaRevisionCil, 1);
                    double cantidadDiasUltimaRevision = (fechaRevision - fechaFabricacion).TotalDays;

                    if (cilindro.IdCRPC != CrossCutting.DatosDiscretos.CRPC.FAB &&
                        (fechaFabricacion != fechaRevision) &&
                        (cantidadDiasUltimaRevision > (365 * 5)))
                    {
                        return true;
                    }
                }
            }


            return false;
        }
        #endregion
    }
}