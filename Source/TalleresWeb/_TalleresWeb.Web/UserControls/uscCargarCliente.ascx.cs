using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.UserControls
{
    public partial class uscCargarCliente : System.Web.UI.UserControl
    {
        ClientesLogic logic = new ClientesLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (txtNroDocCliente.Text != String.Empty)
            {
                if (BuscarCliente())
                {
                    //si existe el cliente cargo los datos
                    HabilitarCliente(true);

                    btnBuscarOtroCliente.Visible = true;

                }
                else
                {
                    //si no existe permito cargar los datos
                    hddID.Value = Guid.NewGuid().ToString();
                    BorrarCliente(false);
                    HabilitarCliente(true);
                    txtNom.Focus();
                }

                pnlCliente.Enabled = true;
            }
            else 
            {
                txtNroDocCliente.Focus();
            }

           

        }
        private Boolean BuscarCliente()
        {
            Boolean valor = false;
            var cliente = logic.ReadByTipoyNroDoc(cboDocCliente.SelectedValue, txtNroDocCliente.Text);
            if (cliente.Count > 0)
            {
                hddID.Value = cliente.FirstOrDefault().ID.ToString();
                txtNom.Text = cliente.FirstOrDefault().Descripcion == null? "0" : cliente.FirstOrDefault().Descripcion.ToUpper();
                txtCalle.Text = cliente.FirstOrDefault().CalleCliente == null ? "" : cliente.FirstOrDefault().CalleCliente.ToUpper();
                txtTelefono.Text = cliente.FirstOrDefault().TelefonoCliente == null ? "0" : cliente.FirstOrDefault().TelefonoCliente.ToUpper();
                cboCiudades.SelectedValue = cliente.FirstOrDefault().IdLocalidad;

                valor = true;
            }
            return valor;
        }
        private void BorrarCliente(Boolean borrarNroDoc)
        {
            if (borrarNroDoc) txtNroDocCliente.Text = String.Empty;
            txtNom.Text = String.Empty;
            txtCalle.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            cboCiudades.SelectedIndex = 0;
        }
        private void HabilitarCliente(Boolean valor)
        {
            txtNroDocCliente.ReadOnlyTxt = !valor;
            txtNom.ReadOnlyTxt = !valor;
            txtCalle.ReadOnlyTxt = !valor;
            txtTelefono.ReadOnlyTxt = !valor;
            cboCiudades.Enabled = valor;
        }

        protected void btnBuscarOtroCliente_Click(object sender, EventArgs e)
        {
            hddID.Value = String.Empty;
            BorrarCliente(true);
            HabilitarCliente(true);
            //Genericos.Utility.SetFocus(txtDominioVehiculo);
            txtNroDocCliente.Focus();
            btnBuscarOtroCliente.Visible = false;
            btnBuscarCliente.Visible = true;
            pnlCliente.Enabled = false;
            txtNroDocCliente.ReadOnlyTxt = false;
        }

        public Clientes ClienteCargado
        {
            get
            {
                Clientes cliente = new Clientes();
                if (hddID.Value != "")
                {
                    cliente.ID = new Guid(hddID.Value);
                    cliente.Descripcion = txtNom.Text.ToUpper().Trim();
                    cliente.CalleCliente = txtCalle.Text.ToUpper().Trim();
                    cliente.IdTipoDniCliente = cboDocCliente.SelectedValue;
                    cliente.NroDniCliente = txtNroDocCliente.Text != String.Empty ? txtNroDocCliente.Text : "0";
                    cliente.TelefonoCliente = txtTelefono.Text;
                    cliente.IdLocalidad = cboCiudades.SelectedValue;
                }
                else
                {
                    cliente.ID = Guid.Empty;
                }
                return cliente;
            }
            set
            {
                var cliente = (Clientes)value;
                hddID.Value = cliente.ID.ToString();
                txtNom.Text = cliente.Descripcion;
                txtCalle.Text = cliente.CalleCliente;
                cboDocCliente.SelectedValue = cliente.IdTipoDniCliente;
                txtNroDocCliente.Text = cliente.NroDniCliente;
                txtTelefono.Text = cliente.TelefonoCliente != String.Empty ? cliente.TelefonoCliente : "0";
                cboCiudades.SelectedValue = cliente.IdLocalidad;

                btnBuscarCliente.Visible = false;
                btnBuscarOtroCliente.Visible = true;
            }
        }
    }
}