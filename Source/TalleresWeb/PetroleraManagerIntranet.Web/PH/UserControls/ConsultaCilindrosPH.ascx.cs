using PetroleraManagerIntranet.Web.Controls;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace PetroleraManagerIntranet.Web.PH.UserControls
{
    public partial class ConsultaCilindrosPH : System.Web.UI.UserControl
    {
        #region Members

        private List<PHCilindrosConsultaView> tabla = new List<PHCilindrosConsultaView>();

        #endregion

        #region Properties

        public List<PHCilindrosConsultaView> CilindrosPHCargados
        {
            get
            {
                //UpdatePanel1.Update();
                List<PHCilindrosConsultaView> tabla2 = new List<PHCilindrosConsultaView>();
                foreach (GridViewRow row in gdv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        PHCilindrosConsultaView phCilindrosView = new PHCilindrosConsultaView();
                        phCilindrosView.ID = new Guid(gdv.DataKeys[row.RowIndex].Values["ID"].ToString());
                        phCilindrosView.CodigoCil = ((Label)row.FindControl("lblCodigoCil")).Text.ToUpper();
                        phCilindrosView.SerieCil = ((Label)row.FindControl("lblSerieCil")).Text.ToUpper();
                        phCilindrosView.MarcaCil = ((Label)row.FindControl("lblMarcaCil")).Text.ToUpper();
                        phCilindrosView.CapacidadCil = ((Label)row.FindControl("lblCapacidadCil")).Text.ToUpper();
                        phCilindrosView.MesFabricacionCil = ((Label)row.FindControl("lblMesFabricacionCil")).Text.ToUpper();
                        phCilindrosView.AnioFabricacionCil = ((Label)row.FindControl("lblAnioFabricacionCil")).Text.ToUpper();
                        phCilindrosView.CodigoVal = ((Label)row.FindControl("lblCodigoVal")).Text.ToUpper();
                        phCilindrosView.SerieVal = ((Label)row.FindControl("lblSerieVal")).Text.ToUpper();
                        phCilindrosView.Observaciones = ((Label)row.FindControl("lblObservaciones")).Text.ToUpper();
                        Guid idEstadoPH = Guid.Parse(((DropDownList)row.FindControl("cboEstadosPhConsulta")).SelectedValue);
                        phCilindrosView.IDEstadoPH = idEstadoPH == Guid.Empty ? default(Guid?) : idEstadoPH;
                        tabla2.Add(phCilindrosView);
                    }
                }

                return tabla2;
            }
            set
            {
                tabla = value;

                ViewState["lstDetalleCilPH"] = tabla;

                gdv.DataSource = tabla;
                gdv.ShowFooter = PermiteAgregar;
                gdv.DataBind();
            }
        }

        public bool ModificaEstado
        {
            get 
            {                 
                return string.IsNullOrWhiteSpace(hdnModificaEstado.Value) ? false: bool.Parse(hdnModificaEstado.Value.ToString());                    
            }
            set { hdnModificaEstado.Value = value.ToString(); }
        }

        public bool PermiteAgregar 
        {
            get { return ViewState["PERMITEAGREGAR"] != null ? bool.Parse(ViewState["PERMITEAGREGAR"].ToString()) : false; }
            set { ViewState["PERMITEAGREGAR"] = value; } 
        }
        #endregion

        #region Methods

        public void SetFocus()
        {
            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
            }
        }

        protected void grdDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int i = Convert.ToInt32(e.CommandArgument);

                #region ELIMINAR

                if (e.CommandName == "eliminar")
                {
                    tabla.RemoveAt(i);

                    gdv.DataSource = tabla;
                    gdv.ShowFooter = PermiteAgregar;
                    gdv.DataBind();
                }

                if (gdv.Rows.Count == 0)
                {
                    var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                    ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
                }
                else
                {
                    GridViewRow filaGrilla = gdv.FooterRow;
                    ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
                }

                #endregion
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            tabla = this.CilindrosPHCargados;

            String isValid = String.Empty;
            PHCilindrosConsultaView phCilindrosView = new PHCilindrosConsultaView();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                phCilindrosView.CodigoCil = ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                phCilindrosView.SerieCil = ((TextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                phCilindrosView.MarcaCil = ((Label)filaGrilla.FindControl("txtMarcaCil")).Text.ToUpper();
                phCilindrosView.CapacidadCil = ((Label)filaGrilla.FindControl("txtCapacidadCil")).Text.ToUpper();
                phCilindrosView.MesFabricacionCil = ((TextBox)filaGrilla.FindControl("txtMesFabricacionCil")).Text.ToUpper();
                phCilindrosView.AnioFabricacionCil = ((TextBox)filaGrilla.FindControl("txtAnioFabricacionCil")).Text.ToUpper();
                phCilindrosView.CodigoVal = ((TextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                phCilindrosView.SerieVal = ((TextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                phCilindrosView.Observaciones = ((TextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();                
                phCilindrosView.IDEstadoPH = Guid.Empty;
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;

                phCilindrosView.CodigoCil = ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                phCilindrosView.SerieCil = ((TextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                phCilindrosView.MarcaCil = ((Label)filaGrilla.FindControl("txtMarcaCil")).Text.ToUpper();
                phCilindrosView.CapacidadCil = ((Label)filaGrilla.FindControl("txtCapacidadCil")).Text.ToUpper();
                phCilindrosView.MesFabricacionCil = ((TextBox)filaGrilla.FindControl("txtMesFabricacionCil")).Text.ToUpper();
                phCilindrosView.AnioFabricacionCil = ((TextBox)filaGrilla.FindControl("txtAnioFabricacionCil")).Text.ToUpper();
                phCilindrosView.CodigoVal = ((TextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                phCilindrosView.SerieVal = ((TextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                phCilindrosView.Observaciones = ((TextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();                
                phCilindrosView.IDEstadoPH = Guid.Empty;            
            }

            isValid += this.ValidacionesGenerales(phCilindrosView);           

            if (isValid == String.Empty)
            {
                tabla.Add(phCilindrosView);

                gdv.DataSource = tabla;
                gdv.ShowFooter = PermiteAgregar;
                gdv.DataBind();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }

            this.SetFocus();
        }

        private string ValidacionesGenerales(PHCilindrosConsultaView dfc)
        {
            String mensaje = String.Empty;

            //if (String.IsNullOrEmpty(dfc.CodigoReg))
            //{ mensaje += "- Debe ingresar código de homologación del regulador. </br>"; }
            //else
            //{
            //    if(dfc.CodigoReg.Length != 4) mensaje += "- El código de homologación del regulador es incorrecto. </br>";
            //}
            //if (String.IsNullOrEmpty(dfc.NroSerieReg)) mensaje += "- Debe ingresar número de serie del regulador. </br>";

            return mensaje;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleCilPH"] == null)
                ViewState["lstDetalleCilPH"] = tabla;
            else
                tabla = (List<PHCilindrosConsultaView>)ViewState["lstDetalleCilPH"];

            if (!Page.IsPostBack)
            {

            }            
        }

        protected void gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosView item = e.Row.DataItem as PHCilindrosView;

                String eventoCargarDatos = $"cargarDatos('{e.Row.RowIndex}','{item.CodigoCil}','{item.SerieCil}','{item.MarcaCil}','{item.CapacidadCil}','{item.MesFabricacionCil}','{item.AnioFabricacionCil}','{item.CodigoVal}', '{item.SerieVal}','{item.Observaciones}')";
                e.Row.Cells[9].Attributes.Add("onclick", eventoCargarDatos);
                e.Row.Cells[9].Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                var cboEstadosPhConsulta = (DropDownList)e.Row.FindControl("cboEstadosPhConsulta");
                //var modificaEstado = bool.Parse(gdv.DataKeys[e.Row.RowIndex].Values["ModificaEstado"].ToString());
                cboEstadosPhConsulta.Visible = this.ModificaEstado;
            }
        }

        protected void BtnAceptar_ServerClick(object sender, EventArgs e)
        {
            this.tabla = this.CilindrosPHCargados;

            int idx = int.Parse(hdnRowIndex.Value);
            var item = this.tabla[idx];
            item.CodigoCil = txtCodigoCilMod.Value;
            item.SerieCil = txtSerieCilMod.Value;
            item.MarcaCil = txtMarcaCilMod.Value;
            item.CapacidadCil = txtCapacidadCilMod.Value;
            item.MesFabricacionCil = txtMesFabricacionCilMod.Value;
            item.AnioFabricacionCil = txtAnioFabricacionCilMod.Value;
            item.CodigoVal = txtCodigoValMod.Value;
            item.SerieVal = txtSerieValMod.Value;
            item.Observaciones = txtObservacionesMod.Value;

            
            gdv.DataSource = this.tabla;
            gdv.ShowFooter = PermiteAgregar;
            gdv.DataBind();

        }

        #endregion
    }
}