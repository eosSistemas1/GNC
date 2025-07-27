using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;
using System.Text.RegularExpressions;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Usuarios : PageBase
    {
        #region Properties
        private UsuariosLogic usuariosLogic;
        private bool mostrarListado = true;
        public UsuariosLogic UsuariosLogic
        {
            get
            {
                if (this.usuariosLogic == null) usuariosLogic = new UsuariosLogic();
                return usuariosLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Accion();
                if (mostrarListado == true)
                    this.Buscar();
            }

        }

        private void Accion()
        {

            if (Request.QueryString["a"] == null || Request.QueryString["a"] == "B") this.divBuscar.Visible = true;
            if (Request.QueryString["a"] == "A")
            {
                this.AccionUsuario.InnerText = "NUEVO";
                this.AccionAlta();
            }
            if (Request.QueryString["a"] == "M")
            {
                this.AccionConsulta(true);
                this.AccionUsuario.InnerText = "MODIFICAR";
                this.NuevoUsuario.Visible = false;

            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoUsuario.Visible = false;
                this.Usuario.Disabled = true;
                this.ConfirmacionContrasenia.Attributes.Add("class", "consulta");
                this.Contrasenia.Disabled = true;
                this.Contrasenia2.Attributes.Add("class", "consulta");
                this.NombreYApellidoUsuario.Disabled = true;
                this.NroDocumento.Disabled = true;
                this.EmailUsuario.Disabled = true;
                this.IdTipoDoc.Enabled = false;
                this.IdTipoDoc.Attributes.Add("class", "form-control");
                this.IdTaller.Enabled = false;
                this.IdTaller.Attributes.Add("class", "form-control");
                this.IdRol.Enabled = false;
            }
            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());

                this.UsuariosLogic.Delete(id);
            }
            catch (ArgumentNullException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no existe.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
            catch (Exception e)
            {
                List<String> mensaje = new List<String>();
                mensaje.Add("No se pudo eliminar el item seleccionado.");
                mensaje.Add(e.InnerException.Message);
                MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }

        }

        private void AccionAlta()
        {
            this.divBuscar.Visible = false;
            this.divDatos.Disabled = false;
            this.LimpiarCampos();
            Usuario.Focus();
            mostrarListado = false;
            this.NuevoUsuario.Visible = false;
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
            Usuario.Value = string.Empty;
            Contrasenia.Value = string.Empty;
            NombreYApellidoUsuario.Value = string.Empty;
            NroDocumento.Value = string.Empty;
            EmailUsuario.Value = string.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {

            Usuario entity = this.UsuariosLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                NombreYApellidoUsuario.Value = entity.NombreYApellidoUsuario.ToUpper().Trim();

                if (entity.Descripcion != null)
                    Usuario.Value = entity.Descripcion.ToUpper().Trim();
                else
                    Usuario.Value = "";

                if (entity.Contrasenia != null)
                    Contrasenia.Value = entity.Contrasenia.Trim();
                else
                    Contrasenia.Value = null;

                if (entity.Contrasenia != null)
                    Contrasenia2.Value = entity.Contrasenia.Trim();
                else
                    Contrasenia2.Value = null;

                if (entity.NroDocumento != null)
                    NroDocumento.Value = entity.NroDocumento.ToUpper().Trim();
                else
                    NroDocumento.Value = null;

                if (entity.EmailUsuario != null)
                    EmailUsuario.Value = entity.EmailUsuario.ToUpper().Trim();
                else
                    EmailUsuario.Value = null;

                if (entity.IdTipoDoc != null)
                {
                    IdTipoDoc.SelectedValue = entity.IdTipoDoc.ToString();
                }
                else
                {
                    IdTipoDoc.SelectedIndex = -1;
                }

                if (entity.IdTaller != null)
                {
                    IdTaller.SelectedValue = entity.IdTaller.ToString();
                }
                else
                {
                    IdTaller.SelectedIndex = -1;
                }
                if (entity.IdRol != null)
                {
                    IdRol.SelectedValue = entity.IdRol.ToString();
                }
                else
                {
                    IdRol.SelectedIndex = -1;
                }
            }

        }

        private void Buscar()
        {

            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var usuarios = this.UsuariosLogic.ReadListView();

            tablaDatos.DataSource = usuarios;
            tablaDatos.DataBind();
            this.NuevoUsuario.Visible = true;

        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {

            TalleresWeb.Entities.Usuario usuarios = new TalleresWeb.Entities.Usuario();

            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            usuarios.ID = id;
            usuarios.Descripcion = Usuario.Value.ToUpper().Trim();

            usuarios.IdTipoDoc = new Guid(IdTipoDoc.SelectedValue);
            usuarios.IdTaller = new Guid(IdTaller.SelectedValue);

            int value;
            if (int.TryParse(IdRol.SelectedValue, out value))
                usuarios.IdRol = value;


            if (Contrasenia.Value == "")
                usuarios.Contrasenia = "";
            else
                usuarios.Contrasenia = Contrasenia.Value.Trim();


            if (NombreYApellidoUsuario.Value == "")
                usuarios.NombreYApellidoUsuario = null;
            else
                usuarios.NombreYApellidoUsuario = NombreYApellidoUsuario.Value.Trim();

            if (EmailUsuario.Value == "")
                usuarios.EmailUsuario = null;
            else
                usuarios.EmailUsuario = EmailUsuario.Value.Trim();

            if (NroDocumento.Value == "")
                usuarios.NroDocumento = null;
            else
                usuarios.NroDocumento = NroDocumento.Value.Trim();

            var activo = Boolean.Parse(Activo.Value);
            usuarios.Activo = activo;

            List<String> mensajes = this.Validar(usuarios);

            if (!mensajes.Any())
            {
                this.UsuariosLogic.AddUsuario(usuarios);

                LimpiarCampos();
                divDatos.Visible = false;
                divBuscar.Visible = true;
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            if (mostrarListado == true)
                Buscar();
        }

        private List<String> Validar(Usuario entity)
        {
            List<String> mensajes = new List<String>();
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (String.IsNullOrWhiteSpace(entity.NombreYApellidoUsuario))
                mensajes.Add("Ingrese nombre y apellido");
            if (String.IsNullOrWhiteSpace(entity.Descripcion))
                mensajes.Add("Ingrese Usuario");
            if (String.IsNullOrWhiteSpace(entity.EmailUsuario))
                mensajes.Add("Ingrese Email");
            if (Contrasenia.Value != Contrasenia2.Value)
                mensajes.Add("Las contraseñas deben coincidir");

            if (String.IsNullOrWhiteSpace(entity.Contrasenia))
            {
                mensajes.Add("Ingrese Contraseña");
            }
            else
            {
                if (!hasLowerChar.IsMatch(Contrasenia.Value))
                {
                    mensajes.Add("La contraseña debe tener al menos una minúscula");
                }
                if (!hasUpperChar.IsMatch(Contrasenia.Value))
                {
                    mensajes.Add("La contraseña debe tener al menos una mayúscula");
                }
                if (!hasMiniMaxChars.IsMatch(Contrasenia.Value))
                {
                    mensajes.Add("La contraseña debe tener mínimo 8 caracteres, y como máximo 15");
                }
                if (!hasNumber.IsMatch(Contrasenia.Value))
                {
                    mensajes.Add("La contraseña debe tener al menos un número");
                }
                if (!hasSymbols.IsMatch(Contrasenia.Value))
                {
                    mensajes.Add("La contraseña debe tener al menos un caracter especial. Ejemplo . (un punto)");
                }
            }

            mostrarListado = !mensajes.Any();

            return mensajes;
        }
        #endregion
    }
}