using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using System.Web.UI.HtmlControls;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Controls;
using Common.Web.UserControls;
using TalleresWeb.Logic;

namespace TalleresWeb.Web.UserControls
{
    public partial class uscCargarReguladores : System.Web.UI.UserControl
    {
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
                        ocxv.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDReg = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDReg"].ToString());
                        ocxv.IDRegUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDRegUni"].ToString());
                        ocxv.MSDBRegID = new Guid(((PLHidden)gr.FindControl("lblMSDBRegID")).Text);
                        ocxv.CodigoReg = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieReg = ((PLLabel)gr.FindControl("lblSerieReg")).Text;
                        ocxv.MSDBReg = ((PLLabel)gr.FindControl("lblMSDBReg")).Text;
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

        List<ObleasReguladoresExtendedView> tabla = new List<ObleasReguladoresExtendedView>();

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
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            ObleasReguladoresExtendedView dfc = new ObleasReguladoresExtendedView();

            ReguladoresLogic regLogic = new ReguladoresLogic();
            ReguladoresUnidadLogic regUniLogic = new ReguladoresUnidadLogic();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoReg = ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text.ToUpper();
                    dfc.NroSerieReg = ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text.ToUpper();
                    dfc.MSDBRegID = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue;
                    dfc.MSDBReg = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText.ToUpper();

                    var idReg = regLogic.ReadByCodigoHomologacion(dfc.CodigoReg);
                    dfc.IDReg = idReg.Count != 0 ? idReg.FirstOrDefault().ID : Guid.Empty;
                    var idRegUni = regUniLogic.ReadReguladorUnidad(dfc.IDReg, dfc.NroSerieReg);
                    dfc.IDRegUni = idRegUni.Count != 0 ? idRegUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.ID = Guid.NewGuid();
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoReg = ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text.ToUpper();
                    dfc.NroSerieReg = ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text.ToUpper();
                    dfc.MSDBRegID = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue;
                    dfc.MSDBReg = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText.ToUpper();

                    var idReg = regLogic.ReadByCodigoHomologacion(dfc.CodigoReg);
                    dfc.IDReg = idReg.Count != 0 ? idReg.FirstOrDefault().ID : Guid.Empty;
                    var idRegUni = regUniLogic.ReadReguladorUnidad(dfc.IDReg, dfc.NroSerieReg);
                    dfc.IDRegUni = idRegUni.Count != 0 ? idRegUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.ID = Guid.NewGuid();
                }
            }

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();

            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);   
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
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Visible = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieReg")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieReg")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Visible = txtVisible;

                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue = new Guid(((PLHidden)filaGrilla.FindControl("lblMSDBRegID")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblMSDBReg")).Visible = lblVisible;
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).Visible = txtVisible;

                    ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = true;

                    ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Focus();
                }
                #endregion

                #region ACEPTAR MODIFICACION
                if (e.CommandName == "aceptar")
                {
                    var filaGrilla = gdv.Rows[i];
                    Boolean lblVisible = true;
                    Boolean txtVisible = false;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text = ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoReg")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblSerieReg")).Text = ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieReg")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieReg")).Visible = txtVisible;

                    ((PLHidden)filaGrilla.FindControl("lblMSDBRegID")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedValue.ToString();
                    ((PLLabel)filaGrilla.FindControl("lblMSDBReg")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).SelectedText;
                    ((PLLabel)filaGrilla.FindControl("lblMSDBReg")).Visible = lblVisible;
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBReg")).Visible = txtVisible;


                    ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = true;
                    ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = true;
                    ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = false;

                    ((PLTextBox)gdv.FooterRow.FindControl("txtCodigoReg")).Focus();
                }
                #endregion

                #region ELIMINAR
                if (e.CommandName == "eliminar")
                {

                    tabla.RemoveAt(i);

                    gdv.DataSource = tabla;
                    gdv.DataBind();

                }
                #endregion
            }
        }

    }
}