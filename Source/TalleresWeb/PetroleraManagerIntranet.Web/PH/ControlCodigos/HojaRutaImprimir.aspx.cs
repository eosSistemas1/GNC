using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ControlCodigos
{
    public partial class HojaRutaImprimir : PageBase
    {
        #region Members
        private PHCilindrosLogic phCilindrosLogic;
        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) this.phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }

        public static String logoURL = SiteMaster.UrlBase + CrossCutting.DatosDiscretos.GetDinamyc.LogoEmpresa;
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {

            Guid idPhCilindro = new Guid(Request.QueryString[0].ToString());
            Boolean actualizarEstado = false;
            if (Request.QueryString.Count > 1 && Request.QueryString[1] != null)
                Boolean.TryParse(Request.QueryString[1].ToString(), out actualizarEstado);

            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            //Recupero la oblea
            PHCilindrosHojaRutaView pHCilindro = this.PHCilindrosLogic.ReadParaImprimirHojaRuta(idPhCilindro);

            if (actualizarEstado)
            {
                this.PHCilindrosLogic.CambiarEstado(idPhCilindro, EstadosPH.EnProceso, "Se imprime la hoja de ruta e ingresa a proceso", this.UsuarioID);
            }

            #region Impresion Ficha
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

            document.Open();
            iTextSharp.text.pdf.PdfContentByte cb;
            cb = writer.DirectContent;

            cb.BeginText();

            cb.SetFontAndSize(GetBF(), 8);
            var fontTitle = new Font(GetBF(), 9);
            var fontContenido = new Font(GetBF(), 8);

            Paragraph paragraphTable = new Paragraph();
            paragraphTable.SpacingBefore = 200f;
            Table tablaHojaDeRuta = GenerarTablaHojaDeRuta(pHCilindro, cb, fontContenido, fontTitle);
            paragraphTable.Add(tablaHojaDeRuta);
            document.Add(paragraphTable);

            Table tabla = GenerarTablaRegistroDePesos(fontContenido, fontTitle);
            document.Add(tabla);

            tabla = GenerarTablaMedicionEspesores(fontContenido, fontTitle);
            document.Add(tabla);

            tabla = GenerarTablaInspeccionExterior(fontContenido, fontTitle);
            document.Add(tabla);

            tabla = GenerarTablaPruebaHidraulica(fontContenido, fontTitle);
            document.Add(tabla);

            tabla = GenerarTablaInspeccionRosca(fontContenido, fontTitle);
            document.Add(tabla);

            tabla = GenerarTablaInspeccionInterior(fontContenido, fontTitle);
            document.Add(tabla);

            paragraphTable = new Paragraph();
            paragraphTable.SpacingBefore = 20f;
            PdfPTable tabla2 = GenerarTarjetasCilindro(cb, fontContenido, pHCilindro);
            paragraphTable.Add(tabla2);
            document.Add(paragraphTable);

            cb.EndText();

            document.Close();
            #endregion

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        private static BaseFont GetBF()
        {
            return iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
        }

        private static Table GenerarTablaHojaDeRuta(PHCilindrosHojaRutaView pHCilindro, PdfContentByte cb, Font fontContenido, Font fontTitle)
        {
            iTextSharp.text.Table tabla = new iTextSharp.text.Table(8, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(AddCelda("HOJA DE RUTA", fontTitle, 2));
            tabla.AddCell(AddCelda("Nro. de revisión:" + pHCilindro.NroOperacionCRPC, fontContenido, 2));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 2));
            tabla.AddCell(AddCelda("Fecha de rev:" + pHCilindro.FechaOperacion.ToString("dd/MM/yyyy"), fontContenido, 2));

            String identificacionCilindro = "  01 - Identificación del cilindro";
            tabla.AddCell(AddCelda(identificacionCilindro, fontContenido, 2, 4));            

            tabla.AddCell(AddCelda("Nro. de serie: " + pHCilindro.NumeroSerie, fontContenido, 2));
            tabla.AddCell(AddCelda("Código: " + pHCilindro.CodigoHomologacion, fontContenido, 2));
            tabla.AddCell(AddCelda("Marca: " + pHCilindro.Marca, fontContenido, 2));

            tabla.AddCell(AddCelda("Capacidad: " + pHCilindro.Capacidad, fontContenido, 2));
            tabla.AddCell(AddCelda("Diámetro: " + pHCilindro.Diámetro.ToString(), fontContenido, 2));
            tabla.AddCell(AddCelda("Norma Fab.: " + pHCilindro.NormaFabricacion, fontContenido, 2));

            tabla.AddCell(AddCelda("Pared mín.: " + pHCilindro.ParedMinimo, fontContenido, 2));
            tabla.AddCell(AddCelda("Fondo mín.: " + pHCilindro.FondoMinimo.ToString(), fontContenido, 2));
            tabla.AddCell(AddCelda("Marca válvula: " + pHCilindro.MarcaValvula, fontContenido, 2, 2));

            tabla.AddCell(AddCelda("Fecha fab.: " + pHCilindro.MesFabricacion.ToString() + "/" + pHCilindro.AnioFabricacion.ToString(), fontContenido, 2));
            tabla.AddCell(AddCelda("Ult. rev.: " + pHCilindro.FechaUltimaRevision.ToString(), fontContenido, 2));
            String taller = $"{pHCilindro.MatriculaTaller.ToUpper()}-{pHCilindro.RazonSocialTaller.ToUpper()}";
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, taller, 350, 810, 0);

            Cell cellVacia = AddCelda(String.Empty, fontContenido);
            tabla.AddCell(cellVacia);
            return tabla;
        }

        private static Table GenerarTablaRegistroDePesos(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(7, 4);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("REGISTRO DE PESOS", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontContenido));
            tabla.AddCell(AddCelda("Marcado", fontContenido));
            tabla.AddCell(AddCelda("", fontContenido));
            tabla.AddCell(AddCelda("Actual", fontContenido));
            tabla.AddCell(AddCelda("Revisó", fontContenido, 1, 4));

            tabla.AddCell(AddCelda("Observaciones", fontTitle, 2, 2));
            tabla.AddCell(AddCelda("Peso vacío", fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));
            tabla.AddCell(AddCelda("Peso vacío", fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));

            tabla.AddCell(AddCelda(string.Empty, fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));
            tabla.AddCell(AddCelda("Peso con agua", fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));

            tabla.AddCell(AddCelda("Mx=", fontTitle, 2, 1));
            tabla.AddCell(AddCelda("Capacidad", fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));
            tabla.AddCell(AddCelda("Capacidad", fontContenido));
            tabla.AddCell(AddCelda(string.Empty, fontContenido));

            return tabla;
        }

        private static Table GenerarTablaMedicionEspesores(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(15, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("MEDICIÓN DE ESPESORES", fontTitle, 4));
            tabla.AddCell(AddCelda("Lecturas s/pared", fontContenido, 5));
            tabla.AddCell(AddCelda("S/fondo", fontContenido, 2));
            tabla.AddCell(AddCelda("Fondo Tipo", fontContenido, 2));
            tabla.AddCell(AddCelda("Revisó", fontContenido, 2));

            tabla.AddCell(AddCelda("Observaciones", fontContenido, 4, 4));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 2));
            tabla.AddCell(AddCelda("CONVEXO", fontContenido, 2, 2));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 2, 4));

            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda("\n\r", fontContenido, 2));

            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 2));
            tabla.AddCell(AddCelda("CONCAVO", fontContenido, 2, 2));

            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda(string.Empty, fontContenido, 1));
            tabla.AddCell(AddCelda("\n\r", fontContenido, 2));

            return tabla;
        }

        private static Table GenerarTablaInspeccionExterior(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(14, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("INSPECCIÓN INTERIOR", fontTitle, 4));
            tabla.AddCell(AddCelda("Observaciones:", fontContenido, 6));
            tabla.AddCell(AddCelda("Revisó:", fontContenido, 4));

            tabla.AddCell(AddCelda("", fontContenido, 2));
            tabla.AddCell(AddCelda("SI", fontContenido, 1));
            tabla.AddCell(AddCelda("NO", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 2));
            tabla.AddCell(AddCelda("SI", fontContenido, 1));
            tabla.AddCell(AddCelda("NO", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 2));
            tabla.AddCell(AddCelda("SI", fontContenido, 1));
            tabla.AddCell(AddCelda("NO", fontContenido, 1));

            tabla.AddCell(AddCelda("Anotar mediciones de ovalización u otro defecto cuantificable:", fontContenido, 2, 7));

            tabla.AddCell(AddCelda("Globos", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Laminado", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Corrosión general", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("Abolladuras", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Pinchaduras", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Corrosión local", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("Abolladuras c/estrías", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Desgaste local", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Picaduras aisladas", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("Defectos en cuello", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Ovalado", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Espesor insuficiente", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("Fisuras", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Daño x Fuego", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("Para cilindros ECOTEMP", fontContenido, 4));
            tabla.AddCell(AddCelda("Fibras cortadas", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("Capas despegadas", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));



            return tabla;
        }

        private static Table GenerarTablaPruebaHidraulica(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(12, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("PRUEBA HIDRÁULICA", fontTitle, 3));
            tabla.AddCell(AddCelda("Presión de prueba", fontContenido, 2));
            tabla.AddCell(AddCelda("300", fontContenido, 1));
            tabla.AddCell(AddCelda("375", fontContenido, 1));
            tabla.AddCell(AddCelda("Bureta", fontContenido, 1));
            tabla.AddCell(AddCelda("1300 cc", fontContenido, 1));
            tabla.AddCell(AddCelda("2000 cc", fontContenido, 1));
            tabla.AddCell(AddCelda("Revisó:", fontContenido, 2, 4));

            tabla.AddCell(AddCelda("Observaciones:", fontContenido, 3));
            tabla.AddCell(AddCelda("Corresponde:", fontContenido, 2));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            tabla.AddCell(AddCelda("", fontContenido, 3));
            tabla.AddCell(AddCelda("", fontContenido, 2));
            tabla.AddCell(AddCelda("Máx", fontContenido, 1));
            tabla.AddCell(AddCelda("Final", fontContenido, 1));
            tabla.AddCell(AddCelda("Temp. del agua ºC", fontContenido, 3, 2));

            tabla.AddCell(AddCelda("Lectura de la bureta (cc)", fontContenido, 5));
            tabla.AddCell(AddCelda("", fontContenido, 1));
            tabla.AddCell(AddCelda("", fontContenido, 1));

            return tabla;
        }

        private static Table GenerarTablaInspeccionRosca(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(16, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("INSPECCIÓN DE ROSCA", fontTitle, 4));
            tabla.AddCell(AddCelda("Tipo de rosca", fontTitle, 12));

            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));

            tabla.AddCell(AddCelda("Fisuras", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Deformación", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Falta material", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Perfil incomp.", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));

            tabla.AddCell(AddCelda("Observaciones:\n\n\n\n", fontTitle, 13));
            tabla.AddCell(AddCelda("Revisò:", fontTitle, 3));

            return tabla;
        }

        private static Table GenerarTablaInspeccionInterior(Font fontContenido, Font fontTitle)
        {
            Table tabla = new iTextSharp.text.Table(13, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;

            tabla.AddCell(AddCelda("INSPECCIÓN INTERIOR", fontTitle, 3));
            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("SI", fontTitle, 1));
            tabla.AddCell(AddCelda("NO", fontTitle, 1));
            tabla.AddCell(AddCelda("Revisó:", fontTitle, 2, 5));

            tabla.AddCell(AddCelda("Observaciones:", fontTitle, 3, 4));
            tabla.AddCell(AddCelda("Fisuras", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Corrosión general", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));

            tabla.AddCell(AddCelda("Laminado", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Corrosión local", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));

            tabla.AddCell(AddCelda("", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("Picaduras aisladas", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));

            tabla.AddCell(AddCelda("Para cilindros ECOTEMP", fontTitle, 4));
            tabla.AddCell(AddCelda("Pintura int. defectuosa", fontTitle, 2));
            tabla.AddCell(AddCelda("", fontTitle, 1));
            tabla.AddCell(AddCelda("", fontTitle, 1));

            return tabla;
        }

        private static PdfPTable GenerarTarjetasCilindro(PdfContentByte cb, Font fontContenido, PHCilindrosHojaRutaView pHCilindro)
        {
            var font9 = new Font(GetBF(), 9);
            var font12 = new Font(GetBF(), 12);

            PdfPTable tabla = new PdfPTable(11)
            {
                WidthPercentage = 100
            };
            
        
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoURL);
            logo.ScalePercent(30);
            PdfPCell cellLogo = new PdfPCell(logo)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = 5,
            };
            tabla.AddCell(cellLogo);
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(cellLogo);

            String numeroOperacionCRPC = pHCilindro.NroOperacionCRPC.HasValue ? pHCilindro.NroOperacionCRPC.Value.ToString() : String.Empty;
            tabla.AddCell(AddCeldaPdfTable("", fontContenido, 4, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(numeroOperacionCRPC, font9, 1, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido, 4, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(numeroOperacionCRPC, font9, 1, 0, 0, 0, 1));


            tabla.AddCell(AddCeldaPdfTable("Código:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.CodigoHomologacion, fontContenido, 4, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("Código:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.CodigoHomologacion, fontContenido, 4, 0, 0, 0, 1));

            tabla.AddCell(AddCeldaPdfTable("Serie:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.NumeroSerie, font12, 4, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("Serie:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.NumeroSerie, font12, 4, 0, 0, 0, 1));

            tabla.AddCell(AddCeldaPdfTable("Propietario:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.Cliente, fontContenido, 4, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("Propietario:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.Cliente, fontContenido, 4, 0, 0, 0, 1));

            tabla.AddCell(AddCeldaPdfTable("TdM:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.RazonSocialTaller.ToUpper(), fontContenido, 4, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("TdM:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.RazonSocialTaller.ToUpper(), fontContenido, 4, 0, 0, 0, 1));

            tabla.AddCell(AddCeldaPdfTable("Fecha Reg.:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.FechaOperacion.ToString("dd/MM/yyyy"), fontContenido, 4, 0, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("Fecha Reg.:", fontContenido, 1, 0, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable(pHCilindro.FechaOperacion.ToString("dd/MM/yyyy"), fontContenido, 4, 0, 0, 0, 1));

            tabla.AddCell(AddCeldaPdfTable("Obs:", fontContenido, 1, 1, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable($"{pHCilindro.NumeroSerieValvula} - {pHCilindro.CodigoHomologacionValvula}", fontContenido, 4, 1, 0, 0, 1));
            tabla.AddCell(AddCeldaPdfTable("", fontContenido));
            tabla.AddCell(AddCeldaPdfTable("Obs:", fontContenido, 1, 1, 0, 1, 0));
            tabla.AddCell(AddCeldaPdfTable($"{pHCilindro.NumeroSerieValvula} - {pHCilindro.CodigoHomologacionValvula}", fontContenido, 4, 1, 0, 0, 1));            

            return tabla;
        }

        private static Cell AddCelda(String texto, Font fontContenido, int colSpan = 1, int rowSpan = 1)
        {
            Cell cell = new Cell(new Phrase(texto, fontContenido));
            cell.Colspan = colSpan;
            cell.Rowspan = rowSpan;
            return cell;
        }

        private static PdfPCell AddCeldaPdfTable(String texto,
                                                 Font fontContenido,
                                                 int colSpan = 1,
                                                 int borderWidthBottom = 0,
                                                 int borderWidthTop = 0,
                                                 int borderWidthLeft = 0,
                                                 int borderWidthRight = 0)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fontContenido));
            cell.Colspan = colSpan;
            cell.Border = 0;
            cell.BorderWidthBottom = borderWidthBottom;
            cell.BorderWidthTop = borderWidthTop;
            cell.BorderWidthLeft = borderWidthLeft;
            cell.BorderWidthRight = borderWidthRight;
            return cell;
        }
        #endregion
    }
}