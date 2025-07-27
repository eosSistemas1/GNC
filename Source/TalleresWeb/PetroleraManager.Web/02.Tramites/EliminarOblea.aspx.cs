using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Tramites
{
    public partial class EliminarOblea : PageBase
    {
        #region Properties       
        private ObleasLogic _logic;
        private ObleasLogic logic
        {
            get
            {
                if (this._logic == null) this._logic = new ObleasLogic();
                return this._logic;
            }
        }

        private Guid idOblea
        {
            get { return new Guid(ViewState["OBLEAID"].ToString()); }
            set { ViewState["OBLEAID"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[0] != null)
                {
                    try
                    {
                        Guid id = new Guid(Request.QueryString[0].ToString());
                        this.idOblea = id;

                        var oblea = this.logic.ReadDetalladoByID(id);

                        this.CargarDatos(oblea);
                    }
                    catch
                    {
                        String urlRetorno = "ObleasConsultar.aspx";
                        MessageBoxCtrl1.MessageBox(null, "La Ficha Técnica no existe en el sistema", urlRetorno, UserControls.MessageBoxCtrl.TipoWarning.Warning);
                    }
                }
            }
        }

        private void CargarDatos(Obleas oblea)
        {
            lblObleaAnterior.Text = oblea.Descripcion;
            lblTitular.Text = String.Format("{0} - {1} {2} - {3} {4} - {5} - {6}", oblea.Clientes.Descripcion,
                                                                                   oblea.Clientes.DocumentosClientes.Descripcion,
                                                                                   oblea.Clientes.NroDniCliente,
                                                                                   oblea.Clientes.CalleCliente,
                                                                                   oblea.Clientes.NroCalleCliente,
                                                                                   oblea.Clientes.Localidades.Descripcion,
                                                                                   oblea.Clientes.TelefonoCliente);

            lblVehiculo.Text = String.Format("{0} - {1} - {2} - {3}", oblea.Vehiculos.Descripcion,
                                                                      oblea.Vehiculos.MarcaVehiculo.ToUpper(),
                                                                      oblea.Vehiculos.ModeloVehiculo.ToUpper(),
                                                                      oblea.Vehiculos.AnioVehiculo);

            lblTaller.Text = String.Format("{0} - {1} - {2} - {3}", oblea.Talleres.Descripcion,
                                                                    oblea.Talleres.RazonSocialTaller,
                                                                    oblea.Talleres.TelefonoTaller,
                                                                    oblea.Talleres.DomicilioTaller);
            lblObservaciones.Text = oblea.ObservacionesFicha;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            String mensajes = this.Validar();

            if (String.IsNullOrWhiteSpace(mensajes))
            {
                this.logic.CambiarEstado(this.idOblea, ESTADOSFICHAS.Eliminada, txtObservaciones.Text, SiteMaster.IdUsuarioLogueado);

                String urlRetorno = "ObleasConsultar.aspx";
                MessageBoxCtrl1.MessageBox(null, "La Ficha Técnica se ha eliminado", urlRetorno, UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private String Validar()
        {
            String mensajes = String.Empty;

            if (String.IsNullOrWhiteSpace(txtObservaciones.Text)) mensajes += "- Debe ingresar una observación </br>";           

            return mensajes;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ObleasConsultar.aspx");
        }
    }
}