using CrossCutting.DatosDiscretos;
using PetroleraManager.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Data.Objects.DataClasses;
using System.Drawing;

namespace PetroleraManager.Web.Sistema
{
    public partial class Talleres : PageBase
    {
        #region Properties
        private TalleresLogic _Logic;
        public TalleresLogic Logic
        {
            get
            {
                if (this._Logic == null) this._Logic = new TalleresLogic();
                return this._Logic;
            }
            set { this._Logic = value; }
        }

        private TalleresRTLogic _LogicTallerRT;
        public TalleresRTLogic LogicTallerRT
        {
            get
            {
                if (this._LogicTallerRT == null) this._LogicTallerRT = new TalleresRTLogic();
                return this._LogicTallerRT;
            }
            set { this._LogicTallerRT = value; }
        }

        private Guid TallerID
        {
            get { return new Guid(ViewState["TALLERid"].ToString());}
            set { ViewState["TALLERid"] = value.ToString(); }
        }
        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pnlDatos.Visible = false;
                this.CargarCboRt();
            }
        }
        private void CargarCboRt()
        {
            RTLogic logic = new RTLogic();
            var rt = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
            rt.Insert(0, new PL.Fwk.Entities.ViewEntity() { ID = Guid.Empty, Descripcion = "--Seleccione--" });
            this.cboRT.DataValueField = "ID";
            this.cboRT.DataTextField = "Descripcion";
            this.cboRT.DataSource = rt;
            this.cboRT.DataBind();
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            TalleresParameters param = new TalleresParameters();
            param.Descripcion = txtFiltro.Text.Trim();
            List<TalleresExtendedView> datos = this.Logic.ReadExtendedView(param);
            grdFiltro.DataSource = datos;
            grdFiltro.DataBind();
        }

        private void InicializarCampos()
        {
            this.txtID.Text = String.Empty;
            this.txtMatricula.Text = String.Empty;
            this.txtRazonSocial.Text = String.Empty;
            this.txtDomicilio.Text = String.Empty;
            this.txtCuit.Text = String.Empty;
            this.cboLocalidad.SelectedIndex = 0;
            this.cboZona.SelectedIndex = 0;
            this.txtHorarioAtencion.Text = String.Empty;
            this.txtTelefono.Text = String.Empty;
            this.txtFax.Text = String.Empty;
            this.txtMail.Text = String.Empty;
            this.txtContacto.Text = String.Empty;
            this.calFechaVencimientoContrato.Value = String.Empty;
            this.txtUltimoNroIntOperacion.Text = String.Empty;

            this.InicializarTallerRT();
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                var item = this.Logic.Read(idItem);
                item.ActivoTaller = false;
                this.Logic.Update(item);
                this.InicializarCampos();
                this.pnlDatos.Visible = false;
                Buscar();
            }

            else if (e.CommandName == "modificar")
            {
                CargarDatos(idItem);
                pnlDatos.Visible = true;
            }
        }

        private void CargarDatos(Guid id)
        {
            TalleresWeb.Entities.Talleres item = this.Logic.Read(id);

            this.txtID.Text = item.ID.ToString();
            this.txtMatricula.Text = item.Descripcion;
            this.txtRazonSocial.Text = item.RazonSocialTaller;
            this.txtDomicilio.Text = item.DomicilioTaller;
            this.txtCuit.Text = item.CuitTaller;

            if (item.IdCiudad.HasValue)
            {
                this.cboLocalidad.SelectedValue = item.IdCiudad.Value;
            }
            else
            {
                this.cboLocalidad.SelectedIndex = 0;
            }


            if (!String.IsNullOrEmpty(item.Zona))
            {
                if (item.Zona == ZONAS.Centro) this.cboZona.SelectedValue = new Guid("DACFC3AE-417B-4168-B814-6831755452DA");
                if (item.Zona == ZONAS.Este) this.cboZona.SelectedValue = new Guid("2AF9DDDE-2683-4823-8CA6-454730C4F1C3");
                if (item.Zona == ZONAS.Norte) this.cboZona.SelectedValue = new Guid("2B14043A-7B56-4DD3-ABBF-41113A14DB26");
                if (item.Zona == ZONAS.Oeste) this.cboZona.SelectedValue = new Guid("3EA3EE5E-D89B-4456-AEFC-F698477E0C33");
                if (item.Zona == ZONAS.Sur) this.cboZona.SelectedValue = new Guid("6C85D584-AF45-48F5-81C3-3CAF388CE0AC");
            }
            else
            {
                this.cboZona.SelectedIndex = 0;
            }

            this.txtHorarioAtencion.Text = item.HorarioDeAtencion;
            this.txtTelefono.Text = item.TelefonoTaller;
            this.txtFax.Text = item.FaxTaller;
            this.txtMail.Text = item.MailTaller;
            this.txtContacto.Text = item.ContactoTaller;
            this.calFechaVencimientoContrato.Value = item.FechaVencContrato.HasValue ? item.FechaVencContrato.Value.ToString("dd/MM/yyyy") : String.Empty;
            this.txtUltimoNroIntOperacion.Text = item.UltimoNroIntOperacion.ToString();

            this.TallerID = item.ID;
            this.CargarTalleresRT();
        }       

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarCampos();
            pnlDatos.Visible = true;
            TabPanel2.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                TalleresWeb.Entities.Talleres item = new TalleresWeb.Entities.Talleres();
                item.ID = txtID.Text == String.Empty ? Guid.NewGuid() : new Guid(txtID.Text.Trim());
                item.Descripcion = this.txtMatricula.Text;
                item.RazonSocialTaller = this.txtRazonSocial.Text;
                item.DomicilioTaller = this.txtDomicilio.Text;
                item.CuitTaller = this.txtCuit.Text;
                item.IdCiudad = this.cboLocalidad.SelectedValue;
                item.Zona = this.cboZona.SelectedText;
                item.HorarioDeAtencion = this.txtHorarioAtencion.Text;
                item.TelefonoTaller = this.txtTelefono.Text;
                item.FaxTaller = this.txtFax.Text;
                item.MailTaller = this.txtMail.Text;
                item.ContactoTaller = this.txtContacto.Text;
                item.FechaVencContrato = !String.IsNullOrEmpty(this.calFechaVencimientoContrato.Value) ? DateTime.Parse(this.calFechaVencimientoContrato.Value) : default(DateTime?);
                item.UltimoNroIntOperacion = !String.IsNullOrEmpty(this.txtUltimoNroIntOperacion.Text) ? int.Parse(this.txtUltimoNroIntOperacion.Text) : 0;
                item.ActivoTaller = true;

                Boolean tallerRTSeleccionado = (cboRT.Enabled ||
                                                  (!cboRT.Enabled && calFechaHRT.Value != String.Empty)) &&
                                                 new Guid(cboRT.SelectedValue) != Guid.Empty &&
                                                 cboRT.SelectedIndex != -1 &&
                                                 calFechaDRT.Value != String.Empty;



                // si txt id es vacio, creo uno nuevo , si tiene id lo modifico
                if (txtID.Text == String.Empty)
                {
                    this.Logic.Add(item);
                }
                else
                {
                    this.Logic.Update(item);
                }

                this.InicializarCampos();
                pnlDatos.Visible = false;
                this.Buscar();
            }
        }

        private bool Validar()
        {
            String mensaje = String.Empty;

            if (String.IsNullOrWhiteSpace(this.txtMatricula.Text)) mensaje += "Debe ingresar la matrícula <br/>";
            if (String.IsNullOrWhiteSpace(this.txtRazonSocial.Text)) mensaje += "Debe ingresar la razón social <br/>";
            if (this.cboZona.SelectedIndex == -1 || this.cboZona.SelectedValue == Guid.Empty) mensaje += "Debe ingresar una zona <br/>";

            if (mensaje != String.Empty)
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }

            return mensaje == String.Empty;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }

        #endregion

        protected void grdRT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "modificar")
            {
                var tallerRT = this.LogicTallerRT.Read(idItem);

                txtIDTaller.Text = tallerRT.TalleresID.ToString();
                txtIDTalleresRT.Text = tallerRT.ID.ToString();
                cboRT.SelectedValue = tallerRT.RTID.ToString();
                calFechaDRT.Value = tallerRT.FechaDesdeRTT.ToString("dd/MM/yyyy");
                calFechaHRT.Value = tallerRT.FechaHastaRTT.HasValue ? tallerRT.FechaHastaRTT.Value.ToString("dd/MM/yyyy") : String.Empty;
                esPrincipal.Checked = tallerRT.EsRTPrincipal;
            }
        }

        protected void btnGuardarRT_Click(object sender, EventArgs e)
        {
            String validar = this.ValidarRT();

            if (String.IsNullOrWhiteSpace(validar))
            {
                TalleresRT itemTallerRT = new TalleresRT();
                itemTallerRT.TalleresID = this.TallerID;
                itemTallerRT.ID = !String.IsNullOrWhiteSpace(txtIDTalleresRT.Text) ? new Guid(txtIDTalleresRT.Text) : Guid.NewGuid();
                itemTallerRT.RTID = new Guid(cboRT.SelectedValue);
                itemTallerRT.FechaDesdeRTT = DateTime.Parse(calFechaDRT.Value);
                itemTallerRT.FechaHastaRTT = !String.IsNullOrWhiteSpace(calFechaHRT.Value) ? DateTime.Parse(calFechaHRT.Value) : default(DateTime?);
                itemTallerRT.EsRTPrincipal = esPrincipal.Checked;

                Guid id = !String.IsNullOrWhiteSpace(txtIDTalleresRT.Text) ? new Guid(txtIDTalleresRT.Text) : Guid.Empty;
                var tallerRT = this.LogicTallerRT.Read(id);

                if (id == Guid.Empty || tallerRT == null)
                {
                    this.LogicTallerRT.Add(itemTallerRT);
                }
                else
                {
                    this.LogicTallerRT.Update(itemTallerRT);
                }

                this.CargarTalleresRT();

                this.InicializarTallerRT();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, validar, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private String ValidarRT()
        {
            String mensaje = String.Empty;

            if (cboRT.SelectedIndex == -1 || cboRT.SelectedValue == Guid.Empty.ToString()) mensaje = "- Debe seleccionar un Responsable Técnico. <br/>";
            if (String.IsNullOrWhiteSpace(calFechaDRT.Value)) mensaje += "- Debe seleccionar una fecha desde. <br/>";
                      
            return mensaje;           
        }

        private void InicializarTallerRT()
        {
            this.txtIDTalleresRT.Text = String.Empty;
            this.txtIDTaller.Text = String.Empty;
            this.cboRT.SelectedIndex = 0;
            this.calFechaDRT.Value = DateTime.Now.ToString("dd/MM/yyyy");
            this.calFechaHRT.Value = String.Empty;
        }

        private void CargarTalleresRT()
        {
            var talleresRT = this.LogicTallerRT.ReadAllByTallerID(this.TallerID);

            grdRT.DataSource = talleresRT;
            grdRT.DataBind();

            TabPanel2.Visible = true;
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

                if(esPrincipal) { e.Row.BackColor = colorPrincipal; }

            }
        }
    }
}