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
    public partial class BuscarProductoLote : System.Web.UI.UserControl
    {
        public event GridProductoLoteButtonEventHandler GridProductoLoteButtonClick;

        ProductosLogic lista = new ProductosLogic();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblInfo.Text = "";

                //txtSearch.Attributes.Add("onkeypress", "var xbtnPL=document.getElementById('btnSearchP'); ;if (event.keyCode == 13) __doPostBack(xbtnPL.name, '');");
                txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) return false;");
                
                fillSearchDDL();
                //txtSearch.Focus();
            }
        }

        private void bindGrid()
        {

            var resultadoPorCodigo = lista.ReadProductoLoteByCodigo(txtSearch.Text.Trim()).ToList();

            if (resultadoPorCodigo.Count == 1)
            {
                txtSearch.Text = resultadoPorCodigo.FirstOrDefault().Descripcion;
                CargarProveedorSeleccionado(resultadoPorCodigo.FirstOrDefault().ID,
                                            resultadoPorCodigo.FirstOrDefault().ProductoLoteID, 
                                            resultadoPorCodigo.FirstOrDefault().Descripcion);
                MPE.Hide();
            }
            else if (resultadoPorCodigo.Count > 1)
            {
                dgProductoLote.DataSource = resultadoPorCodigo;
                dgProductoLote.DataBind();
                MPE.Show();
            }
            else
            {
                var resultado = lista.ReadProductoLoteByDescripcion(txtSearch.Text.Trim()).ToList();

                if (resultado.Count > 0)
                {
                    dgProductoLote.DataSource = resultado;
                    dgProductoLote.DataBind();
                    MPE.Show();
                }
                else
                {
                    lblInfo.Text = "No existe Productos que coincidan con el criterio de búsqueda.";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridProductoLoteButtonClick(GridProductoLoteButtonEventArgs e)
        {
            if (GridProductoLoteButtonClick != null)
            {
                GridProductoLoteButtonClick(this, e);
            }
        }

        private void fillSearchDDL()
        {
            if (ddlColumnList.Items.Count == 0)
            {
                ddlColumnList.Items.Add(new ListItem("Código de Cliente", "ID"));
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

        private void CargarProveedorSeleccionado(Guid ProductoID, Guid ProductoLoteID, String descripcion)
        {
            GridProductoLoteButtonEventArgs ge;

            ge = new GridProductoLoteButtonEventArgs(ProductoID, ProductoLoteID, descripcion);
            onGridProductoLoteButtonClick(ge);
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
        protected void dgProductoLote_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = dgProductoLote.SelectedRow.Cells[0].Text.Trim();

            CargarProveedorSeleccionado(new Guid(dgProductoLote.DataKeys[dgProductoLote.SelectedIndex].Values["ID"].ToString()), 
                                            new Guid(dgProductoLote.DataKeys[dgProductoLote.SelectedIndex].Values["ProductoLoteID"].ToString()), 
                                            dgProductoLote.SelectedRow.Cells[0].Text.Trim());
        }

    }

    public partial class GridProductoLoteButtonEventArgs
    {
        private Guid _IDProducto;
        private Guid _IDProductoLote;
        private String _Descripcion;

        public Guid IDProducto
        {
            get { return _IDProducto; }
        }

        public Guid IDProductoLote
        {
            get { return _IDProductoLote; }
        }

        public String Descripcion
        {
            get { return _Descripcion; }
        }

        public GridProductoLoteButtonEventArgs(Guid ProductoID, Guid ProductoLoteID, String Descripcion)
        {
            _IDProducto = ProductoID;
            _IDProductoLote = ProductoLoteID;
            _Descripcion = Descripcion;
        }
    }
    public delegate void GridProductoLoteButtonEventHandler(object sender, GridProductoLoteButtonEventArgs e);
   
}