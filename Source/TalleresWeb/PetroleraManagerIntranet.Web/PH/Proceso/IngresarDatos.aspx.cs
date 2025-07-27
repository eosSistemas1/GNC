using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos;
using System;
using System.Web;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;
using static PetroleraManagerIntranet.Web.UserControls.MessageBoxCtrl;

namespace PetroleraManagerIntranet.Web.PH.Proceso
{
    public partial class IngresarDatos : PageBase
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

                    PasosProcesoPH? paso = new PHProcesoLogic().ObtenerProximoPasoPendiente(phCilindros);

                    if (paso.HasValue)
                    {
                        lblTitulo.Text = ProcesoPHHelper.ObtenerTituloProcesoPH(paso.Value);

                        this.CargarDatosEncabezado(phCilindros);

                        this.HabilitarIngresoDatos(paso.Value, phCilindros);
                    }
                    else
                    {
                        MessageBoxCtrl.MessageBox(null, "El cilindro seleccionado no tiene etapas pendientes", "Index.aspx", TipoWarning.Warning);
                    }
                }
                catch
                {
                    MessageBoxCtrl.MessageBox(null, "Ha ocurrido un error", "index.aspx", TipoWarning.Error);
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

        private void HabilitarIngresoDatos(PasosProcesoPH paso, PHCilindros phCilindros)
        {
            switch (paso)
            {
                case PasosProcesoPH.InspeccionVisual:
                    this.InspeccionVisual.Visible = true;
                    this.InspeccionVisual.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.RegistroPeso:
                    this.RegistroPesos.Visible = true;
                    this.RegistroPesos.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.MedicionEspesores:
                    this.MedicionEspesores.Visible = true;
                    this.MedicionEspesores.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.InspeccionExterior:
                    this.InspeccionExterior.Visible = true;
                    this.InspeccionExterior.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.PruebaHidraulica:
                    this.PruebaHidraulica.Visible = true;
                    this.PruebaHidraulica.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.InspeccionRoscas:
                    this.InspeccionRoscas.Visible = true;
                    this.InspeccionRoscas.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.InspeccionInterior:
                    this.InspeccionInterior.Visible = true;
                    this.InspeccionInterior.PHCilindros = phCilindros;
                    break;
                case PasosProcesoPH.CilindrosObservados:
                    Response.Redirect($"ObservarCilindro.aspx?id={phCilindros.ID}", true);
                    break;
                default:
                    throw new Exception("El Paso es incorrecto");
                    //Response.Redirect("index.aspx");                    
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            Aceptar("index.aspx");
        }

        protected void btnAceptarYContinuar_ServerClick(object sender, EventArgs e)
        {
            Aceptar(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        private void Aceptar(string reloadURL)
        {
            try
            {
                uscProcesosPHPasosBase pasoHabilitado = this.ObtenerPasoHabilitado();

                if (pasoHabilitado == null) throw new Exception("Paso seleccionado inválido.");

                var valido = pasoHabilitado.Validar();

                if (!string.IsNullOrWhiteSpace(valido.Item2))
                {
                    ObservarCilindro(pasoHabilitado);

                    if (valido.Item1)
                    {
                        throw new ArgumentException(valido.Item2);
                    }
                    else
                    {
                        throw new Exception(valido.Item2);
                    }
                }

                pasoHabilitado.Guardar(this.UsuarioID, false, true);

                MessageBoxCtrl.MessageBox(null, "Los datos se guardaron correctamente", reloadURL, TipoWarning.Success);
            }
            catch (ArgumentException ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, "index.aspx", TipoWarning.Info);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, "index.aspx", TipoWarning.Error);
            }
        }

        private uscProcesosPHPasosBase ObtenerPasoHabilitado()
        {
            if (InspeccionVisual.Visible) return InspeccionVisual;
            if (RegistroPesos.Visible) return RegistroPesos;
            if (MedicionEspesores.Visible) return MedicionEspesores;
            if (InspeccionExterior.Visible) return InspeccionExterior;
            if (PruebaHidraulica.Visible) return PruebaHidraulica;
            if (InspeccionRoscas.Visible) return InspeccionRoscas;
            if (InspeccionInterior.Visible) return InspeccionInterior;

            return null;
        }

        private void ObservarCilindro(uscProcesosPHPasosBase pasoHabilitado)
        {
            if (this.PHCilindroID.HasValue)
            {
                pasoHabilitado.Guardar(this.UsuarioID, true, false);

                this.PHCilindrosLogic.CambiarEstado(this.PHCilindroID.Value,
                                                    EstadosPH.Observar,
                                                    "Verificar Cilindro",
                                                    this.UsuarioID);
            }
        }
    }
}