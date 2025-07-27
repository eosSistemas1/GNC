using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.Proceso
{
    public partial class SeleccionarCilindro : PageBase
    {
        #region Properties
        private PHCilindrosLogic phCilindrosLogic;
        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string estacion = Request.QueryString["estacion"].ToString();

                lblTitulo.Text = ProcesoPHHelper.ObtenerNombreEstacion(estacion);                

                this.CargarPendientes(estacion);
            }
        }

        private void CargarPendientes(string estacion)
        {

            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPendientesByPaso(estacion);

            if (cilindros.Any())
            {
                grdCilindros.DataSource = cilindros.OrderBy(x => x.NroOperacionCRPC);
                grdCilindros.DataBind();

                lblTituloPendientes.Text = $"PENDIENTES ({cilindros.Count})";
            }
            else
            {
                Response.Redirect("Index.aspx", true);
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosPendientesView item = e.Row.DataItem as PHCilindrosPendientesView;
                e.Row.Attributes.Add("onclick", "openWindow('" + item.ID + "')");
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
            }
        }
    }
}