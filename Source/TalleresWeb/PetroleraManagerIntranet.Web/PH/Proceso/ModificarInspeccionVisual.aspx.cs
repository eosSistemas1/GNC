using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Web.Cross.Configuracion;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using CrossCutting.DatosDiscretos;

namespace PetroleraManagerIntranet.Web.PH.Proceso
{
    public partial class ModificarInspeccionVisual : PageBase
    {
        #region Properties
        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }

        private PHLogic phLogic;
        private PHLogic PHLogic
        {
            get
            {
                if (phLogic == null) phLogic = new PHLogic();
                return phLogic;
            }
        }

        private PHProcesoLogic phProcesoLogic;
        private PHProcesoLogic PHProcesoLogic
        {
            get
            {
                if (phProcesoLogic == null) phProcesoLogic = new PHProcesoLogic();
                return phProcesoLogic;
            }
        }

        public Guid? PHCilindroID
        {
            get
            {
                if (ViewState["PHCILINDROID"] == null) return default(Guid?);
                return new Guid(ViewState["PHCILINDROID"].ToString());
            }
            set
            {
                ViewState["PHCILINDROID"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    PHCilindroID = Request.QueryString["id"] != null ? new Guid(Request.QueryString["id"].ToString()) : default(Guid?);
                    PHCilindros phCilindros = this.PHCilindrosLogic.ReadPhCilindroDetallado(this.PHCilindroID.Value);

                    if (phCilindros.InspeccionVisualCorrecta.HasValue
                        && !phCilindros.InspeccionVisualCorrecta.Value)
                    {
                        this.CargarDatosEncabezado(phCilindros);

                        this.CargarDetalle(phCilindros);
                    }
                    else
                    {
                        //MessageBoxCtrl.MessageBox(null, "No se puede modificar inspección visual", "Index.aspx", TipoWarning.Warning);
                    }
                }
                catch
                {
                    //MessageBoxCtrl.MessageBox(null, "Ha ocurrido un error", "index.aspx", TipoWarning.Error);
                }
            }
        }

        private void CargarDatosEncabezado(PHCilindros phCilindros)
        {
            lblTaller.Text = $"{phCilindros.PH.Talleres.Descripcion}-{phCilindros.PH.Talleres.RazonSocialTaller}";

            lblNumeroSerieCilindro.Text = phCilindros.CilindrosUnidad.Descripcion;
            lblCodigoHomologacionCilindro.Text = phCilindros.CilindrosUnidad.Cilindros.Descripcion;
            lblMarcaCilindro.Text = phCilindros.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion;
            lblCapacidadCilindro.Text = phCilindros.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ?
                                            phCilindros.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : string.Empty;
            lblDiametroCilindro.Text = phCilindros.CilindrosUnidad.Cilindros.DiametroCilindro.HasValue ?
                                            phCilindros.CilindrosUnidad.Cilindros.DiametroCilindro.Value.ToString() : string.Empty;
            lblFechaFabricacionCilindro.Text = $"{phCilindros.CilindrosUnidad.MesFabCilindro.Value.ToString("00")}/{phCilindros.CilindrosUnidad.AnioFabCilindro.Value.ToString("00")}";
            lblParedMinimo.Text = phCilindros.CilindrosUnidad.Cilindros.EspesorAdmisibleCil.HasValue ?
                                            phCilindros.CilindrosUnidad.Cilindros.EspesorAdmisibleCil.Value.ToString() : string.Empty;
            lblFondoMinimo.Text = phCilindros.CilindrosUnidad.Cilindros.EspesorAdmisibleCil.HasValue ?
                                            phCilindros.CilindrosUnidad.Cilindros.EspesorAdmisibleCil.Value.ToString() : string.Empty;
            var ultimaRevision = new ObleasCilindrosLogic().ReadUltimaRevisionCilndro(phCilindros.IdCilindroUnidad);
            if (ultimaRevision != null)
                lblFechaUltimaRevision.Text = $"{ultimaRevision.MesUltimaRevisionCil.ToString("00")}/{ultimaRevision.AnioUltimaRevisionCil.ToString("00")}";

            lblNumeroSerieValvula.Text = phCilindros.Valvula_Unidad.Descripcion;
            lblCodigoHomologacionValvula.Text = phCilindros.Valvula_Unidad.Valvula != null ? phCilindros.Valvula_Unidad.Valvula.Descripcion : "SIN MARCA";
            lblMarcaValvula.Text = phCilindros.Valvula_Unidad.Valvula.MarcasValvulas != null ? phCilindros.Valvula_Unidad.Valvula.MarcasValvulas.Descripcion : "SIN MARCA";
        }

