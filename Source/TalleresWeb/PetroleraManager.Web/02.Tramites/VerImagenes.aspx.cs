using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web
{
    public partial class VerImagenes : PageBase
    {
        #region Properties
        Generic genericos = new Generic();
        ObleasLogic logic = new ObleasLogic();

        private String GetUrlImagenesObleas
        {
            get
            {
                return CrossCutting.DatosDiscretos.GetDinamyc.UrlImagenesObleas;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid idOblea = new Guid(Request.QueryString[0].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            //Recupero la oblea
            Obleas oblea = logic.ReadDetalladoByID(idOblea);

            #region Impresion Ficha
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


            String url = this.GetUrlImagenesObleas;

            this.AgregarImagenes(oblea, document);

            cb.EndText();

            document.Close();
            #endregion

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        private void AgregarImagenes(Obleas oblea, Document document)
        {
            String url = this.GetUrlImagenesObleas;

            String nombreArchivoDniFrente = String.Format("\\{0}_{1}_{2}.jpg", oblea.IdTaller, oblea.Vehiculos.Descripcion, "DNIFRENTE");
            String urlArchivoDniFrente = url + nombreArchivoDniFrente;

            String nombreArchivoDniDorso = String.Format("\\{0}_{1}_{2}.jpg", oblea.IdTaller, oblea.Vehiculos.Descripcion, "DNIDORSO");
            String urlArchivoDniDorso = url + nombreArchivoDniDorso;

            String nombreArchivoTarjetaFrente = String.Format("\\{0}_{1}_{2}.jpg", oblea.IdTaller, oblea.Vehiculos.Descripcion, "TJFRENTE");
            String urlArchivoTarjetaFrente = url + nombreArchivoTarjetaFrente;

            String nombreArchivoTarjetaDorso = String.Format("\\{0}_{1}_{2}.jpg", oblea.IdTaller, oblea.Vehiculos.Descripcion, "TJDORSO");
            String urlArchivoTarjetaDorso = url + nombreArchivoTarjetaDorso;



            if (File.Exists(urlArchivoDniFrente)) AgregarImagen(document, urlArchivoDniFrente, 10, 550);

            if (File.Exists(urlArchivoDniDorso)) AgregarImagen(document, urlArchivoDniDorso, 300, 550);

            if (File.Exists(urlArchivoTarjetaFrente)) AgregarImagen(document, urlArchivoTarjetaFrente, 10, 300);

            if (File.Exists(urlArchivoTarjetaDorso)) AgregarImagen(document, urlArchivoTarjetaDorso, 300, 300);
        }

        private void AgregarImagen(Document document, string urlImagen, int x, int y)
        {
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(File.ReadAllBytes(urlImagen));
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_CENTER;
            imagen.ScalePercent(85);
            imagen.SetAbsolutePosition(x, y);
            document.Add(imagen);
        }
        #endregion
    }
}