using CarlosAg.ExcelXmlWriter;
using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManager.Web.Tramites.Informes
{
    public partial class InformeObleasAVencer : PageBase
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
            if (!Page.IsPostBack)
            {
                DateTime f = DateTime.Now;
                // Primer día del mes actual
                calFechaD.Value = new DateTime(f.Year, f.Month, 1).ToString("yyyy-MM-dd");

                // Último día del mes actual
                calFechaH.Value = new DateTime(f.Year, f.Month, DateTime.DaysInMonth(f.Year, f.Month)).ToString("yyyy-MM-dd");
            }
        }

        private string ValidarFechas()
        {
            String mensaje = String.Empty;

            try
            {
                var fechadesde = GetDinamyc.MinDatetime;

                DateTime.TryParse(calFechaD.Value, out fechadesde);

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
                var fechahasta = GetDinamyc.MaxDatetime;


                DateTime.TryParse(calFechaH.Value, out fechahasta);

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
                    var obleasRealizadas = this.ObtenerObleasAVencer();

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

        private string CrearReporteExcel(List<ObleasExtendedView> obleasAVencer)
        {
            Workbook book = new Workbook();
            book.Properties.Author = GetDinamyc.RazonSocialEmpresa;
            book.Properties.Created = DateTime.Now;
            book.Properties.Version = "1";

            Worksheet sheet = book.Worksheets.Add("Obleas_A_Vencer");

            #region ESTILOS
            WorksheetStyle style = book.Styles.Add("HEADER2");
            style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;

            style = book.Styles.Add("HEADER1");
            style.Font.Bold = true;
            
            style = book.Styles.Add("itemC");
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            #endregion

            sheet.Table.Columns.Add(new WorksheetColumn(200));
            sheet.Table.Columns.Add(new WorksheetColumn(120));
            sheet.Table.Columns.Add(new WorksheetColumn(120));
            sheet.Table.Columns.Add(new WorksheetColumn(120));
            sheet.Table.Columns.Add(new WorksheetColumn(120));

            WorksheetRow fila;
            int mergeAcross = 5;
            fila = sheet.Table.Rows.Add();
            WorksheetCell cell1 = new WorksheetCell(GetDinamyc.RazonSocialEmpresa);
            cell1.MergeAcross = mergeAcross;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            cell1 = new WorksheetCell("Obleas Realizadas");
            cell1.MergeAcross = mergeAcross;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            var fechaDesde = DateTime.Parse(calFechaD.Value).ToString("dd/MM/yyyy");
            var fechaHasta = DateTime.Parse(calFechaH.Value).ToString("dd/MM/yyyy");
            String msgFecha = $"Fecha desde: {fechaDesde} hasta: {fechaHasta}";
            cell1 = new WorksheetCell(msgFecha);
            cell1.MergeAcross = mergeAcross;
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            cell1 = new WorksheetCell("CLIENTE");
            cell1.StyleID = "HEADER1";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("DOMINIO");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("FECHA HAB.");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("FECHA VENC.");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            cell1 = new WorksheetCell("TELÉFONO");
            cell1.StyleID = "HEADER2";
            fila.Cells.Add(cell1);
            fila = sheet.Table.Rows.Add();

            foreach (var item in obleasAVencer)
            {
                cell1 = new WorksheetCell(item.NombreyApellido);                
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.Dominio);
                cell1.StyleID = "itemC";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.FechaHabilitacion.ToString("dd/MM/yyyy"));
                cell1.StyleID = "itemC";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.FechaVencimiento.Value.ToString("dd/MM/yyyy"));
                cell1.StyleID = "itemC";
                fila.Cells.Add(cell1);
                cell1 = new WorksheetCell(item.Telefono);
                cell1.StyleID = "itemC";
                fila.Cells.Add(cell1);
                fila = sheet.Table.Rows.Add();
            }

            String file = "../../temp/";

            if (!Directory.Exists(this.MapPath(file))) Directory.CreateDirectory(this.MapPath(file));

            file += $"ObleasAVencer_{DateTime.Parse(calFechaD.Value):yyyyMMdd}_{DateTime.Parse(calFechaH.Value):yyyyMMdd}.xls";

            book.Save(this.MapPath(file));

            file = file.Replace("../../", UrlBase);

            return file;
        }

        private List<ObleasExtendedView> ObtenerObleasAVencer()
        {
            var fechaDesde = DateTime.Parse(calFechaD.Value);
            var fechaHasta = DateTime.Parse(calFechaH.Value);
            List<ObleasExtendedView> obleas = this.Logic.ReadObleasAVencer(fechaDesde, fechaHasta);

            return obleas;
        }
        #endregion
    }
}