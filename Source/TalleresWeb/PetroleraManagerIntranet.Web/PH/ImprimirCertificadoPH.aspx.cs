using CrossCutting.DatosDiscretos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class ImprimirCertificadoPH : PageBase
    {
        #region Members
        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid idPhCilindro = new Guid(Request.QueryString[0].ToString());

                var phCilindro = PHCilindrosLogic.ReadPhCilindroDetallado(idPhCilindro);

                if (phCilindro.IdEstadoPH == EstadosPH.Informada)
                {                    
                    //si viene de informada la actualizo a finalizada para que pase a despacho
                    ActualizarEstadoPH(phCilindro);
                }

                if (phCilindro.IdEstadoPH == EstadosPH.Informada 
                    || phCilindro.IdEstadoPH == EstadosPH.Despachada
                    || phCilindro.IdEstadoPH == EstadosPH.Finalizada)
                {
                    ImprimirCertificado(phCilindro);
                }
            }
        }

        private void ActualizarEstadoPH(PHCilindros phCilindro)
        {
            PHCilindrosLogic.CambiarEstado(phCilindro.ID, EstadosPH.Finalizada, "Impresión Certificado", this.UsuarioID);
        }

        private void ImprimirCertificado(PHCilindros phCilindro)
        {

            Response.Clear();
            Response.ContentType = "application/pdf";
            System.IO.MemoryStream m = new System.IO.MemoryStream();

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

            //#region FECHAS Y NRO DE OBLEA    (OK)
            cb.SetFontAndSize(bf, 12);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.CRPC.Descripcion, 400, 750, 0);
            var nroCertificado = !string.IsNullOrWhiteSpace(phCilindro.NroCertificadoPH) ? phCilindro.NroCertificadoPH.Trim() : string.Empty;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, nroCertificado, 400, 750, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.Descripcion, 50, 680, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.CalleCliente, 200, 680, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.Localidades.CodigoPostal, 350, 680, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.Localidades.Descripcion, 50, 640, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.Localidades.Provincias.Descripcion, 150, 640, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Clientes.TelefonoCliente, 250, 640, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{phCilindro.PH.Clientes.DocumentosClientes.Descripcion} {phCilindro.PH.Clientes.NroDniCliente}", 350, 640, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion, 50, 620, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.CilindrosUnidad.Cilindros.Descripcion, 100, 620, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.CilindrosUnidad.Descripcion, 150, 620, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{phCilindro.CilindrosUnidad.MesFabCilindro}/{phCilindro.CilindrosUnidad.AnioFabCilindro}", 200, 620, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString(), 250, 620, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.Talleres.Descripcion, 100, 580, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.PEC.Descripcion, 350, 580, 0);

            int rechazado = !phCilindro.Rechazado.Value ? 130 : 180;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "X", rechazado , 540, 0);

            //anomalias
            InspeccionesPHLogic inspeccionesLogic = new InspeccionesPHLogic();
            var inspecciones = inspeccionesLogic.ReadAllInspeccionesByIDPhCil(phCilindro.ID);
            int columna = 50;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.GLOBOS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 440, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ABOLLADURAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 420, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ABOLLADURAS_CON_ESTRIAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 400, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.FISURA_EXTERIOR) ||
                                                                                  x.IdInspeccion.Equals(INSPECCIONES.FISURA_INTERIOR) ||
                                                                                  x.IdInspeccion.Equals(INSPECCIONES.FISURA_ROSCA)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 380, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.LAMINADO_EXTERIOR) ||
                                                                                   x.IdInspeccion.Equals(INSPECCIONES.LAMINADO_INTERIOR)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 360, 0);

            columna = 250;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.PINCHADURAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 440, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DESGASTE_LOCAL) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 420, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.CORROSION_GENERALIZADA_EXTERIOR) ||
                                                                                   x.IdInspeccion.Equals(INSPECCIONES.CORROSION_GENERALIZADA_INTERIOR) ||
                                                                                   x.IdInspeccion.Equals(INSPECCIONES.CORROSION_LOCALIZADA_EXTERIOR) ||
                                                                                   x.IdInspeccion.Equals(INSPECCIONES.CORROSION_LOCALIZADA_INTERIOR)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 400, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.OVALADO) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 380, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.marcado) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : string.Empty, columna, 440, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.otros) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : string.Empty, columna, 440, 0);

            columna = 400;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DAÑO_POR_FUEGO) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 440, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.DEFORMACION_ROSCA) ||
                                                                                    x.IdInspeccion.Equals(INSPECCIONES.FISURA_ROSCA)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 420, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.FALTA_MATERIAL) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : "X", columna, 400, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.expansion) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "X" : string.Empty, columna, 440, 0);




            //observaciones ??
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.ObservacionPH, 50, 300, 0);

            //fecha revision
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.FechaOperacion.ToString("dd/MM/yyyy"), 200, 150, 0);

            //fecha vencimiento revision
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, phCilindro.PH.FechaOperacion.AddYears(5).ToString("dd/MM/yyyy"), 200, 80, 0);

            cb.EndText();

            document.Close();
            #endregion

            //writer.flush();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
    }
}