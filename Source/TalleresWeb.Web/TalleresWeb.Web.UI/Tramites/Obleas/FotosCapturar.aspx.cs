using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites.Obleas
{
    public partial class FotosCapturar : PageBase
    {
        #region Properties
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }

        private ClientesLogic clientesLogic;
        public ClientesLogic ClientesLogic
        {
            get
            {
                if (clientesLogic == null) clientesLogic = new ClientesLogic();
                return clientesLogic;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //ObleasViewWebApi oblea = this.ObtenerDatosOblea();

            //List<String> mensajes = this.ValidarOblea(oblea);

            //if (!mensajes.Any())
            //{
            //    oblea.ObleaNumeroAnterior = oblea.ObleaNumeroAnterior == "0" ? String.Empty : oblea.ObleaNumeroAnterior;
            //    ViewEntity o = this.ObleasLogic.Guardar(oblea);

            //    if (o.ID != Guid.Empty)
            //    {
            //        String urlRetorno = "Default.aspx";
            //        Guid? idPH = oblea.PH != null ? oblea.PH.ID : default(Guid?);
            //        this.ImprimirOblea(o.ID, idPH, urlRetorno);

            //    }
            //    else
            //    {
            //        Master.MessageBox(null, o.Descripcion, UserControls.MessageBoxCtrl.TipoWarning.Error);
            //    }
            //}
            //else
            //{
            //    Master.MessageBox(null, mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            //}
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../default.aspx");
        }

        protected void txtModalDominio_TextChanged(object sender, EventArgs e)
        {
            //if (!this.ObleasLogic.ExisteTramitePendienteParaElDominio(txtDominio.Text))
            //{
            //    VehiculosView vehiculo = this.VehiculosLogic.ReadByDominio(txtDominio.Text);
            //    if (vehiculo != null)
            //    {
            //        txtDominio.Text = vehiculo.VehiculoDominio;
            //        txtMarca.Text = vehiculo.VehiculoMarca;
            //        txtModelo.Text = vehiculo.VehiculoModelo;
            //        txtAnio.Text = vehiculo.VehiculoAnio.HasValue ? vehiculo.VehiculoAnio.Value.ToString() : String.Empty;
            //        chkEsInyeccion.Checked = vehiculo.VehiculoEsInyeccion.HasValue ? vehiculo.VehiculoEsInyeccion.Value : false;
            //        Reguladores.SetFocus();
            //    }
            //    else
            //    {
            //        txtMarca.Text = String.Empty;
            //        txtModelo.Text = String.Empty;
            //        txtAnio.Text = String.Empty;
            //        chkEsInyeccion.Checked = false;

            //        txtMarca.Focus();
            //    }

            //    txtMarca.ReadOnly = false;
            //    txtModelo.ReadOnly = false;
            //    txtAnio.ReadOnly = false;
            //    chkEsInyeccion.Enabled = false;

            //    if (txtDominio.Text != String.Empty)
            //        Session["DOMINIOFOTO"] = txtDominio.Text;
            //}
            //else
            //{
            //    String mensaje = $"El dominio {txtDominio.Text} ya tiene un tramite en curso; por cualquier inconveniente comuníquese con Petrolera Italo Argentina srl.-";
            //    Master.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Warning);

            //    txtMarca.Text = String.Empty;
            //    txtModelo.Text = String.Empty;
            //    txtAnio.Text = String.Empty;
            //    chkEsInyeccion.Checked = false;

            //    txtMarca.Focus();
            //}            
        }
    }
}
