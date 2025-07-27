using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CrossCutting.DatosDiscretos;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;
using TalleresWeb.Web.Cross;
using System.IO;

namespace PetroleraManager.Web.Tramites
{
    public partial class FotosImprimir : System.Web.UI.Page
    {
        #region Properties
        private ObleasLogic obleaslogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleaslogic == null) this.obleaslogic = new ObleasLogic();
                return obleaslogic;
            }
        }       
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid idOblea = new Guid(Request.QueryString["id"].ToString());
            Boolean imprimeFotos = Request.QueryString["fotos"] == null ? true : Boolean.Parse(Request.QueryString["fotos"].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            //Recupero la oblea
            ObleasViewWebApi oblea = this.ObleasLogic.ReadObleaByID(idOblea);           

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

            this.AgregarImagenes(oblea, document);

            cb.EndText();

            document.Close();


            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        private void AgregarImagenes(ObleasViewWebApi oblea, Document document)
        {
            String nombreArchivoDniFrente = String.Format("{0}_{1}_{2}", oblea.TallerID, oblea.VehiculoDominio, "DNIFRENTE");
            String urlArchivoDniFrente = Server.MapPath($"~/Captures/{nombreArchivoDniFrente}.png");

            String nombreArchivoDniDorso = String.Format("{0}_{1}_{2}", oblea.TallerID, oblea.VehiculoDominio, "DNIDORSO");
            String urlArchivoDniDorso = Server.MapPath($"~/Captures/{nombreArchivoDniDorso}.png");

            String nombreArchivoTarjetaFrente = String.Format("{0}_{1}_{2}", oblea.TallerID, oblea.VehiculoDominio, "TJFRENTE");
            String urlArchivoTarjetaFrente = Server.MapPath($"~/Captures/{nombreArchivoTarjetaFrente}.png");

            String nombreArchivoTarjetaDorso = String.Format("{0}_{1}_{2}", oblea.TallerID, oblea.VehiculoDominio, "TJDORSO");
            String urlArchivoTarjetaDorso = Server.MapPath($"~/Captures/{nombreArchivoTarjetaDorso}.png");
           

            if (File.Exists(urlArchivoDniFrente)) AgregarImagen(document, urlArchivoDniFrente, 10, 550);

            if (File.Exists(urlArchivoDniDorso)) AgregarImagen(document, urlArchivoDniDorso, 300, 550);

            if (File.Exists(urlArchivoTarjetaFrente)) AgregarImagen(document, urlArchivoTarjetaFrente, 10, 300);

            if (File.Exists(urlArchivoTarjetaDorso)) AgregarImagen(document, urlArchivoTarjetaDorso, 300, 300);
        }

        private void AgregarImagen(Document document, string urlImagen, int x, int y)
        {
            Image imagen = Image.GetInstance(File.ReadAllBytes(urlImagen));
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_CENTER;
            imagen.ScalePercent(85);
            imagen.SetAbsolutePosition(x, y);
            document.Add(imagen);
        }        
        #endregion
    }
}