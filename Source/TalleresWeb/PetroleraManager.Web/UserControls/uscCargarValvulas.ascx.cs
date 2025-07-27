using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.Web.Controls;
using PL.Fwk.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscCargarValvulas : System.Web.UI.UserControl
    {
        List<ObleasValvulasExtendedView> tabla = new List<ObleasValvulasExtendedView>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleVal"] == null)
                ViewState["lstDetalleVal"] = tabla;
            else
                tabla = (List<ObleasValvulasExtendedView>)ViewState["lstDetalleVal"];

            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            ObleasValvulasExtendedView dfc = new ObleasValvulasExtendedView();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text;
                    dfc.NroSerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text;
                    dfc.MSDBValID = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue;
                    dfc.MSDBVal = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedText;
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (isValid == String.Empty)
                {
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text;
                    dfc.NroSerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text;
                    dfc.MSDBValID = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedValue;
                    dfc.MSDBVal = ((CboMSDB)filaGrilla.FindControl("cboMSDBVal")).SelectedText;
                }
            }

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Focus();

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
                    var filaGrilla = gdv.Rows[i];
                    Boolean lblVisible = true;
                    Boolean txtVisible = false;
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

                    ((PLTextBox)gdv.FooterRow.FindControl("txtCodigoVal")).Focus();
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

        public List<ObleasValvulasExtendedView> ValvulasCargadas
        {
            get
            {
                List<ObleasValvulasExtendedView> tabla2 = new List<ObleasValvulasExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        ObleasValvulasExtendedView ocxv = new ObleasValvulasExtendedView();
                        ocxv.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDVal = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDVal"].ToString());
                        ocxv.IDValUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDValUni"].ToString());
                        ocxv.MSDBValID = new Guid(((PLHidden)gr.FindControl("lblMSDBValID")).Text);
                        ocxv.CodigoVal = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.NroSerieVal = ((PLLabel)gr.FindControl("lblSerieVal")).Text;
                        ocxv.MSDBVal = ((PLLabel)gr.FindControl("lblMSDBVal")).Text;
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
            set
            {
                tabla = value;

                ViewState["lstDetalleVal"] = tabla;

                gdv.DataSource = tabla;
                gdv.DataBind();
            }
        }

        private void CargarCombosCilindros(int cantCilindros)
        {
            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                PLComboBox cbo = (PLComboBox)filaGrilla.FindControl("txtCodigoVal");

                List<ViewEntity> dt = new List<ViewEntity>();
                MesExtendedView dr = new MesExtendedView();
                dr.ID = "-1";
                dr.Descripcion = " --";
                dt.Add(dr);

                for (int i = 0; i <= cantCilindros; i++)
                {
                    MesExtendedView dr2 = new MesExtendedView();
                    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString());
                    dr2.ID = i.ToString();
                    dr2.Descripcion = i.ToString();
                    dt.Add(dr2);
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                PLComboBox cbo = (PLComboBox)filaGrilla.FindControl("txtCodigoVal");

                List<ViewEntity> dt = new List<ViewEntity>();
                MesExtendedView dr = new MesExtendedView();
                dr.ID = "-1";
                dr.Descripcion = " --";
                dt.Add(dr);

                for (int i = 0; i <= cantCilindros; i++)
                {
                    MesExtendedView dr2 = new MesExtendedView();
                    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString());
                    dr2.ID = i.ToString();
                    dr2.Descripcion = i.ToString();
                    dt.Add(dr2);
                }
            }
        }
    }
}