using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.UserControls
{
    public delegate void ClienteChangedEventHandler();

    public partial class CargarCliente : System.Web.UI.UserControl
    {
        #region Members

        private ClientesLogic logic = new ClientesLogic();
        public event ClienteChangedEventHandler ClienteChanged;

        #endregion

        #region Properties

        public Clientes ClienteCargado
        {
            get
            {
                Clientes cliente = new Clientes();
                cliente.ID = new Guid(hddID.Value);
                cliente.Descripcion = txtNom.Text.ToUpper().Trim();
                cliente.CalleCliente = txtCalle.Text.ToUpper().Trim();
                cliente.IdTipoDniCliente = new Guid(cboDocCliente.SelectedValue);
                cliente.NroDniCliente = txtNroDocCliente.Text != String.Empty ? txtNroDocCliente.Text : "0";
                cliente.TelefonoCliente = txtTelefono.Text;
                cliente.IdLocalidad = new Guid(cboCiudades.SelectedValue);

                return cliente;
            }
            set
            {
                var cliente = (Clientes)value;
                hddID.Value = cliente.ID.ToString();
                txtNom.Text = cliente.Descripcion;
                txtCalle.Text = cliente.CalleCliente;                
                txtNroDocCliente.Text = cliente.NroDniCliente;
                txtTelefono.Text = cliente.TelefonoCliente != String.Empty ? cliente.TelefonoCliente : "0";
                txtTelefono.Text = txtTelefono.Text != String.Empty ? txtTelefono.Text : "0";
                this.CargarComboLocalidades();
                cboCiudades.SelectedValue = cliente.IdLocalidad.ToString();

                this.CargarComboDocumentos();
                cboDocCliente.SelectedValue = cliente.IdTipoDniCliente.ToString();

                btnBuscarCliente.Visible = false;
                btnBuscarOtroCliente.Visible = true;
                HabilitarCliente(true);
            }
        }

        public Guid? ClienteCargadoID
        {
            get
            {
                if (String.IsNullOrEmpty(hddID.Value)) return default(Guid?);

                return new Guid(hddID.Value);
            }
            set { hddID.Value = value.ToString(); }
        }

        public Boolean EsClienteValido
        {
            get
            {   
                return ClienteCargadoID.HasValue &&
                        !String.IsNullOrWhiteSpace(txtNroDocCliente.Text) &&
                        !String.IsNullOrWhiteSpace(txtNom.Text) &&
                        !String.IsNullOrWhiteSpace(txtCalle.Text) &&
                        !String.IsNullOrWhiteSpace(txtTelefono.Text) &&
                        cboCiudades.SelectedIndex >= 0;


            }
        }
        #endregion

        #region Methods

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {           
            if (txtNroDocCliente.Text != String.Empty)
            {
                try
                {
                    Int64 nroDocumento = Int64.Parse(txtNroDocCliente.Text);
                }
                catch
                {
                    txtNroDocCliente.Text = String.Empty;
                    txtNroDocCliente.Focus();
                    return;
                }


                if (BuscarCliente())
                {
                    //si existe el cliente cargo los datos
                    HabilitarCliente(true);

                    btnBuscarOtroCliente.Visible = true;
                }
                else
                {
                    hddID.Value = Guid.NewGuid().ToString();
                    //BorrarCliente(false);
                    HabilitarCliente(true);
                    cboDocCliente.Enabled = true;
                    txtNroDocCliente.ReadOnly = false;
                    txtNom.Focus();
                }

                pnlCliente.Enabled = true;
            }
            else
            {
                txtNroDocCliente.Focus();
            }
        }

        protected void btnBuscarOtroCliente_Click(object sender, EventArgs e)
        {
            cboCiudades.SelectedValue = LOCALIDADES.Rosario.ToString();
            hddID.Value = String.Empty;
            BorrarCliente(true);
            HabilitarCliente(true);            
            txtNroDocCliente.Focus();
            btnBuscarOtroCliente.Visible = false;
            btnBuscarCliente.Visible = true;
            pnlCliente.Enabled = false;
            txtNroDocCliente.ReadOnly = false;
            cboDocCliente.Enabled = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CargarComboLocalidades();
                this.CargarComboDocumentos();
            }
        }

        private void BorrarCliente(Boolean borraDni)
        {
            if (borraDni) txtNroDocCliente.Text = String.Empty;
            txtNom.Text = String.Empty;
            txtCalle.Text = String.Empty;
            txtTelefono.Text = "0";
            cboCiudades.SelectedValue = LOCALIDADES.Rosario.ToString();
            btnBuscarOtroCliente.Visible = false;
        }

        private Boolean BuscarCliente()
        {
            Boolean valor = false;
            var cliente = logic.ReadClientesViewByTipoyNroDoc(new Guid(cboDocCliente.SelectedValue), txtNroDocCliente.Text);
            if (cliente.Any())
            {
                hddID.Value = cliente.FirstOrDefault().ID.ToString();
                txtNom.Text = cliente.FirstOrDefault().Descripcion.ToUpper();
                txtCalle.Text = cliente.FirstOrDefault().CalleCliente != null ? cliente.FirstOrDefault().CalleCliente.ToUpper() : String.Empty;
                txtTelefono.Text = cliente.FirstOrDefault().TelefonoCliente != null ? cliente.FirstOrDefault().TelefonoCliente.ToUpper() : "0";
                txtTelefono.Text = txtTelefono.Text != String.Empty ? txtTelefono.Text : "0";
                cboCiudades.SelectedValue = cliente.FirstOrDefault().IdLocalidad.ToString();
                valor = true;

                if (this.ClienteChanged != null)
                {
                    this.ClienteChanged();
                }

            }
            return valor;
        }

        private void CargarComboLocalidades()
        {
            if (cboCiudades.Items.Count > 0) return;

            LocalidadesLogic logic = new LocalidadesLogic();
            List<ViewEntity> dt = new List<ViewEntity>();

            var localidades = logic.ReadAll().OrderBy(x => x.Descripcion);

            foreach (Localidades loc in localidades)
            {
                String descripcion = loc.Descripcion + ", " + loc.Provincias.Descripcion;
                LocalidadesExtendedView dr = new LocalidadesExtendedView();
                dr.ID = loc.ID;
                dr.Descripcion = descripcion;
                dt.Add(dr);
            }
            cboCiudades.DataValueField = "ID";
            cboCiudades.DataTextField = "Descripcion";
            cboCiudades.DataSource = dt;
            cboCiudades.DataBind();

            cboCiudades.SelectedValue = LOCALIDADES.Rosario.ToString();
        }

        private void CargarComboDocumentos()
        {
            if (cboDocCliente.Items.Count > 0) return;

            DocumentosLogic logic = new DocumentosLogic();
            List<ViewEntity> dt = new List<ViewEntity>();

            var documentos = logic.ReadAll().OrderBy(x => x.Descripcion);

            foreach (DocumentosClientes loc in documentos)
            {
                String descripcion = loc.Descripcion;
                DocumentosExtendedView dr = new DocumentosExtendedView();
                dr.ID = loc.ID;
                dr.Descripcion = descripcion;
                dt.Add(dr);
            }
            cboDocCliente.DataValueField = "ID";
            cboDocCliente.DataTextField = "Descripcion";
            cboDocCliente.DataSource = dt;
            cboDocCliente.DataBind();

            cboDocCliente.SelectedValue = TiposDocumentos.DNI.ToString();
        }

        private void HabilitarCliente(Boolean valor)
        {
            txtNroDocCliente.ReadOnly = valor;
            cboDocCliente.Enabled = !valor;
            txtNom.ReadOnly = !valor;
            txtCalle.ReadOnly = !valor;
            txtTelefono.ReadOnly = !valor;
            cboCiudades.Enabled = valor;
        }

        public void SetFocus()
        {
            this.txtNroDocCliente.Focus();
        }

        #endregion
    }
}