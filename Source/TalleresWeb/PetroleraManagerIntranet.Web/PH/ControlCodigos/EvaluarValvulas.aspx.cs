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
    public partial class EvaluarValvulas : PageBase
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
                lblTitulo.Text = "EVALUAR VÁLVULAS";
                hdnUsuarioID.Value = this.UsuarioID.ToString();
                this.CargarCilindros();
            }
        }

        private void CargarCilindros()
        {

            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHParaEvaluarValvulas();

            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"CILINDROS ({cilindros.Count})";
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosPendientesView item = e.Row.DataItem as PHCilindrosPendientesView;

                string Taller = item.Taller;
                string NrointOperacionCRPC = item.NroOperacionCRPC.HasValue ? item.NroOperacionCRPC.Value.ToString() : String.Empty;
                string Dominio = item.Dominio;
                string CodHomolValvula = item.CodigoHomologacionValvula;
                string NroSerieValvula = item.NumeroSerieValvula;


                String eventoCargarDatos = $"cargarDatos('{item.ID}','{Taller}','{NrointOperacionCRPC}','{Dominio}','{CodHomolValvula}','{NroSerieValvula}')";
                e.Row.Cells[5].Attributes.Add("onclick", eventoCargarDatos);
                e.Row.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='pointer';");


            }
        }

        [System.Web.Services.WebMethod]
        public static void AceptarEvaluacionValvula(string id, string func, string rosca, string observacion, string idUsuario)
        {
            Guid IDPhCilindros = new Guid(id);
            Guid IDUsuario = new Guid(idUsuario);
            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.AceptarEvaluacionValvula(IDPhCilindros, func, rosca, observacion, IDUsuario);
        }
        #endregion
    }
}