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
    public partial class ObleasImprimir : System.Web.UI.Page
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
            Guid idOblea = new Guid(Request.QueryString["id"].ToString());
            Guid? idPH = Request.QueryString["idPH"] != null ? new Guid(Request.QueryString["idPH"].ToString()) : default(Guid?);
            Boolean imprimeFotos = Request.QueryString["fotos"] == null ? true : Boolean.Parse(Request.QueryString["fotos"].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            //Recupero la oblea
            ObleasViewWebApi oblea = this.ObleasLogic.ReadObleaByID(idOblea);

            PHPrintViewWebApi ph = null;
            if (idPH.HasValue)
                ph = this.PHLogic.ReadForPrint(idPH.Value);

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

            int cantidadCopiasOblea = 2; // ph != null ? 2 : 1;

            for (int i = 0; i < cantidadCopiasOblea; i++)
            {
                GenerarFichaOblea(oblea, cb, bf);
                if (i == cantidadCopiasOblea - 1) continue;
                document.NewPage();
            }

            if (ph != null)
            {
                TalleresWeb.Web.UI.Tramites.ImprimirPH.ImprimirCartaPH(document, cb, ph, true);
            }

            if (imprimeFotos)
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

            if (File.Exists(urlArchivoDniFrente)
                || File.Exists(urlArchivoDniDorso)
                || File.Exists(urlArchivoTarjetaFrente)
                || File.Exists(urlArchivoTarjetaDorso))
            {
                document.NewPage();
            }

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

        private static void GenerarFichaOblea(ObleasViewWebApi oblea, PdfContentByte cb, BaseFont bf)
        {
            #region FECHAS Y NRO DE OBLEA    (OK)
            cb.SetFontAndSize(bf, 12);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.FechaHabilitacion.ToString("dd/MM/yyyy"), 380, 780, 0); //755
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.ObleaNumeroAnterior, 500, 780, 0);
            //if (!String.IsNullOrEmpty(obj.NroObleaNueva))
            //{
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NroObleaNueva, 500, 740, 0);
            //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaVencimiento.Value.ToShortDateString(), 380, 740, 0);
            //}
            #endregion

            #region DATOS DEL TALLER
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.TallerRazonSocial.ToUpper(), 130, 715, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.TalleresDomicilio.ToUpper(), 115, 695, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.TallerCuit, 110, 675, 0);
            ////----------//FALTA CODIGO DE PEC Y COD TALLER MONTAJE
            ////cb.showTextAligned(PdfContentByte.ALIGN_LEFT, Nucleo.DatosDiscretos.IdPec.idPEAR, 380, 650, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.TallerMatricula.ToUpper(), 500, 675, 0);
            #endregion

            #region VEHICULO    (OK)
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.VehiculoMarca.ToUpper(), 100, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.VehiculoModelo.ToUpper(), 240, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.VehiculoAnio.ToString(), 387, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.VehiculoDominio.ToUpper(), 100, 595, 0);
            if (oblea.VehiculoEsInyeccion.Value)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 229, 595, 0);
            else
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 248, 595, 0);
            #endregion

            #region TIPO VEHICULO  (OK)
            if (oblea.IdUso == TIPOVEHICULO.Taxi)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 279, 595, 0);
            if (oblea.IdUso == TIPOVEHICULO.PickUp)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 304, 595, 0);
            if (oblea.IdUso == TIPOVEHICULO.Particular)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 330, 595, 0);
            if (oblea.IdUso == TIPOVEHICULO.Bus)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 353, 595, 0);
            if (oblea.IdUso == TIPOVEHICULO.Oficial)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 378, 595, 0);
            if (oblea.IdUso == TIPOVEHICULO.Otros)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 401, 595, 0);
            #endregion

            #region TIPO DE OPERACION   (OK)
            if (oblea.OperacionID == TIPOOPERACION.Conversion)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 658, 0);
            if (oblea.OperacionID == TIPOOPERACION.Modificacion)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 638, 0);
            if (oblea.OperacionID == TIPOOPERACION.RevisionAnual)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 619, 0);
            if (oblea.OperacionID == TIPOOPERACION.RevisionCRPC)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 601, 0);
            if (oblea.OperacionID == TIPOOPERACION.Baja)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 582, 0);
            #endregion

            #region PROPIETARIO   (OK)
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.ClienteNombreApellido.ToUpper(), 100, 546, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.ClienteTipoDocumento.ToUpper() + " " + oblea.ClienteNumeroDocumento, 420, 546, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.ClienteDomicilio.ToUpper(), 100, 515, 0);
            //if (obj.Clientes.NroCalleCliente != null) cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.NroCalleCliente.ToUpper(), 470, 515, 0);
            //if (obj.Clientes.PisoDptoCliente != null) cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.PisoDptoCliente.ToUpper(), 540, 515, 0);

            //String cp = obj.ClienteLocalidadCP != null ? obj.ClienteLocalidadCP.Trim().ToUpper() : String.Empty;
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cp, 100, 481, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, oblea.ClienteLocalidad.Trim().ToUpper(), 215, 481, 0);

            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.ClienteLocalidadProvincias.ToUpper(), 400, 481, 0);
            String telefono = !String.IsNullOrWhiteSpace(oblea.ClienteTelefono) ? oblea.ClienteTelefono.ToUpper() : String.Empty;
            //String celular = ""; 
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, telefono, 500, 481, 0);
            #endregion

            #region REGULADOR
            foreach (ObleasReguladoresExtendedView objReg in oblea.Reguladores)
            {
                //Recupero la oblea
                if ((objReg.MSDBRegID == MSDB.Montaje)
                    || (objReg.MSDBRegID == MSDB.Sigue))
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.CodigoReg.ToUpper(), 130, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.NroSerieReg.ToUpper(), 130, 415, 0);
                }
                else if (objReg.MSDBRegID == MSDB.Desmontaje)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.CodigoReg.ToUpper(), 185, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.NroSerieReg.ToUpper(), 185, 415, 0);
                }
                else if (objReg.MSDBRegID == MSDB.Baja)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.CodigoReg.ToUpper(), 240, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.NroSerieReg.ToUpper(), 240, 415, 0);
                }
            }
            #endregion

            int renglon = 375;
            #region CILINDROS
            foreach (ObleasCilindrosExtendedView objCil in oblea.Cilindros)
            {
                String cilFabAnio = string.IsNullOrWhiteSpace(objCil.CilFabAnio) ? String.Empty : Funciones.FormatearAnio(objCil.CilFabAnio).Substring(2, 2);
                String cilRevAnio = string.IsNullOrWhiteSpace(objCil.CilRevAnio) ? String.Empty : Funciones.FormatearAnio(objCil.CilRevAnio).Substring(2, 2);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CodigoCil.ToUpper(), 80, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.NroSerieCil.ToUpper(), 135, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilFabMes, 265, renglon, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cilFabAnio, 280, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilRevMes, 315, renglon, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cilRevAnio, 330, renglon, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CRPCCil.Trim().ToUpper(), 355, renglon, 0);

                String strmsdb = "M";
                if (objCil.MSDBCilID == MSDB.Sigue) strmsdb = "S";
                if (objCil.MSDBCilID == MSDB.Desmontaje) strmsdb = "D";
                if (objCil.MSDBCilID == MSDB.Baja) strmsdb = "B";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, strmsdb, 393, renglon, 0);

                renglon -= 20;
            }
            #endregion

            renglon = 375;
            #region VALVULAS
            foreach (var valvula in oblea.Valvulas)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, valvula.CodigoVal, 428, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, valvula.NroSerieVal, 478, renglon, 0);

                String msdbVal = "M";
                if (valvula.MSDBValID == MSDB.Sigue) msdbVal = "S";
                if (valvula.MSDBValID == MSDB.Desmontaje) msdbVal = "D";
                if (valvula.MSDBValID == MSDB.Baja) msdbVal = "B";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, msdbVal, 568, renglon, 0);

                renglon -= 20;
            }

            #endregion
        }
        #endregion
    }
}