using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ObleasReimpresionTarjetaVerde : PageBase
    {
        #region Members
        private ObleasLogic obleasLogic;
        #endregion

        #region Properties
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null)
                    obleasLogic = new ObleasLogic();

                return obleasLogic;
            }
        }
        #endregion

        #region Methods        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                grdObleas.DataSource = null;
                grdObleas.DataBind();

                String valido = String.Empty;

                if (txtNroOblea.Text != String.Empty)
                {
                    var oblea = ObleasLogic.ReadAllByNroObleaNueva(txtNroOblea.Text);

                    if (oblea != null
                        && oblea.IdEstadoFicha != CrossCutting.DatosDiscretos.ESTADOSFICHAS.Bloqueada
                        && oblea.IdEstadoFicha != CrossCutting.DatosDiscretos.ESTADOSFICHAS.Eliminada)
                    { this.ImprimirTarjetaVerde(oblea.ID); }
                    else
                    { valido += " - No se puede imprimir, la Oblea esta eliminada o bloqueada."; }
                }
                else if (txtDominio.Text != String.Empty)
                {
                    grdObleas.DataSource = ObleasLogic.ReadAllConsultaEnBase(new ObleasParameters() { Dominio = txtDominio.Text });
                    grdObleas.DataBind();
                }
                else
                {
                    valido += " - Debe ingresar un filtro.";
                }

                if (!String.IsNullOrEmpty(valido))
                {
                    MessageBoxCtrl1.MessageBox(null, valido, MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl1.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void ImprimirTarjetaVerde(Guid obleaID)
        {
            var oblea = ObleasLogic.Read(obleaID);

            if (oblea == null
                   || String.IsNullOrEmpty(oblea.NroObleaNueva)
                   || oblea.IdEstadoFicha == CrossCutting.DatosDiscretos.ESTADOSFICHAS.Bloqueada
                   || oblea.IdEstadoFicha == CrossCutting.DatosDiscretos.ESTADOSFICHAS.Eliminada)
            {
                MessageBoxCtrl1.MessageBox(null, " - No se puede imprimir, la Oblea esta eliminada o bloqueada.", MessageBoxCtrl.TipoWarning.Warning);
            }
            else
            {
                Imprimir(obleaID);                
            }
        }

        private void Imprimir(Guid idOblea)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupImprimir('" + idOblea + "');", true);
        }
        #endregion

        protected void grdObleas_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                HtmlAnchor img = (HtmlAnchor)e.Row.Cells[4].FindControl("seleccionar");
                img.Disabled = String.IsNullOrWhiteSpace(e.Row.Cells[2].Text.Replace("&nbsp;",String.Empty)) ? true : false;

                e.Row.Cells[3].Text = e.Row.Cells[3].Text == GetDinamyc.MinDatetime.ToString("dd/MM/yyyy") ? String.Empty : e.Row.Cells[3].Text;
            }
        }
    }
}