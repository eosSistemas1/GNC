using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.Web
{
    public partial class ImprimirOblea : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid idOblea = new Guid(Request.QueryString[0].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            ObleasLogic oLogic = new ObleasLogic();
            var obj = oLogic.ReadDetalladoByID(idOblea);

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

            for (int i = 0; i < GetDinamyc.CantCopiasFichaTecnica; i++)
            {

                cb.BeginText();

                iTextSharp.text.pdf.BaseFont bf;
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                #region FECHAS Y NRO DE OBLEA    (OK)
                cb.SetFontAndSize(bf, 12);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.FechaHabilitacion.Value.ToString("dd/MM/yyyy"), 380, 780, 0); //755
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Descripcion, 500, 780, 0);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Obleas.FechaVencimiento.ToShortDateString(), 380, 740, 0);
                #endregion

                //DATOS DEL TALLER
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.RazonSocialTaller.ToUpper(), 130, 715, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.DomicilioTaller.ToUpper(), 115, 695, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.CuitTaller, 110, 675, 0);
                //----------//FALTA CODIGO DE PEC Y COD TALLER MONTAJE
                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Nucleo.DatosDiscretos.IdPec.idPEAR, 380, 650, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.Descripcion.ToUpper(), 500, 675, 0);


                #region VEHICULO    (OK)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.MarcaVehiculo.ToUpper(), 100, 634, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.ModeloVehiculo.ToUpper(), 240, 634, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.AnioVehiculo.Value.ToString(), 387, 634, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.Descripcion.ToUpper(), 100, 595, 0);
                if (obj.Vehiculos.EsInyeccionVehiculo.Value)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 229, 595, 0);
                else
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 248, 595, 0);
                #endregion


                #region TIPO VEHICULO  (OK)
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.Taxi)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 279, 595, 0);
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.PickUp)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 304, 595, 0);
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.Particular)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 330, 595, 0);
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.Bus)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 353, 595, 0);
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.Oficial)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 378, 595, 0);
                if (obj.IdUso == CrossCutting.DatosDiscretos.TipoVehiculo.Otros)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 401, 595, 0);
                #endregion


                #region TIPO DE OPERACION   (OK)
                if (obj.IdOperacion == CrossCutting.DatosDiscretos.TipoOperacion.Conversion)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 658, 0);
                if (obj.IdOperacion == CrossCutting.DatosDiscretos.TipoOperacion.Modificacion)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 638, 0);
                if (obj.IdOperacion == CrossCutting.DatosDiscretos.TipoOperacion.RevisionAnual)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 619, 0);
                if (obj.IdOperacion == CrossCutting.DatosDiscretos.TipoOperacion.RevisionCRPC)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 601, 0);
                if (obj.IdOperacion == CrossCutting.DatosDiscretos.TipoOperacion.Baja)
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", 558, 582, 0);
                #endregion


                #region PROPIETARIO   (OK)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Descripcion.ToUpper(), 100, 546, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.DocumentosClientes.Descripcion.ToUpper() + " " + obj.Clientes.NroDniCliente, 420, 546, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.CalleCliente.ToUpper(), 100, 515, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, String.Empty, 470, 515, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, String.Empty, 540, 515, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.CodigoPostal != null? obj.Clientes.Localidades.CodigoPostal.ToUpper() : String.Empty, 100, 481, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Descripcion.ToUpper(), 215, 481, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Provincias.Descripcion.ToUpper(), 400, 481, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.TelefonoCliente.ToUpper(), 500, 481, 0);
                #endregion


                #region REGULADOR (OK)
                foreach (ObleasReguladores reg in obj.ObleasReguladores)
                {
                    if ((reg.IdOperacion == CrossCutting.DatosDiscretos.MSDB.Montaje)
                        || (reg.IdOperacion == CrossCutting.DatosDiscretos.MSDB.Sigue))
                    {
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 130, 440, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Descripcion.ToUpper(), 130, 415, 0);
                    }
                    else if (reg.IdOperacion == CrossCutting.DatosDiscretos.MSDB.Desmontaje)
                    {
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 185, 440, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Descripcion.ToUpper(), 185, 415, 0);
                    }
                    else if (reg.IdOperacion == CrossCutting.DatosDiscretos.MSDB.Baja)
                    {
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Reguladores.Descripcion.ToUpper(), 240, 440, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, reg.ReguladoresUnidad.Descripcion.ToUpper(), 240, 415, 0);
                    }
                }
                #endregion

                /*************************************/

                int renglon = 375;
                #region CILINDROS
                foreach (ObleasCilindros cil in obj.ObleasCilindros)
                {
                    foreach (ObleasValvulas val in cil.ObleasValvulas)
                    {
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.Cilindros.Descripcion.ToUpper(), 80, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.Descripcion.ToUpper(), 135, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.MesFabCilindro.Value.ToString(), 265, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Genericos.FormatearAnio(cil.CilindrosUnidad.AnioFabCilindro.Value.ToString()).Substring(2, 2), 280, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.MesUltimaRevisionCil.ToString(), 315, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Genericos.FormatearAnio(cil.AnioUltimaRevisionCil.ToString()).Substring(2, 2), 330, renglon, 0);

                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CRPC.Descripcion.Substring(5).Trim().ToUpper(), 355, renglon, 0);

                        String msdb = cil.Operaciones.CodigoGestionEnte.Trim().ToUpper();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, msdb, 393, renglon, 0);


                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, val.Valvula_Unidad.Valvula.Descripcion.ToUpper(), 428, renglon, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, val.Valvula_Unidad.Descripcion.ToUpper(), 478, renglon, 0);
                        String msdbVal = val.Operaciones.CodigoGestionEnte.Trim().ToUpper();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, msdbVal, 568, renglon, 0);

                        renglon -= 20;
                    }
                }
                #endregion

                cb.EndText();
                document.NewPage();
            }


            document.Close();
            #endregion

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
    }
}