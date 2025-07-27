using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PL.Fwk.Presentation.Web.PLReportCreator
{
    public class PLReportCreator : System.Web.UI.Page
    {
        public enum eOrientacion { HORIZONTAL, VERTICAL };
        protected static Font fuenteTitulos = new Font(Font.HELVETICA, 8, Font.BOLD);
        protected static Font fuenteDatos = new Font(Font.HELVETICA, 8);

        public PLReportCreator() : base()
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            
        }


        /// <summary>
        /// Generación de un listado (al vuelo) en base al datatable pasado
        /// </summary>
        /// <param name="dt">Datatable con los datos a imprimir</param>
        /// <param name="dtCampos">Campos a imprimir del datatable junto con la descripcion de ellos</param>
        /// <param name="orPagina">Orientacion de las paginas</param>
        /// <param name="anchosColumna">Porcentajes de anchos para cada columna (por orden)</param>
        /// <param name="tituloListado">Titulo del Listado</param>
        /// <returns>MemoryStream para poder lanzar con response.write()</returns>
        public static MemoryStream GenerarListado(DataTable dt,
                                                    DtCamposListadoPDF dtCampos,
                                                    eOrientacion orPagina,
                                                    int[] anchosColumna,
                                                    string tituloListado,
                                                    string rutaImagen)
        {
            Document docPDF = null;
            if (orPagina == eOrientacion.HORIZONTAL)
            {
                docPDF = new Document(PageSize.A4.Rotate());
            }
            if (orPagina == eOrientacion.VERTICAL)
            {
                docPDF = new Document(PageSize.A4);
            }

            MemoryStream memStream = new MemoryStream();

            try
            {
                PdfWriter.GetInstance(docPDF, memStream);

                // Incluimos los elementos comunes
                IncluirElementosComunes(docPDF, tituloListado, tituloListado, rutaImagen);

                // Creamos las columnas
                iTextSharp.text.Table tabla = new iTextSharp.text.Table(dtCampos.Rows.Count);

                tabla.WidthPercentage = 100;
                tabla.SetWidths(anchosColumna);
                tabla.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                tabla.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
                tabla.BorderWidth = 1;
                tabla.Cellpadding = 3;

                // Creamos la cabecera
                foreach (DataRow dr in dtCampos.Rows)
                {
                    Phrase frase = new Phrase(dr[DtCamposListadoPDF.NOMBRE_A_MOSTRAR].ToString(), fuenteTitulos);
                    Cell celda = new Cell(frase);
                    celda.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                    tabla.AddCell(celda);
                }

                DataView dv = dt.DefaultView;

                for (int i = 0; i < dv.Count; i++)
                {
                    DataRow drDatos = dv[i].Row;
                    foreach (DataRow drCampo in dtCampos.Rows)
                    {
                        string nombreCampo = drCampo[DtCamposListadoPDF.CAMPO_DATATABLE].ToString();
                        string texto = drDatos[nombreCampo].ToString();
                        if (texto.ToUpper() == "TRUE")
                        {
                            texto = "SI";
                        }
                        if (texto.ToUpper() == "FALSE")
                        {
                            texto = "NO";
                        }
                        Phrase frase = new Phrase(texto, fuenteDatos);
                        tabla.AddCell(frase);
                    }
                }
                //tabla.HeaderRows = 1;
                docPDF.Add(tabla);
            }
            catch
            {
                return null;
            }
            docPDF.Close();
            return memStream;
        }

        /// <summary>
        /// Incluir la cabecera, el pie y el titulo del listado
        /// </summary>
        /// <param name="docPdf"></param>
        /// <param name="cabecera"></param>
        /// <param name="tituloListado"></param>
        /// <param name="rutaImagen"></param>
        protected static void IncluirElementosComunes(Document docPdf, string tituloCabecera,
            string tituloListado, string rutaImagen)
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

            iTextSharp.text.Image imgGif = iTextSharp.text.Image.GetInstance(rutaImagen);
            imgGif.ScaleAbsolute(150,35);
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
