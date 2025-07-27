using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

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
        }

        protected void grdFichas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow filaGrilla = e.Row;
                Guid estadoObleaID = new Guid(grdFichas.DataKeys[e.Row.RowIndex].Values["IdEstadoFicha"].ToString());

                bool imprimirVisible = estadoObleaID == ESTADOSFICHAS.Asignada
                                        || estadoObleaID == ESTADOSFICHAS.AsignadaConError
                                        || estadoObleaID == ESTADOSFICHAS.Finalizada
                                        || estadoObleaID == ESTADOSFICHAS.Entregada;

                ((HtmlAnchor)filaGrilla.Cells[2].FindControl("btnImprimir")).Visible = imprimirVisible;

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
           
            ObleasParameters oParam = ObtenerParametros();
            
            List<string> mensajes = Validar(oParam);

            if (!mensajes.Any())
            {
                ObleasLogic ol = new ObleasLogic();
                var obleas = ol.ReadAllInformeEstadoObleas(oParam);

                bool limitarCantidad = (!string.IsNullOrEmpty(oParam.CodigoHomologacionRegulador) && string.IsNullOrEmpty(oParam.SerieReg))
                                    || (!string.IsNullOrEmpty(oParam.CodigoHomologacionCilindro) && string.IsNullOrEmpty(oParam.SerieCil))
                                    || (!string.IsNullOrEmpty(oParam.CodigoHomologacionValvula) && string.IsNullOrEmpty(oParam.SerieVal));


                grdFichas.DataSource = !limitarCantidad ? obleas : obleas.Take(100);
                grdFichas.DataBind();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensajes, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<string> Validar(ObleasParameters oParam)
        {
            List<string> mensajes = new List<string>();

            if (string.IsNullOrWhiteSpace(oParam.Dominio)
                && string.IsNullOrWhiteSpace(oParam.NroObleaNueva)
                && string.IsNullOrWhiteSpace(oParam.NroDocCliente)
                && string.IsNullOrWhiteSpace(oParam.CodigoHomologacionRegulador)
                && string.IsNullOrWhiteSpace(oParam.NumeroSerieRegulador)
                && string.IsNullOrWhiteSpace(oParam.CodigoHomologacionCilindro)
                && string.IsNullOrWhiteSpace(oParam.NumeroSerieCilindro)
                && string.IsNullOrWhiteSpace(oParam.CodigoHomologacionValvula)
                && string.IsNullOrWhiteSpace(oParam.NumeroSerieValvula)) mensajes.Add("Debe ingresar al menos un filtro");
           
            return mensajes;
        }

        private ObleasParameters ObtenerParametros()
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
            return oParam;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.ToString(), false);
        }
    }
}