        private void CargarDetalle(PHCilindros phCilindros)
        {
            List<PHCilindrosConsultaView> cilindros = new List<PHCilindrosConsultaView>();

            PHCilindrosConsultaView cilindro = new PHCilindrosConsultaView();
            cilindro.CapacidadCil = phCilindros.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ?
                                      phCilindros.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : string.Empty;
            cilindro.MarcaCil = String.Empty;
            try
            {
                cilindro.MarcaCil = phCilindros.CilindrosUnidad.Cilindros.MarcasCilindros != null ?
                                      phCilindros.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion : string.Empty;
            }
            catch { }

            cilindro.ID = phCilindros.ID;
            cilindro.MesFabricacionCil = phCilindros.CilindrosUnidad.MesFabCilindro.HasValue ? phCilindros.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : string.Empty;
            cilindro.AnioFabricacionCil = phCilindros.CilindrosUnidad.AnioFabCilindro.HasValue ? phCilindros.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : string.Empty;
            cilindro.SerieCil = phCilindros.CilindrosUnidad.Descripcion.Trim().ToUpper();
            cilindro.SerieVal = phCilindros.Valvula_Unidad.Descripcion.Trim().ToUpper();
            cilindro.CodigoCil = phCilindros.CilindrosUnidad.Cilindros.Descripcion.Trim().ToUpper();
            cilindro.CodigoVal = phCilindros.Valvula_Unidad.Valvula.Descripcion.Trim().ToUpper();
            cilindro.IDEstadoPH = phCilindros.IdEstadoPH;
            cilindro.Observaciones = phCilindros.ObservacionPH;
            cilindro.ModificaEstado = false;
            cilindros.Add(cilindro);

            this.ConsultaCilindrosPH.PermiteAgregar = false;
            this.ConsultaCilindrosPH.CilindrosPHCargados = cilindros;
        }


        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            if (!this.PHCilindroID.HasValue)
                throw new Exception("Ha ocurrido un error. No existe la oblea a observar.");

            PHViewWebApi cartaCompromiso = ObtenerDatosAGuardar(this.PHCilindroID.Value);

            try
            {
                if (cartaCompromiso == null || !cartaCompromiso.Cilindros.Any())
                    throw new Exception("Ha ocurrido un error o no hay datos para actualizar .");

                this.PHLogic.UpdateCartaCompromiso(cartaCompromiso.ID, cartaCompromiso, this.UsuarioID, false);

                this.MessageBox.MessageBox(null, "La ph se modificó correctamente.", "index.aspx", Web.UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
            catch (Exception ex)
            {
                this.MessageBox.MessageBox(null, ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }

            //volver a proceso si corresponde            
            var pasosPendientes = PHProcesoLogic.ObtenerPasosPendientesPH(this.PHCilindroID.Value);

            if (pasosPendientes.Any())
            {
                PHCilindrosLogic.CambiarEstado(this.PHCilindroID.Value, EstadosPH.EnProceso, "PH Oservada Inspección visual -> Proceso", this.UsuarioID);
            }            
        }

        private PHViewWebApi ObtenerDatosAGuardar(Guid phcilindroID)
        {
            var ph = new PHLogic().ReadDetalladoByphCilindroID(phcilindroID);

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
                ID = ph.ID,
                VehiculoDominio = ph.Vehiculos.Descripcion.Trim(),
                VehiculoMarca = ph.Vehiculos.MarcaVehiculo.Trim(),
                VehiculoModelo = ph.Vehiculos.ModeloVehiculo.Trim(),
                ClienteTipoDocumento = ph.Clientes.IdTipoDniCliente.ToString(),
                ClientesNumeroDocumento = ph.Clientes.NroDniCliente.Trim(),
                ClienteRazonSocial = ph.Clientes.Descripcion.Trim().ToUpper(),
                ClienteDomicilio = ph.Clientes.CalleCliente,                
                ClienteLocalidad = ph.Clientes.Localidades.Descripcion,
                ClienteLocalidadID = ph.Clientes.Localidades.ID.ToString(),
                ClienteCodigoPostal = ph.Clientes.Localidades.CodigoPostal,
                ClienteTelefono = ph.Clientes.TelefonoCliente.Trim().ToUpper(),
                NroObleaHabilitante = ph.NroObleaHabilitante.Trim(),
                Cilindros = cilindros,
                TallerID = ph.IdTaller,
                PECID = ph.IdPEC
            };

            return cartaCompromiso;
        }
    }
}