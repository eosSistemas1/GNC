using PetroleraManager.Entities;
using PetroleraManager.Web.UserControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Sistema
{
    public partial class ResponsableTecnico : PageBase
    {
        #region Propiedades
        private RT item = new RT();

        private RTLogic rtLogic;
        public RTLogic logic
        {
            get
            {
                if (rtLogic == null) rtLogic = new RTLogic();
                return rtLogic;
            }
        }

        private RTParameters rtparameters;
        public RTParameters parameters
        {
            get
            {
                if (rtparameters == null) rtparameters = new RTParameters();
                return rtparameters;
            }
        }
        #endregion

        #region Metodos
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
            parameters.Descripcion = txtFiltro.Text.Trim();
            var extendedView = logic.ReadExtendedView(parameters);
            grdFiltro.DataSource = extendedView;
            grdFiltro.DataBind();
        }

        private void InicializarCampos()
        {
            txtID.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            cboTiposDocumentos.SelectedIndex = -1;
            txtNumeroDocumento.Text = String.Empty;
            txtMatricula.Text = String.Empty;
            txtTitulo.Text = String.Empty;
            txtActivo.Text = true.ToString();
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                var item = logic.Read(idItem);
                item.ActivoRT = false;
                logic.Update(item);
                Buscar();
            }

            else if (e.CommandName == "modificar")
            {
                CargarDatos(idItem);
                Panel1.Visible = true;
            }
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
                item.TipoDniID = cboTiposDocumentos.SelectedValue;
                item.NroDniRT = txtNumeroDocumento.Text;
                item.NombreApellidoRT = txtDescripcion.Text;
                item.MatriculaRT = txtMatricula.Text;
                item.TituloRT = txtTitulo.Text;
                item.ActivoRT = Boolean.Parse(txtActivo.Text);

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

            if (String.IsNullOrWhiteSpace(this.txtMatricula.Text)) mensaje += "Debe ingresar la matrícula <br/>";
            if (String.IsNullOrWhiteSpace(this.txtNumeroDocumento.Text)) mensaje += "Debe ingresar el número de documento <br/>";
            if (String.IsNullOrWhiteSpace(this.txtDescripcion.Text)) mensaje += "Debe ingresar el nombre del RT <br/>";
            if (this.cboTiposDocumentos.SelectedIndex == -1 || this.cboTiposDocumentos.SelectedValue == Guid.Empty) mensaje += "Debe ingresar tipo de documento <br/>";

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

        private void CargarDatos(Guid idItem)
        {
            var item = logic.Read(idItem);
            txtID.Text = item.ID.ToString();
            txtDescripcion.Text = item.NombreApellidoRT;
            cboTiposDocumentos.SelectedValue = item.TipoDniID;
            txtNumeroDocumento.Text = item.NroDniRT;
            txtMatricula.Text = item.MatriculaRT;
            txtTitulo.Text = item.TituloRT;
            txtActivo.Text = item.ActivoRT.ToString();
        }
        #endregion

    }
}