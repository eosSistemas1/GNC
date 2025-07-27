using PetroleraManagerIntranet.Web.Controls;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.PH.UserControls
{
    public partial class CilindrosPH : System.Web.UI.UserControl
    {
        #region Members

        private List<PHCilindrosView> tabla = new List<PHCilindrosView>();

        #endregion

        #region Properties

        public List<PHCilindrosView> CilindrosPHCargados
        {
            get
            {                
                //List<PHCilindrosView> tabla2 = new List<PHCilindrosView>();
                //foreach (GridViewRow row in gdv.Rows)
                //{
                //    if (row.RowType == DataControlRowType.DataRow)
                //    {
                //        PHCilindrosView phCilindrosView = new PHCilindrosView();
                //        phCilindrosView.CodigoCil = ((TextBox)row.FindControl("txtCodigoCil")).Text.ToUpper();
                //        phCilindrosView.SerieCil = ((TextBox)row.FindControl("txtSerieCil")).Text.ToUpper();
                //        phCilindrosView.MarcaCil = ((TextBox)row.FindControl("txtMarcaCil")).Text.ToUpper();
                //        phCilindrosView.CapacidadCil = ((TextBox)row.FindControl("txtCapacidadCil")).Text.ToUpper();
                //        phCilindrosView.FechaFabricacionCil = ((TextBox)row.FindControl("txtFechaFabricacionCil")).Text.ToUpper();
                //        phCilindrosView.CodigoVal = ((TextBox)row.FindControl("txtCodigoVal")).Text.ToUpper();
                //        phCilindrosView.SerieVal = ((TextBox)row.FindControl("txtSerieVal")).Text.ToUpper();
                //        phCilindrosView.Observaciones = ((TextBox)row.FindControl("txtObservaciones")).Text.ToUpper();                       
                //        tabla2.Add(phCilindrosView);
                //    }
                //}

                return tabla;
            }
            set
            {
                tabla = value;

                ViewState["lstDetalleCilPH"] = tabla;

                gdv.DataSource = tabla;
                gdv.DataBind();                
            }
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
            PHCilindrosView phCilindrosView = new PHCilindrosView();
            decimal mesFabricacionCil;
            decimal anioFabricacionCil;

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                decimal.TryParse(((TextBox)filaGrilla.FindControl("txtMesFabricacionCil")).Text, out mesFabricacionCil);
                decimal.TryParse(((TextBox)filaGrilla.FindControl("txtAnioFabricacionCil")).Text, out anioFabricacionCil);

                phCilindrosView.CodigoCil = ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                phCilindrosView.SerieCil = ((TextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                phCilindrosView.MarcaCil = ((Label)filaGrilla.FindControl("txtMarcaCil")).Text;
                phCilindrosView.CapacidadCil = ((Label)filaGrilla.FindControl("txtCapacidadCil")).Text.ToUpper();
                phCilindrosView.MesFabricacionCil = mesFabricacionCil.ToString("00");
                phCilindrosView.AnioFabricacionCil = anioFabricacionCil.ToString("00");
                phCilindrosView.CodigoVal = ((TextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                phCilindrosView.SerieVal = ((TextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                phCilindrosView.Observaciones = ((TextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;

                decimal.TryParse(((TextBox)filaGrilla.FindControl("txtMesFabricacionCil")).Text, out mesFabricacionCil);
                decimal.TryParse(((TextBox)filaGrilla.FindControl("txtAnioFabricacionCil")).Text, out anioFabricacionCil);

                phCilindrosView.CodigoCil = ((TextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                phCilindrosView.SerieCil = ((TextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                phCilindrosView.MarcaCil = ((Label)filaGrilla.FindControl("txtMarcaCil")).Text;
                phCilindrosView.CapacidadCil = ((Label)filaGrilla.FindControl("txtCapacidadCil")).Text.ToUpper();
                phCilindrosView.MesFabricacionCil = mesFabricacionCil.ToString("00");
                phCilindrosView.AnioFabricacionCil = anioFabricacionCil.ToString("00");
                phCilindrosView.CodigoVal = ((TextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                phCilindrosView.SerieVal = ((TextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                phCilindrosView.Observaciones = ((TextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();
            }

            isValid += this.ValidacionesGenerales(phCilindrosView);

            if (isValid == String.Empty)
            {

                tabla.Add(phCilindrosView);

                gdv.DataSource = tabla;
                gdv.DataBind();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }

            this.SetFocus();
        }

        private string ValidacionesGenerales(PHCilindrosView dfc)
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
                tabla = (List<PHCilindrosView>)ViewState["lstDetalleCilPH"];

            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();
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
            }
        }

        protected void BtnAceptar_ServerClick(object sender, EventArgs e)
        {
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

            gdv.DataSource = tabla;
            gdv.DataBind();

        }
        #endregion

    }
}