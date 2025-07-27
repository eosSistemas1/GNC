using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ImprimirOblea : PageBase
    {
        #region Properties     
        private ObleasLogic obleasLogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null) this.obleasLogic = new ObleasLogic();
                return obleasLogic;
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
            var obj = this.ObleasLogic.ReadDetalladoByID(idOblea);

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

            #region FECHAS Y NRO DE OBLEA    (OK)
            cb.SetFontAndSize(bf, 12);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaHabilitacion.Value.ToShortDateString(), 380, 780, 0); //755
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Descripcion, 500, 780, 0);
            if (!String.IsNullOrEmpty(obj.NroObleaNueva))
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NroObleaNueva, 500, 740, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaVencimiento.Value.ToShortDateString(), 380, 740, 0);
            }
            #endregion

            #region DATOS DEL TALLER
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.RazonSocialTaller.ToUpper(), 130, 715, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.DomicilioTaller.ToUpper(), 115, 695, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.CuitTaller, 110, 675, 0);
            //----------//FALTA CODIGO DE PEC Y COD TALLER MONTAJE
            //cb.showTextAligned(PdfContentByte.ALIGN_LEFT, Nucleo.DatosDiscretos.IdPec.idPEAR, 380, 650, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.Descripcion.ToUpper(), 500, 675, 0);
            #endregion

            #region VEHICULO    (OK)
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.MarcaVehiculo.ToUpper(), 100, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.ModeloVehiculo.ToUpper(), 240, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.AnioVehiculo.ToString(), 387, 634, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.Descripcion.ToUpper(), 100, 595, 0);
            if (obj.Vehiculos.EsInyeccionVehiculo.Value)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 229, 595, 0);
            else
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 248, 595, 0);
            #endregion

            #region TIPO VEHICULO  (OK)
            if (obj.IdUso == TIPOVEHICULO.Taxi)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 279, 595, 0);
            if (obj.IdUso == TIPOVEHICULO.PickUp)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 304, 595, 0);
            if (obj.IdUso == TIPOVEHICULO.Particular)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 330, 595, 0);
            if (obj.IdUso == TIPOVEHICULO.Bus)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 353, 595, 0);
            if (obj.IdUso == TIPOVEHICULO.Oficial)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 378, 595, 0);
            if (obj.IdUso == TIPOVEHICULO.Otros)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 401, 595, 0);
            #endregion


            #region TIPO DE OPERACION   (OK)
            if (obj.IdOperacion == TIPOOPERACION.Conversion)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 658, 0);
            if (obj.IdOperacion == TIPOOPERACION.Modificacion)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 638, 0);
            if (obj.IdOperacion == TIPOOPERACION.RevisionAnual)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 619, 0);
            if (obj.IdOperacion == TIPOOPERACION.RevisionCRPC)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 601, 0);
            if (obj.IdOperacion == TIPOOPERACION.Baja)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 582, 0);
            #endregion


            #region PROPIETARIO   (OK)
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Descripcion.ToUpper(), 100, 546, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.DocumentosClientes.Descripcion.ToUpper() + " " + obj.Clientes.NroDniCliente, 420, 546, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.CalleCliente.ToUpper(), 100, 515, 0);
            if (obj.Clientes.NroCalleCliente != null) cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.NroCalleCliente.ToUpper(), 470, 515, 0);
            if (obj.Clientes.PisoDptoCliente != null) cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.PisoDptoCliente.ToUpper(), 540, 515, 0);

            String cp = obj.Clientes.Localidades.CodigoPostal != null ? obj.Clientes.Localidades.CodigoPostal.Trim().ToUpper() : String.Empty;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cp, 100, 481, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Descripcion.Trim().ToUpper(), 215, 481, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Provincias.Descripcion.ToUpper(), 400, 481, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, !String.IsNullOrWhiteSpace(obj.Clientes.TelefonoCliente) ? obj.Clientes.TelefonoCliente.ToUpper() : String.Empty, 500, 481, 0);
            #endregion



            #region REGULADOR (OK)
            foreach (ObleasReguladores objReg in obj.ObleasReguladores)
            {
                //Recupero la oblea
                if ((objReg.IdOperacion == MSDB.Montaje)
                    || (objReg.IdOperacion == MSDB.Sigue))
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 130, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Descripcion.ToUpper(), 130, 415, 0);
                }
                else if (objReg.IdOperacion == MSDB.Desmontaje)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 185, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Descripcion.ToUpper(), 185, 415, 0);
                }
                else if (objReg.IdOperacion == MSDB.Baja)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 240, 440, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objReg.ReguladoresUnidad.Descripcion.ToUpper(), 240, 415, 0);
                }
            }
            #endregion

            ///*************************************/

            int renglon = 375;
            #region CILINDROS
            foreach (ObleasCilindros objCil in obj.ObleasCilindros)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilindrosUnidad.Cilindros.Descripcion.ToUpper(), 80, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CilindrosUnidad.Descripcion.Trim(), 135, renglon, 0);
                string mesFabCilindro = objCil.CilindrosUnidad.MesFabCilindro.HasValue ? objCil.CilindrosUnidad.MesFabCilindro.Value.ToString() : String.Empty;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, mesFabCilindro, 265, renglon, 0);
                string anioFabCilindro = objCil.CilindrosUnidad.AnioFabCilindro.HasValue ? Funciones.FormatearAnio(objCil.CilindrosUnidad.AnioFabCilindro.Value.ToString()).Substring(2, 2) : String.Empty;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, anioFabCilindro, 280, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.MesUltimaRevisionCil.ToString(), 315, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Funciones.FormatearAnio(objCil.AnioUltimaRevisionCil.ToString()).Substring(2, 2), 330, renglon, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, objCil.CRPC.Descripcion.Trim().ToUpper(), 355, renglon, 0);

                String strmsdb = "M";
                if (objCil.IdOperacion == MSDB.Sigue) strmsdb = "S";
                if (objCil.IdOperacion == MSDB.Desmontaje) strmsdb = "D";
                if (objCil.IdOperacion == MSDB.Baja) strmsdb = "B";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, strmsdb, 393, renglon, 0);

                renglon -= 20;
            }
            #endregion




            renglon = 375;
            #region VALVULAS
            foreach (ObleasCilindros objValCil in obj.ObleasCilindros)
            {
                foreach (var valvula in objValCil.ObleasValvulas)
                {
                    String obleaValvulaDescripcion = valvula.Valvula_Unidad != null ? valvula.Valvula_Unidad.Valvula.Descripcion.ToUpper() : String.Empty;
                    String valvulaDescripcion = valvula.Valvula_Unidad != null ? valvula.Valvula_Unidad.Descripcion.ToUpper() : String.Empty;

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obleaValvulaDescripcion, 428, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, valvulaDescripcion, 478, renglon, 0);

                    String msdbVal = "M";
                    if (valvula.IdOperacion == MSDB.Sigue) msdbVal = "S";
                    if (valvula.IdOperacion == MSDB.Desmontaje) msdbVal = "D";
                    if (valvula.IdOperacion == MSDB.Baja) msdbVal = "B";
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, msdbVal, 568, renglon, 0);

                    renglon -= 20;
                }
            }
            #endregion


            cb.EndText();

            document.Close();
            #endregion

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
        #endregion
    }
}