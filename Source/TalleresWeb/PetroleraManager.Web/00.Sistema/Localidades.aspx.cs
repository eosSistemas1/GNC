using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using PetroleraManager.Web.UserControls;

namespace PetroleraManager.Web.Sistema
{
    public partial class Localidades : PageBase
    {
        TalleresWeb.Entities.Localidades item = new TalleresWeb.Entities.Localidades();
        LocalidadesLogic logic = new LocalidadesLogic();
        TalleresWeb.Entities.LocalidadesParameters param = new TalleresWeb.Entities.LocalidadesParameters();

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
            txtCodigoPostal.Text = String.Empty;
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                try
                {
                    logic.Delete(idItem);
                }
                catch (Exception)
                {
                    MessageBoxCtrl1.MessageBox(null, "La localidad no se puede eliminar.", MessageBoxCtrl.TipoWarning.Warning);
                };

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
            txtCodigoPostal.Text = item.CodigoPostal;
            cboProvincia.SelectedValue = item.IdProvincia;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            Panel1.Visible = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                item.ID = txtID.Text == String.Empty ? Guid.NewGuid() : new Guid(txtID.Text.Trim());
                item.Descripcion = txtDescripcion.Text;
                item.CodigoPostal = txtCodigoPostal.Text;
                item.IdProvincia = cboProvincia.SelectedValue;

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
        }

        private bool Validar()
        {
            String mensaje = String.Empty;

            if (String.IsNullOrWhiteSpace(this.txtDescripcion.Text)) mensaje += "Debe ingresar la localidad <br/>";
            if (String.IsNullOrWhiteSpace(this.txtCodigoPostal.Text)) mensaje += "Debe ingresar el código postal <br/>";
            if (this.cboProvincia.SelectedIndex == -1 || this.cboProvincia.SelectedValue == Guid.Empty) mensaje += "Debe ingresar una provincia <br/>";

            if (mensaje != String.Empty)
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }

            return mensaje == String.Empty;
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