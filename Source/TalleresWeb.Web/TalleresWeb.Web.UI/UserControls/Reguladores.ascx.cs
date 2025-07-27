using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.UI.Controls;
using TalleresWeb.Web.UI.UserControls;

namespace PetroleraManager.Web.UserControls
{
    public partial class Reguladores : System.Web.UI.UserControl
    {
        #region Members

        private List<ObleasReguladoresExtendedView> tabla = new List<ObleasReguladoresExtendedView>();

        #endregion

        #region Properties

        public List<ObleasReguladoresExtendedView> ReguladoresCargados
        {
            get
            {
                UpdatePanel1.Update();
                List<ObleasReguladoresExtendedView> tabla2 = new List<ObleasReguladoresExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        ObleasReguladoresExtendedView ocxv = new ObleasReguladoresExtendedView();
                        ocxv.ID = Guid.NewGuid();
                        ocxv.IDReg = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDReg"].ToString());
                        ocxv.IDRegUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDRegUni"].ToString());
                        ocxv.MSDBRegID = new Guid(((HiddenField)gr.FindControl("lblMSDBRegID")).Value);
                        ocxv.CodigoReg = ((Label)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieReg = ((Label)gr.FindControl("lblSerieReg")).Text;
                        ocxv.MSDBReg = ((Label)gr.FindControl("lblMSDBReg")).Text.Trim();
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
            set
            {
                tabla = value;

                ViewState["lstDetalleReg"] = tabla;

                gdv.DataSource = tabla;
                gdv.DataBind();

                UpdatePanel1.Update();
            }
        }

        public Guid TipoperacionID
        {
            get
            {
                if (ViewState["TIPOOPERACIONID"] == null) return Guid.Empty;

                return new Guid(ViewState["TIPOOPERACIONID"].ToString());
            }
            set
            {
                ViewState["TIPOOPERACIONID"] = value;
            }
        }

        #endregion

        #region Methods

        public void SetFocus()
        {
            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
            }
        }

        protected void grdDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int i = Convert.ToInt32(e.CommandArgument);

                #region MODIFICAR

                if (e.CommandName == "modificar")
                {
                    Boolean lblVisible = false;
                    Boolean txtVisible = true;
                    var filaGrilla = gdv.Rows[i];
                    ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Text = ((Label)filaGrilla.FindControl("lblCodigo")).Text;
                    ((Label)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Visible = txtVisible;

                    ((TextBox)filaGrilla.FindControl("txtSerieReg")).Text = ((Label)filaGrilla.FindControl("lblSerieReg")).Text;
                    ((Label)filaGrilla.FindControl("lblSerieReg")).Visible = lblVisible;
                    ((TextBox)filaGrilla.FindControl("txtSerieReg")).Visible = txtVisible;

                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue = new Guid(((HiddenField)filaGrilla.FindControl("lblMSDBRegID")).Value);
                    ((Label)filaGrilla.FindControl("lblMSDBReg")).Visible = lblVisible;
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).Visible = txtVisible;

                    ((ImageButton)filaGrilla.FindControl("btnEliminar")).Visible = false;
                    ((ImageButton)filaGrilla.FindControl("btnModificar")).Visible = false;
                    ((ImageButton)filaGrilla.FindControl("btnAceptar")).Visible = true;

                    ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
                    return;
                }

                #endregion

                #region ACEPTAR MODIFICACION

                if (e.CommandName == "aceptar")
                {
                    var filaGrilla = gdv.Rows[i];

                    ObleasReguladoresExtendedView oev = new ObleasReguladoresExtendedView();
                    oev.CodigoReg = ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Text;
                    oev.NroSerieReg = ((TextBox)filaGrilla.FindControl("txtSerieReg")).Text;
                    oev.MSDBRegID = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue;
                    oev.MSDBReg = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText;

                    String isValid = this.ValidacionesGenerales(oev);                    
                    isValid += this.ValidarTipoMSDB(oev.MSDBRegID);

                    if (String.IsNullOrEmpty(isValid))
                    {
                        Boolean lblVisible = true;
                        Boolean txtVisible = false;
                        ((Label)filaGrilla.FindControl("lblCodigo")).Text = ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Text;
                        ((Label)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                        ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Visible = txtVisible;

                        ((Label)filaGrilla.FindControl("lblSerieReg")).Text = ((TextBox)filaGrilla.FindControl("txtSerieReg")).Text;
                        ((Label)filaGrilla.FindControl("lblSerieReg")).Visible = lblVisible;
                        ((TextBox)filaGrilla.FindControl("txtSerieReg")).Visible = txtVisible;

                        ((HiddenField)filaGrilla.FindControl("lblMSDBRegID")).Value = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue.ToString();
                        ((Label)filaGrilla.FindControl("lblMSDBReg")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText;
                        ((Label)filaGrilla.FindControl("lblMSDBReg")).Visible = lblVisible;
                        ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).Visible = txtVisible;

                        ((ImageButton)filaGrilla.FindControl("btnEliminar")).Visible = true;
                        ((ImageButton)filaGrilla.FindControl("btnModificar")).Visible = true;
                        ((ImageButton)filaGrilla.FindControl("btnAceptar")).Visible = false;

                        ((TextBox)gdv.FooterRow.FindControl("txtCodigoReg")).Focus();
                    }
                    else
                    {
                        MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
                    }
                }

                #endregion

                #region ELIMINAR

                if (e.CommandName == "eliminar")
                {
                    var reg = ReguladoresCargados;
                    reg.RemoveAt(i);
                    ReguladoresCargados = reg;
                    gdv.DataSource = reg;
                    gdv.DataBind();
                }

                if (gdv.Rows.Count == 0)
                {
                    var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                    ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
                }
                else
                {
                    GridViewRow filaGrilla = gdv.FooterRow;
                    ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
                }

                #endregion
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            tabla = this.ReguladoresCargados;

            String isValid = String.Empty;
            ObleasReguladoresExtendedView dfc = new ObleasReguladoresExtendedView();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                dfc.CodigoReg = ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Text.ToUpper();
                dfc.NroSerieReg = ((TextBox)filaGrilla.FindControl("txtSerieReg")).Text.ToUpper();
                dfc.MSDBRegID = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue;
                dfc.MSDBReg = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText.ToUpper();              
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;

                dfc.CodigoReg = ((TextBox)filaGrilla.FindControl("txtCodigoReg")).Text.ToUpper();
                dfc.NroSerieReg = ((TextBox)filaGrilla.FindControl("txtSerieReg")).Text.ToUpper();
                dfc.MSDBRegID = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue;
                dfc.MSDBReg = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText.ToUpper();               
            }

            dfc.ID = Guid.NewGuid();
            dfc.IDReg = Guid.Empty;
            dfc.IDRegUni = Guid.Empty;

            isValid += this.ValidacionesGenerales(dfc);

            isValid += this.ValidarTipoMSDB(dfc.MSDBRegID);

            isValid += this.ValidarCantidadReguladores();

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedIndex = 1;
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }

