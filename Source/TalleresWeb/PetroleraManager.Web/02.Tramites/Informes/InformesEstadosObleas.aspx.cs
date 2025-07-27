using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetroleraManager.Web.UserControls;
using TalleresWeb.Entities;
using CrossCutting.DatosDiscretos;
using TalleresWeb.Logic;
using PL.Fwk.Presentation.Web.Controls;

namespace PetroleraManager.Web.Tramites.Informes
{
    public partial class InformesEstadosObleas : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                                
            }
        }

        protected void grdFichas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "modificar")
            {
                String url = String.Format("../ObleasIngresar.aspx?id={0}", idItem);
                Response.Redirect(url, true);
            }

            if (e.CommandName == "imprimir")
            {
                String url = String.Format("../ObleasImprimirTarjetaVerde.aspx?id={0}", idItem);
                PrintBoxCtrl1.PrintBox("Imprimir", url, "");
            }
        }

        protected void grdFichas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow filaGrilla = e.Row;
                Guid estadoObleaID = new Guid(grdFichas.DataKeys[e.Row.RowIndex].Values["IdEstadoFicha"].ToString());

                if (estadoObleaID.Equals(ESTADOSFICHAS.Asignada)
                    || estadoObleaID.Equals(ESTADOSFICHAS.AsignadaConError)
                    || estadoObleaID.Equals(ESTADOSFICHAS.Finalizada))
                {
                    ((PLImageButton)filaGrilla.Cells[2].FindControl("btnImprimir")).Visible = true;
                }
                else
                {
                    ((PLImageButton)filaGrilla.Cells[2].FindControl("btnImprimir")).Visible = false;
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ObleasParameters oParam = new ObleasParameters();
            oParam.TallerID = Guid.Empty;
            oParam.EstadoFichaID = Guid.Empty;
            oParam.Dominio = txtDominioVehiculo.Text;
            
            oParam.NroObleaNueva = txtNroObleaNueva.Text;

            oParam.FechaDesde = GetDinamyc.MinDatetime;
            oParam.FechaHasta = GetDinamyc.MaxDatetime;
            oParam.NroDocCliente = txtNroDocumento.Text;

            oParam.CodigoHomologacionRegulador = txtCodRegulador.Text;
            oParam.NumeroSerieRegulador = txtSerieRegulador.Text;

            oParam.CodigoHomologacionCilindro = txtCodCilindro.Text;
            oParam.NumeroSerieCilindro = txtSerieCilindro.Text;

            oParam.CodigoHomologacionValvula = txtCodValvula.Text;
            oParam.NumeroSerieValvula = txtSerieValvula.Text;

            ObleasLogic ol = new ObleasLogic();
            var obleas = ol.ReadAllInformeEstadoObleas(oParam);

            grdFichas.DataSource = obleas;
            grdFichas.DataBind();

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.ToString(), false);
        }

 
    }
}