using iTextSharp.text;
using CrossCutting.DatosDiscretos;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class VerImagenes : PageBase
    {
        #region Properties

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
            var oblea = logic.ReadDetalladoByID(idOblea);

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

        private void AgregarImagenes(TalleresWeb.Entities.Obleas oblea, Document document)
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


            List<string> imagenes = new List<string>();

            if (File.Exists(urlArchivoTarjetaFrente)) imagenes.Add(urlArchivoTarjetaFrente);          
            if (File.Exists(urlArchivoTarjetaDorso)) imagenes.Add(urlArchivoTarjetaDorso);
            if (File.Exists(urlArchivoDniFrente)) imagenes.Add(urlArchivoDniFrente);
            if (File.Exists(urlArchivoDniDorso)) imagenes.Add(urlArchivoDniDorso);

            AgregarImagen(document, imagenes);

        }

        private void AgregarImagen(Document document, List<string> imagenes)
        {
            var pageHeigt = iTextSharp.text.PageSize.A5.Height;

            int mediaPagina = 0;

            foreach (var urlImagen in imagenes)
            {
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(File.ReadAllBytes(urlImagen));
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                imagen.ScalePercent(70);
                
                if (imagen.Height > (iTextSharp.text.PageSize.A4.Height / 2))
                {
                    if (mediaPagina == 1) document.NewPage();
                    
                    imagen.SetAbsolutePosition(10, 10);
                    imagen.ScaleToFit(iTextSharp.text.PageSize.A4.Width, iTextSharp.text.PageSize.A4.Height);
                    mediaPagina = 2;                    
                }
                else
                {
                    if(mediaPagina == 0)
                        imagen.SetAbsolutePosition(10, 10);
                    else
                        imagen.SetAbsolutePosition(10, (iTextSharp.text.PageSize.A4.Height/2));

                    //falta ver si puso dos imagenes que agregue pagina y ubicar las imagenes
                    imagen.ScaleToFit(iTextSharp.text.PageSize.A5.Width, iTextSharp.text.PageSize.A5.Height);
                    mediaPagina++;
                }

                document.Add(imagen);

                if (imagenes.Last() != urlImagen && mediaPagina == 2)
                {
                    mediaPagina = 0;
                    document.NewPage();
                }                
            }

            
            
            
            

            

            //if (imagen.Height > (iTextSharp.text.PageSize.A4.Height / 2))
            //    document.NewPage();

            
        }
        #endregion
    }
}