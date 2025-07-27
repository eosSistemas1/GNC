using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.DataAccess;
using PetroleraManager.Logic;
using PetroleraManager.Entities;
using PL.Fwk.Presentation.Web.Pages;

namespace PetroleraManager.Web.Sistema
{
    public partial class Transportes : PageBase
    {
        TRANSPORTES item = new TRANSPORTES();
        TransportesLogic logic = new TransportesLogic();
        TransportesParameters param = new TransportesParameters();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Panel1.Visible = false;
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            param.Descripcion = txtFiltro.Text.Trim();
            var extendedView = logic.ReadExtendedView(param);
            grdFiltro.DataSource = extendedView;
            grdFiltro.DataBind();
        }

        private void InicializarCampos()
        {
            txtID.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtDomicilio.Text = String.Empty;
            txtTelefono.Text = String.Empty;
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                var item = logic.Read(idItem);
                item.Activo = false;
                logic.Update(item);
                Buscar();
            }

            else if (e.CommandName == "modificar")
            {

                CargarDatos(idItem);
                Panel1.Visible = true;
            }
        }

        private void CargarDatos(Guid idItem)
        {
            var item = logic.Read(idItem);
            txtID.Text = item.ID.ToString();
            txtDescripcion.Text = item.Descripcion;
            txtDomicilio.Text = item.Domicilio;
            txtTelefono.Text = item.Telefono;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            Panel1.Visible = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {  
            item.ID = txtID.Text == String.Empty ? Guid.NewGuid() : new Guid(txtID.Text.Trim());
            item.Descripcion = txtDescripcion.Text;
            item.Domicilio = txtDomicilio.Text;
            item.Telefono = txtTelefono.Text;
            item.Activo = true;

            // si txt id es vacio, creo uno nuevo , si tiene id lo modifico
            if (txtID.Text == String.Empty) 
            {
                logic.Add(item);
            }
            else
            {
                logic.Update(item);
            }

            Cancelar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            InicializarCampos();
            Panel1.Visible = false;
            txtFiltro.Focus();
            Buscar();
        }
    }
}