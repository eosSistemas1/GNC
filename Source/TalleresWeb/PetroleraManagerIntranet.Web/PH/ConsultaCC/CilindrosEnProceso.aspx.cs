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

namespace PetroleraManagerIntranet.Web.PH.ConsultaCC
{
    public partial class CilindrosEnProceso : PageBase
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
                lblTitulo.Text = "Cilindros en proceso";
                lblCodigoCRPC.Text = CrossCutting.DatosDiscretos.CRPC.CodigoCRPC;
                hdnUsuarioID.Value = this.UsuarioID.ToString();
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

                string eventoImprimir = $"imprimir('{item.ID}')";
                string eventoEliminar = $"eliminar('{item.ID}', '{this.UsuarioID.ToString()}')";

                string nroCertificado = item.NroOperacionCRPC.HasValue ? item.NroOperacionCRPC.Value.ToString("000000") : string.Empty;
                string eventoCargarDatos = $"cargarDatos('{item.ID}', '{nroCertificado}')";

                e.Row.Cells[5].Attributes.Add("onclick", eventoImprimir);
                e.Row.Cells[6].Attributes.Add("onclick", eventoEliminar);
                e.Row.Cells[7].Attributes.Add("onclick", eventoCargarDatos);

                e.Row.Cells[5].Attributes.Add("onmouseover", "this.style.cursor='pointer';");
                e.Row.Cells[6].Attributes.Add("onmouseover", "this.style.cursor='pointer';");
                e.Row.Cells[7].Attributes.Add("onmouseover", "this.style.cursor='pointer';");
            }
        }

        [System.Web.Services.WebMethod]
        public static void Eliminar(string id, string idUsuario)
        {
            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.CambiarEstado(Guid.Parse(id), EstadosPH.Bloqueada, "Bloqueada en proceso", Guid.Parse(idUsuario));
        }

        [System.Web.Services.WebMethod]
        public static void AceptarCargarResultados(string id, string resultado, string numeroCertificado, string idUsuario, string observaciones)
        {
            if (String.IsNullOrWhiteSpace(numeroCertificado))
                throw new Exception("Debe ingresar número de certificado.");

            Guid idPHCilindro = Guid.Parse(id);
            Boolean aprobado = Boolean.Parse(resultado);
            Guid usuarioID = Guid.Parse(idUsuario);

            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.ActualizarResultadoPH(idPHCilindro, aprobado, numeroCertificado, usuarioID, observaciones, true);
        }
        #endregion
    }
}