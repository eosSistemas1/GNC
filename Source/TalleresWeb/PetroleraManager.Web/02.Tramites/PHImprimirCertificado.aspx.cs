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
    public partial class PHImprimirCertificado : PageBase
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


            #region Impresion Certificado
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

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
            var font = new Font(bf,7);

            #region imprimo tarjeta
            cb.SetFontAndSize(bf, 10);
            int renglon = 700;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.CodCRPC, 420, renglon+30, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NroCertif, 420, renglon+10, 0);
            renglon -= 60;
            //PROPIETARIO
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NombreyApellido.ToUpper(), 50, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Domicilio.ToUpper(), 200, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.CodPostal.ToUpper(), 480, renglon, 0);
            renglon -= 30;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Localidad.ToUpper(), 50, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Provincia.ToUpper(), 200, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Telefono.ToUpper(), 380, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.TipoDoc.ToUpper() + " " + obj.NroDoc, 450, renglon, 0);

            renglon -= 80;
            //CILINDRO
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.MarcaCil.ToUpper(), 40, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.CodigoCil.ToUpper(), 140, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.SerieCil.ToUpper(), 200, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.MesFabCil.ToString() + "/" + obj.AnioFabCil.ToString(), 300, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Capacidad.ToString(), 380, renglon, 0);
            //CASO
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 530, renglon+27, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 530, renglon+10, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 530, renglon-5, 0);
            renglon -= 60;
            //TALLER DE MONTAJE
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Taller, 100, renglon-15, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Pec, 350, renglon-15, 0);
            renglon -= 35;
            //CUMPLIO/NO CUMPLIO
            int colRechazado = 185;
            if (obj.Rechazado) colRechazado = 270;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", colRechazado, renglon-3, 0);

            //ANOMALIAS DETECTADAS
            renglon -= 80;
            int col1 = 55, col2 = 230, col3 = 405;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col3, renglon, 0);
            renglon -= 18;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col3, renglon, 0);
            renglon -= 18;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col3, renglon, 0);
            renglon -= 18;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col3, renglon, 0);
            renglon -= 18;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            renglon -= 18;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col1, renglon, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", col2, renglon, 0);
            
            //OBSERVACION
            renglon -= 30;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Observacion, col1, renglon, 0);

            //FECHAS
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaHabilitacion.ToString("dd/MM/yyyy"), col1, 90, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaHabilitacion.ToString("dd/MM/yyyy"), col1, 40, 0);
            #endregion

            document.Add(tabla);

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
