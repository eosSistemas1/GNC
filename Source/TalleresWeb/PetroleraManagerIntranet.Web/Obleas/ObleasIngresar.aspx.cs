using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ObleasIngresar : PageBase
    {
        #region Members
        private ObleasLogic obleaslogic;
        private ClientesLogic clientesLogic;
        private VehiculosLogic vehiculosLogic;
        private ObleasReguladoresLogic obleasReguladoresLogic;
        private ObleasCilindrosLogic obleasCilindrosLogic;
        private ObleasValvulasLogic obleasValvulasLogic;
        private CilindrosUnidadLogic cilindrosUnidadLogic;
        private ValvulaUnidadLogic valvulaUnidadLogic;
        #endregion

        #region Properties

        private Guid ObleaID
        {
            get
            {
                if (ViewState["OBLEAID"] == null) return Guid.Empty;

                return new Guid(ViewState["OBLEAID"].ToString());
            }
            set
            {
                ViewState["OBLEAID"] = value;
            }
        }

        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleaslogic == null) obleaslogic = new ObleasLogic();
                return obleaslogic;
            }
        }
        private ObleasReguladoresLogic ObleasReguladoresLogic
        {
            get
            {
                if (obleasReguladoresLogic == null) obleasReguladoresLogic = new ObleasReguladoresLogic();
                return obleasReguladoresLogic;
            }
        }
        private ObleasCilindrosLogic ObleasCilindrosLogic
        {
            get
            {
                if (obleasCilindrosLogic == null) obleasCilindrosLogic = new ObleasCilindrosLogic();
                return obleasCilindrosLogic;
            }
        }
        private ObleasValvulasLogic ObleasValvulasLogic
        {
            get
            {
                if (obleasValvulasLogic == null) obleasValvulasLogic = new ObleasValvulasLogic();
                return obleasValvulasLogic;
            }
        }
        private VehiculosLogic VehiculosLogic
        {
            get
            {
                if (vehiculosLogic == null) vehiculosLogic = new VehiculosLogic();
                return vehiculosLogic;
            }
        }
        private ClientesLogic ClientesLogic
        {
            get
            {
                if (clientesLogic == null) clientesLogic = new ClientesLogic();
                return clientesLogic;
            }
        }
        private CilindrosUnidadLogic CilindrosUnidadLogic
        {
            get
            {
                if (this.cilindrosUnidadLogic == null) this.cilindrosUnidadLogic = new CilindrosUnidadLogic();
                return this.cilindrosUnidadLogic;
            }
        }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                calFecha.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");

                this.btnVerImagenes.Visible = false;
                this.HabilitarBotonesEstado(true);

                if (Request.QueryString["id"] != null)
                {
                    this.ObleaID = new Guid(Request.QueryString["id"].ToString());

                    var oblea = this.ObleasLogic.ReadDetalladoByID(this.ObleaID);

                    var fechaHabilitacion = oblea.FechaHabilitacion;  

                    calFecha.Value = fechaHabilitacion.HasValue?
                                        fechaHabilitacion.Value.ToString("dd/MM/yyyy")
                                        : string.Empty;

                    this.HabilitarBotonesEstado(false);

                    this.BuscarFichaTecnicaPorID(this.ObleaID);

                    this.btnVerImagenes.Visible = HabilitarBotonImagenes(oblea.IdTaller, oblea.Vehiculos.Descripcion);
                }

                ViewState["UrlReferrer"] = Request.UrlReferrer != null && this.ObleaID != Guid.Empty ? Request.UrlReferrer.OriginalString : "../default.aspx";

                this.BuscarTaller1.SetFocus();

                this.BuscarTaller1.SelectedValue = new ViewEntity(new Guid("37724FC6-F735-488B-B3FA-9137F54C5F0E"), "MOCCIARO GNC MOCCIARO RUBEN ALBERTO");
            }
        }

        private bool HabilitarBotonImagenes(Guid idTaller, string dominio)
        {
            String url = GetDinamyc.UrlImagenesObleas;

            String nombreArchivoDniFrente = String.Format("\\{0}_{1}_{2}.jpg", idTaller, dominio, "DNIFRENTE");
            String urlArchivoDniFrente = url + nombreArchivoDniFrente;

            String nombreArchivoDniDorso = String.Format("\\{0}_{1}_{2}.jpg", idTaller, dominio, "DNIDORSO");
            String urlArchivoDniDorso = url + nombreArchivoDniDorso;

            String nombreArchivoTarjetaFrente = String.Format("\\{0}_{1}_{2}.jpg", idTaller, dominio, "TJFRENTE");
            String urlArchivoTarjetaFrente = url + nombreArchivoTarjetaFrente;

            String nombreArchivoTarjetaDorso = String.Format("\\{0}_{1}_{2}.jpg", idTaller, dominio, "TJDORSO");
            String urlArchivoTarjetaDorso = url + nombreArchivoTarjetaDorso;


            List<string> imagenes = new List<string>();

            if (File.Exists(urlArchivoTarjetaFrente)) return true;
            if (File.Exists(urlArchivoTarjetaDorso)) return true;
            if (File.Exists(urlArchivoDniFrente)) return true;
            if (File.Exists(urlArchivoDniDorso)) return true;

            return false;
        }

        protected void changeTipoOP(object sender, EventArgs e)
        {
            txtNroObleaAnterior.Visible = (cboTipoOperacion.SelectedValue != TIPOOPERACION.Conversion.ToString());
            btnBuscarOblea.Visible = false;

            if (cboTipoOperacion.SelectedValue == TIPOOPERACION.Conversion.ToString())
            {
                uscCargarVehiculo1.SetFocus();
            }
            else
            {
                txtNroObleaAnterior.Focus();
            }

            uscCargarReguladores1.TipoperacionID = new Guid(cboTipoOperacion.SelectedValue);
            uscCargarCilindrosValvulas1.TipoperacionID = new Guid(cboTipoOperacion.SelectedValue);
        }

        /// <summary>
        /// habilito los botones de estado o de guardar        
        /// </summary>
        /// <param name="esObleaNueva"></param>
        private void HabilitarBotonesEstado(Boolean esObleaNueva)
        {
            lnkGuardar.Visible = esObleaNueva;
            lnkGuardarCambios.Visible = !esObleaNueva;
            lnkAprobar.Visible = !esObleaNueva;
            lnkAprobarConError.Visible = !esObleaNueva;
            lnkBloquear.Visible = !esObleaNueva;
        }

        /// <summary>
        /// busco la ficha tecnica por ID
        /// </summary>
        private Boolean BuscarFichaTecnicaPorID(Guid idOblea)
        {
            Boolean valor = true;

            var oblea = this.ObleasLogic.ReadDetalladoByID(idOblea);

            if (oblea != null)
            {
                //si viene por aca es porque entro para cambiar estado
                this.BindearFichaTecnica(oblea);

                this.HabilitarBotonesAcciones(oblea.IdEstadoFicha);
            }
            else
            {
                valor = false;
            }

            return valor;
        }

        /// <summary>
        /// bindea a la vista los datos de la ficha (oblea) que recibe como parametro
        /// </summary>
        private void BindearFichaTecnica(TalleresWeb.Entities.Obleas oblea)
        {
            BuscarTaller1.SelectedValue = new ViewEntity(oblea.IdTaller);

            uscCargarCliente1.ClienteCargado = oblea.Clientes;

            VehiculosExtendedView vehiculosExtendedView = new VehiculosExtendedView();
            vehiculosExtendedView.ID = oblea.Vehiculos.ID;
            vehiculosExtendedView.Descripcion = oblea.Vehiculos.Descripcion;
            vehiculosExtendedView.MarcaVehiculo = oblea.Vehiculos.MarcaVehiculo;
            vehiculosExtendedView.ModeloVehiculo = oblea.Vehiculos.ModeloVehiculo;
            vehiculosExtendedView.AnioVehiculo = oblea.Vehiculos.AnioVehiculo.HasValue ? oblea.Vehiculos.AnioVehiculo.Value : 0;
            vehiculosExtendedView.EsInyeccionVehiculo = oblea.Vehiculos.EsInyeccionVehiculo.Value;
            vehiculosExtendedView.IdUso = oblea.IdUso;
            uscCargarVehiculo1.VehiculoCargado = vehiculosExtendedView;
            

            List<ObleasReguladoresExtendedView> lstObleasRegExView = new List<ObleasReguladoresExtendedView>();
            List<ObleasValvulasExtendedView> lstObleasValExView = new List<ObleasValvulasExtendedView>();
            List<ObleasCilindrosExtendedView> lstObleasCilExView = new List<ObleasCilindrosExtendedView>();

            Boolean esRevision = cboTipoOperacion.SelectedIndex > -1 && (
                                 cboTipoOperacion.SelectedValue == TIPOOPERACION.RevisionAnual.ToString() ||
                                 cboTipoOperacion.SelectedValue == TIPOOPERACION.RevisionCRPC.ToString() ||
                                 cboTipoOperacion.SelectedValue == TIPOOPERACION.Modificacion.ToString());

            foreach (ObleasReguladores or in oblea.ObleasReguladores)
            {
                //Cargo reguladores
                ObleasReguladoresExtendedView orx = new ObleasReguladoresExtendedView();
                orx.ID = or.ID;
                orx.NroSerieReg = or.ReguladoresUnidad.Descripcion.ToUpper().Trim();
                orx.CodigoReg = or.ReguladoresUnidad.Reguladores.Descripcion.ToUpper().Trim();
                orx.MSDBRegID = !esRevision? or.IdOperacion : MSDB.Sigue;
                orx.MSDBReg = !esRevision ? or.Operaciones.Descripcion.ToUpper().Trim() : "SIGUE";
                orx.IDReg = or.ReguladoresUnidad.Reguladores.ID;
                orx.IDRegUni = or.ReguladoresUnidad.ID;
                lstObleasRegExView.Add(orx);
            }
                        
            foreach (ObleasCilindros oc in oblea.ObleasCilindros)
            {      
                //Cargo Cilindros          
                ObleasCilindrosExtendedView ocx = new ObleasCilindrosExtendedView();
                ocx.ID = oc.ID;
                ocx.NroSerieCil = oc.CilindrosUnidad.Descripcion.ToUpper().Trim();
                ocx.CodigoCil = oc.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();

                ocx.CilFabMes = oc.CilindrosUnidad.MesFabCilindro.HasValue ? oc.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : "0";
                ocx.CilFabAnio = oc.CilindrosUnidad.AnioFabCilindro.HasValue ? oc.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : "0";
                ocx.CilRevMes = oc.MesUltimaRevisionCil.ToString("00");
                ocx.CilRevAnio = oc.AnioUltimaRevisionCil.ToString("00");

                ocx.CRPCCilID = oc.IdCRPC.HasValue ? oc.IdCRPC.Value : Guid.Empty;
                ocx.CRPCCil = oc.CRPC != null ? oc.CRPC.Descripcion.ToUpper().Trim() : String.Empty;
                ocx.MSDBCilID = !esRevision ? oc.IdOperacion : MSDB.Sigue;
                ocx.MSDBCil = !esRevision ? oc.Operaciones.Descripcion[0].ToString().ToUpper() : "SIGUE";
                ocx.IDCil = oc.CilindrosUnidad.Cilindros.ID;
                ocx.IDCilUni = oc.CilindrosUnidad.ID;
                ocx.NroCertificadoPH = oc.NroCertificadoPH;
                lstObleasCilExView.Add(ocx);

                //cargo la valvula del cil
                foreach (ObleasValvulas ov in oc.ObleasValvulas)
                {
                    ObleasValvulasExtendedView ovx = new ObleasValvulasExtendedView();
                    ovx.ID = ov.ID;
                    ovx.NroSerieVal = ov.Valvula_Unidad.Descripcion.ToUpper().Trim();
                    ovx.CodigoVal = ov.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim();
                    ovx.MSDBValID = !esRevision ? ov.IdOperacion : MSDB.Sigue;
                    ovx.MSDBVal = !esRevision ? ov.Operaciones.Descripcion[0].ToString().ToUpper() : "SIGUE";
                    ovx.IDVal = ov.Valvula_Unidad.Valvula.ID;
                    ovx.IDValUni = ov.Valvula_Unidad.ID;
                    ovx.IdObleaCil = ocx.ID;
                    lstObleasValExView.Add(ovx);
                }
            }

            uscCargarReguladores1.ReguladoresCargados = lstObleasRegExView;
            uscCargarCilindrosValvulas1.CargarDetalle(lstObleasCilExView, lstObleasValExView);            

            txtObservaciones.Text = oblea.ObservacionesFicha;

            this.cboTipoOperacion.LoadData();
            this.cboTipoOperacion.SelectedValue = oblea.Operaciones.ID.ToString();
            this.txtNroObleaAnterior.Text = oblea.Descripcion;

            txtNroObleaAnterior.Visible = (oblea.Operaciones.ID != TIPOOPERACION.Conversion);
            btnBuscarOblea.Visible = (oblea.Operaciones.ID != TIPOOPERACION.Conversion);
            this.btnBuscarOblea.Visible = false;
        }

        /// <summary>
        /// Habilito los botones de cambio de estado según corresponda por el estado actual
        /// </summary>        
        private void HabilitarBotonesAcciones(Guid idEstadoFichaActual)
        {
            this.lnkBloquear.Visible = false;
            this.lnkAprobarConError.Visible = false;
            this.lnkAprobar.Visible = false;

            if (idEstadoFichaActual == ESTADOSFICHAS.Aprobada)
            {
                this.lnkBloquear.Visible = true;
                this.lnkAprobarConError.Visible = true;
            }
            if (idEstadoFichaActual == ESTADOSFICHAS.AprobadaConError)
            {
                this.lnkBloquear.Visible = true;
                this.lnkAprobar.Visible = true;
            }
            if (idEstadoFichaActual == ESTADOSFICHAS.Bloqueada)
            {
                this.lnkAprobarConError.Visible = true;
                this.lnkAprobar.Visible = true;
            }
            if (idEstadoFichaActual == ESTADOSFICHAS.Eliminada
                || idEstadoFichaActual == ESTADOSFICHAS.PendienteRevision
                || idEstadoFichaActual == ESTADOSFICHAS.RechazadaPorEnte)
            {
                this.lnkBloquear.Visible = true;
                this.lnkAprobarConError.Visible = true;
                this.lnkAprobar.Visible = true;
            }
        }

        /// <summary>
        /// Guardo una oblea nueva
        /// </summary>
        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            String msjValida = this.ValidarOblea(false);            

            if (msjValida == String.Empty)
            {
                List<TalleresRTExtendedView> talleresRT = (new TalleresRTLogic()).ReadByTallerID(this.BuscarTaller1.SelectedValue.ID);

                if (talleresRT.Count == 1)
                {
                    this.GrabarOblea(talleresRT.First().ID);
                }
                else if (talleresRT.Count > 1)
                {
                    cboRTTaller.DataSource = talleresRT;
                    cboRTTaller.DataTextField = "Descripcion";
                    cboRTTaller.DataValueField = "ID";
                    cboRTTaller.DataBind();

                    if (talleresRT.Any(x => x.EsRTPrincipal))
                    {
                        cboRTTaller.SelectedValue = talleresRT.First(x => x.EsRTPrincipal).ID.ToString();
                    }
                    else
                    {
                        cboRTTaller.SelectedIndex = 0;
                    }
                    mpeRT.Show();
                }
                else
                {
                    MessageBoxCtrl1.MessageBox(null, "- El taller seleccionado no posee RT asociado.", MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, msjValida, MessageBoxCtrl.TipoWarning.Warning);
            }
        }
        protected void lnkGuardarCambios_Click(object sender, EventArgs e)
        {
            Guid idOblea = this.ObleaID;
            this.ModificarOblea(idOblea);  
            MessageBoxCtrl1.MessageBox(null, "Los cambios se han efectuado", "ConsultarFichasTecnicas.aspx", MessageBoxCtrl.TipoWarning.Warning);
       
        }
        

        /// <summary>
        /// Validar oblea
        /// </summary>            
        private String ValidarOblea(Boolean esModificacion)
        {
            String msjValida = String.Empty;

            if (cboTipoOperacion.SelectedIndex == -1) msjValida += "- Seleccione tipo de operación <br>";

            if (!uscCargarCliente1.EsClienteValido) msjValida += "- El cliente es obligatorio. Debe completar todos los datos del mismo <br>";

            if (!uscCargarVehiculo1.EsVehiculoValido)
            {
                msjValida += "- El vehículo es obligatorio. Debe completar todos los datos del mismo <br>";
            }
            else
            {
                if (String.IsNullOrEmpty(uscCargarVehiculo1.VehiculoCargado.Descripcion) ||
                        !VehiculosLogic.ValidarDominio(uscCargarVehiculo1.VehiculoCargado))
                    msjValida += "El dominio no tiene el formato correcto o no fue ingresado <br>";

                if (!esModificacion && this.VehiculosLogic.TieneTramitesPendientes(uscCargarVehiculo1.VehiculoCargado.Descripcion))
                    msjValida += "- El vehículo ingresado posee trámites pendientes <br>";
            }
            if (uscCargarReguladores1.ReguladoresCargados.Count == 0) msjValida += "- Debe cargar un regulador <br>";
            if (uscCargarReguladores1.ReguladoresCargados.Where(t => t.MSDBRegID == MSDB.Sigue).Count() > 1) msjValida += "- No puede ingresar mas de un regulador en SIGUE. </br>";

            if (!uscCargarCilindrosValvulas1.CilindrosCargados.Any())
            {
                msjValida += "- Debe cargar un cilindro <br>";
            }
          
            if (uscCargarCilindrosValvulas1.ValvulasCargadas.Count == 0) msjValida += "- Debe cargar una válvula <br>";

            if (this.BuscarTaller1.SelectedValue.ID == Guid.Empty) msjValida += "- Debe seleccionar un taller <br>";

            if (cboTipoOperacion.SelectedIndex != -1 
                    && cboTipoOperacion.SelectedValue != TIPOOPERACION.Conversion.ToString() 
                    && String.IsNullOrWhiteSpace(txtNroObleaAnterior.Text))
                                msjValida += "- Debe ingresar numero de oblea anterior <br>";

            if (!string.IsNullOrWhiteSpace(txtNroObleaAnterior.Text))
            {
                string estadoObleaExistente = ObleasLogic.ExisteObleaConNroObleaAnterior(txtNroObleaAnterior.Text, this.ObleaID);
                if (!string.IsNullOrWhiteSpace(estadoObleaExistente))
                    msjValida += $"- Ya existe un trámite con la oblea anterior ingresada en estado <strong>{estadoObleaExistente}</strong> <br>";
            }
            return msjValida;
        }

        private void GrabarOblea(Guid idTallerRT)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    Guid idOblea = Guid.NewGuid();

                    var cliente = this.GuardarCliente();

                    var vehiculo = this.GuardarVehiculo(cliente.ID);

                    this.GuardarFicha(idOblea, vehiculo, cliente, idTallerRT);

                    this.GrabarRegulador(idOblea);

                    this.GrabarCilindros(idOblea);

                    ss.Complete();

                    this.ImprimirOblea(idOblea);
                }
                catch (Exception ex)
                {
                    MessageBoxCtrl1.MessageBox(null, ex.Message + "-<br/>-" + ex.InnerException.Message, MessageBoxCtrl.TipoWarning.Error);
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        /// <summary>
        /// Guarda el cliente
        /// </summary>      
        private Clientes GuardarCliente()
        {
            var cliente = uscCargarCliente1.ClienteCargado;
            this.ClientesLogic.AddCliente(cliente);
            return cliente;
        }

        /// <summary>
        /// Guarda el cliente
        /// </summary>        
        private VehiculosExtendedView GuardarVehiculo(Guid clienteID)
        {
            var vehiculo = uscCargarVehiculo1.VehiculoCargado;
            vehiculo.IdDuenioVehiculo = clienteID;
            this.VehiculosLogic.Add(vehiculo);
            return vehiculo;
        }

        /// <summary>
        /// Guardo la ficha 
        /// </summary>        
        private void GuardarFicha(Guid idOblea, VehiculosExtendedView vehiculo, Clientes cliente, Guid idTallerRT)
        {
            Guid idOperacion = new Guid(cboTipoOperacion.SelectedValue);

            TalleresWeb.Entities.Obleas oblea = new TalleresWeb.Entities.Obleas();
            oblea.ID = idOblea;
            oblea.FechaHabilitacion = new DateTime(int.Parse(calFecha.Value.Substring(6, 4)), int.Parse(calFecha.Value.Substring(3, 2)), int.Parse(calFecha.Value.Substring(0, 2)));
            oblea.Descripcion = txtNroObleaAnterior.Text;
            oblea.IdVehiculo = vehiculo.ID;
            oblea.IdUso = vehiculo.IdUso;
            oblea.IdOperacion = idOperacion;
            oblea.IdEstadoFicha = ESTADOSFICHAS.PendienteRevision;
            oblea.IdPEC = CrossCutting.DatosDiscretos.PEC.PEAR;
            oblea.IdTaller = this.BuscarTaller1.SelectedValue.ID;
            oblea.NroIntOperacTP = TalleresLogic.GetProximoNumeroInternoOperacion(this.BuscarTaller1.SelectedValue.ID);
            oblea.IdCliente = cliente.ID;
            oblea.IdTitular = cliente.ID;
            oblea.ObservacionesFicha = txtObservaciones.Text.Trim();
            oblea.IdUsuarioAlta = this.UsuarioID;
            oblea.FechaRealAlta = DateTime.Now;

            oblea.IdRTPEC = this.RTPEC;
            oblea.IdTallerRT = idTallerRT;
            this.ObleasLogic.Add(oblea);

            //actualizo el puntero del último nro de operación del taller
            TalleresLogic.SetUltimoNumeroOperacionTaller(oblea.IdTaller, oblea.NroIntOperacTP.Value);                        
        }

        /// <summary>
        /// Grabo el regulador
        /// </summary>        
        private void GrabarRegulador(Guid idOblea)
        {
            var reguladores = uscCargarReguladores1.ReguladoresCargados;

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
        private void GrabarCilindros(Guid idOblea)
        {
            var cilindros = uscCargarCilindrosValvulas1.CilindrosCargados;
            var cilindrosPH = uscCargarCilindrosValvulas1.CilindrosCargados.Where(c => c.RealizaPH);
            var valvulas = uscCargarCilindrosValvulas1.ValvulasCargadas;

            Guid? idPH = cilindrosPH.Any(oc => oc.RealizaPH) ? Guid.NewGuid() : default(Guid?);

            ObleasLogic.GrabarCilindros(idOblea,
                                        idPH,
                                        cilindros,
                                        cilindrosPH,
                                        valvulas);           
        }

        /// <summary>
        /// Imprimo la oblea creada
        /// </summary>        
        private void ImprimirOblea(Guid idOblea)
        {            
            String urlObleasImprimir = SiteMaster.UrlBase + @"Obleas/ObleasImprimir.aspx?id=" + idOblea;
            PrintBoxCtrl1.PrintBox("Imprimir", urlObleasImprimir, null, "ObleasIngresar.aspx");

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupImprimir('" + idOblea + "');", true);
        }
        #endregion

        #region Oblea con nro de tramite anterior (Revision) 
        /// <summary>
        /// busca por oblea anterior
        /// </summary>        
        protected void btnBuscarOblea_Click(object sender, EventArgs e)
        {
            //si viene por aca es porque es revision y busca por nro anterior
            var obleaID = this.ObleaID != null && this.ObleaID != Guid.Empty ? this.ObleaID : default(Guid?);

            string estadoObleaAnterior = ObleasLogic.ExisteObleaConNroObleaAnterior(txtNroObleaAnterior.Text, obleaID);

            if (string.IsNullOrEmpty(estadoObleaAnterior))
            {
                if (!BuscarFichaTecnica())
                {
                    uscCargarVehiculo1.SetFocus();
                }
                else
                {
                    btnAceptarProcesar.Focus();
                }
            }
            else
            {
                String mensaje = $"Ya existe una oblea con el número de oblea anterior <strong>{txtNroObleaAnterior.Text}</strong> y estado <strong>{estadoObleaAnterior}</strong> .";
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        /// <summary>
        /// Busca la ficha técnica por el nro de oblea anterior
        /// </summary>
        private Boolean BuscarFichaTecnica()
        {
            double v;
            if (String.IsNullOrEmpty(txtNroObleaAnterior.Text) || !Double.TryParse(txtNroObleaAnterior.Text.Trim(), out v))
            {
                txtNroObleaAnterior.Text = String.Empty;
                txtNroObleaAnterior.Focus();
                return false;
            }

            var oblea = this.ObleasLogic.ReadDetalladoByObleaAnterior(txtNroObleaAnterior.Text);

            if (oblea != null)
            {
                var o = this.PrepararObleaParaBindear(oblea);

                this.BindearFichaTecnica(o);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Prepara la oblea para que sea un nuevo trámite
        /// </summary>        
        private TalleresWeb.Entities.Obleas PrepararObleaParaBindear(TalleresWeb.Entities.Obleas oblea)
        {
            var o = new TalleresWeb.Entities.Obleas();
            o.Vehiculos = oblea.Vehiculos;
            o.FechaHabilitacion = DateTime.Parse(calFecha.Value);
            o.Descripcion = txtNroObleaAnterior.Text;
            o.IdUso = oblea.IdUso;
            o.Operaciones = new Operaciones() { ID = new Guid(cboTipoOperacion.SelectedValue), Descripcion = cboTipoOperacion.SelectedItem.Text };
            o.IdEstadoFicha = ESTADOSFICHAS.PendienteRevision;
            o.IdPEC = CrossCutting.DatosDiscretos.PEC.PEAR;
            o.Talleres = new Talleres() { ID = this.BuscarTaller1.SelectedValue.ID, Descripcion = this.BuscarTaller1.SelectedValue.Descripcion };
            o.Clientes = oblea.Clientes;
            o.IdTitular = o.IdCliente;
            o.IdUsuarioAlta = this.UsuarioID;
            o.FechaRealAlta = DateTime.Now;

            foreach (var regulador in oblea.ObleasReguladores)
            {
                if (regulador.IdOperacion == MSDB.Sigue || regulador.IdOperacion == MSDB.Montaje)
                {
                    ObleasReguladores r = new ObleasReguladores();
                    r.IdOblea = o.ID;
                    r.IdOperacion = regulador.IdOperacion;
                    r.IdReguladorUnidad = regulador.IdReguladorUnidad;
                    o.ObleasReguladores.Add(r);
                }
            }

            foreach (var cilindro in oblea.ObleasCilindros)
            {
                if (cilindro.IdOperacion == MSDB.Sigue || cilindro.IdOperacion == MSDB.Montaje)
                {
                    ObleasCilindros c = new ObleasCilindros();
                    c.IdOblea = o.ID;
                    c.IdCilindroUnidad = cilindro.IdCilindroUnidad;
                    c.MesUltimaRevisionCil = cilindro.MesUltimaRevisionCil;
                    c.AnioUltimaRevisionCil = cilindro.AnioUltimaRevisionCil;
                    c.NroCertificadoPH = cilindro.NroCertificadoPH;
                    c.IdCRPC = cilindro.IdCRPC;
                    c.IdOperacion = cilindro.IdOperacion;

                    foreach (var valvula in cilindro.ObleasValvulas)
                    {
                        if (valvula.IdOperacion == MSDB.Sigue || valvula.IdOperacion == MSDB.Montaje)
                        {
                            ObleasValvulas v = new ObleasValvulas();
                            v.IdObleaCilindro = c.ID;
                            v.IdValvulaUnidad = valvula.IdValvulaUnidad;
                            v.IdOperacion = valvula.IdOperacion;
                            c.ObleasValvulas.Add(v);
                        }
                    }

                    o.ObleasCilindros.Add(c);
                }
            }


            return o;
        }
        #endregion

        #region Eventos navegacion
        protected void uscCargarCliente1_ClienteChanged()
        {
            this.uscCargarReguladores1.SetFocus();
        }

        protected void uscCargarVehiculo1_VehiculoChanged()
        {
            this.uscCargarCliente1.SetFocus();
        }

        /// <summary>
        /// si tiene estado , vuelve a la pantalla anterior con su estado
        /// </summary>        
        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            String url = ViewState["UrlReferrer"].ToString();

            if (Request.QueryString["e"] != null)
            {
                if (url.Contains("?"))
                {
                    url = url.Split('?')[0].ToString();
                }
                url += "?e=" + Request.QueryString["e"].ToString();
            }

            Response.Redirect(url, false);
        }
        #endregion

        #region Eventos Correccion Errores
        /// <summary>
        /// Evento botón aceptar correcccion de error
        /// </summary>            
        protected void btnAceptarProcesar_Click(object sender, EventArgs e)
        {
            if (ValidarAceptarProcesar())
            {
                this.AceptarProcesar();
            }
            else
            {
                lblErrorMsj.Text = "Debe ingresar al menos una corrección.";
                this.MPE.Show();
            }
        }

        /// <summary>
        /// Aceptar corrección de error y actualiza estado de oblea
        /// </summary>
        private void AceptarProcesar()
        {
            try
            {
                List<ObleaErrorDetalle> correccionErrores = this.ObtenerCorreccionErrores();

                this.ModificarOblea(this.ObleaID);

                this.ObleasLogic.CambiarEstado(this.ObleaID,
                                               new Guid(hddEstadoficha.Value),
                                               String.Empty,
                                               correccionErrores,
                                               this.UsuarioID);

                String urlRetorno = ViewState["UrlReferrer"].ToString();
                MessageBoxCtrl1.MessageBox(null, String.Format("La Ficha Técnica cambió su estado a: {0}", dscEstadoficha.Value.ToUpper()), urlRetorno, MessageBoxCtrl.TipoWarning.Success);
            }
            catch (Exception e)
            {
                MessageBoxCtrl1.MessageBox(null, e.Message, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        /// <summary>
        /// Validar corrección de error
        /// </summary>        
        private Boolean ValidarAceptarProcesar()
        {
            Boolean valor = false;

            if (!String.IsNullOrWhiteSpace(txtErrorDominio.Text)) return true;

            if (!String.IsNullOrWhiteSpace(txtErrorCodHomoREG.Text)) return true;

            if (!String.IsNullOrWhiteSpace(txtErrorSerieREG.Text)) return true;

            var cilindros = this.ObtenerCilindrosError();
            if (cilindros.Any(c => !String.IsNullOrWhiteSpace(c.NroSerieNuevo) ||
                                    !String.IsNullOrWhiteSpace(c.CodHomologacionNuevo))) return true;

            var valvulas = this.ObtenerValvulasError();
            if (valvulas.Any(c => !String.IsNullOrWhiteSpace(c.NroSerieNuevo) ||
                                    !String.IsNullOrWhiteSpace(c.CodHomologacionNuevo))) return true;

            return valor;
        }

        private void CargarDatosError()
        {
            lblErrorDominioActual.Text = uscCargarVehiculo1.VehiculoCargado.Descripcion;

            lblErrorCodHomoREGActual.Text = uscCargarReguladores1.ReguladoresCargados.First().CodigoReg;
            lblErrorSerieREGActual.Text = uscCargarReguladores1.ReguladoresCargados.First().NroSerieReg;

            List<CilindroErrorView> cilindros = new List<CilindroErrorView>();
            foreach (var c in uscCargarCilindrosValvulas1.CilindrosCargados)
            {
                CilindroErrorView cilindro = new CilindroErrorView();
                cilindro.IDCilindroOblea = c.ID;
                cilindro.NroSerieActual = c.NroSerieCil;
                cilindro.CodHomologacionActual = c.CodigoCil;
                cilindros.Add(cilindro);
            }
            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            List<ValvulaErrorView> valvulas = new List<ValvulaErrorView>();
            foreach (var v in uscCargarCilindrosValvulas1.ValvulasCargadas)
            {
                ValvulaErrorView valvula = new ValvulaErrorView();
                valvula.IDValvulaOblea = v.ID;
                valvula.NroSerieActual = v.NroSerieVal;
                valvula.CodHomologacionActual = v.CodigoVal;
                valvulas.Add(valvula);
            }
            grdValvulas.DataSource = valvulas;
            grdValvulas.DataBind();

        }

        private List<CilindroErrorView> ObtenerCilindrosError()
        {
            List<CilindroErrorView> cilindros = new List<CilindroErrorView>();
            foreach (GridViewRow v in grdCilindros.Rows)
            {
                CilindroErrorView cilindro = new CilindroErrorView();
                cilindro.IDCilindroOblea = new Guid(grdCilindros.DataKeys[v.RowIndex].Values["IDCilindroOblea"].ToString());
                cilindro.CodHomologacionActual = v.Cells[0].Text;
                cilindro.CodHomologacionNuevo = ((TextBox)v.FindControl("txtCodHomoCIL")).Text.ToUpper();
                cilindro.NroSerieActual = v.Cells[2].Text;
                cilindro.NroSerieNuevo = ((TextBox)v.FindControl("txtSerieCIL")).Text.ToUpper();

                cilindros.Add(cilindro);
            }

            return cilindros;
        }

        private List<ValvulaErrorView> ObtenerValvulasError()
        {
            List<ValvulaErrorView> valvulas = new List<ValvulaErrorView>();
            foreach (GridViewRow v in grdValvulas.Rows)
            {
                ValvulaErrorView valvula = new ValvulaErrorView();
                valvula.IDValvulaOblea = new Guid(grdValvulas.DataKeys[v.RowIndex].Values["IDValvulaOblea"].ToString());
                valvula.CodHomologacionActual = v.Cells[0].Text;
                valvula.CodHomologacionNuevo = ((TextBox)v.FindControl("txtCodHomoVAL")).Text.ToUpper();
                valvula.NroSerieActual = v.Cells[2].Text;
                valvula.NroSerieNuevo = ((TextBox)v.FindControl("txtSerieVAL")).Text.ToUpper();

                valvulas.Add(valvula);
            }

            return valvulas;
        }

        private List<ObleaErrorDetalle> ObtenerCorreccionErrores()
        {
            List<ObleaErrorDetalle> correcciones = new List<ObleaErrorDetalle>();

            if (!String.IsNullOrWhiteSpace(txtErrorDominio.Text))
            {
                ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                correccion.ID = Guid.NewGuid();
                correccion.IDObleaErrorTipo = ERRORTIPO.DOMINIO;
                correccion.Correccion = txtErrorDominio.Text;
                correccion.Solucionado = true;
                correcciones.Add(correccion);
            }

            if (!String.IsNullOrWhiteSpace(txtErrorCodHomoREG.Text))
            {
                ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                correccion.ID = Guid.NewGuid();
                correccion.IDObleaErrorTipo = ERRORTIPO.REGULADORHomologacion;
                correccion.Correccion = txtErrorCodHomoREG.Text;
                correccion.Solucionado = true;
                correcciones.Add(correccion);
            }

            if (!String.IsNullOrWhiteSpace(txtErrorSerieREG.Text))
            {
                ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                correccion.ID = Guid.NewGuid();
                correccion.IDObleaErrorTipo = ERRORTIPO.REGULADORSerie;
                correccion.Correccion = txtErrorSerieREG.Text;
                correccion.Solucionado = true;
                correcciones.Add(correccion);
            }

            var cilindros = this.ObtenerCilindrosError();
            if (cilindros.Any(c => !String.IsNullOrWhiteSpace(c.NroSerieNuevo) ||
                                    !String.IsNullOrWhiteSpace(c.CodHomologacionNuevo)))
            {
                foreach (var cilindro in cilindros)
                {
                    if (!String.IsNullOrWhiteSpace(cilindro.CodHomologacionNuevo))
                    {
                        ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                        correccion.ID = Guid.NewGuid();
                        correccion.IDObjetoCorregido = cilindro.IDCilindroOblea;
                        correccion.IDObleaErrorTipo = ERRORTIPO.CILINDROHomologacion;
                        correccion.Correccion = cilindro.CodHomologacionNuevo;
                        correccion.Solucionado = true;
                        correcciones.Add(correccion);
                    }

                    if (!String.IsNullOrWhiteSpace(cilindro.NroSerieNuevo))
                    {
                        ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                        correccion.ID = Guid.NewGuid();
                        correccion.IDObjetoCorregido = cilindro.IDCilindroOblea;
                        correccion.IDObleaErrorTipo = ERRORTIPO.CILINDROSerie;
                        correccion.Correccion = cilindro.NroSerieNuevo;
                        correccion.Solucionado = true;
                        correcciones.Add(correccion);
                    }
                }
            }

            var valvulas = this.ObtenerValvulasError();
            if (valvulas.Any(c => !String.IsNullOrWhiteSpace(c.NroSerieNuevo) ||
                                    !String.IsNullOrWhiteSpace(c.CodHomologacionNuevo)))
            {
                foreach (var valvula in valvulas)
                {
                    if (!String.IsNullOrWhiteSpace(valvula.CodHomologacionNuevo))
                    {
                        ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                        correccion.ID = Guid.NewGuid();
                        correccion.IDObjetoCorregido = valvula.IDValvulaOblea;
                        correccion.IDObleaErrorTipo = ERRORTIPO.VALVULAHomologacion;
                        correccion.Correccion = valvula.CodHomologacionNuevo;
                        correccion.Solucionado = true;
                        correcciones.Add(correccion);
                    }

                    if (!String.IsNullOrWhiteSpace(valvula.NroSerieNuevo))
                    {
                        ObleaErrorDetalle correccion = new ObleaErrorDetalle();
                        correccion.ID = Guid.NewGuid();
                        correccion.IDObjetoCorregido = valvula.IDValvulaOblea;
                        correccion.IDObleaErrorTipo = ERRORTIPO.VALVULASerie;
                        correccion.Correccion = valvula.NroSerieNuevo;
                        correccion.Solucionado = true;
                        correcciones.Add(correccion);
                    }
                }
            }

            return correcciones;
        }
        #endregion

        #region Botones cambio estado fichas
        /// <summary>
        /// Aprobar Oblea
        /// </summary>
        protected void lnkAprobar_Click(object sender, EventArgs e)
        {            
            this.CambiarEstadoFicha(ESTADOSFICHAS.Aprobada, "Aprobada", false);
        }

        /// <summary>
        /// Aprobar con error oblea
        /// </summary>
        protected void lnkAprobarConError_Click(object sender, EventArgs e)
        {
            this.CargarDatosError();
            this.CambiarEstadoFicha(ESTADOSFICHAS.AprobadaConError, "Aprobada con error", true);
        }

        /// <summary>
        /// Bloqueo una oblea existente
        /// </summary>
        protected void lnkBloquear_Click(object sender, EventArgs e)
        {
            this.CargarDatosError();
            this.CambiarEstadoFicha(ESTADOSFICHAS.Bloqueada, "Bloqueada", true);
        }

        /// <summary>
        /// Cambia el estado de la ficha
        /// </summary>
        private void CambiarEstadoFicha(Guid estadoficha, String descripcionEstado, Boolean requiereObservaciones)
        {
            hddEstadoficha.Value = estadoficha.ToString();
            dscEstadoficha.Value = descripcionEstado;

            if (requiereObservaciones)
            {
                MPE.Show();
            }
            else
            {
                MPE.Hide();
                this.AceptarProcesar();
            }
        }
        #endregion

        #region Modificar Fichas
        private void ModificarOblea(Guid idOblea)
        {
            String msjValida = this.ValidarOblea(true);

            if (msjValida == String.Empty)
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        var cliente = this.GuardarCliente();

                        var vehiculo = this.GuardarVehiculo(cliente.ID);

                        this.ModificarOblea(idOblea, vehiculo, cliente);

                        this.ModificarRegulador(idOblea);

                        this.ModificarCilindrosYValvulas(idOblea);

                        ss.Complete();

                        //se comento para no dar imprimir la oblea al modificar o cambiar el estado 
                        //String urlRetorno = ViewState["UrlReferrer"].ToString();
                        //this.ImprimirOblea(idOblea, urlRetorno);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCtrl1.MessageBox(null, ex.Message + "<br />" + ex.InnerException.Message, MessageBoxCtrl.TipoWarning.Error);
                    }
                    finally
                    {
                        ss.Dispose();
                    }
                }
            }
            else
            {
                throw new Exception(msjValida);
            }
        }

        private void ModificarOblea(Guid idOblea, VehiculosExtendedView vehiculo, Clientes cliente)
        {
            var oblea = this.ObleasLogic.ReadDetalladoByID(idOblea);
            Guid idOperacion = new Guid(cboTipoOperacion.SelectedValue);

            oblea.ID = idOblea;
            oblea.FechaHabilitacion = new DateTime(int.Parse(calFecha.Value.Substring(6, 4)), int.Parse(calFecha.Value.Substring(3, 2)), int.Parse(calFecha.Value.Substring(0, 2)));


            if (Request.QueryString["id"] != null)
            {
                DateTime temp;
                if (DateTime.TryParse(calFecha.Value, out temp))
                    oblea.FechaHabilitacion = temp;
            }
            oblea.Descripcion = txtNroObleaAnterior.Text;

            oblea.IdVehiculo = vehiculo.ID;
            oblea.IdUso = vehiculo.IdUso;
            oblea.IdOperacion = idOperacion;
            oblea.IdPEC = CrossCutting.DatosDiscretos.PEC.PEAR;

            var actualizarNumeroOperacionTaller = false;
            if (oblea.IdTaller != this.BuscarTaller1.SelectedValue.ID)
            {
                List<TalleresRTExtendedView> talleresRT = (new TalleresRTLogic()).ReadByTallerID(this.BuscarTaller1.SelectedValue.ID);

                if (talleresRT.FirstOrDefault(x => x.EsRTPrincipal) != null)
                {
                    oblea.IdTallerRT = talleresRT.FirstOrDefault(x => x.EsRTPrincipal).ID;
                }
                else if (talleresRT.FirstOrDefault() != null)
                {
                    oblea.IdTallerRT = talleresRT.FirstOrDefault().ID;
                }
                else
                {
                    oblea.IdTallerRT = default(Guid?);
                }

                oblea.NroIntOperacTP = TalleresLogic.GetProximoNumeroInternoOperacion(this.BuscarTaller1.SelectedValue.ID);
                actualizarNumeroOperacionTaller = true;
            }

            oblea.IdTaller = this.BuscarTaller1.SelectedValue.ID;
            oblea.IdCliente = cliente.ID;
            oblea.IdTitular = cliente.ID;
            oblea.ObservacionesFicha = txtObservaciones.Text.Trim();

            if (!oblea.IdRTPEC.HasValue) oblea.IdRTPEC = PEC_RT.PEC_RT_Principal;

            this.ObleasLogic.Update(oblea);

            //si modifico el taller actualizo el puntero del último nro de operación
            if (actualizarNumeroOperacionTaller)
            {
                TalleresLogic.SetUltimoNumeroOperacionTaller(oblea.IdTaller, oblea.NroIntOperacTP.Value);
            }
        }

        private void ModificarRegulador(Guid idOblea)
        {
            var reguladores = uscCargarReguladores1.ReguladoresCargados;

            foreach (ObleasReguladoresExtendedView regulador in reguladores)
            {
                String CodigoREG = regulador.CodigoReg.ToUpper().Trim();
                String SerieREG = regulador.NroSerieReg.ToUpper().Trim();
                Guid MSDBREG = regulador.MSDBRegID;

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

                ObleasReguladores oR = this.ObleasReguladoresLogic.Read(regulador.ID);                                
                if (oR != null)
                {
                    oR.ID = regulador.ID;
                    oR.IdOblea = idOblea;
                    oR.IdReguladorUnidad = regUni.ID;
                    oR.IdOperacion = MSDBREG;
                    this.ObleasReguladoresLogic.Update(oR);
                }
                else
                {
                    oR = new ObleasReguladores();
                    oR.ID = regulador.ID;
                    oR.IdOblea = idOblea;
                    oR.IdReguladorUnidad = regUni.ID;
                    oR.IdOperacion = MSDBREG;
                    this.ObleasReguladoresLogic.Add(oR);
                }
            }
        }

        private void ModificarCilindrosYValvulas(Guid idOblea)
        {
            var cilindros = uscCargarCilindrosValvulas1.CilindrosCargados;
            var valvulas = uscCargarCilindrosValvulas1.ValvulasCargadas;

            ///cilindros
            foreach (ObleasCilindrosExtendedView cilindro in cilindros)
            {
                #region Grabo el Cilindro
                String CodigoCIL = cilindro.CodigoCil;
                String SerieCIL = cilindro.NroSerieCil;
                int FabMes = int.Parse(cilindro.CilFabMes);
                int FabAnio = int.Parse(cilindro.CilFabAnio);
                int RevMes = int.Parse(cilindro.CilRevMes);
                int RevAnio = int.Parse(cilindro.CilRevAnio);
                Guid CRPC = cilindro.CRPCCilID;
                Guid MSDB = cilindro.MSDBCilID;
                String NroCertifPH = cilindro.NroCertificadoPH;

                CilindrosLogic cilindrosLogic = new CilindrosLogic();
                Cilindros cil = cilindrosLogic.ReadByCodigoHomologacion(CodigoCIL).FirstOrDefault();
                if (cil == null)
                {
                    //si viene el ID Cilindro = guid.empty es porque no existe ,
                    //lo creo y guardo el valor del ID en idCil                    
                    cil = new Cilindros();
                    cil.ID = Guid.NewGuid();
                    cil.IdMarcaCilindro = MARCASINEXISTENTES.Reguladores;
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

                ObleasCilindros oC = this.ObleasCilindrosLogic.Read(cilindro.ID);
                if (oC == null)
                {
                    oC = new ObleasCilindros();
                    oC.ID = cilindro.ID;
                    oC.IdOblea = idOblea;
                    oC.IdCilindroUnidad = cilUni.ID;
                    oC.MesUltimaRevisionCil = RevMes;
                    oC.AnioUltimaRevisionCil = RevAnio;
                    oC.IdCRPC = CRPC;
                    oC.IdOperacion = MSDB;
                    oC.NroCertificadoPH = NroCertifPH;
                    this.ObleasCilindrosLogic.Add(oC);
                }
                else
                {
                    oC.IdOblea = idOblea;
                    oC.IdCilindroUnidad = cilUni.ID;
                    oC.MesUltimaRevisionCil = RevMes;
                    oC.AnioUltimaRevisionCil = RevAnio;
                    oC.IdCRPC = CRPC;
                    oC.IdOperacion = MSDB;
                    oC.NroCertificadoPH = NroCertifPH;
                    this.ObleasCilindrosLogic.Update(oC);
                }

                #endregion
            }

            //Valvulas
            foreach (ObleasValvulasExtendedView valvula in valvulas)
            {
                #region Grabo la Valvula

                ObleasValvulas obleaVal = new ObleasValvulas();
                String CodigoVAL = valvula.CodigoVal.ToUpper().Trim();
                String SerieVAL = valvula.NroSerieVal.ToUpper().Trim();
                Guid IDObleaCil = cilindros.FirstOrDefault(x => x.OrdenCil == valvula.OrdenCil).ID;
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
                
                ObleasValvulas oV = this.ObleasValvulasLogic.Read(valvula.ID);
                if (oV == null)
                {
                    oV = new ObleasValvulas();
                    oV.ID = valvula.ID;
                    oV.IdObleaCilindro = valvula.IdObleaCil;
                    oV.IdValvulaUnidad = valUni.ID;
                    oV.IdOperacion = MSDBVAL;
                    this.ObleasValvulasLogic.Add(oV);
                }
                else
                {
                    oV.ID = valvula.ID;
                    oV.IdObleaCilindro = valvula.IdObleaCil;
                    oV.IdValvulaUnidad = valUni.ID;
                    oV.IdOperacion = MSDBVAL;
                    this.ObleasValvulasLogic.Update(oV);
                }

                #endregion
            }
        }
        #endregion

        protected void btnAceptarRT_Click(object sender, EventArgs e)
        {
            Guid idTallerRT = new Guid(cboRTTaller.SelectedValue);
            this.GrabarOblea(idTallerRT);
        }

        protected void btnVerImagenes_Click(object sender, EventArgs e)
        {
            String urlObleasImprimir = SiteMaster.UrlBase + @"Obleas/VerImagenes.aspx?id=" + this.ObleaID;
            PrintBoxCtrl1.PrintBox("Ver Imagenes", urlObleasImprimir);
        }

        protected void txtNroObleaAnterior_OnTextChanged(object sender, EventArgs e)
        {
            this.btnBuscarOblea_Click(this, new EventArgs());
        }
    }
}
