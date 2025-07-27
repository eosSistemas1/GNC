using iTextSharp.text;
using System;

namespace PetroleraManager.Web
{
    public class Report
    {
        public static Font FuenteTitulos = new Font(Font.HELVETICA, 8, Font.BOLD);
        public static Font FuenteDatos = new Font(Font.HELVETICA, 8);
        public static Font FuenteDatosBold = new Font(Font.HELVETICA, 8, Font.BOLD);
        public static Color ColorCabeceraTabla = new Color(0xC0, 0xC0, 0xC0);
        public static String RutaImagen = SiteMaster.UrlBase + CrossCutting.DatosDiscretos.GetDinamyc.LogoEmpresa;
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

            // Abrimos el documento
            docPdf.Open();

            // Titulo del listado con la imagen del programa
            iTextSharp.text.Table tablaImagen = new iTextSharp.text.Table(2);
            tablaImagen.WidthPercentage = 100;
            tablaImagen.BorderWidth = 0;
            tablaImagen.Cellpadding = 2;

            iTextSharp.text.Image imgGif = iTextSharp.text.Image.GetInstance(RutaImagen);
            imgGif.ScaleAbsolute(150, 35);
            imgGif.Alignment = iTextSharp.text.Image.LEFT_ALIGN;
            Cell celdaImagen = new Cell(imgGif);
            celdaImagen.Rowspan = 2;
            tablaImagen.AddCell(celdaImagen);

            Phrase fraseTituloListado = new Phrase(tituloListado, new Font(Font.HELVETICA, 12, Font.BOLD));
            Cell celdaTexto = new Cell(fraseTituloListado);
            celdaTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            tablaImagen.AddCell(celdaTexto);

            Phrase fraseFecha = new Phrase("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font(Font.HELVETICA, 8, Font.BOLD));
            Cell celdaFecha = new Cell(fraseFecha);
            celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;
            tablaImagen.AddCell(celdaFecha);

            docPdf.Add(tablaImagen);
        }
    }
}