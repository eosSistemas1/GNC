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
using System.IO;
using CrossCutting.DatosDiscretos;

namespace PetroleraManager.Web.Tramites
{
    public partial class ObleasImprimirTarjetaVerde : PageBase
    {
        Generic genericos = new Generic();
        ObleasLogic logic = new ObleasLogic();
        ObleaErrorDetalleLogic obleaErrorDetalleLogic = new ObleaErrorDetalleLogic();

        protected void Page_Load(object sender, EventArgs e)
        {        
            if (!IsPostBack)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";

                List<Guid> idObleasImprimir = new List<Guid>();
                if (Request.QueryString.Count > 0)
                {
                    Guid idOblea = new Guid(Request.QueryString[0].ToString());
                    idObleasImprimir.Add(idOblea);
                }
                else
                {
                    idObleasImprimir = Session["OBLEASSELECCIONADASIMPRIMIR"] as List<Guid>;

                }

                if(idObleasImprimir != null && idObleasImprimir.Any()) this.GenerarTarjetaVerde(idObleasImprimir);

                Session.Remove("OBLEASSELECCIONADASIMPRIMIR");
            }
        }

        private void GenerarTarjetaVerde(List<Guid> idObleasImprimir)
        {
            MemoryStream m = new MemoryStream();
            Document document = new Document(PageSize.A4, 50, 20, 50, 0);
            PdfContentByte cb = null;

            PdfWriter writer = PdfWriter.GetInstance(document, m);

            document.Open();

            cb = writer.DirectContent;

            List<TarjetasImpresasBasicView> obleasImpresas = new List<TarjetasImpresasBasicView>();

            foreach (var idOblea in idObleasImprimir)
            {
                #region Impresion Ficha

                //Recupero la oblea y los errores
                var oblea = logic.ReadDetalladoByID(idOblea);
                var errores = this.obleaErrorDetalleLogic.ReadErroresByIDOblea(idOblea);

                obleasImpresas.Add(new TarjetasImpresasBasicView()
                {
                    Taller = oblea.Talleres.RazonSocialTaller,
                    Dominio = oblea.Vehiculos.Descripcion,
                    NumeroObleaNueva = oblea.NroObleaNueva
                });

                HeaderFooter header;
                header = new HeaderFooter(new Phrase(new Chunk("")), false);
                header.Border = Rectangle.NO_BORDER;
                document.Header = header;

                cb.BeginText();

                int renglon = 794;

                iTextSharp.text.pdf.BaseFont bf;
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 9);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.Vehiculos.MarcaVehiculo.ToUpper(), 100, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.Vehiculos.ModeloVehiculo.ToUpper(), 230, renglon, 0);

                renglon -= 10;
                String dominio = errores.Any(e => e.IDObleaErrorTipo == ERRORTIPO.DOMINIO) ?
                                          errores.Where(e => e.IDObleaErrorTipo == ERRORTIPO.DOMINIO).First().Correccion :
                                          oblea.Vehiculos.Descripcion;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, dominio.Trim().ToUpper(), 100, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.NroObleaNueva, 230, renglon, 0);

                renglon -= 28;
                ObleasReguladores regulador = oblea.ObleasReguladores.FirstOrDefault(x => x.IdOperacion == MSDB.Sigue || x.IdOperacion == MSDB.Montaje);
                String homoREG = errores.Any(e => e.IDObleaErrorTipo == ERRORTIPO.REGULADORHomologacion) ?
                                         errores.Where(e => e.IDObleaErrorTipo == ERRORTIPO.REGULADORHomologacion).First().Correccion :
                                         regulador.ReguladoresUnidad.Reguladores.Descripcion;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, homoREG.ToUpper(), 100, renglon, 0);

                String serieREG = errores.Any(e => e.IDObleaErrorTipo == ERRORTIPO.REGULADORSerie) ?
                                          errores.Where(e => e.IDObleaErrorTipo == ERRORTIPO.REGULADORSerie).First().Correccion :
                                         regulador.ReguladoresUnidad.Descripcion;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, serieREG.ToUpper(), 230, renglon, 0);

                renglon -= 20;
                foreach (ObleasCilindros objCil in oblea.ObleasCilindros.Where(x => x.IdOperacion == MSDB.Sigue || x.IdOperacion == MSDB.Montaje))
                {

                    String homoCIL = errores.Any(e => e.IDObleaErrorTipo == ERRORTIPO.CILINDROHomologacion && e.IDObjetoCorregido == objCil.ID) ?
                                              errores.Where(e => e.IDObleaErrorTipo == ERRORTIPO.CILINDROHomologacion && e.IDObjetoCorregido == objCil.ID).First().Correccion :
                                              objCil.CilindrosUnidad.Cilindros.Descripcion;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, homoCIL.Trim().ToUpper(), 100, renglon, 0);

                    String serieCIL = errores.Any(e => e.IDObleaErrorTipo == ERRORTIPO.CILINDROSerie && e.IDObjetoCorregido == objCil.ID) ?
                                             errores.Where(e => e.IDObleaErrorTipo == ERRORTIPO.CILINDROSerie && e.IDObjetoCorregido == objCil.ID).First().Correccion :
                                             objCil.CilindrosUnidad.Descripcion;
                    String vto = "vto. " + objCil.MesUltimaRevisionCil.ToString().Trim() + "/" + genericos.FormatearAnio((objCil.AnioUltimaRevisionCil + 5).ToString()).Substring(2, 2);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, serieCIL.Trim().ToUpper() + " - " + vto, 230, renglon, 0);

                    renglon -= 10;
                }

                renglon = 688;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.Talleres.Descripcion.ToUpper(), 180, renglon, 0);

                renglon -= 10;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.FechaVencimiento.Value.ToString("MM/yy"), 100, renglon, 0);


                //REVERSO
                PECLogic peclogic = new PECLogic();
                var pec = peclogic.ReadDetalladoByID(SiteMaster.Pec).FirstOrDefault();
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_BOLD, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 9);
                renglon = 800;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.Localidades.Descripcion.Substring(0, 3) + ". " + oblea.FechaHabilitacion.Value.ToString("MM/yy"), 470, renglon, 0);

                renglon -= 70;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.RazonSocialPEC, 350, renglon, 0);
                renglon -= 10;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pec.DomicilioPEC + " - " + pec.Localidades.Descripcion.Trim() + " - " +
                                                              pec.Localidades.Provincias.Descripcion + " - R.A.", 350, renglon, 0);
                renglon -= 10;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TEL." + pec.TelefonoAPEC, 350, renglon, 0);

                String CodigoBarra = "22";// oblea.NroObleaNueva.Trim();
                Barcode39 code39 = new Barcode39();
                code39.Code = CodigoBarra;
                iTextSharp.text.Image image39 = code39.CreateImageWithBarcode(writer.DirectContent, null, null);
                image39.ScaleAbsoluteWidth(110);
                image39.ScaleAbsoluteHeight(15);
                image39.SetAbsolutePosition(415, renglon - 10);
                cb.AddImage(image39);
                #endregion

                if (idObleasImprimir.Count > 1 && idOblea != idObleasImprimir.Last()) document.NewPage();
            }

            cb.EndText();

            document.Close();

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
    }
}