using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Valvula : PageBase
    {
        #region Properties
        private ValvulasLogic valvulasLogic;
        private bool mostrarListado = true;
        public ValvulasLogic ValvulasLogic
        {
            get
            {
                if (this.valvulasLogic == null) valvulasLogic = new ValvulasLogic();
                return valvulasLogic;
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
                this.NuevaValvula.Visible = false;

            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevaValvula.Visible = false;
                this.CodHomologacionValvula.Disabled = true;
                this.cboMarcasValvulas.Enabled = false;
            }
            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());

                this.ValvulasLogic.Delete(id);
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
            CodHomologacionValvula.Focus();
            mostrarListado = false;
            this.NuevaValvula.Visible = false;
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
            CodHomologacionValvula.Value = string.Empty;
            cboMarcasValvulas.SelectedIndex = -1;

        }

        private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.Valvula entity = this.ValvulasLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                CodHomologacionValvula.Value = entity.Descripcion.ToUpper().Trim();
                
                if (entity.IdMarcaValvula != null)
                {
                    cboMarcasValvulas.SelectedValue = entity.IdMarcaValvula.Value.ToString();
                }
                else
                {
                    cboMarcasValvulas.SelectedIndex = -1;
                }
            }

        }
        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;          
            var valvulas = this.ValvulasLogic.ReadListView();
            tablaDatos.DataSource = valvulas;
            tablaDatos.DataBind();
            this.NuevaValvula.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Valvula valvulas = new TalleresWeb.Entities.Valvula();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            valvulas.ID = id;
            valvulas.Descripcion = CodHomologacionValvula.Value.ToUpper().Trim(); //o Descripcion
            valvulas.IdMarcaValvula = new Guid(cboMarcasValvulas.SelectedValue);

            List<String> mensajes = this.Validar(valvulas);

            if (!mensajes.Any())
            {
                this.ValvulasLogic.AddValvula(valvulas);

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

        private List<String> Validar(TalleresWeb.Entities.Valvula entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese Código de homologación");
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