using Common.Web.UserControls;
using System;
using System.Web.UI;
using TalleresWeb.Logic;

namespace TalleresWeb.Web.MiCuenta
{
    public partial class Usuario : PageBase
    {
        #region Members

        private TalleresLogic tallerLogic = new TalleresLogic();
        private UsuariosLogic usrLogic = new UsuariosLogic();

        #endregion

        #region Methods

        protected void btnAceptarTaller_Click(object sender, EventArgs e)
        {
            String valido = String.Empty;

            if (txtMatricula.Text.Equals(String.Empty)) valido += "- Ingrese matricula del taller. <br/>";
            if (txtRazonSocial.Text.Equals(String.Empty)) valido += "- Ingrese razón social del taller. <br/>";
            if (txtDomicilio.Text.Equals(String.Empty)) valido += "- Ingrese domicilio del taller. <br/>";
            if (cboLocalidad.SelectedValue.Equals(Guid.Empty)) valido += "- Ingrese localidad del taller. <br/>";

            if (valido.Equals(String.Empty))
            {
                var taller = tallerLogic.Read(new Guid(hddTallerID.Value.ToString()));

                taller.Descripcion = txtMatricula.Text;
                taller.RazonSocialTaller = txtRazonSocial.Text;
                taller.DomicilioTaller = txtDomicilio.Text;
                taller.IdCiudad = cboLocalidad.SelectedValue;
                taller.CuitTaller = txtCuit.Text;
                taller.TelefonoTaller = txtTelefono.Text;
                taller.FaxTaller = txtFax.Text;
                taller.MailTaller = txtEmailTaller.Text;
                taller.ContactoTaller = txtContacto.Text;
                taller.FechaVencContrato = DateTime.Parse(txtVenceContrato.Text);
                taller.Zona = cboZona.SelectedValueString;
                taller.HorarioDeAtencion = txtHorarioAtencion.Text;

                tallerLogic.Update(taller);

                MessageBoxCtrl1.MessageBox(null, "El taller se actualizó correctamente", MessageBoxCtrl.TipoWarning.Success);

                CargarUsuario();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, valido, MessageBoxCtrl.TipoWarning.Success);
            }
        }

        protected void btnAceptarUsuario_Click(object sender, EventArgs e)
        {
            String valido = String.Empty;

            if (txtNombreApellido.Text.Equals(String.Empty)) valido += "- Ingrese nombre y apellido del usuario. <br/>";
            if (cboTipoDoc.SelectedValue.Equals(Guid.Empty)) valido += "- Ingrese tipo de documento. <br/>";
            if (txtNroDocumento.Text.Equals(String.Empty)) valido += "- Ingrese nro. de documento. <br/>";

            if (valido.Equals(String.Empty))
            {
                var usuario = usrLogic.Read(new Guid(hddUsuarioID.Value.ToString()));

                usuario.NombreYApellidoUsuario = txtNombreApellido.Text;
                usuario.IdTipoDoc = cboTipoDoc.SelectedValue;
                usuario.NroDocumento = txtNroDocumento.Text;
                usuario.EmailUsuario = txtEmail.Text;

                usrLogic.Update(usuario);

                MessageBoxCtrl1.MessageBox(null, "El usuario se actualizó correctamente", MessageBoxCtrl.TipoWarning.Success);

                CargarUsuario();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, valido, MessageBoxCtrl.TipoWarning.Success);
            }
        }

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            var usuario = usrLogic.Read(new Guid(hddUsuarioID.Value.ToString()));
            String valido = String.Empty;

            if (txtPwActual.Text.Equals(String.Empty)) valido += "- Ingrese contaseña actual. <br/>";
            if (txtPwNueva.Text.Equals(String.Empty)) valido += "- Ingrese contaseña nueva. <br/>";
            if (txtPwConfirma.Text.Equals(String.Empty)) valido += "- Ingrese confirmación de contaseña. <br/>";
            if (Genericos.EncriptaContrasenia(txtPwActual.Text) != usuario.Contrasenia) valido += "- La contaseña actual no coincide con la ingresada. <br/>";
            if (txtPwNueva.Text != txtPwConfirma.Text) valido += "- La contaseña nueva no coincide con la confirmación. <br/>";

            if (valido.Equals(String.Empty))
            {
                usuario.Contrasenia = Genericos.EncriptaContrasenia(txtPwNueva.Text);
                usrLogic.Update(usuario);

                MessageBoxCtrl1.MessageBox(null, "La contraseña se cambió correctamente", MessageBoxCtrl.TipoWarning.Success);

                CargarUsuario();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, valido, MessageBoxCtrl.TipoWarning.Success);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hddUsuarioID.Value = MasterTalleres.IdUsuarioLogueado.ToString();
                tab.ActiveTabIndex = 0;

                CargarUsuario();
                CargarTaller();
            }
        }

        private void CargarTaller()
        {
            var taller = tallerLogic.Read(new Guid(hddTallerID.Value.ToString()));

            txtMatricula.Text = taller.Descripcion.Trim();
            txtRazonSocial.Text = taller.RazonSocialTaller.Trim();
            txtDomicilio.Text = taller.DomicilioTaller;
            cboLocalidad.SelectedValue = taller.IdCiudad.Value;
            txtCuit.Text = taller.CuitTaller;
            txtTelefono.Text = taller.TelefonoTaller;
            txtFax.Text = taller.FaxTaller;
            txtEmailTaller.Text = taller.MailTaller;
            txtContacto.Text = taller.ContactoTaller;
            txtVenceContrato.Text = taller.FechaVencContrato.Value.ToString("dd/MM/yyyy");
            cboZona.SelectedValueString = taller.Zona.ToUpper();
            txtHorarioAtencion.Text = taller.HorarioDeAtencion;
        }

        private void CargarUsuario()
        {
            var usuario = usrLogic.Read(new Guid(hddUsuarioID.Value.ToString()));

            txtUsuario.Text = usuario.Descripcion;
            txtNombreApellido.Text = usuario.NombreYApellidoUsuario;
            cboTipoDoc.SelectedValue = usuario.IdTipoDoc.Value;
            txtNroDocumento.Text = usuario.NroDocumento;
            txtEmail.Text = usuario.EmailUsuario;

            txtPwActual.Text = String.Empty;
            txtPwNueva.Text = String.Empty;
            txtPwConfirma.Text = String.Empty;

            hddTallerID.Value = usuario.IdTaller.Value.ToString();
        }

        #endregion
    }
}