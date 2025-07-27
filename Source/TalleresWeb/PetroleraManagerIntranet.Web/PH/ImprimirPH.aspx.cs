using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class ImprimirPH : PageBase
    {
        #region Properties     
        private PHLogic phLogic;
        private PHLogic PHLogic
        {
            get
            {
                if (this.phLogic == null) this.phLogic = new PHLogic();
                return phLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {

            Guid idPH = new Guid(Request.QueryString[0].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            PHPrintViewWebApi ph = this.PHLogic.ReadForPrint(idPH);

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

            HeaderFooter header;
            header = new HeaderFooter(new Phrase(new Chunk("")), false);
            header.Border = Rectangle.NO_BORDER;
            document.Header = header;

            document.Open();
            iTextSharp.text.pdf.PdfContentByte cb;
            cb = writer.DirectContent;

            cb.BeginText();

            iTextSharp.text.pdf.BaseFont bf;
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            if (ph != null)
            {
                this.ImprimirCartaPH(document, cb, ph, false);
            }

            cb.EndText();
            document.Close();

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        public void ImprimirCartaPH(Document document,
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

            if (newPage) document.NewPage();


            cb.BeginText();

            iTextSharp.text.pdf.BaseFont bf;
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            cb.SetFontAndSize(bf, 12);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Petrolera Italo Argentina SRL.-", 180, 634, 0);

            int renglon = 450;
            foreach (PHCilindrosPrintView cil in ph.Cilindros)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.NumeroSerieCilindro.ToUpper(), 100, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.MarcaCilindro.ToUpper(), 175, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.Capacidad, 250, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{cil.MesFabCilindro}/{cil.AnioFabCilindro}", 290, renglon, 0);
                String valvula = cil.MarcaValvula + " " + cil.NumeroSerieValvula;
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
        #endregion
    }
}