using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Sistema
{
    public partial class Reguladores : PageBase
    {
        TalleresWeb.Entities.Reguladores item = new TalleresWeb.Entities.Reguladores();
        ReguladoresLogic logic = new ReguladoresLogic();
        ReguladoresParameters param = new ReguladoresParameters();

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
            cboMarcasReguladores.SelectedIndex = -1;
            txtModeloReg.Text = String.Empty;
            txtMatriculaOCReg.Text = String.Empty;
            txtCaudalReg.Text = String.Empty;
            txtEtapasReg.Text = String.Empty;
            txtTipoReg.Text = String.Empty;
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                throw new NotImplementedException();
                //var item = logic.Read(idItem);
                //item.Activo = false;
                //logic.Update(item);
                //Buscar();
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
            cboMarcasReguladores.SelectedValue = item.IdMarcaRegulador.HasValue ? item.IdMarcaRegulador.Value : Guid.Empty;
            txtModeloReg.Text = item.Modelo != null ? item.Modelo: String.Empty;
            txtMatriculaOCReg.Text = item.MatriculaOC != null ? item.MatriculaOC : String.Empty;
            txtCaudalReg.Text = item.Caudal.HasValue ? item.Caudal.Value.ToString() : String.Empty;
            txtEtapasReg.Text = item.Etrapas.HasValue ? item.Etrapas.Value.ToString() : String.Empty;
            txtTipoReg.Text = item.Tipo;
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
            item.IdMarcaRegulador = cboMarcasReguladores.SelectedValue;
            item.Modelo = txtModeloReg.Text;
            item.MatriculaOC = txtMatriculaOCReg.Text;
            item.Caudal = !String.IsNullOrWhiteSpace(txtCaudalReg.Text) ?  Double.Parse(txtCaudalReg.Text) : default(Double?);
            item.Etrapas = !String.IsNullOrWhiteSpace(txtEtapasReg.Text) ? int.Parse(txtEtapasReg.Text) : default(int?);
            item.Tipo = txtTipoReg.Text;

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