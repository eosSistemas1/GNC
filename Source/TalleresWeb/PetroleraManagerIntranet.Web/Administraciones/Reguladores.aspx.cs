using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Reguladores : PageBase
    {
        #region Properties
        private ReguladoresLogic reguladoresLogic;
        private bool mostrarListado = true;
        public ReguladoresLogic ReguladoresLogic
        {
            get
            {
                if (this.reguladoresLogic == null) reguladoresLogic = new ReguladoresLogic();
                return reguladoresLogic;
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
                this.NuevoRegulador.Visible = false;

            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoRegulador.Visible = false;
                this.CodigoHomologacionRegulador.Disabled = true;
                this.Modelo.Disabled = true;
                this.MatriculaOC.Disabled = true;
                this.txtCaudal.Disabled = true;
                this.Tipo.Disabled = true;
                this.txtEtrapas.Disabled = true;
                this.cboMarcasRegulador.Enabled = false;
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
                this.ReguladoresLogic.Delete(id);
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
            CodigoHomologacionRegulador.Focus();
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
            CodigoHomologacionRegulador.Value = string.Empty;
            Modelo.Value = string.Empty;
            MatriculaOC.Value = string.Empty;
            cboMarcasRegulador.SelectedIndex = -1;
            txtCaudal.Value = string.Empty;
            Tipo.Value = string.Empty;
            txtEtrapas.Value = string.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {

            TalleresWeb.Entities.Reguladores entity = this.ReguladoresLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                CodigoHomologacionRegulador.Value = entity.Descripcion.ToUpper().Trim();

                if (entity.Modelo != null)
                    Modelo.Value = entity.Modelo.Trim().ToUpper();
                else
                    Modelo.Value = "";

                if (entity.MatriculaOC != null)
                    MatriculaOC.Value = entity.MatriculaOC.Trim().ToUpper();
                else
                    MatriculaOC.Value = "";

                if (entity.Caudal != null)
                    txtCaudal.Value = entity.Caudal.ToString();
                else
                    txtCaudal.Value = "";

                if (entity.Tipo != null)
                    Tipo.Value = entity.Tipo.Trim().ToUpper();
                else
                    Tipo.Value = "";

                if (entity.Etrapas != null)
                    txtEtrapas.Value = entity.Etrapas.ToString();
                else
                    txtEtrapas.Value = "";

                if (entity.IdMarcaRegulador != null)
                {
                    cboMarcasRegulador.SelectedValue = entity.IdMarcaRegulador.Value.ToString();
                }
                else
                {
                    cboMarcasRegulador.SelectedIndex = -1;
                }
            }
        }
        
        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var reguladores = this.ReguladoresLogic.ReadListView();
            tablaDatos.DataSource = reguladores;
            tablaDatos.DataBind();
            this.NuevoRegulador.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Reguladores reguladores = new TalleresWeb.Entities.Reguladores();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            reguladores.ID = id;
            reguladores.Descripcion = CodigoHomologacionRegulador.Value.ToUpper().Trim(); //o Descripcion
            reguladores.IdMarcaRegulador = new Guid(cboMarcasRegulador.SelectedValue);
            reguladores.Modelo = Modelo.Value.Trim();
            reguladores.MatriculaOC = MatriculaOC.Value.Trim();

            if (String.IsNullOrWhiteSpace(txtCaudal.Value))
            {               
                reguladores.Caudal = null;
            }
            else
            {
                var cau = Double.Parse(txtCaudal.Value);
                reguladores.Caudal = cau;
            }

            reguladores.Tipo = Tipo.Value.Trim();

            if (String.IsNullOrWhiteSpace(txtEtrapas.Value))
            {
                reguladores.Etrapas = null;
            }
            else
            {
                var cau = Int32.Parse(txtEtrapas.Value);
                reguladores.Etrapas = cau;
            }

            List<String> mensajes = this.Validar(reguladores);

            if (!mensajes.Any())
            {
                this.ReguladoresLogic.AddRegulador(reguladores);

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

        private List<String> Validar(TalleresWeb.Entities.Reguladores entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese Codigo de homologación");
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