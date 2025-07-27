using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetroleraManager.Web.UserControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Lotes
{
    public partial class Lotes : PageBase
    {
        LOTES item = new LOTES();
        LotesLogic logic = new LotesLogic();
        LotesParameters param = new LotesParameters();

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
            //param.Descripcion = txtFiltro.Text.Trim();
            //var extendedView = logic.ReadExtendedView(param);
            //grdFiltro.DataSource = extendedView;
            //grdFiltro.DataBind();
        }

        private void InicializarCampos()
        {
            txtID.Text = String.Empty;
            txtFecha.Value = String.Empty;
            txtNumeroObleaDesde.Value = String.Empty;
            txtNumeroObleaHasta.Value = String.Empty;
            txtAnioVencimiento.Value = String.Empty;
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                logic.Delete(idItem);
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
            //txtDescripcion.Text = item.Descripcion;
            //txtCodigoPostal.Text = item.CP;
            //cboProvincia.SelectedValue = item.ProvinciasID;
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
                //item.Descripcion = txtDescripcion.Text;
                //item.CP = txtCodigoPostal.Text;
                //item.ProvinciasID = cboProvincia.SelectedValue;
                //item.Activo = true;

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

            //if (String.IsNullOrWhiteSpace(this.txtDescripcion.Text)) mensaje += "Debe ingresar la localidad <br/>";
            //if (String.IsNullOrWhiteSpace(this.txtCodigoPostal.Text)) mensaje += "Debe ingresar el código postal <br/>";
            //if (this.cboProvincia.SelectedIndex == -1 || this.cboProvincia.SelectedValue == Guid.Empty) mensaje += "Debe ingresar una provincia <br/>";

            //if (mensaje != String.Empty)
            //{
            //    MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            //}

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