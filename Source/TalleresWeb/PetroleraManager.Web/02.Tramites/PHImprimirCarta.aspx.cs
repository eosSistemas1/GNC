using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using iTextSharp.text;
using DatosDiscretos;
using iTextSharp.text.pdf;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Tramites
{
    public partial class PHImprimirCarta : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Guid idPH = new Guid(Request.QueryString["id"].ToString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

            PHLogic oLogic = new PHLogic();
            var obj = oLogic.ReadDetalladoByID(idPH);

            ImprimirCarta(m, obj);
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }

        private static void ImprimirCarta(System.IO.MemoryStream m, PH obj)
        {
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

            for (int i = 0; i < GetDinamyc.CantCopiasCartaPH; i++)
            {

                cb.BeginText();

                iTextSharp.text.pdf.BaseFont bf;
                bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                cb.SetFontAndSize(bf, 12);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.RazonSocialTaller.ToUpper(), 180, 634, 0);

                int renglon = 450;
                foreach (PHCilindros cil in obj.PHCilindros)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.Descripcion.ToUpper(), 100, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion.ToUpper(), 175, renglon, 0);
                    String capacidad = cil.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ? cil.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : String.Empty;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, capacidad, 250, renglon, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cil.CilindrosUnidad.MesFabCilindro.Value.ToString() +
                                                                  "/" + cil.CilindrosUnidad.AnioFabCilindro.Value.ToString(), 290, renglon, 0);
                    String valvula = cil.Valvula_Unidad.Valvula.Descripcion + " " + cil.Valvula_Unidad.Descripcion;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, valvula.ToUpper(), 355, renglon, 0);
                    String obsCil = cil.ObservacionValvulaCilindro != null ? cil.ObservacionValvulaCilindro : String.Empty;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obsCil.ToUpper(), 470, renglon, 0);

                    renglon -= 20;
                }

                renglon = 387;
                #region VEHICULO    (OK)
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Descripcion.ToUpper(), 170, renglon, 0);
                String docCliente = obj.Clientes.DocumentosClientes.Descripcion.ToUpper() + " " + obj.Clientes.NroDniCliente;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, docCliente, 450, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.CalleCliente.ToString(), 170, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.TelefonoCliente.ToUpper(), 450, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Descripcion.ToString(), 170, renglon, 0);
                String cpa = "2000";// obj.Clientes.Localidades.CodigoPostal != null ? obj.Clientes.Localidades.CodigoPostal : String.Empty;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cpa.ToUpper(), 360, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Clientes.Localidades.Provincias.Descripcion.ToUpper(), 445, renglon, 0);
                renglon -= 21;
                String vehiculo = obj.Vehiculos.MarcaVehiculo + " " + obj.Vehiculos.ModeloVehiculo;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, vehiculo.ToString(), 170, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Vehiculos.Descripcion.ToUpper(), 335, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.NroObleaHabilitante.Trim().ToUpper(), 480, renglon, 0);
                renglon -= 21;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.Talleres.RazonSocialTaller.ToUpper(), 200, renglon, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, obj.PEC.Descripcion, 450, renglon, 0);
                #endregion

                cb.EndText();
                document.NewPage();
            }


            document.Close();
            #endregion
        }
    }
}