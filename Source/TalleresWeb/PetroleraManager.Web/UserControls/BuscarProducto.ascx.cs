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
    public partial class BuscarProducto : System.Web.UI.UserControl
    {
        public event GridProductoButtonEventHandler GridProductoButtonClick;

        ProductosLogic lista = new ProductosLogic();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblInfo.Text = "";

               //txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) __doPostBack('" + btnSearchP.ClientID + "');");
                txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) return false;");

                fillSearchDDL();
            }
        }

        private void bindGrid()
        {
            var resultadoPorCodigo = lista.ReadProductoByCodigo(txtSearch.Text.Trim()).ToList();

            var resultado = lista.ReadProductoByDescripcion(txtSearch.Text.Trim()).ToList();


            if (resultadoPorCodigo.Count == 1)
            {
                if (!resultadoPorCodigo.FirstOrDefault().UsaLote)
                {
                    txtSearch.Text = resultadoPorCodigo.FirstOrDefault().Descripcion;
                    CargarProductoSeleccionado(resultadoPorCodigo.FirstOrDefault().ID, resultadoPorCodigo.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    dgProducto.DataSource = resultado;
                    dgProducto.DataBind();
                    MPE.Show();
                }
            }
            else
            {
                if (resultado.Count > 0)
                {
                    dgProducto.DataSource = resultado;
                    dgProducto.DataBind();
                    MPE.Show();
                }
                else
                {
                    lblInfo.Text = "No existe Productos que coincidan con el criterio de búsqueda.";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridProductoButtonClick(GridProductoButtonEventArgs e)
        {
            if (GridProductoButtonClick != null)
            {
                GridProductoButtonClick(this, e);
            }
        }

        private void fillSearchDDL()
        {
            if (ddlColumnList.Items.Count == 0)
            {
                ddlColumnList.Items.Add(new ListItem("Código", "ID"));
                ddlColumnList.Items.Add(new ListItem("Descripción", "Descripcion"));
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

        private void CargarProductoSeleccionado(Guid ID, String descripcion)
        {
            GridProductoButtonEventArgs ge;

            ge = new GridProductoButtonEventArgs(ID, descripcion);
            onGridProductoButtonClick(ge);
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

        public void LimpiarTexto()
        {
            txtSearch.Text = "";
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


            //if (this.ddlColumnList.SelectedValue == "ID")
            //{
                
                //List<ProveedoresExtendedView> resultado = lista.ReadExtendedViewByCodigo(txtSearch.Text.Trim());
                
            //    if (resultado.Count == 1)
            //    {
            //        CargarProveedorSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
            //    }
            //    else
            //    {
            //        lblInfo.Text = "El cliente solicitado no existe.";
            //        MPE.Hide();
            //    }
            //}
            //else
            //{
                MPE.Show();
                bindGrid();
            //}
        }
        protected void dgProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = dgProducto.SelectedRow.Cells[0].Text.Trim();

            CargarProductoSeleccionado(new Guid(dgProducto.DataKeys[dgProducto.SelectedIndex].Values["ID"].ToString()), dgProducto.SelectedRow.Cells[0].Text.Trim());
        }

    }

    public partial class GridProductoButtonEventArgs
    {
        private Guid _IDProducto;
        private String _Descripcion;

        public Guid IDProducto
        {
            get { return _IDProducto; }
        }

        public String Descripcion
        {
            get { return _Descripcion; }
        }

        public GridProductoButtonEventArgs(Guid ID, String Descripcion)
        {
            _IDProducto = ID;
            _Descripcion = Descripcion;
        }
    }
    public delegate void GridProductoButtonEventHandler(object sender, GridProductoButtonEventArgs e);
   
}