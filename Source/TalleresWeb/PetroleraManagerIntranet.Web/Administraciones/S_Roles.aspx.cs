using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class S_Roles : PageBase
    {
        #region Properties
        private S_RolesLogic s_rolesLogic;
        private bool mostrarListado = true;
        public S_RolesLogic S_RolesLogic
        {
            get
            {
                if (this.s_rolesLogic == null) s_rolesLogic = new S_RolesLogic();
                return s_rolesLogic;
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
                this.NuevoRol.Visible = false;
  
            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoRol.Visible = false;
                this.HoraInicioSemanaM.Disabled = true;
                this.CodigoRol.Disabled = true;
                this.HoraFinSemanaM.Disabled = true;            
                this.HoraInicioSemanaT.Disabled = true;
                this.HoraFinSemanaT.Disabled = true;
                this.HoraInicioSabado.Disabled = true;
                this.HoraFinSabado.Disabled = true;
                this.DescRol.Disabled = true;

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
                this.S_RolesLogic.Delete(id);
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
            CodigoRol.Focus();
            mostrarListado = false;
            this.NuevoRol.Visible = false;
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
            HoraInicioSemanaM.Value = string.Empty;
            CodigoRol.Value = string.Empty;
            DescRol.Value = string.Empty;
            HoraFinSemanaM.Value = string.Empty;
            HoraInicioSemanaT.Value = string.Empty;
            HoraFinSemanaT.Value = string.Empty;
            HoraInicioSabado.Value = string.Empty;
            HoraInicioSabado.Value = string.Empty;
            HoraFinSabado.Value = string.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {
           TalleresWeb.Entities.S_ROLES entity = this.S_RolesLogic.Read(id);
            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                IdRol.Value = entity.IdRol.ToString().Trim();
                CodigoRol.Value = entity.CodigoRol.Trim(); 
                DescRol.Value = entity.Descripcion.Trim(); 
                HoraInicioSemanaM.Value = entity.HoraInicioSemanaM.ToString();
                HoraFinSemanaM.Value = entity.HoraFinSemanaM.ToString();
                HoraInicioSemanaT.Value = entity.HoraInicioSemanaT.ToString();
                HoraFinSemanaT.Value = entity.HoraFinSemanaT.ToString();
                HoraInicioSabado.Value = entity.HoraInicioSabado.ToString();
                HoraFinSabado.Value = entity.HoraFinSabado.ToString();              
            }           
        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var roles = this.S_RolesLogic.ReadListView();
            tablaDatos.DataSource = roles;
            tablaDatos.DataBind();
            this.NuevoRol.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
                 TalleresWeb.Entities.S_ROLES roles = new TalleresWeb.Entities.S_ROLES();                
                 Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
                 
                 roles.ID = id;
                 roles.Descripcion = DescRol.Value.ToUpper();
                 roles.CodigoRol = CodigoRol.Value;
                 roles.CodigoRol = CodigoRol.Value;
                 roles.IdRol = Int32.Parse(IdRol.Value.Trim());

            Decimal HISM;

            if (Decimal.TryParse(HoraInicioSemanaM.Value, out HISM))
            {
                roles.HoraInicioSemanaM = HISM;
            }
            else
            {
                roles.HoraInicioSemanaM = null;
            }

            Decimal HFSM;

            if (Decimal.TryParse(HoraFinSemanaM.Value, out HFSM))
            {
                roles.HoraFinSemanaM = HFSM;
            }
            else
            {
                roles.HoraFinSemanaM = null;
            }

            Decimal HIST;

            if (Decimal.TryParse(HoraInicioSemanaT.Value, out HIST))
            {
                roles.HoraInicioSemanaT = HIST;
            }
            else
            {
                roles.HoraInicioSemanaT = null;
            }

            Decimal HFST;

            if (Decimal.TryParse(HoraFinSemanaT.Value, out HFST))
            {
                roles.HoraFinSemanaT = HFST;
            }
            else
            {
                roles.HoraFinSemanaT = null;
            }

            Decimal HIS;

            if (Decimal.TryParse(HoraInicioSabado.Value, out HIS))
            {
                roles.HoraInicioSabado = HIS;
            }
            else
            {
                roles.HoraInicioSabado = null;
            }

            Decimal HFS;

            if (Decimal.TryParse(HoraFinSabado.Value, out HFS))
            {
                roles.HoraFinSabado = HFS;
            }
            else
            {
                roles.HoraFinSabado = null;
            }
           
                List<String> mensajes = this.Validar(roles);

                 if (!mensajes.Any())
                 {
                     this.S_RolesLogic.AddRol(roles);
                     LimpiarCampos();
                     divDatos.Visible = false;
                     divBuscar.Visible = true;
                 }
                 else
                 {
                     mostrarListado = false;
                      MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
                 }
                 if (mostrarListado == true)
                     Buscar();
        }
         private List<String> Validar(TalleresWeb.Entities.S_ROLES entity)
         {
                 List<String> mensajes = new List<String>();

           if (String.IsNullOrWhiteSpace(entity.Descripcion))
           {
              mensajes.Add("Ingrese el rol");
              mostrarListado = false;
           }
           else
           {
              mostrarListado = true;
           }       
          
            if (String.IsNullOrWhiteSpace(entity.CodigoRol))
            {
                mensajes.Add("Ingrese el codigo de rol");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraInicioSemanaM.ToString()))
            {
                mensajes.Add("Ingrese el Hora Inicio Semana M");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraFinSemanaM.ToString()))
            {
                mensajes.Add("Ingrese el Hora Fin semana M ");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraInicioSemanaT.ToString()))
            {
                mensajes.Add("Ingrese el Hora Inicio Semana T ");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraFinSemanaT.ToString()))
            {
                mensajes.Add("Ingrese el Hora Fin Semana T ");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraInicioSabado.ToString()))
            {
                mensajes.Add("Ingrese el Hora Inicio Sabado ");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.HoraFinSabado.ToString()))
            {
                mensajes.Add("Ingrese el Hora Fin Sabado ");
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