using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites
{
    public partial class EstadosTramites : PageBase
    {
        #region Properties
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }            
        }

        private readonly Color evenColor = Color.White;
        private readonly Color oddColor = Color.LightGray;
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LeerTramites();
        }

        private void LeerTramites()
        {
            Guid idTaller = SiteMaster.Taller.ID;
            List<EstadosTramitesView> tramites  = this.ObleasLogic.ReadTramitesByTallerID(idTaller);

            var obleas = tramites.Where(x => x.TipoTramite == "OBLEA");
            var ph = tramites.Where(x => x.TipoTramite == "PH");
            var pedido = tramites.Where(x => x.TipoTramite == "PEDIDO");

            lblObleas.Text = $"OBLEAS ({obleas.Count()})";
            grdObleas.DataSource = obleas;
            grdObleas.DataBind();

            lblPH.Text = $"Pruebas hidráulicas ({ph.Count()})";
            grdPH.DataSource = ph;
            grdPH.DataBind();

            lblPedidos.Text = $"Pedidos de Mercadería ({pedido.Count()})";
            grdPedido.DataSource = pedido;
            grdPedido.DataBind();
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grilla = sender as GridView;
                Guid estadoID = (Guid)grilla.DataKeys[e.Row.RowIndex].Values["IdEstado"];

                ObtenerColorEstado(e, estadoID);

                SetearColorFilaGrilla(e, grilla);
            }
        }

        private void SetearColorFilaGrilla(GridViewRowEventArgs e, GridView grilla)
        {
            var filaActual = e.Row;

            if (filaActual.RowIndex == 0) e.Row.BackColor = evenColor;

            if (filaActual.RowIndex > 0)
            {
                var filaAnterior = grilla.Rows[e.Row.RowIndex - 1];

                if (filaActual.Cells[3].Text != filaAnterior.Cells[3].Text)
                {
                    filaActual.BackColor = oddColor;
                    if (filaAnterior.BackColor == oddColor)
                        e.Row.BackColor = evenColor;
                }
                else
                {
                    filaActual.BackColor = filaAnterior.BackColor;
                }
            }
        }

        private static void ObtenerColorEstado(GridViewRowEventArgs e, Guid estadoID)
        {
            if (estadoID == ESTADOSFICHAS.Bloqueada || estadoID == EstadosPH.Bloqueada)
            {
                e.Row.Cells[6].CssClass = "estado-rojo";
            }
            else if (estadoID == ESTADOSFICHAS.Finalizada
                      || estadoID == ESTADOSFICHAS.FinalizadaConError
                      || estadoID == EstadosPH.Finalizada
                      //|| estadoID == ESTADOSPEDIDOS.Finalizado
                      )
            {
                e.Row.Cells[6].CssClass = "estado-verde";
                
            }
            else
            {
                e.Row.Cells[6].CssClass = "estado-amarillo";
            }
        }
        #endregion

        protected void btnVer_Click(object sender, EventArgs e)
        {
            //Button b = sender as Button;
            ////Guid ID = new Guid(b.CommandArgument);
            // MPE.Show();
        }
    }
}