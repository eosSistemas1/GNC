using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class RT : PageBase
    {
        #region Properties
        private RTLogic rtLogic;        
        private bool mostrarListado = true;
        public RTLogic RTLogic
        {
            get
            {
                if (this.rtLogic == null) rtLogic = new RTLogic();
                return rtLogic; 
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Accion();
                if(mostrarListado == true) this.Buscar();
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
                this.NuevoRT.Visible = false;

            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoRT.Visible = false;
                this.NombreApellidoRT.Disabled = true;
                this.NroDniRT.Disabled = true;
                this.MatriculaRT.Disabled = true;
                this.TituloRT.Disabled = true;
                this.cboTiposDocumentos.Enabled = false;
            }
            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());
                this.RTLogic.Delete(id);
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
            NombreApellidoRT.Focus();
            mostrarListado = false;
            this.NuevoRT.Visible = false;
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
            NombreApellidoRT.Value = string.Empty;
            NroDniRT.Value = string.Empty;
            MatriculaRT.Value = string.Empty;
            TituloRT.Value = string.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {

            TalleresWeb.Entities.RT entity = this.RTLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                NombreApellidoRT.Value = entity.Descripcion.ToUpper().Trim();
                NroDniRT.Value = entity.NroDniRT.ToUpper().Trim();
                MatriculaRT.Value = entity.MatriculaRT.ToUpper().Trim();
                TituloRT.Value = entity.TituloRT.ToUpper().Trim();
                if (entity.TipoDniID != null)
                {
                    cboTiposDocumentos.SelectedValue = entity.TipoDniID.ToString();
                }
                else
                {
                    cboTiposDocumentos.SelectedIndex = -1;
                }


            }

        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var rt = this.RTLogic.ReadListView();
            tablaDatos.DataSource = rt;
            tablaDatos.DataBind();
            this.NuevoRT.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.RT rt = new TalleresWeb.Entities.RT();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            rt.ID = id;
            rt.NombreApellidoRT = NombreApellidoRT.Value.ToUpper().Trim();
            rt.TipoDniID = new Guid(cboTiposDocumentos.SelectedValue);
            rt.NroDniRT = NroDniRT.Value.Trim();
            
            rt.MatriculaRT = MatriculaRT.Value.Trim();
            rt.TituloRT = TituloRT.Value.Trim();
            rt.ActivoRT = true;

            List<String> mensajes = this.Validar(rt);

            if (!mensajes.Any())
            {
                this.RTLogic.AddRT(rt);
                LimpiarCampos();
                divDatos.Visible = false;
                divBuscar.Visible = true;
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            if(mostrarListado == true)
            Buscar();
        }

        private List<String> Validar(TalleresWeb.Entities.RT entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.MatriculaRT))
            {
                mensajes.Add("Ingrese matrícula");
                mostrarListado = false;
            }
            else if (String.IsNullOrWhiteSpace(entity.NombreApellidoRT))
            {
                mensajes.Add("Ingrese nombre y apellido");
                mostrarListado = false;
            }
            else if (String.IsNullOrWhiteSpace(entity.NroDniRT) || entity.TipoDniID == Guid.Empty)
            {
                mensajes.Add("Ingrese tipo y nro. documento");
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