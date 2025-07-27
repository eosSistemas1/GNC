using CarlosAg.ExcelXmlWriter;
using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ConsultaCC
{
    public partial class GenerarExcel : PageBase
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

        private CilindrosLogic cilindrosLogic;
        public CilindrosLogic CilindrosLogic
        {
            get
            {
                if (cilindrosLogic == null) cilindrosLogic = new CilindrosLogic();
                return cilindrosLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitulo.Text = "Generar Excel";
                calFechaD.Value = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
                calFechaH.Value = DateTime.Now.ToString("yyyy-MM-dd");
                this.CargarCilindros();
                this.BuscarArchivos();
            }
        }

        private void CargarCilindros()
        {
            List<PHCilindrosPendientesView> cilindros = this.PHCilindrosLogic.ReadCilindrosPHPorEstado(EstadosPH.EnProceso);

            grdCilindros.DataSource = cilindros;
            grdCilindros.DataBind();

            lblTituloPendientes.Text = $"CILINDROS ({cilindros.Count})";
        }
        #endregion

        protected void GenerarExcel_ServerClick(object sender, EventArgs e)
        {
            Workbook book = new Workbook();

            List<PHCilindros> seleccionadas = this.GetSeleccionadas();

            if (seleccionadas.Any())
            {

                foreach (var item in seleccionadas)
                {
                    item.NroOperacionCRPC = this.PHCilindrosLogic.SetearNumeroInternoOperacion(item.ID);

                    if (item.NroOperacionCRPC.HasValue)
                        this.PHCilindrosLogic.CambiarEstado(item.ID, EstadosPH.ExcelGenerado, "Excel Generado", this.UsuarioID);
                }

                var seleccionadasConNroInternoOperacion = seleccionadas.Where(x => x.NroOperacionCRPC.HasValue).ToList();

                Worksheet sheet1 = AgregarPrimerHoja(book, seleccionadasConNroInternoOperacion);
                Worksheet sheet2 = AgregarSegundaHoja(book, seleccionadasConNroInternoOperacion);

                CargarCilindros();

                String dirPath = MapPath("~/Archivos/PH/temp/").ToString();
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                string fileExcel = $"phAlberto_{DateTime.Now.ToString("ddMMyyyyHHmm")}.xls";
                book.Save(Path.Combine(dirPath, fileExcel));
                
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, "Seleccione al menos un registro para generar el excel", Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<PHCilindros> GetSeleccionadas()
        {
            List<PHCilindros> phCilindrosList = new List<PHCilindros>();
            foreach (GridViewRow item in grdCilindros.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSeleccionar");
                if (chk.Checked)
                {
                    Guid id = (Guid)grdCilindros.DataKeys[item.RowIndex].Values["ID"];
                    phCilindrosList.Add(this.PHCilindrosLogic.ReadPhCilindroDetallado(id));
                }
            }

            return phCilindrosList.OrderByDescending(x => x.NroOperacionCRPC).ToList();
        }

        private static Worksheet AgregarPrimerHoja(Workbook book, List<PHCilindros> seleccionados)
        {
            var hoja = book.Worksheets.Add("revs");
            EncabezadoPrimerHoja(hoja);
            DetallePrimerHoja(hoja, seleccionados);
            return hoja;
        }

        private static Worksheet AgregarSegundaHoja(Workbook book, List<PHCilindros> seleccionados)
        {
            var hoja = book.Worksheets.Add("prbs");
            EncabezadoSegundaHoja(hoja);
            DetalleSegundaHoja(hoja, seleccionados);
            return hoja;
        }

        private static void EncabezadoPrimerHoja(Worksheet hoja)
        {
            WorksheetRow encabezado = hoja.Table.Rows.Add();
            encabezado.Cells.Add("");
            encabezado.Cells.Add("RCRPCRT");
            encabezado.Cells.Add("RPECCOD");
            encabezado.Cells.Add("RTALCOD");
            encabezado.Cells.Add("RTALCUIT");
            encabezado.Cells.Add("RPROAPYN");
            encabezado.Cells.Add("RPROTDOC");
            encabezado.Cells.Add("RPRONDOC");
            encabezado.Cells.Add("RPRODMCL");
            encabezado.Cells.Add("RPROLCLD");
            encabezado.Cells.Add("RPROPRVN");
            encabezado.Cells.Add("RPROCDPT");
            encabezado.Cells.Add("RPROTLFN");
            encabezado.Cells.Add("RPRODMNO");
            encabezado.Cells.Add("RCILCODH");
            encabezado.Cells.Add("RCILNSER");
            encabezado.Cells.Add("RCILMESF");
            encabezado.Cells.Add("RCILANOF");
            encabezado.Cells.Add("RCILMTRL");
            encabezado.Cells.Add("RCILCPCD");
            encabezado.Cells.Add("RCILREV");
            encabezado.Cells.Add("RREVRSLT");
            encabezado.Cells.Add("RREVGLOB");
            encabezado.Cells.Add("RREVABOL");
            encabezado.Cells.Add("RREVABOE");
            encabezado.Cells.Add("RREVFISU");
            encabezado.Cells.Add("RREVLAMI");
            encabezado.Cells.Add("RREVPINC");
            encabezado.Cells.Add("RREVDEFR");
            encabezado.Cells.Add("RREVDESL");
            encabezado.Cells.Add("RREVCORR");
            encabezado.Cells.Add("RREVOVAL");
            encabezado.Cells.Add("RREVDFME");
            encabezado.Cells.Add("RREVEVSA");
            encabezado.Cells.Add("RREVPERM");
            encabezado.Cells.Add("RREVDPFC");
            encabezado.Cells.Add("RREVOTR1");
            encabezado.Cells.Add("RREVOTR2");
            encabezado.Cells.Add("RREVFREV");
            encabezado.Cells.Add("RREVFVEN");
            encabezado.Cells.Add("XRECTIPOPR");
            encabezado.Cells.Add("XRECFECMODE");
            encabezado.Cells.Add("XRECFECTRF");

            encabezado = hoja.Table.Rows.Add();
            encabezado.Cells.Add("*");
        }

        private static void DetallePrimerHoja(Worksheet hoja, List<PHCilindros> seleccionados)
        {
            foreach (var phCilindros in seleccionados)
            {
                WorksheetRow encabezado = hoja.Table.Rows.Add();
                encabezado.Cells.Add("PEAR");
                encabezado.Cells.Add("2-0094-8");
                encabezado.Cells.Add("3154");
                encabezado.Cells.Add($"{phCilindros.PH.Talleres.Descripcion}");
                string cuit = String.IsNullOrWhiteSpace(phCilindros.PH.Talleres.CuitTaller) ||
                                phCilindros.PH.Talleres.CuitTaller.All(ch => ch == '0') ? "0000000" : Funciones.FormatearNroCuit(phCilindros.PH.Talleres.CuitTaller);
                encabezado.Cells.Add($"{cuit}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.DocumentosClientes.Descripcion}");
                var dniCliente = phCilindros.PH.Clientes.NroDniCliente.Length > 8 ?
                                            Funciones.FormatearNroCuit(phCilindros.PH.Clientes.NroDniCliente) :
                                            phCilindros.PH.Clientes.NroDniCliente.Trim().Replace(".", string.Empty);
                encabezado.Cells.Add($"{dniCliente}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.CalleCliente}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.Localidades.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.Localidades.Provincias.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.PH.Clientes.Localidades.CodigoPostal}");
                encabezado.Cells.Add("0"); //RPROTLFN
                encabezado.Cells.Add($"{phCilindros.PH.Vehiculos.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Cilindros.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.MesFabCilindro?.ToString("0")}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.AnioFabCilindro?.ToString("0")}");
                encabezado.Cells.Add(""); //RCILMTRL
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Cilindros.CapacidadCil?.ToString()}");
                encabezado.Cells.Add($"{phCilindros.NroOperacionCRPC?.ToString()}");
                encabezado.Cells.Add("0");
                encabezado.Cells.Add("");//REVGLOB");
                encabezado.Cells.Add("");//REVABOL");
                encabezado.Cells.Add("");//REVABOE");
                encabezado.Cells.Add("");//REVFISU");
                encabezado.Cells.Add("");//REVLAMI");
                encabezado.Cells.Add("");//REVPINC");
                encabezado.Cells.Add("");//REVDEFR");
                encabezado.Cells.Add("");//REVDESL");
                encabezado.Cells.Add("");//REVCORR");
                encabezado.Cells.Add("");//REVOVAL");
                encabezado.Cells.Add("");//REVDFME");
                encabezado.Cells.Add("");//REVEVSA");
                encabezado.Cells.Add("");//REVPERM");
                encabezado.Cells.Add("");//REVDPFC");
                encabezado.Cells.Add("");//REVOTR1");
                encabezado.Cells.Add("");//RREVOTR2");
                encabezado.Cells.Add(DateTime.Now.ToString("d/M/yyyy"));//RREVFREV");
                encabezado.Cells.Add(DateTime.Now.ToString("d/M/yyyy"));//RREVFVEN");
                encabezado.Cells.Add(CrossCutting.DatosDiscretos.CRPC.CodigoCRPC + phCilindros.NroOperacionCRPC ?? string.Empty);//XRECTIPOPR");
                encabezado.Cells.Add("A");//XRECFECMODE");
                encabezado.Cells.Add(DateTime.Now.ToString("d/M/yyyy"));//XRECFECTRF");
            }
        }

        private static void EncabezadoSegundaHoja(Worksheet hoja)
        {
            WorksheetRow encabezado = hoja.Table.Rows.Add();
            encabezado.Cells.Add("Operación");
            encabezado.Cells.Add("Código");
            encabezado.Cells.Add("Serie");
            encabezado.Cells.Add("Mes fabr.");
            encabezado.Cells.Add("Año fabr.");
            encabezado.Cells.Add("Material");
            encabezado.Cells.Add("Esp. Adm.");
            encabezado.Cells.Add("Esp.mín.");
            encabezado.Cells.Add("Esp.máx.");
            encabezado.Cells.Add("Esp.fond.");
            encabezado.Cells.Add("Tipo fondo");
            encabezado.Cells.Add("Ctrl.esp.");
            encabezado.Cells.Add("Tara");
            encabezado.Cells.Add("P. vacío");
            encabezado.Cells.Add("Capac.");
            encabezado.Cells.Add("Ctrl.peso");
            encabezado.Cells.Add("Exp. perm.");
            encabezado.Cells.Add("Exp.elast.");
            encabezado.Cells.Add("P.hidr.");
            encabezado.Cells.Add("Anomalías");
            encabezado.Cells.Add("Conclus.");
            encabezado.Cells.Add("Mes Rev.");
            encabezado.Cells.Add("Ant.Año");

            encabezado.Cells.Add("Válvula marca");
            encabezado.Cells.Add("Válvula serie");

            encabezado = hoja.Table.Rows.Add();
            encabezado.Cells.Add("*");
        }

        private static void DetalleSegundaHoja(Worksheet hoja, List<PHCilindros> seleccionados)
        {
            foreach (var phCilindros in seleccionados)
            {
                WorksheetRow encabezado = hoja.Table.Rows.Add();
                encabezado.Cells.Add($"{phCilindros.NroOperacionCRPC}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Cilindros.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.MesFabCilindro?.ToString("0")}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.AnioFabCilindro?.ToString("0")}");
                encabezado.Cells.Add($"{phCilindros.CilindrosUnidad.Cilindros.MaterialCilindro.Trim().Replace("0", string.Empty)}");
                encabezado.Cells.Add($" {phCilindros.CilindrosUnidad.Cilindros.EspesorAdmisibleCil?.ToString("0.00")}");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");
                encabezado.Cells.Add("");

                encabezado.Cells.Add($"{phCilindros.Valvula_Unidad.Valvula.Descripcion}");
                encabezado.Cells.Add($"{phCilindros.Valvula_Unidad.Descripcion}");
            }
        }

        /// <summary>
        /// Open specified excel document.
        /// </summary>
        private void OpenMicrosoftExcel(string file)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "EXCEL.EXE",
                    Arguments = file
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }

        }

        protected void Search_ServerClick(object sender, EventArgs e)
        {
            BuscarArchivos();
        }

        private void BuscarArchivos()
        {
            try
            {
                List<PHArchivosExcel> archivos = new List<PHArchivosExcel>();
                String url = MapPath("~/Archivos/PH/temp/").ToString();

                var files = System.IO.Directory.GetFiles(url, "phAlberto_*.xls");
                Array.Sort(files);
                for (int i = 0; i < files.Length; i++)
                {
                    var parse = files[i].Replace(@"\", @"*").Split('*');
                    PHArchivosExcel archivo = new PHArchivosExcel();
                    archivo.NombreExcel = parse.Last();
                    archivo.FechaHoraExcel = System.IO.File.GetCreationTimeUtc(files[i]).ToString();
                    archivo.UrlExcel = files[i];

                    archivos.Add(archivo);
                }

                divArchivos.Visible = archivos.Any();

                DateTime fechaDesde = DateTime.Parse(calFechaD.Value);                
                DateTime fechaHasta = DateTime.Parse(calFechaH.Value).AddDays(1).AddMinutes(-1);

                grdArchivos.DataSource = archivos.Where(x => DateTime.Parse(x.FechaHoraExcel).Date >= fechaDesde.Date &&
                                                             DateTime.Parse(x.FechaHoraExcel).Date <= fechaHasta.Date);
                grdArchivos.DataBind();
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox("Ha ocurrido un error al recuperar los archivos.", ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void grdArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var file = e.CommandArgument.ToString().Split('\\').Last();
                String path = $"../../Archivos/PH/temp/{file}";
               
                MessageBoxCtrl.MessageBox(null, $"El archivo excel se creó correctamente. <strong><a href=\"{path}\" target=\"_blank\">Abrir</a></strong>", Web.UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox("", ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void grdCilindros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string codHomologacion = e.Row.Cells[3].Text;

                bool tieneDatosCompletos = this.CilindrosLogic.TieneDatosCompletos(codHomologacion);

                ((CheckBox)e.Row.FindControl("chkSeleccionar")).Enabled = tieneDatosCompletos;
            }
        }
    }
}