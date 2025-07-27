using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ReInformarFichaEntregada : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            var numeroObleaAsignada = txtNroOblea.Text;
            var dominio = txtDominio.Text;
            List<ObleasExtendedView> obleasAReinformar = ObleasLogic.ReadFichaParaReInformarFichaEntregada(numeroObleaAsignada, dominio);

            grdObleas.DataSource = obleasAReinformar;
            grdObleas.DataBind();
        }

        protected void grdObleas_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text == GetDinamyc.MinDatetime.ToString("dd/MM/yyyy") ? String.Empty : e.Row.Cells[3].Text;
            }
        }

        protected void grdObleas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                #region MODIFICAR Estado
                if (e.CommandName == "modificar")
                {
                    Guid id = Guid.Parse(e.CommandArgument.ToString());

                    ObleasLogic.CambiarEstado(id, ESTADOSFICHAS.Aprobada, "Re-informar ficha", this.UsuarioID);

                    MessageBoxCtrl1.MessageBox(null, "La oblea se modificó correctamente. Está lista para informar", "ReInformarFichaEntregada.aspx", MessageBoxCtrl.TipoWarning.Warning);
                }
                #endregion
            }
                
        }
    }
}