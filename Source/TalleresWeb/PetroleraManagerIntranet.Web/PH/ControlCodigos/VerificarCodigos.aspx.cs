using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ControlCodigos
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
                hdnUsuarioID.Value = this.UsuarioID.ToString();

                this.CargarCilindros();
            }
        }

        private void CargarCilindros()
        {

            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHParaVerificarCodigos();

            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"PENDIENTES ({cilindros.Count})";
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PHCilindrosPendientesView item = e.Row.DataItem as PHCilindrosPendientesView;
                if (item.IDEstadoPH == EstadosPH.VerificarCodigos)
                {
                    String serieCilindro = !String.IsNullOrWhiteSpace(item.NumeroSerieCilindro) ? item.NumeroSerieCilindro.Trim().ToUpper() : String.Empty;
                    String homoCilindro = !String.IsNullOrWhiteSpace(item.CodigoHomologacionCilindro) ? item.CodigoHomologacionCilindro.Trim().ToUpper() : String.Empty;
                    String serieValvula = !String.IsNullOrWhiteSpace(item.NumeroSerieValvula) ? item.NumeroSerieValvula.Trim().ToUpper() : String.Empty;
                    String homoValvula = !String.IsNullOrWhiteSpace(item.CodigoHomologacionValvula) ? item.CodigoHomologacionValvula.Trim().ToUpper() : String.Empty;

                    String evento = $"openModal('{item.ID}', " +
                                    $"'{item.NroOperacionCRPC}', " +
                                    $"'{item.Dominio.Trim().ToUpper()}', " +
                                    $"'{item.Taller.Trim().ToUpper()}', " +
                                    $"'{serieCilindro}', " +
                                    $"'{homoCilindro}', " +
                                    $"'{serieValvula}', " +
                                    $"'{homoValvula}' )";
                    e.Row.Attributes.Add("onclick", evento);
                }
                else if (item.IDEstadoPH == EstadosPH.IngresarEnLinea)
                {
                    String evento = $"imprimir('{item.ID}')";
                    e.Row.Attributes.Add("onclick", evento);
                }

                e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                ObtenerColorEstado(e, item.IDEstadoPH);
            }
        }

        private static void ObtenerColorEstado(GridViewRowEventArgs e, Guid estadoID)
        {
            if (estadoID == EstadosPH.VerificarCodigos)
            {
                e.Row.Cells[5].CssClass = "estado-amarillo";
            }
            if (estadoID == EstadosPH.IngresarEnLinea)
            {
                e.Row.Cells[5].CssClass = "estado-verde";
            }
            else
            {
                e.Row.Cells[5].CssClass = "estado-rojo";
            }
        }

        [System.Web.Services.WebMethod]
        public static string Aceptar(string id,
                                     string nroSerieCilLeido,
                                     string codHomoCilLeido,
                                     string nroSerieValLeido,
                                     string codHomoValLeido,
                                     Boolean solicitarRevision,
                                     string usuarioID)
        {
            PHCilindrosVerificarCodigosParameter phCilindrosVerificarCodigosParameter = new PHCilindrosVerificarCodigosParameter()
            {
                ID = new Guid(id),
                SolicitarRevision = solicitarRevision,
                NumeroSerieCilLeido = nroSerieCilLeido,
                CodigoHomologacionCilLeido = codHomoCilLeido,
                NumeroSerieValLeido = nroSerieValLeido,
                CodigoHomologacionValLeido = codHomoValLeido,
                UsuarioID = new Guid(usuarioID)
            };

            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            phCilindrosLogic.SolicitarVerificarCodigos(phCilindrosVerificarCodigosParameter);

            return id;
        }
        #endregion
    }
}