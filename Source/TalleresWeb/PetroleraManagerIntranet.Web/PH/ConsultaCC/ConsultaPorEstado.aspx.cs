using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ConsultaCC
{
    public partial class ConsultaPorEstado : PageBase
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
                lblTitulo.Text = "Consulta por estados";

                fDesde.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                fHasta.Value = DateTime.Now.ToString("yyyy-MM-dd");

                CargarComboEstados();
            }
        }

        private void CargarComboEstados()
        {
            List<ViewEntity> lista = new List<ViewEntity>();
            lista.Add(new ViewEntity(EstadosPH.Bloqueada, "Bloqueada"));
            lista.Add(new ViewEntity(EstadosPH.Despachada, "Despachada"));
            lista.Add(new ViewEntity(EstadosPH.EnEsperaCilindros, "En espera de cilindros"));
            lista.Add(new ViewEntity(EstadosPH.EnProceso, "En proceso"));
            lista.Add(new ViewEntity(EstadosPH.Entregada, "Entregada"));
            lista.Add(new ViewEntity(EstadosPH.Finalizada, "Finalizada"));
            lista.Add(new ViewEntity(EstadosPH.Ingresada, "Ingresada"));
            lista.Add(new ViewEntity(EstadosPH.IngresarEnLinea, "Ingresar en línea"));
            lista.Add(new ViewEntity(EstadosPH.SolicitaVerificacion, "Solicitar verificación"));
            lista.Add(new ViewEntity(EstadosPH.VerificarCodigos, "Verificar códigos"));

            cboEstados.DataTextField = "Descripcion";
            cboEstados.DataValueField = "ID";
            cboEstados.DataSource = lista;
            cboEstados.DataBind();

        }

        protected void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CargarCilindros();
        }

        protected void Recargar(object sender, EventArgs e)
        {
            this.CargarCilindros();
        }

        private void CargarCilindros()
        {
            Guid idEstado = new Guid(cboEstados.SelectedValue);
            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHPorEstado(idEstado);

            if (idEstado == EstadosPH.EnProceso)
            {
                var phExcelGenerado = this.PHCilindrosLogic.ReadCilindrosPHPorEstado(EstadosPH.ExcelGenerado);
                cilindros.AddRange(phExcelGenerado);                
            }

            grdCilindros.Columns[6].Visible = idEstado == EstadosPH.EnProceso;

            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MaxValue;

            DateTime.TryParse(fDesde.Value, out fechaDesde);
            DateTime.TryParse(fHasta.Value, out fechaHasta);

            grdCilindros.DataSource = cilindros.Where(c => c.FechaOperacion >= fechaDesde && c.FechaOperacion <= fechaHasta);
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"CILINDROS ({cilindros.Count})";
        }
        #endregion
    }
}