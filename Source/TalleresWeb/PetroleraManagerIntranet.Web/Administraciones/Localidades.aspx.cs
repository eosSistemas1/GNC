using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Localidades : PageBase
    {
        #region Properties
        private LocalidadesLogic localidadesLogic;
        private bool mostrarListado = true;
        public LocalidadesLogic LocalidadesLogic
        {
            get
            {
                if (this.localidadesLogic == null) localidadesLogic = new LocalidadesLogic();
                return localidadesLogic;
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
                {
                    this.Buscar();
                }
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
                this.NuevaLocalidad.Visible = false;
              
            }
            if (Request.QueryString["a"] == "C")
                {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevaLocalidad.Visible = false;
                this.txtLocalidad.Disabled = true;
                this.txtCodigo.Disabled = true;
                this.cboProvincia.Enabled = false;


            }
            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());
                this.LocalidadesLogic.Delete(id);
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
            this.txtLocalidad.Value.Trim();
            this.LimpiarCampos();
            txtLocalidad.Focus();
            mostrarListado = false;
            this.NuevaLocalidad.Visible = false;
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
            txtLocalidad.Value = string.Empty;     
            txtCodigo.Value = string.Empty;
            txtCodigo.Value.Trim();
        }

       private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.Localidades entity = this.LocalidadesLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtLocalidad.Value = entity.Descripcion.ToUpper().Trim();
                txtCodigo.Value = entity.CodigoPostal.ToUpper().Trim();
                
                if (entity.IdProvincia != null)
                 {
                    cboProvincia.SelectedValue = entity.IdProvincia.ToString();
                }
                else
                {
                    cboProvincia.SelectedIndex = -1;
                }


            }

        }
        
        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;

            var localidades = this.LocalidadesLogic.ReadListView();
            
            tablaDatos.DataSource = localidades;
            tablaDatos.DataBind();
            this.NuevaLocalidad.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Localidades localidad = new TalleresWeb.Entities.Localidades();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            localidad.ID = id;
            localidad.Descripcion = txtLocalidad.Value.ToUpper().Trim();           
            localidad.IdProvincia = new Guid(cboProvincia.SelectedValue);
            localidad.CodigoPostal = txtCodigo.Value.Trim();

            List<String> mensajes = this.Validar(localidad);

            if (!mensajes.Any())
            {
                this.LocalidadesLogic.AddLocalidad(localidad);

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

        private List<String> Validar(TalleresWeb.Entities.Localidades entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion) || String.IsNullOrWhiteSpace(entity.CodigoPostal))
            {
                mensajes.Add("Por favor ingrese todos los datos");
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