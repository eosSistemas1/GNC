using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ConsultaCC
{
    public partial class VerificarCodigos : PageBase
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
                lblTitulo.Text = "VERIFICAR CÓDIGOS";
                hdnUsuarioID.Value = this.UsuarioID.ToString();
                this.CargarCilindros();
            }
        }

        private void CargarCilindros()
        {

            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHPorEstado(EstadosPH.SolicitaVerificacion);

            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"CILINDROS ({cilindros.Count})";
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosPendientesView item = e.Row.DataItem as PHCilindrosPendientesView;

                var estado = new PHCilindroHistoricoEstadoLogic().ReadUltimoEstadoByIDPhCilindro(item.ID);
                var observaciones = estado.Descripcion.Split('-').ToList();

                String homoCilOriginal = item.CodigoHomologacionCilindro.Trim();
                String serieCilOriginal = item.NumeroSerieCilindro.Trim();
                String homoValOriginal = item.CodigoHomologacionValvula.Trim();
                String serieValOriginal = item.NumeroSerieValvula.Trim();

                String homoCilLeido = this.DevolverValorLeido(observaciones[0]);
                String serieCilLeido = this.DevolverValorLeido(observaciones[1]);
                String homoValLeido = this.DevolverValorLeido(observaciones[2]);
                String serieValLeido = this.DevolverValorLeido(observaciones[3]);


                String eventoCargarDatos = $"cargarDatos('{item.ID}', '{serieCilLeido}', '{homoCilLeido}', '{serieValLeido}', '{homoValLeido}', '{serieCilOriginal}', '{homoCilOriginal}', '{serieValOriginal}', '{homoValOriginal}')";
                e.Row.Cells[5].Attributes.Add("onclick", eventoCargarDatos);
                e.Row.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='pointer';");

            }
        }

        private string DevolverValorLeido(string valor)
        {
            if (!String.IsNullOrWhiteSpace(valor))
            {
                var v = valor.Split(':').Last();
                return v.Trim();
            }

            return String.Empty;
        }

        [System.Web.Services.WebMethod]
        public static void AceptarVerificarCodigos(string id, string serieCil, string homoCil, string serieVal, string homoVal, string idUsuario)
        {
            Guid IDPhCilindros = new Guid(id);
            Guid IDUsuario = new Guid(idUsuario);
            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.AceptarVerificarCodigos(IDPhCilindros, serieCil, homoCil, serieVal, homoVal, IDUsuario);
        }
        #endregion
    }
}