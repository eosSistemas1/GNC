using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.Web.Controls;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscCargarCilindros : System.Web.UI.UserControl
    {
        #region eventos
        public event EventHandler SelectedCilindroChange;
        #endregion

        #region Propiedades
        public List<ObleasCilindrosExtendedView> CilindrosCargados
        {
            get
            {
                List<ObleasCilindrosExtendedView> tabla2 = new List<ObleasCilindrosExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        ObleasCilindrosExtendedView ocxv = new ObleasCilindrosExtendedView();
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
            }
        }

        public int CantCilindrosCargados { 
            get
            {
                return gdv.Rows.Count;
            }
        }
        #endregion

        List<ObleasCilindrosExtendedView> tabla = new List<ObleasCilindrosExtendedView>();
        Generic genericos = new Generic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleCil"] == null)
                ViewState["lstDetalleCil"] = tabla;
            else
                tabla = (List<ObleasCilindrosExtendedView>)ViewState["lstDetalleCil"];

            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            ObleasCilindrosExtendedView dfc = new ObleasCilindrosExtendedView();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";
                if (((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString == "-1") isValid += "- Seleccione mes de fabricación. </br>";
                if (((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString == "-1") isValid += "- Seleccione año de fabricación. </br>";
                if (((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString == "-1") isValid += "- Seleccione mes última revisión. </br>";
                if (((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString == "-1") isValid += "- Seleccione año última revisión. </br>";
                if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text;
                    dfc.NroSerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text;
                    
                    dfc.CilFabMes = ((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString;
                    dfc.CilFabAnio = ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString;
                    dfc.CilRevMes = ((CboMes)filaGrilla.FindControl("cboCilRevMes")).SelectedValueString;
                    dfc.CilRevAnio = ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString;

                    dfc.CRPCCilID = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue;
                    dfc.CRPCCil = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedText;
                    dfc.MSDBCilID = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue;
                    dfc.MSDBCil = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedText;
                    dfc.NroCertificadoPH = ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text;
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;

                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";
                if (((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString == "-1") isValid += "- Seleccione mes de fabricación. </br>";
                if (((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString == "-1") isValid += "- Seleccione año de fabricación. </br>";
                if (((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString == "-1") isValid += "- Seleccione mes última revisión. </br>";
                if (((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString == "-1") isValid += "- Seleccione año última revisión. </br>";
                if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text;
                    dfc.NroSerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text;

                    dfc.CilFabMes = ((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString;
                    dfc.CilFabAnio = ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString;
                    dfc.CilRevMes = ((CboMes)filaGrilla.FindControl("cboCilRevMes")).SelectedValueString;
                    dfc.CilRevAnio = ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString;

                    dfc.CRPCCilID = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValue;
                    dfc.CRPCCil = ((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedText;
                    dfc.MSDBCilID = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedValue;
                    dfc.MSDBCil = ((CboMSDB)filaGrilla.FindControl("cboMSDBCil")).SelectedText;
                    dfc.NroCertificadoPH = ((PLTextBox)filaGrilla.FindControl("txtNroCertifPH")).Text;
                }
            }

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
            }
            else
            {
                MessageBox(null, isValid, PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);   
            }
        }

        private void MessageBox(String Titulo, String Mensaje, PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            lblTituloMsj.Text = Titulo;
            lblMsj.Text = Mensaje;
            imgMsg.ImageUrl = "~/Imagenes/Iconos/warning.png";
            MPE.Show();
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
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                    ((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString = ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                    ((CboMes)filaGrilla.FindControl("cboCilFabMes")).Visible = txtVisible;

                    ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString = genericos.FormatearAnio(((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                    ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).Visible = txtVisible;

                    ((CboMes)filaGrilla.FindControl("cboCilRevMes")).SelectedValueString = ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Visible = lblVisible;
                    ((CboMes)filaGrilla.FindControl("cboCilRevMes")).Visible = txtVisible;

                    ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString = genericos.FormatearAnio(((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Text);
                    ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Visible = lblVisible;
                    ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).Visible = txtVisible;

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
                    Boolean lblVisible = true;
                    Boolean txtVisible = false;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text;
                    ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text = ((CboMes)filaGrilla.FindControl("cboCilFabMes")).SelectedValueString;
                    ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                    ((CboMes)filaGrilla.FindControl("cboCilFabMes")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text = ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).SelectedValueString;
                    ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                    ((CboAnio)filaGrilla.FindControl("cboCilFabAnio")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Text = ((CboMes)filaGrilla.FindControl("cboCilRevMes")).SelectedValueString;
                    ((PLLabel)filaGrilla.FindControl("lblCilRevMes")).Visible = lblVisible;
                    ((CboMes)filaGrilla.FindControl("cboCilRevMes")).Visible = txtVisible;

                    ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Text = ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).SelectedValueString;
                    ((PLLabel)filaGrilla.FindControl("lblCilRevAnio")).Visible = lblVisible;
                    ((CboAnio)filaGrilla.FindControl("cboCilRevAnio")).Visible = txtVisible;


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
    public delegate void SelectedCilindroChange(object sender, EventArgs e);
}