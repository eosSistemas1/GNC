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
using TalleresWeb.Logic;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Sistema
{
    public partial class Cilindros : PageBase
    {
        TalleresWeb.Entities.Cilindros item = new TalleresWeb.Entities.Cilindros();
        CilindrosLogic logic = new CilindrosLogic();
        CilindrosParameters param = new CilindrosParameters();

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
            txtCapacidad.Text = String.Empty;
            txtMAtriculaOCCil.Text = String.Empty;
            txtModeloCil.Text = String.Empty;
            txtEspesor.Text = String.Empty;
            txtDiametro.Text = String.Empty;
            txtLargo.Text = String.Empty;
            txtRotura.Text = String.Empty;
            txtMaterial.Text = String.Empty;
            txtNormaFab.Text = String.Empty;
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
            txtCapacidad.Text = item.CapacidadCil.HasValue ? item.CapacidadCil.Value.ToString() : String.Empty;
            txtMAtriculaOCCil.Text = item.MatriculaOCCil != null ? item.MatriculaOCCil : String.Empty;
            txtModeloCil.Text = item.ModeloCilindro != null ? item.ModeloCilindro : String.Empty;
            txtEspesor.Text = item.EspesorAdmisibleCil.HasValue ? item.EspesorAdmisibleCil.Value.ToString() : String.Empty;
            txtDiametro.Text = item.DiametroCilindro.HasValue ? item.DiametroCilindro.Value.ToString() : String.Empty;
            txtLargo.Text = item.LargoCilindro.HasValue ? item.LargoCilindro.Value.ToString() : String.Empty;
            txtRotura.Text = item.RoturaCilindro != null ? item.RoturaCilindro.ToString() : String.Empty;
            txtMaterial.Text = item.MaterialCilindro != null ? item.MaterialCilindro.ToString() : String.Empty;
            txtNormaFab.Text = item.NormaFabCilindro != null ? item.NormaFabCilindro.ToString() : String.Empty;
            cboMarcasCilindros.SelectedValue = item.IdMarcaCilindro.HasValue ? item.IdMarcaCilindro.Value : Guid.Empty;
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
            item.CapacidadCil = Decimal.Parse(txtCapacidad.Text);
            item.MatriculaOCCil = txtMAtriculaOCCil.Text;
            item.ModeloCilindro = txtModeloCil.Text;
            item.EspesorAdmisibleCil = Double.Parse(txtEspesor.Text);
            item.DiametroCilindro = Double.Parse(txtDiametro.Text);
            item.LargoCilindro = Double.Parse(txtLargo.Text);
            item.RoturaCilindro = txtRotura.Text;
            item.MaterialCilindro = txtMaterial.Text;
            item.NormaFabCilindro = txtNormaFab.Text;
            item.IdMarcaCilindro = cboMarcasCilindros.SelectedValue;

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