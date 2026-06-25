using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

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
                var datos = this.ObtenerDatos();

                if (datos.Any())
                {
                    fldResultados.Visible = true;
                    grdLibroDiario.DataSource = datos;
                    grdLibroDiario.DataBind();
                }
                else
                {
                    fldResultados.Visible = false;
                    MessageBoxCtrl.MessageBox(null, "No se encontraron operaciones para los filtros ingresados.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var datos = this.ObtenerDatos();

                if (datos.Any())
                {
                    String file = this.CrearInformePDF(datos);
                    PrintBoxCtrl.PrintBox("Libro Diario", file);
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "No se encontraron operaciones para los filtros ingresados.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        private List<LibroDiarioView> ObtenerDatos()
        {
            string dominio = txtDominio.Text.Trim();
            string cliente = txtCliente.Text.Trim();

            return this.Logic.ReadLibroDiario(
                filtrosFechas.FechaDesde,
                filtrosFechas.FechaHasta,
                dominio,
                cliente);
        }

        private String CrearInformePDF(List<LibroDiarioView> datos)
        {
            String carpeta = "../../tmp/";
            String archivo = String.Format("LibroDiario_{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss"));

            if (!Directory.Exists(this.MapPath(carpeta)))
                Directory.CreateDirectory(this.MapPath(carpeta));

            String fileUrl = carpeta + archivo;

            using (MemoryStream pdfContent = this.CrearReportePDF(datos))
            {
                File.WriteAllBytes(this.MapPath(fileUrl), pdfContent.ToArray());
            }

            fileUrl = fileUrl.Replace(carpeta, SiteMaster.UrlBase + @"temp/");

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
                if (filtrosFechas.FechaDesde != GetDinamyc.MinDatetime || filtrosFechas.FechaHasta != GetDinamyc.MaxDatetime)
                {
                    string desde = filtrosFechas.FechaDesde != GetDinamyc.MinDatetime
                        ? filtrosFechas.FechaDesde.ToString("dd/MM/yyyy")
                        : "inicio";
                    string hasta = filtrosFechas.FechaHasta != GetDinamyc.MaxDatetime
                        ? filtrosFechas.FechaHasta.ToString("dd/MM/yyyy")
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

                string[] encabezados = new string[]
                {
                    "FECHA", "OPERACIÓN", "CLIENTE", "VEHÍCULO", "DOMINIO",
                    "CÓD. REG.", "NRO. REG.", "CÓD. CIL.", "NRO. CIL.",
                    "CÓD. VÁL.", "NRO. VÁL.", "OBLEA ANT.", "OBLEA NUEVA"
                };

                foreach (string enc in encabezados)
                {
                    Cell celda = new Cell(new Phrase(enc, Report.FuenteTitulos));
                    celda.BackgroundColor = Report.ColorCabeceraTabla;
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda);
                }

                Font fuenteDatos = new Font(Font.HELVETICA, 7);

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

                Cell celdaTotalLabel = new Cell(new Phrase("TOTAL: " + datos.Count + " operaciones", Report.FuenteTitulos));
                celdaTotalLabel.Colspan = cantCols;
                celdaTotalLabel.BackgroundColor = Report.ColorCabeceraTabla;
                celdaTotalLabel.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(celdaTotalLabel);

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
