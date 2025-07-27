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
    public partial class BuscarCliente : System.Web.UI.UserControl
    {
        public event GridClientesButtonEventHandler GridClientesButtonClick;
        ClientesLogic lista = new ClientesLogic();

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
            ClientesLogic lista = new ClientesLogic();

            //si busca por código
            if (FilterValue == "C")
            {
                ClientesParameters param = new ClientesParameters();
                param.Codigo = int.Parse(txtSearch.Text.Trim());
                List<ClientesExtendedView> resultado = lista.ReadExtendedViewByCodigo(param);

                if (resultado.Count > 1)
                {
                    dgClientes.DataSource = resultado;
                    dgClientes.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Clientes para el texto ingresado.";
                    MPE.Hide();
                }
            }

            //si busca por Descripcion
            if (FilterValue == "D")
            {
                ClientesParameters param = new ClientesParameters();
                param.Descripcion = txtSearch.Text.Trim();
                List<ClientesExtendedView> resultado = lista.ReadExtendedView(param);

                if (resultado.Count > 1)
                {
                    dgClientes.DataSource = resultado;
                    dgClientes.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Clientes para el texto ingresado.";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridClientesButtonClick(GridClientesButtonEventArgs e)
        {
            if (GridClientesButtonClick != null)
            {
                GridClientesButtonClick(this, e);
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

        private void CargarSeleccionado(Guid IDCliente, String razonSocial)
        {
            txtSearch.Text = razonSocial;
            GridClientesButtonEventArgs ge;

            ge = new GridClientesButtonEventArgs(IDCliente, razonSocial);
            onGridClientesButtonClick(ge);
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
        protected void dgClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSeleccionado(new Guid(dgClientes.DataKeys[dgClientes.SelectedIndex].Values["ID"].ToString()), dgClientes.SelectedRow.Cells[0].Text.Trim());
        }
    }

    public partial class GridClientesButtonEventArgs
    {
        private Guid _IDCliente;
        private String _RazonSocial;

        public Guid IDCliente
        {
            get { return _IDCliente; }
        }

        public String RazonSocial
        {
            get { return _RazonSocial; }
        }

        public GridClientesButtonEventArgs(Guid IDCliente, String razonSocial)
        {
            _IDCliente = IDCliente;
            _RazonSocial = razonSocial;
        }
    }
    public delegate void GridClientesButtonEventHandler(object sender, GridClientesButtonEventArgs e);
}