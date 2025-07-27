using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Controls;
using PL.Fwk.Entities;
using TalleresWeb.Web;
using Common.Web.UserControls;
using TalleresWeb.Logic;

namespace TalleresWeb.Web.UserControls
{
    public partial class uscCargarCilindrosValvulas : System.Web.UI.UserControl
    {

        #region Propiedades
        public Boolean _PermiteSeleccionar;
        public Boolean PermiteSeleccionar 
        {
            get
            {
                return PermiteSeleccionar = _PermiteSeleccionar == null ? false : _PermiteSeleccionar;
            }
            set
            {
                _PermiteSeleccionar = value == null ? false : value;
            }
        }

        public List<ObleasCilindrosExtendedView> CilindrosCargados
        {
            get
            {
                UpdatePanel1.Update();
                List<ObleasCilindrosExtendedView> tabla2 = new List<ObleasCilindrosExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        ObleasCilindrosExtendedView ocxv = new ObleasCilindrosExtendedView();
                        ocxv.OrdenCil = ((PLLabel)gr.FindControl("lblItem")).Text;
                        ocxv.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDCil = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDCil"].ToString());
                        ocxv.IDCilUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDCilUni"].ToString());
                        ocxv.MSDBCilID = new Guid(((PLHidden)gr.FindControl("lblMSDBCilID")).Text);
                        ocxv.CodigoCil = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieCil = ((PLLabel)gr.FindControl("lblSerieCil")).Text;
                        ocxv.CilFabMes = ((PLLabel)gr.FindControl("lblCilFabMes")).Text;
                        ocxv.CilFabAnio = ((PLLabel)gr.FindControl("lblCilFabAnio")).Text;
                        ocxv.CilRevMes = ((PLLabel)gr.FindControl("lblCilRevMes")).Text;
                        ocxv.CilRevAnio = ((PLLabel)gr.FindControl("lblCilRevAnio")).Text;
                        ocxv.CRPCCil = ((PLLabel)gr.FindControl("lblCRPCCil")).Text;
                        ocxv.CRPCCilID = new Guid(((PLHidden)gr.FindControl("lblCRPCCilID")).Text);
                        ocxv.MSDBCil = ((PLLabel)gr.FindControl("lblMSDBCil")).Text;
                        ocxv.NroCertificadoPH = ((PLLabel)gr.FindControl("lblNroCertifPH")).Text;
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
            set
            {
                tabla = value;

                ViewState["lstDetalleCil"] = tabla;
                
                gdv.DataSource = tabla;
                gdv.DataBind();
                UpdatePanel1.Update();
                //CargarCombosCilindros();
            }
        }
        public List<ObleasValvulasExtendedView> ValvulasCargadas
        {
            get
            {
                UpdatePanel1.Update();
                List<ObleasValvulasExtendedView> tabla2 = new List<ObleasValvulasExtendedView>();
                foreach (GridViewRow gr in gdvValvulas.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        ObleasValvulasExtendedView ocxv = new ObleasValvulasExtendedView();
                        ocxv.ID = new Guid(gdvValvulas.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDVal = new Guid(gdvValvulas.DataKeys[gr.RowIndex].Values["IDVal"].ToString());
                        ocxv.IDValUni = new Guid(gdvValvulas.DataKeys[gr.RowIndex].Values["IDValUni"].ToString());
                        ocxv.MSDBValID = new Guid(((PLHidden)gr.FindControl("lblMSDBValID")).Text);
                        ocxv.CodigoVal = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieVal = ((PLLabel)gr.FindControl("lblSerieVal")).Text;
                        ocxv.MSDBVal = ((PLLabel)gr.FindControl("lblMSDBVal")).Text;
                        //int index = int.Parse(((DropDownList)gr.FindControl("cboOrden")).SelectedItem.Text) - 1;
                        ocxv.IdObleaCil = new Guid(gdvValvulas.DataKeys[gr.RowIndex].Values["IdObleaCil"].ToString());
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
            set
            {
                tablaVal = value;

                ViewState["lstDetalleVal"] = tablaVal;
                gdvValvulas.DataSource = tablaVal;
                gdvValvulas.DataBind();

                UpdatePanel1.Update();
                CargarCombosCilindros();
                
            }
        }

        public List<ObleasCilindrosExtendedView> CilindrosSeleccionados
        {
            get
            {
                UpdatePanel1.Update();
                List<ObleasCilindrosExtendedView> tabla2 = new List<ObleasCilindrosExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if ((gr.RowType == DataControlRowType.DataRow) && (((PLCheckBox)gr.FindControl("chkSeleccionar")).Checked))
                    {
                        ObleasCilindrosExtendedView ocxv = new ObleasCilindrosExtendedView();
                        ocxv.OrdenCil = ((PLLabel)gr.FindControl("lblItem")).Text;
                        ocxv.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDCil = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDCil"].ToString());
                        ocxv.IDCilUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDCilUni"].ToString());
                        ocxv.MSDBCilID = new Guid(((PLHidden)gr.FindControl("lblMSDBCilID")).Text);
                        ocxv.CodigoCil = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieCil = ((PLLabel)gr.FindControl("lblSerieCil")).Text;
                        ocxv.CilFabMes = ((PLLabel)gr.FindControl("lblCilFabMes")).Text;
                        ocxv.CilFabAnio = ((PLLabel)gr.FindControl("lblCilFabAnio")).Text;
                        ocxv.CilRevMes = ((PLLabel)gr.FindControl("lblCilRevMes")).Text;
                        ocxv.CilRevAnio = ((PLLabel)gr.FindControl("lblCilRevAnio")).Text;
                        ocxv.CRPCCil = ((PLLabel)gr.FindControl("lblCRPCCil")).Text;
                        ocxv.CRPCCilID = new Guid(((PLHidden)gr.FindControl("lblCRPCCilID")).Text);
                        ocxv.MSDBCil = ((PLLabel)gr.FindControl("lblMSDBCil")).Text;
                        ocxv.NroCertificadoPH = ((PLLabel)gr.FindControl("lblNroCertifPH")).Text;
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
        }
        #endregion

        List<ObleasValvulasExtendedView> tablaVal = new List<ObleasValvulasExtendedView>();
        List<ObleasCilindrosExtendedView> tabla = new List<ObleasCilindrosExtendedView>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleCil"] == null)
                ViewState["lstDetalleCil"] = tabla;
            else
                tabla = (List<ObleasCilindrosExtendedView>)ViewState["lstDetalleCil"];

            if (ViewState["lstDetalleVal"] == null)
                ViewState["lstDetalleVal"] = tablaVal;
            else
                tablaVal = (List<ObleasValvulasExtendedView>)ViewState["lstDetalleVal"];

            if (!Page.IsPostBack)
            {
                gdv.Columns[10].Visible = !_PermiteSeleccionar;
                gdv.Columns[11].Visible = _PermiteSeleccionar;
                gdvValvulas.Columns[4].Visible = !_PermiteSeleccionar;

                gdv.DataSource = tabla;
                gdv.DataBind();

                gdvValvulas.DataSource = tablaVal;
                gdvValvulas.DataBind();
            }
        }

        #region CILINDROS
        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            ObleasCilindrosExtendedView dfc = new ObleasCilindrosExtendedView();
            CilindrosLogic cilLogic = new CilindrosLogic();
            CilindrosUnidadLogic cilUniLogic = new CilindrosUnidadLogic();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];         
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text == String.Empty)
                { isValid += "- Ingrese mes de fabricación. </br>"; }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text) > 12) 
                        isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }
                
                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text == String.Empty)
                { isValid += "- Ingrese mes de fabricación. </br>"; }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text) > 12)
                        isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text == String.Empty) isValid += "- Ingrese año última revisión. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text.Length != 4) isValid += "- El año última revisión debe tener 4 números. Ej: 2010. </br>";

                if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                if (isValid == String.Empty)
                {
                   

                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                    dfc.NroSerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();

                    var idCil = cilLogic.ReadByCodigoHomologacion(dfc.CodigoCil);
                    dfc.IDCil = idCil.Count != 0 ? idCil.FirstOrDefault().ID : Guid.Empty;
                    var idCilUni = cilUniLogic.ReadCilindroUnidad(dfc.IDCil, dfc.NroSerieCil);
                    dfc.IDCilUni = idCilUni.Count != 0 ? idCilUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.CilFabMes = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text;
                    dfc.CilFabAnio = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text;
                    dfc.CilRevMes = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text;
                    dfc.CilRevAnio = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text;

                    dfc.CRPCCilID = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue;
                    dfc.CRPCCil = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedText.ToUpper();
                    dfc.MSDBCilID = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue;
                    dfc.MSDBCil = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedText[0].ToString().ToUpper();
                    dfc.NroCertificadoPH = ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text.ToUpper();

                    dfc.ID = Guid.NewGuid();
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text == String.Empty)
                { isValid += "- Ingrese mes de fabricación. </br>"; }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text) > 12)
                        isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text == String.Empty)
                { isValid += "- Ingrese mes de fabricación. </br>"; }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text) > 12)
                        isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }

                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text == String.Empty) isValid += "- Ingrese año última revisión. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text.Length != 4) isValid += "- El año última revisión debe tener 4 números. Ej: 2010. </br>";

                if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                    dfc.NroSerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();

                    var idCil = cilLogic.ReadByCodigoHomologacion(dfc.CodigoCil);
                    dfc.IDCil = idCil.Count != 0 ? idCil.FirstOrDefault().ID : Guid.Empty;
                    var idCilUni = cilUniLogic.ReadCilindroUnidad(dfc.IDCil, dfc.NroSerieCil);
                    dfc.IDCilUni = idCilUni.Count != 0 ? idCilUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.CilFabMes = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text;
                    dfc.CilFabAnio = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text;
                    dfc.CilRevMes = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text;
                    dfc.CilRevAnio = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text;

                    dfc.CRPCCilID = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue;
                    dfc.CRPCCil = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedText.ToUpper();
                    dfc.MSDBCilID = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue;
                    dfc.MSDBCil = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedText[0].ToString().ToUpper();
                    dfc.NroCertificadoPH = ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text.ToUpper();

                    dfc.ID = Guid.NewGuid();
                }
            }

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();

                CargarCombosCilindros();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);   
            }
        }

        protected void gdv_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text = ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Visible = txtVisible;

                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text = Genericos.FormatearAnio(((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Visible = txtVisible;

                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text = ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Visible = lblVisible;
                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Visible = txtVisible;

                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text = Genericos.FormatearAnio(((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Visible = lblVisible;
                    ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Visible = txtVisible;

                    ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue = new Guid(((PLHidden)filaGrilla.FindControl("lblCRPCCilID")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblCRPCCil")).Visible = lblVisible;
                    ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).Visible = txtVisible;

                    ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue = new Guid(((PLHidden)filaGrilla.FindControl("lblMSDBCilID")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblMSDBCil")).Visible = lblVisible;
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).Visible = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text = ((PLLabel)filaGrilla.FindControl("lblNroCertifPH")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblNroCertifPH")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Visible = txtVisible;

                    ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = true;

                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
                }
                #endregion

                #region ACEPTAR MODIFICACION
                if (e.CommandName == "aceptar")
                {
                    var filaGrilla = gdv.Rows[i];
                    String isValid = String.Empty;

                    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text == String.Empty)
                    { isValid += "- Ingrese mes de fabricación. </br>"; }
                    else
                    {
                        if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text) > 12)
                            isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                    }

                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text == String.Empty)
                    { isValid += "- Ingrese mes de fabricación. </br>"; }
                    else
                    {
                        if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text) > 12)
                            isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                    }

                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text == String.Empty) isValid += "- Ingrese año última revisión. </br>";
                    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text.Length != 4) isValid += "- El año última revisión debe tener 4 números. Ej: 2010. </br>";

                    if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                    if (isValid == String.Empty)
                    {
                        Boolean lblVisible = true;
                        Boolean txtVisible = false;
                        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                        ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                        ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                        ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabMes")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                        ((PLTextBoxMasked)filaGrilla.FindControl("cboCilFabAnio")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Visible = lblVisible;
                        ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Visible = lblVisible;
                        ((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Visible = txtVisible;


                        ((PLHidden)filaGrilla.FindControl("lblCRPCCilID")).Text = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue.ToString();
                        ((PLLabel)filaGrilla.FindControl("lblCRPCCil")).Text = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedText;
                        ((PLLabel)filaGrilla.FindControl("lblCRPCCil")).Visible = lblVisible;
                        ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).Visible = txtVisible;

                        ((PLHidden)filaGrilla.FindControl("lblMSDBCilID")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue.ToString();
                        ((PLLabel)filaGrilla.FindControl("lblMSDBCil")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedText;
                        ((PLLabel)filaGrilla.FindControl("lblMSDBCil")).Visible = lblVisible;
                        ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblNroCertifPH")).Text = ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblNroCertifPH")).Visible = lblVisible;
                        ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Visible = txtVisible;

                        ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = true;
                        ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = true;
                        ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = false;

                        ((PLTextBox)gdv.FooterRow.FindControl("txtCodigoCil")).Focus();
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

                    tabla.RemoveAt(i);

                    gdv.DataSource = tabla;
                    gdv.DataBind();

                }
                #endregion
            }
        }
        #endregion

        #region VALVULAS
        protected void ibtAgregarValvula_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            ObleasValvulasExtendedView dfc = new ObleasValvulasExtendedView();
            ValvulaLogic valLogic = new ValvulaLogic();
            ValvulaUnidadLogic valUniLogic = new ValvulaUnidadLogic();

            if (gdvValvulas.Rows.Count == 0)
            {
                var filaGrilla = gdvValvulas.Controls[0].Controls[1].Controls[0];
                if (((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedValue.ToString() == Guid.Empty.ToString()) isValid += "- Seleccione nro. de cilindro. </br>";
                if (((DropDownList)filaGrilla.FindControl("cboOrden")).Items.Count == 0) isValid += "- Seleccione nro. de cilindro. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.IdObleaCil = new Guid(((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedValue);
                    dfc.OrdenCil = ((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedItem.Text;
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                    dfc.NroSerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                    dfc.MSDBValID = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue;
                    dfc.MSDBVal = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedText[0].ToString().ToUpper();

                    var idVal = valLogic.ReadByCodigoHomologacion(dfc.CodigoVal);
                    dfc.IDVal = idVal.Count != 0 ? idVal.FirstOrDefault().ID : Guid.Empty;
                    var idValUni = valUniLogic.ReadValvulaUnidad(dfc.IDVal, dfc.NroSerieVal);
                    dfc.IDValUni = idValUni.Count != 0 ? idValUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.ID = Guid.NewGuid();
                }
            }
            else
            {
                GridViewRow filaGrilla = gdvValvulas.FooterRow;
                if (((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedValue.ToString() == Guid.Empty.ToString()) isValid += "- Seleccione nro. de cilindro. </br>";
                if (((DropDownList)filaGrilla.FindControl("cboOrden")).Items.Count == 0) isValid += "- Seleccione nro. de cilindro. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.IdObleaCil = new Guid(((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedValue);
                    dfc.OrdenCil = ((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedItem.Text;
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                    dfc.NroSerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                    dfc.MSDBValID = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue;
                    dfc.MSDBVal = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedText[0].ToString().ToUpper();

                    var idVal = valLogic.ReadByCodigoHomologacion(dfc.CodigoVal);
                    dfc.IDVal = idVal.Count != 0 ? idVal.FirstOrDefault().ID : Guid.Empty;
                    var idValUni = valUniLogic.ReadValvulaUnidad(dfc.IDVal, dfc.NroSerieVal);
                    dfc.IDValUni = idValUni.Count != 0 ? idValUni.FirstOrDefault().ID : Guid.Empty;

                    dfc.ID = Guid.NewGuid();
                }
            }

            if (isValid == String.Empty)
            {
                tablaVal.Add(dfc);

                gdvValvulas.DataSource = tablaVal;
                gdvValvulas.DataBind();

                GridViewRow filaGrilla = gdvValvulas.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Focus();

                CargarCombosCilindros();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void gdvValvulas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int i = Convert.ToInt32(e.CommandArgument);

                #region MODIFICAR
                if (e.CommandName == "modificar")
                {
                    Boolean lblVisible = false;
                    Boolean txtVisible = true;
                    var filaGrilla = gdvValvulas.Rows[i];

                    ((DropDownList)filaGrilla.FindControl("cboOrden")).Enabled = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Visible = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Visible = txtVisible;

                    ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue = new Guid(((PLHidden)filaGrilla.FindControl("lblMSDBValID")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblMSDBVal")).Visible = lblVisible;
                    ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).Visible = txtVisible;

                    ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = false;
                    ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = true;

                    ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Focus();
                }
                #endregion

                #region ACEPTAR MODIFICACION
                if (e.CommandName == "aceptar")
                {
                    var filaGrilla = gdvValvulas.Rows[i];
                    Boolean lblVisible = true;
                    Boolean txtVisible = false;
                    String isValid = String.Empty;

                    if (((DropDownList)filaGrilla.FindControl("cboOrden")).SelectedValue.ToString() == Guid.Empty.ToString()) isValid += "- Seleccione nro. de cilindro. </br>";
                    if (((DropDownList)filaGrilla.FindControl("cboOrden")).Items.Count == 0) isValid += "- Seleccione nro. de cilindro. </br>";
                    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código. </br>";
                    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie. </br>";

                    if (isValid == String.Empty)
                    {
                        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                        ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Visible = txtVisible;

                        ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Text = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text;
                        ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Visible = lblVisible;
                        ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Visible = txtVisible;

                        ((PLHidden)filaGrilla.FindControl("lblMSDBValID")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue.ToString();
                        ((PLLabel)filaGrilla.FindControl("lblMSDBVal")).Text = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedText;
                        ((PLLabel)filaGrilla.FindControl("lblMSDBVal")).Visible = lblVisible;
                        ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).Visible = txtVisible;


                        ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = true;
                        ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = true;
                        ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = false;

                        ((PLTextBox)gdvValvulas.FooterRow.FindControl("txtCodigoVal")).Focus();
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

                    tablaVal.RemoveAt(i);

                    gdvValvulas.DataSource = tablaVal;
                    gdvValvulas.DataBind();

                }
                #endregion
            }
        }

        protected void gdvValvulas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
            {
                UpdatePanel1.Update();
                DropDownList cbo = (DropDownList)e.Row.FindControl("cboOrden");
                CargarCombo(cbo);
                if (gdv.Rows.Count > 0)
                {
                    String value = gdvValvulas.DataKeys[e.Row.RowIndex].Values["IdObleaCil"] != null ? gdvValvulas.DataKeys[e.Row.RowIndex].Values["IdObleaCil"].ToString() : Guid.Empty.ToString();
                    cbo.SelectedValue = value;
                }
            }
        }
        #endregion

        private void CargarCombosCilindros()
        {
            //int cantCilindros = gdv.Rows.Count;
            DropDownList cbo = null;
            
            if (gdvValvulas.Rows.Count == 0)
            {
                var filaGrilla = gdvValvulas.Controls[0].Controls[1].Controls[0];
                cbo = (DropDownList)filaGrilla.FindControl("cboOrden");
            }
            else
            {
                GridViewRow filaGrilla = gdvValvulas.FooterRow;
                cbo = (DropDownList)filaGrilla.FindControl("cboOrden");
            }

            CargarCombo(cbo);
        }

        private void CargarCombo(DropDownList cbo)
        {
            List<ViewEntity> dt = new List<ViewEntity>();

            ViewEntity dr = new ViewEntity();
            dr.ID = Guid.Empty;
            dr.Descripcion = " --";
            dt.Add(dr);

            foreach (GridViewRow gr in gdv.Rows)
            {
                if (gr.RowType == DataControlRowType.DataRow)
                {
                    ViewEntity dr2 = new ViewEntity();
                    dr2.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                    dr2.Descripcion = ((PLLabel)gr.FindControl("lblItem")).Text;
                    dt.Add(dr2);
                }
            }

            cbo.DataSource = dt;
            cbo.DataTextField = "Descripcion";
            cbo.DataValueField = "ID";
            cbo.DataBind();
        }

    }
}