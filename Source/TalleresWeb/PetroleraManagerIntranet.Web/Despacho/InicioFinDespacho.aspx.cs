using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Despacho
{
    public partial class InicioFinDespacho : PageBase
    {
        #region Properties

        private DespachoLogic despachoLogic;
        public DespachoLogic DespachoLogic
        {
            get
            {
                if (this.despachoLogic == null) despachoLogic = new DespachoLogic();
                return this.despachoLogic;
            }
        }

        DateTime fechaDesde;
        DateTime fechaHasta;
        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InicializarFechas();
                this.LeerDespachosEntreFechas();
            }
        }
        private void InicializarFechas()
        {
            txtFechaDesde.Value = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
            txtFechaHasta.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void LeerDespachosEntreFechas()
        {
            try
            {

                ValidarLeerDespachos();

                List<DespachoEnCursoView> despachos = this.DespachoLogic.ReadDespachosEnCursoEntreFechas(fechaDesde, fechaHasta);
                grdDespachos.DataSource = despachos;
                grdDespachos.DataBind();
            }
            catch (Exception ex)
            {
                MessageBoxCtrl1.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }


        private void ValidarLeerDespachos()
        {
            if (string.IsNullOrWhiteSpace(txtFechaDesde.Value) || string.IsNullOrWhiteSpace(txtFechaHasta.Value))
                throw new Exception("Debe ingresar las fechas");
            if (!DateTime.TryParse(txtFechaDesde.Value, out fechaDesde) || !DateTime.TryParse(txtFechaHasta.Value, out fechaHasta))
                throw new Exception("Las fechas ingresadas son incorrectas");
            if (fechaDesde > fechaHasta)
                throw new Exception("La fecha desde no puede ser mayor a la fecha hasta");

            int diasEntreFechas = (fechaHasta - fechaDesde).Days;
            //if (diasEntreFechas > 7)
            //    throw new Exception("La cantidad de dias entre fechas no puede superar los 7");
        }

        private void ProcesarDespacho(String numeroDespacho)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(numeroDespacho))
                    throw new Exception("Debe ingresar un despacho.");

                this.DespachoLogic.InicioFinDespacho(numeroDespacho, this.UsuarioID);

                this.LeerDespachosEntreFechas();

                MessageBoxCtrl1.MessageBox(null, $"El despacho {numeroDespacho} se actualizó correctamente", UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl1.MessageBox(null, $"El despacho {numeroDespacho} no se pudo actualizar </br> {ex.Message}", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void grdDespachos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grilla = sender as GridView;
                String fechaInicio = e.Row.Cells[4].Text.Replace("&nbsp;", "");

                Button btnEditarDespacho = (Button)e.Row.FindControl("btnEditarDespacho");
                Button btnIniciarDespacho = (Button)e.Row.FindControl("btnIniciarDespacho");
                Button btnFinalizarDespacho = (Button)e.Row.FindControl("btnFinalizarDespacho");
                Button btnRechazarDespacho = (Button)e.Row.FindControl("btnRechazarDespacho");
                Button btnEliminarDespacho = (Button)e.Row.FindControl("btnEliminarDespacho");

                btnEditarDespacho.Visible = String.IsNullOrWhiteSpace(fechaInicio);
                btnIniciarDespacho.Visible = String.IsNullOrWhiteSpace(fechaInicio);
                btnFinalizarDespacho.Visible = !String.IsNullOrWhiteSpace(fechaInicio);
                btnRechazarDespacho.Visible = !String.IsNullOrWhiteSpace(fechaInicio);
                btnEliminarDespacho.Visible = String.IsNullOrWhiteSpace(fechaInicio);
            }
        }

        protected void btnIniciar_ServerClick(object sender, EventArgs e)
        {
            this.ProcesarDespacho(despachoAProcesar.Value);
            LeerDespachosEntreFechas();
        }

        protected void btnFinalizar_ServerClick(object sender, EventArgs e)
        {
            this.ProcesarDespacho(despachoAProcesar.Value);
            LeerDespachosEntreFechas();
        }

        protected void btnRechazar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                this.ValidarParametrosRechazo();

                this.DespachoLogic.RechazarDespachoTaller(int.Parse(despachoAProcesar.Value),
                                                          new Guid(cboTalleresRechazar.SelectedValue),
                                                          this.UsuarioID);

                MessageBoxCtrl1.MessageBox(null, $"Se rechazó el despacho {despachoAProcesar.Value} para el taller {cboTalleresRechazar.SelectedItem.Text}.", UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl1.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Warning);

            }
        }

        private void ValidarParametrosRechazo()
        {
            try
            {
                int numero = int.Parse(despachoAProcesar.Value);

                new Guid(cboTalleresRechazar.SelectedValue);
            }
            catch (Exception)
            {
                throw new Exception("Los parámetros ingresados para rechazar el despacho, son incorrectos.");
            }
        }

        protected void btnEliminar_ServerClick(object sender, EventArgs e)
        {
            this.DespachoLogic.EliminarDespacho(despachoAProcesar.Value, this.UsuarioID);
            LeerDespachosEntreFechas();
        }

        protected void btnMostrarDespachoEntreFechas_ServerClick(object sender, EventArgs e)
        {
            this.LeerDespachosEntreFechas();

        }
        #endregion

    }
}