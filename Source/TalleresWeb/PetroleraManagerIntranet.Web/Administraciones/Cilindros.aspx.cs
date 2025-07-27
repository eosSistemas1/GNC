using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Cilindros : PageBase
    {
        #region Properties
        private CilindrosLogic cilindrosLogic;
        private bool mostrarListado = true;
        public CilindrosLogic CilindrosLogic
        {
            get
            {
                if (this.cilindrosLogic == null) cilindrosLogic = new CilindrosLogic();
                return cilindrosLogic;
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
                this.AccionAlta();
                this.AccionUsuario.InnerText = "NUEVO";
            }
            if (Request.QueryString["a"] == "M")
            {
                this.AccionConsulta(true);
                this.AccionUsuario.InnerText = "MODIFICAR";
                this.NuevoCilindro.Visible = false;
            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoCilindro.Visible = false;
                this.txtDescripcion.Disabled = true;
                this.txtCapacidad.Disabled = true;
                this.txtNuevaMarca.Disabled = true;                
                this.cboMarcaCilindro.Enabled = false;   
                this.txtMatricula.Disabled = true;
                this.txtModelo.Disabled = true;
                this.txtEspesorAdmisible.Disabled = true;
                this.txtDiametro.Disabled = true;
                this.txtLargo.Disabled = true;
                this.txtRotura.Disabled = true;
                this.txtMaterial.Disabled = true;
                this.txtNorma.Disabled = true;
                this.txtFluencia.Disabled = true;
                this.txtDureza.Disabled = true;
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
                this.CilindrosLogic.Delete(id);
            }
            catch (ArgumentNullException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no existe.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
            catch (UpdateException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no se puede eliminar porque tiene trámites relacionados.", UserControls.MessageBoxCtrl.TipoWarning.Error);
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
            txtDescripcion.Focus();
            mostrarListado = false;
            this.NuevoCilindro.Visible = false;
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
            txtDescripcion.Value = string.Empty;
            txtCapacidad.Value = string.Empty;
            cboMarcaCilindro.SelectedIndex = -1;
            txtMatricula.Value = string.Empty;
            txtModelo.Value = string.Empty;
            txtEspesorAdmisible.Value = string.Empty;
            txtDiametro.Value = string.Empty;
            txtLargo.Value = string.Empty;
            txtRotura.Value = string.Empty;
            txtMaterial.Value = string.Empty;
            txtNorma.Value = string.Empty;
            txtDureza.Value = string.Empty;
            txtFluencia.Value = string.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.Cilindros entity = this.CilindrosLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtDescripcion.Value = entity.Descripcion.ToUpper();
                txtCapacidad.Value = entity.CapacidadCil.HasValue ? entity.CapacidadCil.Value.ToString() : string.Empty;

                if (entity.IdMarcaCilindro.HasValue)
                {
                    cboMarcaCilindro.SelectedValue = entity.IdMarcaCilindro.Value.ToString();
                }
                else
                {
                    cboMarcaCilindro.SelectedIndex = -1;
                }

                txtMatricula.Value = entity.MatriculaOCCil;
                txtModelo.Value = entity.ModeloCilindro;
                txtEspesorAdmisible.Value = entity.EspesorAdmisibleCil.HasValue ? entity.EspesorAdmisibleCil.Value.ToString() : string.Empty;
                txtDiametro.Value = entity.DiametroCilindro.HasValue ? entity.DiametroCilindro.Value.ToString() : string.Empty;
                txtLargo.Value = entity.LargoCilindro.HasValue ? entity.LargoCilindro.Value.ToString() : string.Empty;
                txtRotura.Value = entity.RoturaCilindro;
                txtMaterial.Value = entity.MaterialCilindro;
                txtNorma.Value = entity.NormaFabCilindro;
                txtFluencia.Value = entity.Fluencia;
                txtDureza.Value = entity.Dureza;
            }
        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var cilindros = this.CilindrosLogic.ReadListView();
            tablaDatos.DataSource = cilindros;
            tablaDatos.DataBind();
            this.NuevoCilindro.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Cilindros cilindro = new TalleresWeb.Entities.Cilindros();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            cilindro.ID = id;
            cilindro.Descripcion = txtDescripcion.Value.ToUpper();
            cilindro.CapacidadCil = !String.IsNullOrWhiteSpace(txtCapacidad.Value) ? decimal.Parse(txtCapacidad.Value) : default(decimal?);
            cilindro.IdMarcaCilindro = new Guid(cboMarcaCilindro.SelectedValue);
            cilindro.MatriculaOCCil = txtMatricula.Value;
            cilindro.ModeloCilindro = txtModelo.Value;
            cilindro.EspesorAdmisibleCil = !String.IsNullOrWhiteSpace(txtEspesorAdmisible.Value) ? Double.Parse(txtEspesorAdmisible.Value) : default(Double?);
            cilindro.DiametroCilindro = !String.IsNullOrWhiteSpace(txtDiametro.Value) ? Double.Parse(txtDiametro.Value) : default(Double?);
            cilindro.LargoCilindro = !String.IsNullOrWhiteSpace(txtLargo.Value) ? Double.Parse(txtLargo.Value) : default(Double?);
            cilindro.RoturaCilindro = txtRotura.Value;
            cilindro.MaterialCilindro = txtMaterial.Value;
            cilindro.NormaFabCilindro = txtNorma.Value;
            cilindro.Fluencia = txtFluencia.Value;
            cilindro.Dureza = txtDureza.Value;
            cilindro.Activo = true;

            List<String> mensajes = this.Validar(cilindro);

            if (!mensajes.Any())
            {
                this.CilindrosLogic.AddCilindro(cilindro, txtNuevaMarca.Value);
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
        private List<String> Validar(TalleresWeb.Entities.Cilindros entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese código de homologación");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (cboMarcaCilindro.SelectedIndex==0)
            {
                mensajes.Add("Seleccione marca");
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