            this.SetFocus();
        }

        private string ValidacionesGenerales(ObleasReguladoresExtendedView dfc)
        {
            String mensaje = String.Empty;

            if (String.IsNullOrEmpty(dfc.CodigoReg))
            { mensaje += "- Debe ingresar código de homologación del regulador. </br>"; }
            else
            {
                if(dfc.CodigoReg.Length != 4) mensaje += "- El código de homologación del regulador es incorrecto. </br>";
            }
            if (String.IsNullOrEmpty(dfc.NroSerieReg)) mensaje += "- Debe ingresar número de serie del regulador. </br>";

            return mensaje;
        }

        private String ValidarCantidadReguladores()
        {
            if ((this.TipoperacionID == TIPOOPERACION.Conversion
                || this.TipoperacionID == TIPOOPERACION.Baja) && tabla.Any())
                return "- Solo se permite un regulador cuando el tipo de operación es Conversión o Baja";            

            return String.Empty;
        }        

        private String ValidarTipoMSDB(Guid msdbID)
        {
            if (this.TipoperacionID == TIPOOPERACION.Conversion && msdbID != MSDB.Montaje)
                return "- Solo se permite Montaje cuando el tipo de operación es conversión";

            if (this.TipoperacionID == TIPOOPERACION.RevisionAnual && msdbID != MSDB.Sigue)
                return "- Solo se permite Sigue cuando el tipo de operación es Revisión Anual";

            if (this.TipoperacionID == TIPOOPERACION.Baja && msdbID != MSDB.Baja && msdbID != MSDB.Desmontaje)
            {                
                return "- Solo se permite Sigue cuando el tipo de operación es Revisión Anual";
            }

            return String.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleReg"] == null)
                ViewState["lstDetalleReg"] = tabla;
            else
                tabla = (List<ObleasReguladoresExtendedView>)ViewState["lstDetalleReg"];

            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();

                if (!tabla.Any())
                {
                    var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedIndex = 1;

                }
            }
        }

        #endregion
    }
}