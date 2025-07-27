using System;
using iTextSharp.text;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManager.Web.Tramites
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
                TalleresWeb.Web.UI.Tramites.ImprimirPH.ImprimirCartaPH(document, cb, ph, false);
            }

            cb.EndText();

            document.Close();


            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }     
        #endregion
    }
}