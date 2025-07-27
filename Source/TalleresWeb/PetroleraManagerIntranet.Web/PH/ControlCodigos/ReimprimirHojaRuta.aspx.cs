using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ControlCodigos
{
    public partial class ReimprimirHojaRuta : PageBase
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

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitulo.Text = "REIMPRIMIR HOJA DE RUTA";                

                this.CargarCilindros();
            }
        }

        private void CargarCilindros()
        {

            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHParaReimprimirHojaDeRuta();

            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"CILINDROS ({cilindros.Count})";
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosPendientesView item = e.Row.DataItem as PHCilindrosPendientesView;

                String eventoImprimir = $"imprimir('{item.ID}')";
                e.Row.Cells[5].Attributes.Add("onclick", eventoImprimir);

                e.Row.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='pointer';");
            }
        }

        [System.Web.Services.WebMethod]
        public static void Eliminar(string id, string idUsuario)
        {
            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.CambiarEstado(Guid.Parse(id), EstadosPH.Bloqueada, "Bloqueada en proceso", Guid.Parse(idUsuario));
        }
        #endregion
    }
}