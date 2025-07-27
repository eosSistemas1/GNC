using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.Proceso
{
    public partial class ObservarCilindro : PageBase
    {
        #region Properties
        private PHCilindrosLogic phCilindrosLogic;

        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }

        public Guid phID
        {
            get { return Guid.Parse(ViewState["PHID"].ToString()); }
            set { ViewState["PHID"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    this.phID = new Guid(Request.QueryString["id"].ToString());

                    CargarPhCilindro();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
        }

        private void CargarPhCilindro()
        {
            PHCilindros phCilindros = this.PHCilindrosLogic.ReadPhCilindroDetallado(phID);

            if (phCilindros.InspeccionVisualCorrecta.HasValue
                  && !phCilindros.InspeccionVisualCorrecta.Value) Response.Redirect($"ModificarInspeccionVisual.aspx?id={phID}", true);

            this.InspeccionRoscas.PHCilindros = phCilindros;
            this.MedicionEspesores.PHCilindros = phCilindros;
            this.InspeccionExterior.PHCilindros = phCilindros;
            this.RegistroPesos.PHCilindros = phCilindros;
            this.PruebaHidraulica.PHCilindros = phCilindros;
            this.InspeccionInterior.PHCilindros = phCilindros;
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            PHProcesoLogic phProcesoLogic = new PHProcesoLogic();

            var pasosPendientes = phProcesoLogic.ObtenerPasosPendientesPH(phID);

            this.InspeccionVisual.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.InspeccionVisual), false);
            this.InspeccionRoscas.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.InspeccionRoscas), false);
            this.MedicionEspesores.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.MedicionEspesores), false);
            this.InspeccionExterior.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.InspeccionExterior), false);
            this.RegistroPesos.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.RegistroPeso), false);
            this.PruebaHidraulica.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.PruebaHidraulica), false);
            this.InspeccionInterior.Guardar(this.UsuarioID, pasosPendientes.Any(x => x == PasosProcesoPH.InspeccionVisual), true);

            var tienePasosPendientes = phProcesoLogic.ObtenerPasosPendientesPH(phID);
            if (tienePasosPendientes.Any())
            {
                PHCilindrosLogic.CambiarEstado(phID, EstadosPH.EnProceso, "PH vuelve a proceso", this.UsuarioID);
            }
        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {

        }
    }
}