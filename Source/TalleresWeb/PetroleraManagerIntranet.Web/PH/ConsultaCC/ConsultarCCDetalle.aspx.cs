using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class ConsultarCCDetalle : PageBase
    {
        #region Properties
        private PHLogic phLogic;
        public PHLogic PHLogic
        {
            get
            {
                if (this.phLogic == null) phLogic = new PHLogic();
                return this.phLogic;
            }
        }

        private PHCilindrosLogic phCilindrosLogic;
        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (this.phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return this.phCilindrosLogic;
            }
        }

        public Guid EstadoAnteriorID
        {
            get { return new Guid(ViewState["ESTADOANTERIORID"].ToString()); }
            set { ViewState["ESTADOANTERIORID"] = value; }
        }

        public Guid PHID
        {
            get
            {
                if (ViewState["PHID"] == null) return Guid.Empty;

                return new Guid(ViewState["PHID"].ToString());
            }
            set { ViewState["PHID"] = value; }
        }

        public bool ModificaEstado
        {
            get { return bool.Parse(ViewState["MODIFICAESTADO"].ToString()); }
            set { ViewState["MODIFICAESTADO"] = value; }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    try
                    {
                        this.PHID = new Guid(Request.QueryString["id"].ToString());
                        this.EstadoAnteriorID = new Guid(Request.QueryString["e"].ToString());
                        this.ModificaEstado = Request.QueryString["m"] != null
                                                && Request.QueryString["m"].ToString().ToUpper() == bool.TrueString.ToUpper() ? true : false;
                        this.ConsultaCilindrosPH.ModificaEstado = this.ModificaEstado;
                        this.CargarDatos();
                    }
                    catch (Exception ex)
                    {
                        this.Cancelar();
                    }
                }
            }
        }

        private void CargarDatos()
        {
            TalleresWeb.Entities.PH cartaCompromiso = null;
            if (ModificaEstado)
               cartaCompromiso = this.PHLogic.ReadDetalladoByIDParaConsulta(this.PHID);
            else
                cartaCompromiso = this.PHLogic.ReadDetalladoByID(this.PHID);

            if (cartaCompromiso != null && cartaCompromiso.PHCilindros.Any())
            {
                this.BindearDatos(cartaCompromiso);
            }
            else
            {
                this.Cancelar();
            }
        }

        private void BindearDatos(TalleresWeb.Entities.PH cartaCompromiso)
        {
            List<PHCilindrosConsultaView> cilindros = new List<PHCilindrosConsultaView>();
            var phCilindros = ModificaEstado ? cartaCompromiso.PHCilindros.Where(c => c.IdEstadoPH == EstadosPH.EnEsperaCilindros
                                                                        || c.IdEstadoPH == EstadosPH.Ingresada
                                                                        || c.IdEstadoPH == EstadosPH.Bloqueada) :
                                               cartaCompromiso.PHCilindros;


            foreach (var item in phCilindros)
            {
                PHCilindrosConsultaView cilindro = new PHCilindrosConsultaView();
                cilindro.CapacidadCil = item.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ?
                                          item.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : string.Empty;
                cilindro.MarcaCil = String.Empty;
                try
                {
                    cilindro.MarcaCil = item.CilindrosUnidad.Cilindros.MarcasCilindros != null ?
                                          item.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion : string.Empty;
                }
                catch { }

                cilindro.ID = item.ID;
                cilindro.MesFabricacionCil = item.CilindrosUnidad.MesFabCilindro.HasValue? item.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : string.Empty;
                cilindro.AnioFabricacionCil = item.CilindrosUnidad.AnioFabCilindro.HasValue? item.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : string.Empty;
                cilindro.SerieCil = item.CilindrosUnidad.Descripcion.Trim().ToUpper();
                cilindro.SerieVal = item.Valvula_Unidad.Descripcion.Trim().ToUpper();
                cilindro.CodigoCil = item.CilindrosUnidad.Cilindros.Descripcion.Trim().ToUpper();
                cilindro.CodigoVal = item.Valvula_Unidad.Valvula.Descripcion.Trim().ToUpper();
                cilindro.IDEstadoPH = item.IdEstadoPH;
                cilindro.Observaciones = item.ObservacionPH;
                cilindro.ModificaEstado = ModificaEstado;
                cilindros.Add(cilindro);
            }

            this.ConsultaCilindrosPH.PermiteAgregar = true;
            this.ConsultaCilindrosPH.CilindrosPHCargados = cilindros;

            txtDominio.Value = cartaCompromiso.Vehiculos.Descripcion.Trim().ToUpper();
            txtMarca.Value = cartaCompromiso.Vehiculos.MarcaVehiculo.Trim().ToUpper();
            txtModelo.Value = cartaCompromiso.Vehiculos.ModeloVehiculo.Trim().ToUpper();
            cboTipoDocumento.SelectedValue = cartaCompromiso.Clientes.IdTipoDniCliente.ToString();
            txtNumeroDocumento.Value = cartaCompromiso.Clientes.NroDniCliente.Trim();
            txtNombre.Value = cartaCompromiso.Clientes.Descripcion.Trim().ToUpper();
            txtDomicilio.Value = cartaCompromiso.Clientes.CalleCliente;
            cboLocalidad.SelectedValue = cartaCompromiso.Clientes.IdLocalidad.ToString();
            txtCodigoPostal.Value = cartaCompromiso.Clientes.Localidades.CodigoPostal.Trim().ToUpper();
            txtTelefono.Value = cartaCompromiso.Clientes.TelefonoCliente.Trim();
            txtNroOblea.Value = cartaCompromiso.NroObleaHabilitante.Trim();
            cboTalleres.SelectedValue = cartaCompromiso.IdTaller.ToString();
            cboPEC.SelectedValue = cartaCompromiso.IdPEC.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> mensajes = this.Validar();

                if (!mensajes.Any())
                {
                    PHViewWebApi cartaCompromiso = ObtenerDatosAGuardar();

                    if (this.PHID == null || this.PHID == Guid.Empty || !cartaCompromiso.Cilindros.Any())
                            throw new Exception("Ha ocurrido un error o no hay datos para actualizar estado.");

                    foreach (var phcilindro in cartaCompromiso.Cilindros)
                    {
                        this.PHCilindrosLogic.SetearNumeroInternoOperacion(phcilindro.ID);
                    }

                    this.PHLogic.UpdateCartaCompromiso(this.PHID, cartaCompromiso, this.UsuarioID, ModificaEstado);
                    this.Cancelar();
                }
                else
                {
                    this.MessageBox.MessageBox(null, mensajes, Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                this.MessageBox.MessageBox(null, ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        private PHViewWebApi ObtenerDatosAGuardar()
        {
            List<PHCilindrosViewWebApi> cilindros = new List<PHCilindrosViewWebApi>();
            foreach (var item in this.ConsultaCilindrosPH.CilindrosPHCargados)
            {
                PHCilindrosViewWebApi cilindro = new PHCilindrosViewWebApi();
                cilindro.ID = item.ID;
                cilindro.Capacidad = item.CapacidadCil;
                cilindro.MarcaCilindro = item.MarcaCil;
                cilindro.MesFabCilindro = !String.IsNullOrWhiteSpace(item.FechaFabricacionCil) ? DateTime.Parse(item.FechaFabricacionCil).Month.ToString("00") : string.Empty;
                cilindro.AnioFabCilindro = !String.IsNullOrWhiteSpace(item.FechaFabricacionCil) ? DateTime.Parse(item.FechaFabricacionCil).Year.ToString().Substring(2, 2) : string.Empty;
                cilindro.NumeroSerieCilindro = item.SerieCil.ToUpper();
                cilindro.NumeroSerieValvula = item.SerieVal.ToUpper();
                cilindro.CodigoHomologacionCilindro = item.CodigoCil.ToUpper();
                cilindro.CodigoHomologacionValvula = item.CodigoVal.ToUpper();
                cilindro.IdEstadoPH = item.IDEstadoPH;
                cilindro.Observacion = item.Observaciones;
                cilindros.Add(cilindro);
            }

            PHViewWebApi cartaCompromiso = new PHViewWebApi()
            {
                ID = Guid.NewGuid(),
                VehiculoDominio = txtDominio.Value.Trim().ToUpper(),
                VehiculoMarca = txtMarca.Value.Trim().ToUpper(),
                VehiculoModelo = txtModelo.Value.Trim().ToUpper(),
                ClienteTipoDocumento = cboTipoDocumento.SelectedValue,
                ClientesNumeroDocumento = txtNumeroDocumento.Value.Trim(),
                ClienteRazonSocial = txtNombre.Value.Trim().ToUpper(),
                ClienteDomicilio = txtDomicilio.Value.Trim().ToUpper(),
                ClienteLocalidad = cboLocalidad.SelectedItem.Text,
                ClienteLocalidadID = cboLocalidad.SelectedValue,
                ClienteCodigoPostal = txtCodigoPostal.Value.Trim().ToUpper(),
                ClienteTelefono = txtTelefono.Value.Trim().ToUpper(),
                NroObleaHabilitante = txtNroOblea.Value.Trim(),
                Cilindros = cilindros,
                TallerID = new Guid(cboTalleres.SelectedValue),
                PECID = new Guid(cboPEC.SelectedValue)
            };
            return cartaCompromiso;
        }

        private List<String> Validar()
        {
            List<String> mensajes = new List<String>();
            if (String.IsNullOrWhiteSpace(txtDominio.Value)) mensajes.Add("- Debe ingresar un vehículo");
            if (String.IsNullOrWhiteSpace(txtNumeroDocumento.Value)
                   || cboTipoDocumento.SelectedIndex == -1) mensajes.Add("- Debe ingresar un cliente");
            if (String.IsNullOrWhiteSpace(txtNroOblea.Value)) mensajes.Add("- Debe ingresar número oblea");
            //if (!ConsultaCilindrosPH.CilindrosPHCargados.Any()) mensajes.Add("- Debe ingresar al menos un cilindro");
            if (cboTalleres.SelectedIndex == -1) mensajes.Add("- Debe seleccionar un taller");
            return mensajes;
        }

        private void ImprimirPH(Guid phID)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupImprimir('" + phID + "');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            Response.Redirect("Index.aspx");
        }
        #endregion

    }
}