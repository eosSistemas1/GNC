using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites
{
    public class ImprimirPH
    {
        public static void ImprimirCartaPH(Document document,
                                            PdfContentByte cb,
                                            Guid idPH, 
                                            Boolean newPage)
        {
            PHPrintViewWebApi ph = new PHLogic().ReadForPrint(idPH);

            ImprimirCartaPH(document, cb, ph, newPage);
        }


        public static void ImprimirCartaPH(Document document,
                                           PdfContentByte cb,
                                           PHPrintViewWebApi ph,
                                           Boolean newPage)
        {
            #region Impresion Ficha
            //iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

            //iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

            //HeaderFooter header;
            //header = new HeaderFooter(new Phrase(new Chunk("")), false);
            //header.Border = Rectangle.NO_BORDER;
            //document.Header = header;

            //document.Open();
            //iTextSharp.text.pdf.PdfContentByte cb;
            //cb = writer.DirectContent;

            if(newPage) document.NewPage();            

           
                cb.BeginText();

                iTextSharp.text.pdf.BaseFont bf;
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                cb.SetFontAndSize(bf, 12);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"Petrolera Italo Argentina SRL, {ph.FechaOperacion.ToString("dd/MM/yyyy")}", 180, 634, 0);

                int renglon = 450;
                foreach (PHCilindrosPrintView cil in ph.Cilindros)
                {
                    String cilindro = String.Format("{0}-{1}", cil.CodigoHomologacionCilindro.ToUpper().Trim(), cil.NumeroSerieCilindro.ToUpper().Trim()); 

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cilindro, 100, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.MarcaCilindro.ToUpper(), 175, renglon, 0);                    
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.Capacidad, 250, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{cil.MesFabCilindro}/{cil.AnioFabCilindro}", 290, renglon, 0);
                    String valvula = String.Format("{0}-{1}-{2}", cil.CodigoHomologacionValvula.ToUpper().Trim(), cil.MarcaValvula.ToUpper().Trim(), cil.NumeroSerieValvula.ToUpper().Trim());
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, valvula.ToUpper(), 355, renglon, 0);
                    //String obsCil = cil.ObservacionValvulaCilindro != null ? cil.ObservacionValvulaCilindro : String.Empty;
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obsCil.ToUpper(), 470, renglon, 0);

                    renglon -= 20;
                }

                renglon = 387;
                #region VEHICULO    (OK)
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteRazonSocial.ToUpper(), 170, renglon, 0);
                String docCliente = ph.ClienteTipoDocumento.ToUpper() + " " + ph.ClientesNumeroDocumento;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, docCliente, 450, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteDomicilio.ToString(), 170, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteTelefono.ToUpper(), 450, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteLocalidad.ToString(), 170, renglon, 0);                
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteCodigoPostal.ToUpper(), 360, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.ClienteProvincia.ToUpper(), 445, renglon, 0);
                renglon -= 21;
                String vehiculo = ph.VehiculoMarca + " " + ph.VehiculoModelo;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, vehiculo.ToString(), 170, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.VehiculoDominio.ToUpper(), 335, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.NroObleaHabilitante.Trim().ToUpper(), 480, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.TallerRazonSocial.ToUpper(), 200, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ph.PECRazonSocial, 450, renglon, 0);
                #endregion

                cb.EndText();             
            
            #endregion
        }
    }
}