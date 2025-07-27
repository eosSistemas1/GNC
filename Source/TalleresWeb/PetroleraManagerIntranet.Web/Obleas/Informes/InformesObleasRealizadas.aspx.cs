using CarlosAg.ExcelXmlWriter;
using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManager.Web.Tramites.Informes
{
    public partial class InformesObleasRealizadas : PageBase
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

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            String mensaje = this.ValidarFechas();

            if (String.IsNullOrWhiteSpace(mensaje))
            {
                var obleasRealizadas = this.ObtenerObleasRealizadas();

                if (obleasRealizadas.Any())
                {
                    String file = this.CrearInformePDFCompleto(obleasRealizadas);

                    PrintBoxCtrl.PrintBox("Obleas Realizadas", file);
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "No se encontraron obleas para el período seleccionado", MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private String CrearInformePDFCompleto(List<ObleasExtendedView> obleasRealizadas)
        {
            List<InformeObleasRealizadasView> obleasinforme = ObleasRealizadasPorTaller(obleasRealizadas);

            String carpeta = "../../temp/";
            String archivo = $"ObleasRealizadas_{DateTime.Now.ToString("yyyyMMdd")}.pdf";

            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            String fileUrl = carpeta + archivo;

            using (MemoryStream pdfContent = this.CrearReportePDF(obleasinforme))
            {
                File.WriteAllBytes(this.MapPath(fileUrl), pdfContent.ToArray());
            }

            fileUrl = fileUrl.Replace(carpeta, UrlBase + @"temp/");

            return fileUrl;
        }

        private List<InformeObleasRealizadasView> ObleasRealizadasPorTaller(List<ObleasExtendedView> obleasRealizadas)
        {
            List<InformeObleasRealizadasView> obleasinforme = new List<InformeObleasRealizadasView>();

            foreach (var item in obleasRealizadas.GroupBy(t => t.IdTaller))
            {
                InformeObleasRealizadasView o = new InformeObleasRealizadasView();

                var obleasPorTaller = obleasRealizadas.Where(t => t.IdTaller == item.First().IdTaller);

                o.CantidadConversion = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.Conversion);
                o.CantidadModificacion = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.Modificacion);
                o.CantidadReemplazo = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.Reemplazo);
                o.CantidadRevisionCRPC = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.RevisionCRPC);
                o.CantidadRevision = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.RevisionAnual);
                o.CantidadBaja = obleasPorTaller.Count(x => x.IdOpreracion == TIPOOPERACION.Baja);
                o.CantidadTotalRealizadas = obleasPorTaller.Count();
                o.CantidadTotalEntregadas = obleasPorTaller.Count(x => x.IdOpreracion != TIPOOPERACION.Baja);
                o.Taller = obleasPorTaller.First().Taller;
                o.FechaDesde = DateTime.Parse(calFechaD.Value);
                o.FechaHasta = DateTime.Parse(calFechaH.Value);

                obleasinforme.Add(o);
            }

            return obleasinforme;
        }

        /// <summary>
        /// Crea el pdf de 1 o mas talleres y devuelve la url del mismo
        /// </summary>
        /// <param name="obleasinforme"></param>
        /// <returns></returns>
        private MemoryStream CrearReportePDF(List<InformeObleasRealizadasView> obleasinforme)
        {

            Document docPDF = Report.OrientacionPagina(Report.eOrientacion.VERTICAL);

            MemoryStream memStream = new MemoryStream();

            try
            {
                PdfWriter.GetInstance(docPDF, memStream);
                int cantCols = 2;

                // Incluimos los elementos comunes
                Report.IncluirElementosComunes(docPDF,
                                               "Obleas Realizadas",
                                               "Petrolera ItaloArgentina");

                // Creamos las columnas
                iTextSharp.text.Table tabla = new iTextSharp.text.Table(cantCols);

                tabla.WidthPercentage = 100;
                tabla.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                tabla.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
                tabla.BorderWidth = 1;
                tabla.Cellpadding = 3;

                // Creamos la cabecera
                Phrase frase0 = new Phrase("DESCRIPCION", Report.FuenteTitulos);
                Cell celda0 = new Cell(frase0);
                celda0.BackgroundColor = Report.ColorCabeceraTabla;
                tabla.AddCell(celda0);
                Phrase frase1 = new Phrase("CANTIDAD", Report.FuenteTitulos);
                Cell celda1 = new Cell(frase1);
                celda1.BackgroundColor = Report.ColorCabeceraTabla;
                tabla.AddCell(celda1);

                frase0 = new Phrase("DESCRIPCION", Report.FuenteTitulos);
                celda0 = new Cell(frase0);
                celda0.BackgroundColor = Report.ColorCabeceraTabla;
                tabla.AddCell(celda0);
                frase1 = new Phrase("CANTIDAD", Report.FuenteTitulos);
                celda1 = new Cell(frase1);
                celda1.BackgroundColor = Report.ColorCabeceraTabla;
                tabla.AddCell(celda1);



                docPDF.Add(tabla);
            }
            catch
            {
                return null;
            }

            docPDF.Close();

            return memStream;
        }

        private string ValidarFechas()
        {
            String mensaje = String.Empty;

            try
            {
                var fechadesde = DateTime.Parse(calFechaD.Value);

                if (fechadesde == GetDinamyc.MinDatetime)
                {
                    mensaje += "Debe ingresar una fecha desde";
                }
            }
            catch (Exception)
            {
                mensaje += "La fecha desde ingresada no es válida";
            }

            try
            {
                var fechahasta = DateTime.Parse(calFechaH.Value);

                if (fechahasta == GetDinamyc.MaxDatetime)
                {
                    mensaje += "Debe ingresar una fecha hasta";
                }
            }
            catch (Exception)
            {
                mensaje += "La fecha hasta ingresada no es válida";
            }

            return mensaje;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                String mensaje = this.ValidarFechas();

                if (String.IsNullOrWhiteSpace(mensaje))
                {
                    var obleasRealizadas = this.ObtenerObleasRealizadas();

                    if (obleasRealizadas.Any())
                    {
                        String file = this.CrearReporteExcel(obleasRealizadas);
                        MessageBoxCtrl.MessageBox(null, $"El archivo excel se creó correctamente. <br/><br/><a href=\"{file}\" target=\"_blank\">Abrir</a>", MessageBoxCtrl.TipoWarning.Success);                        
                    }
                    else
                    {
                        MessageBoxCtrl.MessageBox(null, "No se encontraron obleas para el período seleccionado", MessageBoxCtrl.TipoWarning.Warning);
                    }
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
            }
        }

        private String CrearReporteExcel(List<ObleasExtendedView> obleasRealizadas)
        {
            List<InformeObleasRealizadasView> obleasinforme = ObleasRealizadasPorTaller(obleasRealizadas);

            Workbook book = new Workbook();
            book.Properties.Author = GetDinamyc.RazonSocialEmpresa;
            book.Properties.Created = DateTime.Now;
            book.Properties.Version = "1";

            Worksheet sheet = book.Worksheets.Add("Obleas Realizadas");

            #region ESTILOS
            WorksheetStyle style = book.Styles.Add("HEADER2");
            style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;

            style = book.Styles.Add("HEADER1");
            style.Font.Bold = true;
            #endregion

            sheet.Table.Columns.Add(new WorksheetColumn(200));
            sheet.Table.Columns.Add(new WorksheetColumn(120));

            WorksheetRow fila;

            fila = sheet.Table.Rows.Add();
            WorksheetCell cell1 = new WorksheetCell(GetDinamyc.RazonSocialEmpresa);
            cell1.MergeAcross = 9;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            cell1 = new WorksheetCell("Obleas Realizadas");
            cell1.MergeAcross = 9;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            var fechaDesde = DateTime.Parse(calFechaD.Value).ToString("dd/MM/yyyy");
            var fechaHasta = DateTime.Parse(calFechaH.Value).ToString("dd/MM/yyyy");
            String msgFecha = $"Fecha desde: {fechaDesde} hasta: {fechaHasta}";
            cell1 = new WorksheetCell(msgFecha);
            cell1.MergeAcross = 9;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            cell1 = new WorksheetCell("Taller / Operación");
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Conversión:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Modificación:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Reemplazo:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Revisión Anual:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Revisión CRPC:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Baja:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Total Realizadas:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("Total Entregadas:");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            foreach (var item in obleasinforme)
            {
                String msgCliente = item.Taller;
                cell1 = new WorksheetCell(msgCliente);
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadConversion.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadModificacion.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadReemplazo.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadRevision.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadRevisionCRPC.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadBaja.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadTotalRealizadas.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.CantidadTotalEntregadas.ToString());
                cell1.Data.Type = DataType.Number;
                cell1.StyleID = "HEADER1";
                fila.Cells.Add(cell1);
                fila = sheet.Table.Rows.Add();
            }

            cell1 = new WorksheetCell("TOTAL:");
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.Conversion).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.Modificacion).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.Reemplazo).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.RevisionAnual).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.RevisionCRPC).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion == TIPOOPERACION.Baja).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count().ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell(obleasRealizadas.Count(x => x.IdOpreracion != TIPOOPERACION.Baja).ToString());
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            String file = "../../temp/";

            if (!Directory.Exists(this.MapPath(file))) Directory.CreateDirectory(this.MapPath(file));

            file += "ObleasRealizadas.xls";

            book.Save(this.MapPath(file));

            file = file.Replace("../../", UrlBase);

            return file;
        }

        private List<ObleasExtendedView> ObtenerObleasRealizadas()
        {
            var fechaDesde = DateTime.Parse(calFechaD.Value);
            var fechaHasta = DateTime.Parse(calFechaH.Value);
            List<ObleasExtendedView> obleas = this.Logic.ReadObleasRealizadas(fechaDesde, fechaHasta);

            return obleas;
        }
        #endregion
    }
}