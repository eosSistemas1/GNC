using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManager.Web.Tramites.Informes
{
    public partial class InformesLibroDiario : PageBase
    {
        #region Members

        private ObleasLogic _logic;

        #endregion

        #region Properties

        public ObleasLogic Logic
        {
            get
            {
                if (this._logic == null) this._logic = new ObleasLogic();
                return this._logic;
            }
            set { _logic = value; }
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                var datos = ObtenerDatos();

                if (datos.Any())
                {
                    divResultados.Visible = true;
                    grdLibroDiario.DataSource = datos;
                    grdLibroDiario.DataBind();
                }
                else
                {
                    divResultados.Visible = false;
                    MessageBoxCtrl.MessageBox(null, "No se encontraron operaciones para los filtros ingresados.", MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var datos = ObtenerDatos();

                if (datos.Any())
                {
                    String file = CrearInformePDF(datos);
                    PrintBoxCtrl.PrintBox("Libro Diario", file);
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "No se encontraron operaciones para los filtros ingresados.", MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
            }
        }

        private List<LibroDiarioView> ObtenerDatos()
        {
            DateTime fechaDesde = GetDinamyc.MinDatetime;
            DateTime fechaHasta = GetDinamyc.MaxDatetime;

            if (!string.IsNullOrWhiteSpace(calFechaD.Value))
                fechaDesde = DateTime.Parse(calFechaD.Value);

            if (!string.IsNullOrWhiteSpace(calFechaH.Value))
                fechaHasta = DateTime.Parse(calFechaH.Value).AddDays(1).AddSeconds(-1);

            string dominio = txtDominio.Value.Trim();
            string cliente = txtCliente.Value.Trim();

            return Logic.ReadLibroDiario(fechaDesde, fechaHasta, dominio, cliente);
        }

        private String CrearInformePDF(List<LibroDiarioView> datos)
        {
            String carpeta = "../../temp/";
            String archivo = String.Format("LibroDiario_{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss"));

            if (!Directory.Exists(this.MapPath(carpeta)))
                Directory.CreateDirectory(this.MapPath(carpeta));

            String fileUrl = carpeta + archivo;

            using (MemoryStream pdfContent = CrearReportePDF(datos))
            {
                File.WriteAllBytes(this.MapPath(fileUrl), pdfContent.ToArray());
            }

            fileUrl = fileUrl.Replace(carpeta, UrlBase + @"temp/");

            return fileUrl;
        }

        private MemoryStream CrearReportePDF(List<LibroDiarioView> datos)
        {
            Document docPDF = Report.OrientacionPagina(Report.eOrientacion.HORIZONTAL);
            MemoryStream memStream = new MemoryStream();

            try
            {
                PdfWriter.GetInstance(docPDF, memStream);

                string subTitulo = "Libro Diario";
                if (!string.IsNullOrWhiteSpace(calFechaD.Value) || !string.IsNullOrWhiteSpace(calFechaH.Value))
                {
                    string desde = !string.IsNullOrWhiteSpace(calFechaD.Value)
                        ? DateTime.Parse(calFechaD.Value).ToString("dd/MM/yyyy")
                        : "inicio";
                    string hasta = !string.IsNullOrWhiteSpace(calFechaH.Value)
                        ? DateTime.Parse(calFechaH.Value).ToString("dd/MM/yyyy")
                        : "hoy";
                    subTitulo = String.Format("Libro Diario  |  Período: {0} al {1}", desde, hasta);
                }

                Report.IncluirElementosComunes(docPDF, subTitulo, GetDinamyc.RazonSocialEmpresa);

                int cantCols = 13;
                iTextSharp.text.Table tabla = new iTextSharp.text.Table(cantCols);
                tabla.WidthPercentage = 100;
                tabla.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                tabla.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
                tabla.BorderWidth = 1;
                tabla.Cellpadding = 2;

                int[] anchos = new int[] { 7, 9, 16, 11, 7, 7, 9, 8, 9, 8, 9, 8, 8 };
                tabla.SetWidths(anchos);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
                Font fuenteHeader = new Font(bf, 8, Font.BOLD);
                Font fuenteDatos = new Font(bf, 7, Font.NORMAL);

                string[] encabezados = new string[]
                {
                    "FECHA", "OPERACIÓN", "CLIENTE", "VEHÍCULO", "DOMINIO",
                    "CÓD. REG.", "NRO. REG.", "CÓD. CIL.", "NRO. CIL.",
                    "CÓD. VÁL.", "NRO. VÁL.", "OBLEA ANT.", "OBLEA NUEVA"
                };

                foreach (string enc in encabezados)
                {
                    Cell celda = new Cell(new Phrase(enc, fuenteHeader));
                    celda.BackgroundColor = Report.ColorCabeceraTabla;
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda);
                }

                foreach (var item in datos)
                {
                    tabla.AddCell(new Cell(new Phrase(item.Fecha.ToString("dd/MM/yyyy"), fuenteDatos)));
                    tabla.AddCell(new Cell(new Phrase(item.Operacion ?? string.Empty, fuenteDatos)));

                    Cell celdaCliente = new Cell(new Phrase(item.NombreCliente ?? string.Empty, fuenteDatos));
                    celdaCliente.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla.AddCell(celdaCliente);

                    Cell celdaVehiculo = new Cell(new Phrase(item.Vehiculo ?? string.Empty, fuenteDatos));
                    celdaVehiculo.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla.AddCell(celdaVehiculo);

                    tabla.AddCell(new Cell(new Phrase(item.Dominio ?? string.Empty, fuenteDatos)));
                    tabla.AddCell(new Cell(new Phrase(item.CodigoRegulador ?? string.Empty, fuenteDatos)));
                    tabla.AddCell(new Cell(new Phrase(item.NroSerieRegulador ?? string.Empty, fuenteDatos)));
                    tabla.AddCell(new Cell(new Phrase(item.CodigosCilindros ?? string.Empty, fuenteDatos)));

                    Cell celdaNrosCil = new Cell(new Phrase(item.NrosSeriesCilindros ?? string.Empty, fuenteDatos));
                    celdaNrosCil.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla.AddCell(celdaNrosCil);

                    tabla.AddCell(new Cell(new Phrase(item.CodigosValvulas ?? string.Empty, fuenteDatos)));

                    Cell celdaNrosVal = new Cell(new Phrase(item.NrosSeriasValvulas ?? string.Empty, fuenteDatos));
                    celdaNrosVal.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla.AddCell(celdaNrosVal);

                    tabla.AddCell(new Cell(new Phrase(item.NroObleaAnterior ?? string.Empty, fuenteDatos)));
                    tabla.AddCell(new Cell(new Phrase(item.NroObleaNueva ?? string.Empty, fuenteDatos)));
                }

                Cell celdaTotal = new Cell(new Phrase("TOTAL: " + datos.Count + " operaciones", fuenteHeader));
                celdaTotal.Colspan = cantCols;
                celdaTotal.BackgroundColor = Report.ColorCabeceraTabla;
                celdaTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(celdaTotal);

                docPDF.Add(tabla);
            }
            catch
            {
                return null;
            }

            docPDF.Close();

            return memStream;
        }

        #endregion
    }
}
