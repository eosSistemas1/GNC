using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using TalleresWeb.Web.Cross.Configuracion;

namespace TalleresWeb.Web.Cross
{
    public class Report
    {
        public static Font FuenteTitulos = new Font(Font.HELVETICA, 8, Font.BOLD);
        public static Font FuenteDatos = new Font(Font.HELVETICA, 8);
        public static Font FuenteDatosBold = new Font(Font.HELVETICA, 8, Font.BOLD);
        public static Color ColorCabeceraTabla = new Color(0xC0, 0xC0, 0xC0);
        public static String RutaImagen = PageBase.UrlBase + CrossCutting.DatosDiscretos.GetDinamyc.LogoEmpresa;
        public enum eOrientacion { HORIZONTAL, VERTICAL };

        public static Document OrientacionPagina(eOrientacion orPagina)
        {
            Document docPdf = null;
            if (orPagina == eOrientacion.HORIZONTAL)
            {
                docPdf = new Document(PageSize.A4.Rotate());
            }
            if (orPagina == eOrientacion.VERTICAL)
            {
                docPdf = new Document(PageSize.A4);
            }
            return docPdf;
        }

        public static void IncluirElementosComunes(Document docPdf, string tituloCabecera, string tituloListado)
        {
            // Ponemos la cabecera del documento
            HeaderFooter cabecera = new HeaderFooter(new Phrase(tituloCabecera, new Font(Font.HELVETICA, 9)), false);
            HeaderFooter pie = new HeaderFooter(new Phrase("Página: ", new Font(Font.HELVETICA, 8)), true);
            docPdf.Header = cabecera;
            docPdf.Footer = pie;
            pie.Border = Rectangle.TOP_BORDER;
            pie.Alignment = HeaderFooter.ALIGN_CENTER;
            cabecera.Border = Rectangle.BOTTOM_BORDER;

            docPdf.Open();

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

            // Logo: badge GNC naranja + nombre empresa
            iTextSharp.text.Table tablaImagen = new iTextSharp.text.Table(3);
            tablaImagen.WidthPercentage = 100;
            tablaImagen.BorderWidth = 0;
            tablaImagen.Cellpadding = 3;
            tablaImagen.SetWidths(new int[] { 6, 20, 74 });

            Cell celdaBadge = new Cell(new Phrase("GNC", new Font(bf, 13, Font.BOLD, new Color(255, 255, 255))));
            celdaBadge.BackgroundColor = new Color(0xF0, 0x90, 0x30);
            celdaBadge.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaBadge.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaBadge.Border = Rectangle.NO_BORDER;
            celdaBadge.Rowspan = 2;
            tablaImagen.AddCell(celdaBadge);

            Cell celdaEmpresa = new Cell(new Phrase("MOCCIARO", new Font(bf, 15, Font.BOLD, new Color(0x0D, 0x3A, 0x5C))));
            celdaEmpresa.HorizontalAlignment = Element.ALIGN_LEFT;
            celdaEmpresa.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaEmpresa.Border = Rectangle.NO_BORDER;
            celdaEmpresa.Rowspan = 2;
            tablaImagen.AddCell(celdaEmpresa);

            Cell celdaTexto = new Cell(new Phrase(tituloListado, new Font(bf, 12, Font.BOLD)));
            celdaTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaTexto.Border = Rectangle.NO_BORDER;
            tablaImagen.AddCell(celdaTexto);

            Cell celdaFecha = new Cell(new Phrase("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font(bf, 8, Font.BOLD)));
            celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaFecha.Border = Rectangle.NO_BORDER;
            tablaImagen.AddCell(celdaFecha);

            docPdf.Add(tablaImagen);
        }
    }
}