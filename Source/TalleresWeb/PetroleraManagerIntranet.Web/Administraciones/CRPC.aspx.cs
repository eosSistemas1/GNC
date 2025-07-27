using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class CRPC : PageBase
    {
        #region Properties
        private CRPCLogic cRPCLogic;
        private bool mostrarListado = true;
        public CRPCLogic CRPCLogic
        {
            get
            {
                if (this.cRPCLogic == null) cRPCLogic = new CRPCLogic();
                return cRPCLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Accion();
                if (mostrarListado == true) this.Buscar();
            }
        }

        private void Accion()
        {

            if (Request.QueryString["a"] == null || Request.QueryString["a"] == "B")
                this.divBuscar.Visible = true;
            if (Request.QueryString["a"] == "A")
            {
                this.AccionUsuario.InnerText = "NUEVO";
                this.AccionAlta();
            }
            if (Request.QueryString["a"] == "M")
            {
                this.AccionConsulta(true);
                this.AccionUsuario.InnerText = "MODIFICAR";
                this.NuevoRegulador.Visible = false;

            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoRegulador.Visible = false;
                this.txtMatricula.Disabled = true;
                this.txtSiglas.Disabled = true;
                this.txtRazonSocial.Disabled = true;
                this.txtTelefono1.Disabled = true;
                this.txtTelefono2.Disabled = true;
                this.txtMail1.Disabled = true;
                this.txtMail2.Disabled = true;
                this.txtDomicilio.Disabled = true;
                this.cboLocalidad.Enabled = false;
            }
            if (Request.QueryString["a"] == "B")
                this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());
                this.CRPCLogic.Delete(id);
            }
            catch (ArgumentNullException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no existe.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
            catch (Exception e)
            {                
                MessageBoxCtrl.MessageBox(null, "No se puede eliminar el item seleccionado.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }

        }

        private void AccionAlta()
        {
            this.divBuscar.Visible = false;
            this.divDatos.Disabled = false;
            this.LimpiarCampos();
            txtMatricula.Focus();
            mostrarListado = false;
            this.NuevoRegulador.Visible = false;
        }

        private void AccionConsulta(Boolean modifica)
        {
            if (Request.QueryString["id"] != null)
            {
                this.divBuscar.Visible = false;
                this.divDatos.Disabled = !modifica;
                this.btnAceptar.Visible = modifica;
                Guid id = new Guid(Request.QueryString["id"].ToString());
                this.CargarDatosModificacion(id);
            }
            mostrarListado = false;
        }

        private void LimpiarCampos()
        {
            txtMatricula.Value = string.Empty;
            txtSiglas.Value = string.Empty;
            txtRazonSocial.Value = string.Empty;
            txtTelefono1.Value = string.Empty;
            txtTelefono2.Value = string.Empty;
            txtMail1.Value = string.Empty;
            txtMail2.Value = string.Empty;
            txtDomicilio.Value = string.Empty;
            cboLocalidad.SelectedIndex = -1;
        }

        private void CargarDatosModificacion(Guid id)
        {

            var entity = this.CRPCLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtMatricula.Value = string.IsNullOrEmpty(entity.MatriculaCRPC) ?string.Empty : entity.MatriculaCRPC.ToUpper().Trim();
                txtSiglas.Value = entity.Descripcion.ToUpper().Trim();
                txtRazonSocial.Value = string.IsNullOrEmpty(entity.RazonSocialCRPC) ? string.Empty : entity.RazonSocialCRPC.ToUpper().Trim();
                txtTelefono1.Value = string.IsNullOrEmpty(entity.TelefonoACRPC) ? string.Empty : entity.TelefonoACRPC.ToUpper().Trim();
                txtTelefono2.Value = string.IsNullOrEmpty(entity.TelefonoB_CRPC) ? string.Empty : entity.TelefonoB_CRPC.ToUpper().Trim();
                txtMail1.Value = string.IsNullOrEmpty(entity.MailACRPC) ? string.Empty : entity.MailACRPC.ToUpper().Trim();
                txtMail2.Value = string.IsNullOrEmpty(entity.MailBCRPC) ? string.Empty : entity.MailBCRPC.ToUpper().Trim();
                txtDomicilio.Value = string.IsNullOrEmpty(entity.DomicilioCRPC) ? string.Empty : entity.DomicilioCRPC.ToUpper().Trim();
                cboLocalidad.SelectedValue = !entity.CpaCRPC.HasValue? Guid.Empty.ToString() : entity.CpaCRPC.ToString();
            }
        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var reguladores = this.CRPCLogic.ReadListView();
            tablaDatos.DataSource = reguladores.Where(x => x.ID != Guid.Empty);
            tablaDatos.DataBind();
            this.NuevoRegulador.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            var crpc = new TalleresWeb.Entities.CRPC();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            crpc.ID = id;
            crpc.MatriculaCRPC = txtMatricula.Value.ToUpper().Trim(); //o Descripcion
            crpc.Descripcion = txtSiglas.Value.ToUpper().Trim(); //o Descripcion
            crpc.RazonSocialCRPC = txtRazonSocial.Value.Trim();
            crpc.TelefonoACRPC = txtTelefono1.Value.Trim();
            crpc.TelefonoB_CRPC = txtTelefono2.Value.Trim();
            crpc.MailACRPC = txtMail1.Value.Trim();
            crpc.MailBCRPC = txtMail2.Value.Trim();
            crpc.DomicilioCRPC = txtDomicilio.Value.Trim();
            crpc.CpaCRPC = cboLocalidad.SelectedIndex == -1 ? default(Guid?) : new Guid(cboLocalidad.SelectedValue);

            List<String> mensajes = this.Validar(crpc);

            if (!mensajes.Any())
            {
                if (String.IsNullOrWhiteSpace(hddID.Value))
                {
                    this.CRPCLogic.Add(crpc);
                }
                else
                {
                    this.CRPCLogic.Update(crpc);
                }
                                   
                LimpiarCampos();
                divDatos.Visible = false;
                divBuscar.Visible = true;
                Buscar();
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }            
        }

        private List<String> Validar(TalleresWeb.Entities.CRPC entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese Siglas");
                mostrarListado = false;
            }
            else if (String.IsNullOrWhiteSpace(entity.MatriculaCRPC))
            {
                mensajes.Add("Ingrese Matrícula");
                mostrarListado = false;
            }
            else if (String.IsNullOrWhiteSpace(entity.RazonSocialCRPC))
            {
                mensajes.Add("Ingrese Razón Social");
                mostrarListado = false;
            }            
            else
            {
                mostrarListado = true;
            }

            return mensajes;
        }
        #endregion
    }

}