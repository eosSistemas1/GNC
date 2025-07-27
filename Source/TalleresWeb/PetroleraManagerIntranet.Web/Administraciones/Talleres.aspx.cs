using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Talleres : PageBase
    {
        #region Properties       
        private TalleresLogic talleresLogic;
        public TalleresLogic TalleresLogic
        {
            get
            {
                if (this.talleresLogic == null) this.talleresLogic = new TalleresLogic();
                return this.talleresLogic;
            }
            set { this.talleresLogic = value; }
        }

        private TalleresRTLogic talleresRTLogic;
        public TalleresRTLogic TalleresRTLogic
        {
            get
            {
                if (this.talleresRTLogic == null) this.talleresRTLogic = new TalleresRTLogic();
                return this.talleresRTLogic;
            }
            set { this.talleresRTLogic = value; }
        }

        private List<TalleresRT> TalleresRTTallerNuevo()
        {
            return ViewState["TALLERESRTTALLERNUEVO"] != null ? (List<TalleresRT>)ViewState["TALLERESRTTALLERNUEVO"] : new List<TalleresRT>();
        }
        private List<TalleresRT> TalleresRTTallerNuevo(TalleresRT tallerRT)
        {
            List<TalleresRT> lista = ViewState["TALLERESRTTALLERNUEVO"] != null ? (List<TalleresRT>)ViewState["TALLERESRTTALLERNUEVO"] : new List<TalleresRT>();

            lista.Add(tallerRT);
            ViewState["TALLERESRTTALLERNUEVO"] = lista;

            return lista;
        }

        private Guid? TallerID
        {
            get
            {
                try
                {
                    return Guid.Parse(ViewState["TALLERID"].ToString());
                }
                catch
                {
                    return default(Guid?);
                }                                   
            }
            set
            {
                ViewState["TALLERID"] = value.ToString();
            }
        }

        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TallerID = Request.QueryString["id"] != null ? new Guid(Request.QueryString["id"].ToString()) : default(Guid?);

            if (!IsPostBack)
            {
                this.Accion();
            }
        }

        private void Accion()
        {
            
            this.divBuscar.Visible = false;
            if (Request.QueryString["a"] == null || Request.QueryString["a"] == "B")
            {
                this.Buscar();
                this.divBuscar.Visible = true;
            }
            if (Request.QueryString["a"] == "A")
            {
                this.AccionAlta();
                this.AccionUsuario.InnerText = "NUEVO";
            }
            if (Request.QueryString["a"] == "M")
            {
                this.AccionConsulta(true);
                this.AccionUsuario.InnerText = "MODIFICAR";
                this.NuevoTaller.Visible = false;
            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                DeshabilitarCampos();
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
                this.TalleresLogic.Delete(id);
                Buscar();
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
            this.InicializarCampos();
            txtMatricula.Focus();
            this.NuevoTaller.Visible = false;
        }

        private void AccionConsulta(Boolean modifica)
        {
            if (Request.QueryString["id"] != null)
            {
                this.divBuscar.Visible = false;
                this.divDatos.Disabled = !modifica;
                this.btnAceptar.Visible = modifica;
               
                this.CargarDatos(TallerID.Value);
            }
        }

        private void InicializarCampos()
        {
            this.TallerID = default(Guid?);
            this.txtMatricula.Value = String.Empty;
            this.txtRazonSocial.Value = String.Empty;
            this.txtDomicilio.Value = String.Empty;
            this.txtCuit.Value = String.Empty;
            this.cboLocalidad.SelectedIndex = 0;
            this.cboZona.SelectedIndex = 0;
            this.txtHorarioAtencion.Value = String.Empty;
            this.txtTelefono.Value = String.Empty;
            this.txtFax.Value = String.Empty;
            this.txtMail.Value = String.Empty;
            this.txtContacto.Value = String.Empty;
            this.calFechaVencimientoContrato.Value = String.Empty;
            this.txtUltimoNroIntOperacion.Value = String.Empty;
            this.InicializarTallerRT();
        }
        private void DeshabilitarCampos()
        {
            this.NuevoTaller.Visible = false;
            this.txtMatricula.Disabled = true;
            this.txtRazonSocial.Disabled = true;
            this.cboLocalidad.Enabled = false;
            this.cboZona.Enabled = false;
            this.txtDomicilio.Disabled = true;
            this.txtCuit.Disabled = true;
            this.txtHorarioAtencion.Disabled = true;
            this.txtTelefono.Disabled = true;
            this.txtFax.Disabled = true;
            this.txtMail.Disabled = true;
            this.txtContacto.Disabled = true;
            this.calFechaVencimientoContrato.Disabled = true;
            this.txtUltimoNroIntOperacion.Disabled = true;
            this.calFechaHRT.Disabled = true;
            this.calFechaDRT.Disabled = true;
            this.esPrincipal.Enabled = false;
            this.cboRT.Enabled = false;
            this.btnGuardarRT.Visible = false;
            this.grdRT.Columns[3].Visible = false;
        }

        private void CargarDatos(Guid id)
        {
            TalleresWeb.Entities.Talleres item = this.TalleresLogic.Read(id);

            this.txtMatricula.Value = item.Descripcion;
            this.txtRazonSocial.Value = item.RazonSocialTaller;
            this.txtDomicilio.Value = item.DomicilioTaller;
            this.txtCuit.Value = item.CuitTaller;

            if (item.IdCiudad.HasValue)
            {
                this.cboLocalidad.SelectedValue = item.IdCiudad.Value.ToString();
            }
            else
            {
                this.cboLocalidad.SelectedIndex = -1;
            }


            if (!String.IsNullOrEmpty(item.Zona))
            {
                this.cboZona.SelectedValue = item.Zona;
            }
            else
            {
                this.cboZona.SelectedIndex = -1;
            }

            this.txtHorarioAtencion.Value = item.HorarioDeAtencion ?? string.Empty;
            this.txtTelefono.Value = item.TelefonoTaller ?? string.Empty;
            this.txtFax.Value = item.FaxTaller ?? string.Empty;
            this.txtMail.Value = item.MailTaller ?? string.Empty;
            this.txtContacto.Value = item.ContactoTaller ?? string.Empty;
            this.calFechaVencimientoContrato.Value = item.FechaVencContrato.HasValue ? item.FechaVencContrato.Value.ToString("yyyy-MM-dd") : String.Empty;
            this.txtUltimoNroIntOperacion.Value = item.UltimoNroIntOperacion.ToString().Trim();

            this.TallerID = item.ID;
            this.CargarTalleresRT();
        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            var datos = this.TalleresLogic.ReadListView();
            tablaDatos.DataSource = datos;
            tablaDatos.DataBind();
            this.NuevoTaller.Visible = true;
            this.TallerID = default(Guid?);
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Talleres item = new TalleresWeb.Entities.Talleres
            {
                ID = TallerID ?? Guid.NewGuid(),
                Descripcion = this.txtMatricula.Value,
                RazonSocialTaller = this.txtRazonSocial.Value,
                DomicilioTaller = this.txtDomicilio.Value,
                CuitTaller = this.txtCuit.Value,
                IdCiudad = this.cboLocalidad.SelectedIndex != -1 ? new Guid(this.cboLocalidad.SelectedValue) : default(Guid?),
                Zona = this.cboZona.SelectedItem.Text,
                HorarioDeAtencion = this.txtHorarioAtencion.Value,
                TelefonoTaller = this.txtTelefono.Value,
                FaxTaller = this.txtFax.Value,
                MailTaller = this.txtMail.Value,
                ContactoTaller = this.txtContacto.Value,
                FechaVencContrato = Funciones.ObtenerValorFecha(this.calFechaVencimientoContrato.Value),
                UltimoNroIntOperacion = !String.IsNullOrEmpty(this.txtUltimoNroIntOperacion.Value) ? int.Parse(this.txtUltimoNroIntOperacion.Value) : 0,
                ActivoTaller = true
            };

            var errores = this.ValidarTaller(item);

            if (!errores.Any())
            {
                var esNuevo = this.TalleresLogic.Read(item.ID) == null;

                // si txt id es vacio, creo uno nuevo , si tiene id lo modifico
                if (esNuevo)
                {
                    this.TalleresLogic.Add(item);

                    foreach (var tallerRT in this.TalleresRTTallerNuevo())
                    {
                        tallerRT.TalleresID = item.ID;
                        this.TalleresRTLogic.Add(tallerRT);
                    }
                }
                else
                {
                    this.TalleresLogic.Update(item);
                }

                this.InicializarCampos();

                Buscar();
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, errores, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }

        }

        private List<String> ValidarTaller(TalleresWeb.Entities.Talleres entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(this.txtMatricula.Value)) mensajes.Add("- Ingrese matrícula");

            if (String.IsNullOrWhiteSpace(this.txtRazonSocial.Value)) mensajes.Add("- Ingrese razón social");

            if (String.IsNullOrWhiteSpace(this.txtDomicilio.Value)) mensajes.Add("- Ingrese domicilio");

            if (String.IsNullOrWhiteSpace(this.txtTelefono.Value)) mensajes.Add("- Ingrese teléfono");

            if (!String.IsNullOrWhiteSpace(this.txtMail.Value) && !Funciones.EmailValido(this.txtMail.Value)) mensajes.Add("- Ingrese email válido");

            if (String.IsNullOrWhiteSpace(this.txtCuit.Value))
            {
                mensajes.Add("- Ingrese CUIT");
            }
            else if (!String.IsNullOrWhiteSpace(this.txtCuit.Value) && !Funciones.ValidarCuit(this.txtCuit.Value))
            {
                mensajes.Add("- Ingrese CUIT válido");
            }

            if (this.cboZona.SelectedIndex == -1) mensajes.Add("- Seleccione una zona");


            return mensajes;
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Buscar();
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                var item = this.TalleresLogic.Read(idItem);
                item.ActivoTaller = false;
                this.TalleresLogic.Update(item);
                this.InicializarCampos();

                Buscar();
            }

            else if (e.CommandName == "modificar")
            {
                CargarDatos(idItem);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            //pnlDatos.Visible = true;
            //TabPanel2.Visible = false;
        }

        #endregion

        #region TalleresRT
        protected void grdRT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "modificar")
            {
                var tallerRT = this.TalleresRTLogic.Read(idItem);

                this.TallerID = tallerRT.TalleresID;
                txtIDTalleresRT.Value = tallerRT.ID.ToString();
                cboRT.SelectedValue = tallerRT.RTID.ToString();
                calFechaDRT.Value = tallerRT.FechaDesdeRTT.ToString("dd/MM/yyyy");
                calFechaHRT.Value = tallerRT.FechaHastaRTT.HasValue ? tallerRT.FechaHastaRTT.Value.ToString("dd/MM/yyyy") : String.Empty;
                esPrincipal.Checked = tallerRT.EsRTPrincipal;
            }
        }

        protected void btnGuardarRT_Click(object sender, EventArgs e)
        {
            List<string> erroresRT = this.ValidarRT();

            try
            {
                if (!erroresRT.Any())
                {
                    TalleresRT itemTallerRT = new TalleresRT();
                    itemTallerRT.TalleresID = this.TallerID.Value;
                    itemTallerRT.ID = !String.IsNullOrWhiteSpace(txtIDTalleresRT.Value) ? new Guid(txtIDTalleresRT.Value) : Guid.NewGuid();
                    itemTallerRT.RTID = new Guid(cboRT.SelectedValue);
                    itemTallerRT.FechaDesdeRTT = DateTime.Parse(calFechaDRT.Value);
                    itemTallerRT.FechaHastaRTT = !String.IsNullOrWhiteSpace(calFechaHRT.Value) ? DateTime.Parse(calFechaHRT.Value) : default(DateTime?);
                    itemTallerRT.EsRTPrincipal = esPrincipal.Checked;

                    if (Request.QueryString["a"] == "M")
                    {
                        Guid id = !String.IsNullOrWhiteSpace(txtIDTalleresRT.Value) ? new Guid(txtIDTalleresRT.Value) : Guid.Empty;
                        var tallerRT = this.TalleresRTLogic.Read(id);

                        if (id == Guid.Empty || tallerRT == null)
                        {
                            this.TalleresRTLogic.Add(itemTallerRT);
                        }
                        else
                        {
                            this.TalleresRTLogic.Update(itemTallerRT);
                        }



                        this.CargarTalleresRT();
                    }

                    if (Request.QueryString["a"] == "A")
                    {
                        TalleresRTTallerNuevo(itemTallerRT);
                        grdRT.DataSource = TalleresRTTallerNuevo();
                        grdRT.DataBind();
                    }

                    this.InicializarTallerRT();
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, erroresRT, UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (FormatException fx)
            {
                MessageBoxCtrl.MessageBox(null, "La fecha desde o fecha hasta del RT no son válidas.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            
        }

        private List<String> ValidarRT()
        {
            List<String> mensaje = new List<String>();

            if (cboRT.SelectedIndex == -1 || cboRT.SelectedValue == Guid.Empty.ToString()) mensaje.Add("- Seleccione un responsable técnico");
            if (String.IsNullOrWhiteSpace(calFechaDRT.Value)) mensaje.Add("- Seleccione una fecha desde");

            return mensaje;
        }

        private void InicializarTallerRT()
        {
            this.txtIDTalleresRT.Value = String.Empty;
            this.cboRT.SelectedIndex = 0;
            this.calFechaDRT.Value = DateTime.Now.ToString("dd/MM/yyyy");
            this.calFechaHRT.Value = String.Empty;
        }

        private void CargarTalleresRT()
        {
            var talleresRT = this.TalleresRTLogic.ReadAllByTallerID(this.TallerID.Value);

            grdRT.DataSource = talleresRT;
            grdRT.DataBind();
        }

        protected void grdRT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Color colorPrincipal = Color.LightGreen;
                Color colorVencido = Color.Coral;

                if (!String.IsNullOrWhiteSpace(e.Row.Cells[1].Text) && e.Row.Cells[1].Text != "&nbsp;")
                {
                    DateTime fechaInicio = DateTime.Parse(e.Row.Cells[1].Text);
                    e.Row.Cells[1].Text = fechaInicio.ToString("dd/MM/yyyy");
                }

                if (!String.IsNullOrWhiteSpace(e.Row.Cells[2].Text) && e.Row.Cells[2].Text != "&nbsp;")
                {
                    DateTime fechaFin = DateTime.Parse(e.Row.Cells[2].Text);

                    if (fechaFin <= DateTime.Now) { e.Row.BackColor = colorVencido; }

                    e.Row.Cells[2].Text = fechaFin.ToString("dd/MM/yyyy");
                }


                Boolean esPrincipal = Boolean.Parse(grdRT.DataKeys[e.Row.RowIndex].Values["EsRTPrincipal"].ToString());

                if (esPrincipal) { e.Row.BackColor = colorPrincipal; }

            }
        }
        #endregion
    }
}