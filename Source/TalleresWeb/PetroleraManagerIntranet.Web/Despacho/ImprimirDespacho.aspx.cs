using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Despacho
{
    public partial class ImprimirDespacho : PageBase
    {

        #region Properties
        private DespachoLogic despachoLogic;
        public DespachoLogic DespachoLogic
        {
            get
            {
                if (despachoLogic == null) despachoLogic = new DespachoLogic();
                return despachoLogic;
            }
        }

        private TalleresLogic talleresLogic;
        public TalleresLogic TalleresLogic
        {
            get
            {
                if (talleresLogic == null) talleresLogic = new TalleresLogic();
                return talleresLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Guid despachoID = new Guid(Request.QueryString["id"].ToString());
                var despacho = this.DespachoLogic.Read(despachoID);

                int fontSize = 9;

                Response.Clear();
                Response.ContentType = "application/pdf";
                System.IO.MemoryStream m = new System.IO.MemoryStream();

                #region Imprimo el despacho
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 20, 50, 0);

                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, m);

                HeaderFooter header;
                header = new HeaderFooter(new Phrase(new Chunk("")), false);
                header.Border = Rectangle.NO_BORDER;
                document.Header = header;

                document.Open();
                iTextSharp.text.pdf.PdfContentByte cb;
                cb = writer.DirectContent;
                iTextSharp.text.pdf.BaseFont bf;
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                #region impresion despacho
                int cantRenglones = 0;
                cb.BeginText();
                cb.SetFontAndSize(bf, fontSize);

                // Titulo del listado con la imagen del programa
                iTextSharp.text.Table tablaImagen = new iTextSharp.text.Table(2);
                tablaImagen.WidthPercentage = 100;
                tablaImagen.BorderWidth = 0;
                tablaImagen.Cellpadding = 2;

                //String rutaImagen = PageBase.UrlBase + DatosDiscretos.GetDinamyc.LogoEmpresa;
                //iTextSharp.text.Image imgGif = iTextSharp.text.Image.GetInstance(rutaImagen);
                //imgGif.ScaleAbsolute(150, 50);
                //imgGif.Alignment = iTextSharp.text.Image.LEFT_ALIGN;
                //Cell celdaImagen = new Cell(imgGif);
                //celdaImagen.Rowspan = 2;
                //celdaImagen.Border = 0;
                //celdaImagen.BorderWidth = 0;
                //tablaImagen.AddCell(celdaImagen);
                //document.Add(tablaImagen);

                #region Encabezado Comp
                //fecha
                cb.SetFontAndSize(bf, fontSize);
                

                ////datos hoja de ruta
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Flete: " + despacho.FLETE.Descripcion.ToUpper(), 100, 755, 0);                               
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Fecha Entrega: " + DateTime.Now.ToString("dd/MM/yyyy"), 100, 740, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nro. Despacho: " + despacho.Numero, 100, 725, 0);

                String code = despacho.Numero.ToString("00000000");
                var fontFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                var bcbf = BaseFont.CreateFont(fontFile, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var B = new iTextSharp.text.pdf.BarcodeInter25();
                B.Font = bcbf;
                B.Code = code;
                var img = B.CreateImageWithBarcode(writer.DirectContent, Color.BLACK, Color.BLACK);
                img.ScaleToFit(75, 75);
                img.SetAbsolutePosition(400, 750);
                cb.AddImage(img);
                #endregion

                int renglon = 700;
                foreach (var taller in despacho.DESPACHODETALLE.GroupBy(x => x.IdTaller))
                {
                    List<DESPACHODETALLE> datosPorTaller = despacho.DESPACHODETALLE.Where(c => c.IdTaller == taller.First().IdTaller).ToList();

                    var datosTaller = this.TalleresLogic.Read(datosPorTaller.First().IdTaller.Value);
                    String strCliente = $"TALLER: ({datosTaller.Descripcion}) { datosTaller.RazonSocialTaller}    /    {datosTaller.DomicilioTaller}       ZONA: {datosTaller.Zona}       TRÁMITES: {datosPorTaller.Count()}";
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, strCliente, 20, renglon, 0);
                    renglon -= 15;

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TIPO TRAMITE", 20, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "OPERACIÓN", 120, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "OBLEA HABILITANTE", 220, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "DOMINIO", 320, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "OBLEA ASIGN./CERTIF. PH", 420, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CILINDRO", 520, renglon, 0);
                    cantRenglones++;

                    foreach (var item in datosPorTaller)
                    {
                        renglon -= 15;
                        if (item.IdOblea.HasValue)
                        {
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Oblea", 20, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.Obleas.Operaciones.Descripcion.ToString(), 120, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.Obleas.Descripcion.ToString(), 220, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.Obleas.Vehiculos.Descripcion.ToString(), 320, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.Obleas.NroObleaNueva ?? String.Empty, 420, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "", 520, renglon, 0);
                        }
                        if (item.IdPHCilindro.HasValue)
                        {
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PH", 20, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.PHCilindros.PH.Vehiculos.Descripcion.ToString(), 120, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.PHCilindros.PH.NroObleaHabilitante.ToString(), 220, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.PHCilindros.PH.Vehiculos.Descripcion.ToString(), 320, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.PHCilindros.PH.Descripcion.ToString(), 420, renglon, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "", 520, renglon, 0);
                        }
                        if (item.IdPedido.HasValue)
                        {
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Pedido", 20, renglon, 0);
                            //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, item.Obleas.Vehiculos.Descripcion.ToString(), 310, renglon, 0);
                        }


                        cantRenglones++;

                        if (cantRenglones >= 30)
                        {
                            cb.EndText();
                            cb.PdfDocument.NewPage();

                            //reinicio los contadores
                            cantRenglones = 0;
                            renglon = 800;

                            cb.BeginText();
                            cb.SetFontAndSize(bf, fontSize);
                        }
                    }

                    renglon -= 90;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "-------------------------------------", 250, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Recibí Conforme", 300, renglon -15, 0);
                    renglon -= 45;

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", 15, renglon, 0);

                    renglon -= 30;

                    cantRenglones += 15;

                    if (cantRenglones >= 30)
                    {
                        cb.EndText();
                        cb.PdfDocument.NewPage();

                        //reinicio los contadores
                        cantRenglones = 0;
                        renglon = 800;

                        cb.BeginText();
                        cb.SetFontAndSize(bf, fontSize);
                    }
                }

                cb.EndText();
                cb.PdfDocument.NewPage();

                #endregion



                document.Close();
                #endregion

                Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
            }
        }
        #endregion
    }
}