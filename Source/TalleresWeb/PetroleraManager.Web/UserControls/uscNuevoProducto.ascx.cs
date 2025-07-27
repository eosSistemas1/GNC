using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.Logic;
using ET = PetroleraManager.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscNuevoProducto : System.Web.UI.UserControl
    {
        ProductosLogic logic = new ProductosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
                cboTipoProducto.Enabled = false;
                txtID.EnabledTxt = false;
                //LimpiarCampos();
            //}
        }

        public void Grabar()
        {
            ET.Productos productosEntity = new ET.Productos();
            if (txtID.Text == String.Empty)
            {
                //NUEVO
                productosEntity.Descripcion = txtDescripcion.Text;
                productosEntity.RubrosID = cboRubro.SelectedValue;
                productosEntity.PrecioCpraActual = txtPrecioCompra.Text != String.Empty ? Double.Parse(txtPrecioCompra.Text) : 0;
                productosEntity.PrecioVentaActual = txtPrecioVenta.Text != String.Empty ? Double.Parse(txtPrecioVenta.Text) : 0;
                productosEntity.StockMinimo = txtStockMinimo.Text != String.Empty ? Double.Parse(txtStockMinimo.Text) : 0;
                productosEntity.StockActual = txtStockActual.Text != String.Empty ? Double.Parse(txtStockActual.Text): 0;
                productosEntity.BaseImponibleID = cboBaseImponible.SelectedValue;
                productosEntity.TipoProductoID = cboTipoProducto.SelectedValue;
                logic.Add(productosEntity);
            }
            else 
            {
                //MODIFICAR 
                productosEntity.ID = new Guid(txtID.Text);
                productosEntity.Descripcion = txtDescripcion.Text;
                productosEntity.RubrosID = cboRubro.SelectedValue;
                productosEntity.PrecioCpraActual = txtPrecioCompra.Text != String.Empty ? Double.Parse(txtPrecioCompra.Text) : 0;
                productosEntity.PrecioVentaActual = txtPrecioVenta.Text != String.Empty ? Double.Parse(txtPrecioVenta.Text) : 0;
                productosEntity.StockMinimo = txtStockMinimo.Text != String.Empty ? Double.Parse(txtStockMinimo.Text) : 0;
                productosEntity.StockActual = txtStockActual.Text != String.Empty ? Double.Parse(txtStockActual.Text) : 0;
                productosEntity.BaseImponibleID = cboBaseImponible.SelectedValue;
                productosEntity.TipoProductoID = cboTipoProducto.SelectedValue;
                logic.Update(productosEntity);
            }
        }


        public void LimpiarCampos()
        {
            txtID.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            //cboRubro.Selectedin = ;
            txtPrecioCompra.Text = String.Empty;
            txtPrecioVenta.Text = String.Empty;
            txtStockMinimo.Text = String.Empty;
            txtStockActual.Text = String.Empty;
            //cboBaseImponible.SelectedValue = ;
            //cboTipoProducto.SelectedValue = ;
        }


        public void CargarDatos(Guid idProducto)
        {
            var producto = logic.Read(idProducto);

            txtID.Text = producto.ID.ToString();
            txtDescripcion.Text = producto.Descripcion.ToString();
            cboRubro.SelectedValue = producto.RubrosID;
            txtPrecioCompra.Text = producto.PrecioCpraActual.ToString();
            txtPrecioVenta.Text = producto.PrecioVentaActual.ToString();
            txtStockMinimo.Text = producto.StockMinimo.ToString();
            txtStockActual.Text = producto.StockActual.ToString();
            cboBaseImponible.SelectedValue = producto.BaseImponibleID;
            cboTipoProducto.SelectedValue = producto.TipoProductoID;
        }
    }
}