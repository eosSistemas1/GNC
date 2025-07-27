using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.UI.UserControls;

namespace PetroleraManager.Web.UserControls
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
                List<PHCilindrosView> tabla2 = new List<PHCilindrosView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        PHCilindrosView ocxv = new PHCilindrosView()
                        {
                            ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString()),
                            CapacidadCil = ((Label)gr.FindControl("lblCapacidadCil")).Text,
                            CodigoCil = ((Label)gr.FindControl("lblCodigoCil")).Text,
                            CodigoVal = ((Label)gr.FindControl("lblCodigoVal")).Text,
                            MesFabricacionCil = ((Label)gr.FindControl("lblMesFabricacionCil")).Text,
                            AnioFabricacionCil = ((Label)gr.FindControl("lblAnioFabricacionCil")).Text,
                            MarcaCil = ((Label)gr.FindControl("lblMarcaCil")).Text,
                            SerieCil = ((Label)gr.FindControl("lblSerieCil")).Text,
                            SerieVal = ((Label)gr.FindControl("lblSerieVal")).Text,
                            Observaciones = ((Label)gr.FindControl("lblObservaciones")).Text,
                        };
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
                ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Focus();
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Focus();
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
                    tabla = CilindrosPHCargados;
                    tabla.RemoveAt(i);

                    gdv.DataSource = tabla;
                    gdv.DataBind();
                }

                if (gdv.Rows.Count == 0)
                {
                    var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                    ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Focus();
                }
                else
                {
                    GridViewRow filaGrilla = gdv.FooterRow;
                    ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Focus();
                }

                #endregion
            }
        }

        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            tabla = this.CilindrosPHCargados;

            String isValid = String.Empty;
            PHCilindrosView dfc = new PHCilindrosView();

            String mes = String.Empty;
            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];

                dfc.CodigoCil = ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Value.ToUpper();
                dfc.SerieCil = ((HtmlInputText)filaGrilla.FindControl("txtSerieCil")).Value.ToUpper();
                dfc.MarcaCil = ((HtmlInputText)filaGrilla.FindControl("txtMarcaCil")).Value.ToUpper();
                dfc.CapacidadCil = ((HtmlInputText)filaGrilla.FindControl("txtCapacidadCil")).Value.ToUpper();
                dfc.MesFabricacionCil = ((HtmlInputText)filaGrilla.FindControl("txtMesFabricacionCil")).Value;
                dfc.AnioFabricacionCil = ((HtmlInputText)filaGrilla.FindControl("txtAnioFabricacionCil")).Value;
                dfc.CodigoVal = ((HtmlInputText)filaGrilla.FindControl("txtCodigoVal")).Value.ToUpper();
                dfc.SerieVal = ((HtmlInputText)filaGrilla.FindControl("txtSerieVal")).Value.ToUpper();
                dfc.Observaciones = ((HtmlInputText)filaGrilla.FindControl("txtObservaciones")).Value.ToUpper();
                mes = ((HtmlInputText)filaGrilla.FindControl("txtMesFabricacionCil")).Value;
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;

                dfc.CodigoCil = ((HtmlInputText)filaGrilla.FindControl("txtCodigoCil")).Value.ToUpper();
                dfc.SerieCil = ((HtmlInputText)filaGrilla.FindControl("txtSerieCil")).Value.ToUpper();
                dfc.MarcaCil = ((HtmlInputText)filaGrilla.FindControl("txtMarcaCil")).Value.ToUpper();
                dfc.CapacidadCil = ((HtmlInputText)filaGrilla.FindControl("txtCapacidadCil")).Value.ToUpper();
                dfc.MesFabricacionCil = ((HtmlInputText)filaGrilla.FindControl("txtMesFabricacionCil")).Value;
                dfc.AnioFabricacionCil = ((HtmlInputText)filaGrilla.FindControl("txtAnioFabricacionCil")).Value;
                dfc.CodigoVal = ((HtmlInputText)filaGrilla.FindControl("txtCodigoVal")).Value.ToUpper();
                dfc.SerieVal = ((HtmlInputText)filaGrilla.FindControl("txtSerieVal")).Value.ToUpper();
                dfc.Observaciones = ((HtmlInputText)filaGrilla.FindControl("txtObservaciones")).Value.ToUpper();
                mes = ((HtmlInputText)filaGrilla.FindControl("txtMesFabricacionCil")).Value;
            }

            dfc.ID = Guid.NewGuid();
            

            isValid += this.ValidacionesGenerales(dfc, mes);

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "BuscarMarca();", true);
            }

                       
            this.SetFocus();
        }

        private string ValidacionesGenerales(PHCilindrosView dfc, string mes)
        {
            String mensaje = String.Empty;

            if (String.IsNullOrEmpty(dfc.CodigoCil))
            { mensaje += "- Debe ingresar código de homologación del cilindro. </br>"; }
            else
            {
                if (dfc.CodigoCil.Length != 4) mensaje += "- El código de homologación del cilindro es incorrecto. </br>";
            }
            if (String.IsNullOrEmpty(dfc.SerieCil)) mensaje += "- Debe ingresar número de serie del cilindro. </br>";

            if(!String.IsNullOrWhiteSpace(mes))
            {
                int m = int.Parse(mes);
                if(m < 1 || m > 12) mensaje += "- Debe ingresar un mes entre 1 y 12. </br>";
            }

            return mensaje;
        }

     

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();
            }
        }

        #endregion
    }
}