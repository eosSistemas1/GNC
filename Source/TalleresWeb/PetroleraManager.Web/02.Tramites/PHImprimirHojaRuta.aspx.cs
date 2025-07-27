using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Tramites
{
    public partial class PHImprimirHojaRuta : PageBase
    {
        Generic genericos = new Generic();
        PHCilindrosLogic logic = new PHCilindrosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

            Guid idPhCil = new Guid(Request.QueryString[0].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            //Recupero la oblea
            var obj = logic.ReadDetallado(idPhCil);


            #region Impresion Ficha
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

            //HeaderFooter header;
            //header = new HeaderFooter(new Phrase(new Chunk("")), false);
            //header.Border = Rectangle.NO_BORDER;
            //document.Header = header;

            document.Open();
            iTextSharp.text.pdf.PdfContentByte cb;
            cb = writer.DirectContent;

            cb.BeginText();

            iTextSharp.text.pdf.BaseFont bf;
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            cb.SetFontAndSize(bf, 8);
            iTextSharp.text.Table tabla = new iTextSharp.text.Table(8, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;
            var font = new Font(bf,9);

            #region imprimo encabezado de Hoja de ruta
            Cell celTitulo1 = new Cell(new Phrase("HOJA DE RUTA",font));
            celTitulo1.Colspan = 2;
            celTitulo1.HorizontalAlignment = 1;
            celTitulo1.VerticalAlignment = 1;
            tabla.AddCell(celTitulo1);
            Cell celTitulo2 = new Cell(new Phrase("Nro. de revisión:" + "falta nro", font));
            celTitulo2.Colspan = 2;
            tabla.AddCell(celTitulo2);
            celTitulo2 = new Cell("");
            celTitulo2.Colspan = 2;
            tabla.AddCell(celTitulo2);
            Cell celTitulo3 = new Cell(new Phrase("Fecha de rev:" + obj.FechaHabilitacion.ToString("dd/MM/yyyy"), font));
            celTitulo3.Colspan = 2;
            tabla.AddCell(celTitulo3);

            Cell celTitulo4 = new Cell("");
            celTitulo4.Colspan = 2;
            celTitulo4.Rowspan = 4;
            tabla.AddCell(celTitulo4);

            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "01 - Identificación ", 100, 760, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "del cilindro ", 100, 750, 0);


            Cell cellSerieCil = new Cell(new Phrase("Nro. de serie: " + obj.SerieCil, font));
            cellSerieCil.Colspan = 2;
            tabla.AddCell(cellSerieCil);
            Cell cellCodigoCil = new Cell(new Phrase("Código: " + obj.CodigoCil, font));
            cellCodigoCil.Colspan = 2;
            tabla.AddCell(cellCodigoCil);
            Cell cellMarcaCil = new Cell(new Phrase("Marca: " + obj.MarcaCil, font));
            cellMarcaCil.Colspan = 2;
            tabla.AddCell(cellMarcaCil);

            Cell cellCapacidadCil = new Cell(new Phrase("Capacidad: " + obj.Capacidad, font));
            cellCapacidadCil.Colspan = 2;
            tabla.AddCell(cellCapacidadCil);
            Cell cellDiametroCil = new Cell(new Phrase("Diámetro: " + obj.Diametro.ToString(), font));
            cellDiametroCil.Colspan = 2;
            tabla.AddCell(cellDiametroCil);
            Cell cellNormaFabCil = new Cell(new Phrase("Norma Fab.: " + obj.NormaFabCil, font));
            cellNormaFabCil.Colspan = 2;
            tabla.AddCell(cellNormaFabCil);

            Cell cellParedCil = new Cell(new Phrase("Pared mín.: " + obj.Capacidad, font));
            cellParedCil.Colspan = 2;
            tabla.AddCell(cellParedCil);
            Cell cellFondoCil = new Cell(new Phrase("Fondo mín.: " + obj.Diametro.ToString(), font));
            cellFondoCil.Colspan = 2;
            tabla.AddCell(cellFondoCil);
            Cell cellMarcaVal = new Cell(new Phrase("Marca válvula: " + obj.MarcaVal, font));
            cellMarcaVal.Colspan = 2;
            cellMarcaVal.Rowspan = 2;
            tabla.AddCell(cellMarcaVal);

            Cell cellFechaFabCil = new Cell(new Phrase("Fecha fab.: " + obj.MesFabCil.ToString() + "/" + obj.AnioFabCil.ToString() , font));
            cellFechaFabCil.Colspan = 2;
            tabla.AddCell(cellFechaFabCil);
            Cell cellUltimaRevCil = new Cell(new Phrase("Ultima rev.: " + obj.Diametro.ToString(), font));
            cellUltimaRevCil.Colspan = 2;
            tabla.AddCell(cellUltimaRevCil);
            
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Taller.ToUpper(), 350, 710, 0);
            document.Add(tabla);
            #endregion
            cb.EndText();

            document.NewPage();

            cb.BeginText();

            tabla = new iTextSharp.text.Table(8, 5);
            tabla.WidthPercentage = 100;
            tabla.DefaultVerticalAlignment = 1;
            font = new Font(bf, 7);

            #region imprimo tarjeta
            cb.SetFontAndSize(bf, 8);
            int renglon = 800;
            int col1 = 50;
            int col2 = 325;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Código: " + obj.CodigoCil.ToUpper(), col1, renglon, 0); 
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Código: " + obj.CodigoCil.ToUpper(), col2, renglon, 0);
            renglon -= 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Serie: " + obj.SerieCil.ToUpper(), col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Serie: " + obj.SerieCil.ToUpper(), col2, renglon, 0);
            renglon -= 10;
            String taller = obj.Taller.Length > 50 ? obj.Taller.Substring(0, 48) + ".." : obj.Taller;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TdM: " + taller.ToUpper(), col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TdM: " + taller.ToUpper(), col2, renglon, 0);
            renglon -= 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Fecha reg.: " + obj.FechaHabilitacion.ToString("dd/MM/yyyy"), col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Fecha reg.: " + obj.FechaHabilitacion.ToString("dd/MM/yyyy"), col2, renglon, 0);
            renglon -= 10;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Obs: " + obj.Observacion, col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Obs: " + obj.Observacion, col2, renglon, 0);
            renglon -= 10;
            #endregion

            document.Add(tabla);
            ////imprimo encabezado de Hoja de ruta
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.MarcaVehiculo.ToUpper(), 50, renglon, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.ModeloVehiculo.ToUpper(), 180, renglon, 0);

            //renglon -= 10;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.Descripcion.Trim().ToUpper(), 60, renglon, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NroObleaNueva, 180, renglon, 0);

            //renglon -= 28;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.ObleasReguladores.FirstOrDefault().ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 55, renglon, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.ObleasReguladores.FirstOrDefault().ReguladoresUnidad.Descripcion.ToUpper(), 165, renglon, 0);

            //renglon -= 20;
            //foreach (ObleasCilindros objCil in obj.ObleasCilindros)
            //{
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilindrosUnidad.Cilindros.Descripcion.Trim().ToUpper(), 55, renglon, 0);

            //    String vto = "vto. " + objCil.MesUltimaRevisionCil.ToString().Trim() + "/" + genericos.FormatearAnio(objCil.AnioUltimaRevisionCil.ToString()).Substring(2, 2);
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilindrosUnidad.Descripcion.Trim() + " - " + vto, 165, renglon, 0);

            //    renglon -= 10;
            //}

            //renglon = 700;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.Descripcion.ToUpper(), 120, renglon, 0);

            //renglon -= 10;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaVencimiento.Value.ToString("MM/yy"), 25, renglon, 0);


            ////imprimo papelitos identificadores de cilindro y valvula
            //PECLogic peclogic = new PECLogic();
            //var pec = peclogic.ReadDetalladoByID(SiteMaster.Pec).FirstOrDefault();
            //bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            //cb.SetFontAndSize(bf, 8);
            //renglon = 813;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.Localidades.Descripcion.Substring(0, 3) + ". " + obj.FechaHabilitacion.Value.ToString("MM/yy"), 450, renglon, 0);

            //renglon -= 70;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.RazonSocialPEC, 320, renglon, 0);
            //renglon -= 10;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.DomicilioPEC + " - " + pec.Localidades.Descripcion.Trim() + " - " +
            //                                              pec.Localidades.Provincias.Descripcion + " - R.A.", 320, renglon, 0);
            //renglon -= 10;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TEL." + pec.TelefonoAPEC, 320, renglon, 0);

            //String CodigoBarra = obj.NroObleaNueva.Trim();
            //Barcode39 code39 = new Barcode39();
            //code39.Code = CodigoBarra;
            //iTextSharp.text.Image image39 = code39.CreateImageWithBarcode(writer.DirectContent, null, null);
            //image39.ScaleAbsoluteWidth(110);
            //image39.ScaleAbsoluteHeight(15);
            //image39.SetAbsolutePosition(415, renglon - 10);
            //cb.AddImage(image39);

            cb.EndText();

            document.Close();
            #endregion

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
    }
}
