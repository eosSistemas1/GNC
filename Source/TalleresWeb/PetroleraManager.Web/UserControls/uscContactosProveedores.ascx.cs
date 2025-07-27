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

    public partial class uscContactosProveedores : System.Web.UI.UserControl
    {
        ContactosLogic contactos = new ContactosLogic();
        public Guid _idProveedor;
        public Guid idProveedor {
            get { return _idProveedor; }
            set { _idProveedor = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InicializarCampos();
            }
        }

        public void CargaProveedor(Guid idP)
        {
            idProveedor = idP;
            if (idProveedor != null)
            {
                txtIDProveedor.Text = idProveedor.ToString();
                Buscar();
            }
        }

        private void InicializarCampos()
        {
            txtNombre.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtCelular.Text = String.Empty;
            txtEmail.Text = String.Empty;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ContactosLogic lista = new ContactosLogic();
            Guid idItem = Guid.NewGuid();
            if (txtID.Text != String.Empty) { idItem = new Guid(txtID.Text); }

            CONTACTOS itemNuevo = new CONTACTOS();
            itemNuevo.ID = idItem;
            itemNuevo.ProveedoresID = new Guid(txtIDProveedor.Text);
            itemNuevo.Nombre = txtNombre.Text;
            itemNuevo.Descripcion = txtDescripcion.Text;
            itemNuevo.Telefono = txtTelefono.Text;
            itemNuevo.Celular = txtCelular.Text;
            itemNuevo.Email = txtEmail.Text;

            if (txtID.Text != String.Empty)
            {
                lista.Update(itemNuevo);
            }
            else
            {
                lista.Add(itemNuevo);
            }

            txtID.Text = String.Empty;
            Buscar();
            InicializarCampos();
        }

        protected void grdContactos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                var item = contactos.Read(idItem);
                contactos.Delete(item.ID);
                Buscar();
            }

            else if (e.CommandName == "modificar")
            {
                Guid idP = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ProveedoresID"].ToString());

                txtID.Text = idItem.ToString();
                txtIDProveedor.Text = idP.ToString();
                txtNombre.Text = grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text;
                txtDescripcion.Text = grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text;
                txtTelefono.Text = grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[2].Text;
                txtCelular.Text = grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[3].Text;
                txtEmail.Text = grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[4].Text;
            }
        }

        private void Buscar()
        {
            var c = contactos.ReadAllByIdProveedor(new Guid(txtIDProveedor.Text));
            grdContactos.DataSource = c;
            grdContactos.DataBind();
        }
    }
}