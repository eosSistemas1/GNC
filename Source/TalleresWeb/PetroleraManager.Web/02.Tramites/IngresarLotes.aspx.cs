using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.DataAccess;
using PetroleraManager.Logic;
using PetroleraManager.Entities;
using System.Drawing;

namespace PetroleraManager.Web.Tramites
{
   public partial class IngresarLotes : PL.Fwk.Presentation.Web.Pages.PLPageBase
    {
       LotesLogic logic = new LotesLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            var param = new LotesParameters();

            param.NroObleaDesde = txtFiltro.Text.Trim().Equals(String.Empty) ? 0 : Decimal.Parse(txtFiltro.Text.Trim());
            var LotesExt = logic.ReadExtendedView(param);
            grdFiltro.DataSource = LotesExt;
            grdFiltro.DataBind();
        }

        private void InicializarCampos()
        {
            //borrar los campos

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!uscNuevoLote1.Grabar())
            {
                MPE.Show();
            }
            else
            {
                Buscar();
            }
        }

        protected void grdFiltro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "eliminar")
            {
                try
                {
                    logic.Delete(idItem);
                }
                catch {
                    String mensaje = "El lote no puede ser eliminado. Verifique que no existan obleas asignadas.";
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + mensaje + "');", true);
                }
                
                Buscar();
            }

            else if (e.CommandName == "modificar")
            {

                uscNuevoLote1.CargarDatos(idItem);
                MPE.Show();
                Buscar();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            uscNuevoLote1.LimpiarCampos();
            MPE.Show();
            Buscar();
        }

        protected void grdFiltro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                Boolean loteActivo = Boolean.Parse(grdFiltro.DataKeys[e.Row.RowIndex].Values["LoteActivo"].ToString());
                if (loteActivo) e.Row.BackColor = Color.LawnGreen;

                e.Row.Cells[0].Text = grdFiltro.DataKeys[e.Row.RowIndex].Values["NroObleaDesde"].ToString() + "-" +
                                            grdFiltro.DataKeys[e.Row.RowIndex].Values["NroObleaHasta"].ToString();
            }
        }

    }
}