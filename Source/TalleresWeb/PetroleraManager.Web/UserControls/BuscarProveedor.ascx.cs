using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetroleraManager.Logic;
using PetroleraManager.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class BuscarProveedor : System.Web.UI.UserControl
    {
        public event GridProveedoresButtonEventHandler GridProveedoresButtonClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblInfo.Text = "";

                //txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) __doPostBack('"+btnSearchP.ClientID+"');");
                txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) return false;");

                fillSearchDDL();
                txtSearch.Focus();
            }
        }

        private void bindGrid()
        {
            ProveedoresLogic lista = new ProveedoresLogic();

            //si busca por código
            if (FilterValue == "C")
            {
                ProveedoresParameters param = new ProveedoresParameters();
                param.Codigo = int.Parse(txtSearch.Text.Trim());
                List<ProveedoresExtendedView> resultado = lista.ReadExtendedViewByCodigo(param);

                if (resultado.Count > 1)
                {
                    dgProveedores.DataSource = resultado;
                    dgProveedores.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarProveedorSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Proveedores para el texto ingresado";
                    MPE.Hide();
                }
            }

            //si busca por Descripcion
            if (FilterValue == "D")
            {
                ProveedoresParameters param = new ProveedoresParameters();
                param.Descripcion = txtSearch.Text.Trim();
                List<ProveedoresExtendedView> resultado = lista.ReadExtendedView(param);
                
                if (resultado.Count > 1)
                {
                    dgProveedores.DataSource = resultado;
                    dgProveedores.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarProveedorSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Proveedores para el texto ingresado";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridProveedoresButtonClick(GridProveedoresButtonEventArgs e)
        {
            if (GridProveedoresButtonClick != null)
            {
                GridProveedoresButtonClick(this, e);
            }
        }

        private void fillSearchDDL()
        {
            if (ddlColumnList.Items.Count == 0)
            {
                ddlColumnList.Items.Add(new ListItem("Razón Social", "D"));
                ddlColumnList.Items.Add(new ListItem("Código", "C"));               
                //ddlColumnList.Items.Add(new ListItem("CUIT", "CUIT"));
                ddlColumnList.DataBind();
            }
        }

        public void Clear()
        {
            this.txtSearch.Text = "";
            this.ddlColumnList.SelectedIndex = 0;
            MPE.Hide();
        }

        private void CargarProveedorSeleccionado(Guid IdProveedor, String razonSocial)
        {
            txtSearch.Text = razonSocial;
            GridProveedoresButtonEventArgs ge;

            ge = new GridProveedoresButtonEventArgs(IdProveedor, razonSocial);
            onGridProveedoresButtonClick(ge);
        }

        public string FilterValue
        {
            get { return ddlColumnList.SelectedValue; }
        }

        public string SearchValue
        {
            get { return txtSearch.Text; }
        }

        public bool ShowGrid
        {
            get { return this.Panel1.Visible; }
            set { this.Panel1.Visible = value; }
        }

        public void SetFocus()
        {
            txtSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";

            if (txtSearch.Text.Trim() == "")
            {
                lblInfo.Text = "Debe ingresar un texto para la búsqueda.";
                MPE.Hide();
                return;
            }

            bindGrid();

        }
        protected void dgProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProveedorSeleccionado(new Guid(dgProveedores.DataKeys[dgProveedores.SelectedIndex].Values["ID"].ToString()), dgProveedores.SelectedRow.Cells[0].Text.Trim());
        }
    }

    public partial class GridProveedoresButtonEventArgs
    {
        private Guid _IDProveedor;
        private String _RazonSocial;

        public Guid IDProveedor
        {
            get { return _IDProveedor; }
        }

        public String RazonSocial
        {
            get { return _RazonSocial; }
        }

        public GridProveedoresButtonEventArgs(Guid idProveedor, String razonSocial)
        {
            _IDProveedor = idProveedor;
            _RazonSocial = razonSocial;
        }
    }
    public delegate void GridProveedoresButtonEventHandler(object sender, GridProveedoresButtonEventArgs e);
}