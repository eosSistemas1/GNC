using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class InformarPH : PageBase
    {
        PHCilindrosLogic logic = new PHCilindrosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            lblUltimaActualización.Text = DateTime.Now.ToLongDateString();

            grdPH.DataSource = null;
            grdPH.DataBind();

            List<PHCilindrosInformarView> ph = logic.ReadPHParaInformar();

            if (ph.Count > 0)
            {
                grdPH.DataSource = ph;
                grdPH.DataBind();
                lblMensaje.Visible = false;
                btnAceptar.Visible = true;
            }
            else
            {
                btnAceptar.Visible = false;
                lblMensaje.Visible = true;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            List<REVTxtExtendedView> lstREV = new List<REVTxtExtendedView>();
            
            String dirPath = MapPath("~/Archivos/PH/").ToString();
         
            Boolean noHaSeleccionadoFila = true;
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    foreach (GridViewRow gr in grdPH.Rows)
                    {
                        if (gr.RowType != DataControlRowType.DataRow) continue;

                        Guid idItem = new Guid(grdPH.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        CheckBox chk = (CheckBox)gr.Cells[8].Controls[1];

                        if (chk.Checked)
                        {
                            noHaSeleccionadoFila = false;
                            // Agregar a la lista 
                            var o = logic.ReadPhCilindroDetallado(idItem);

                            #region USR.txt
                            REVTxtExtendedView PH = new REVTxtExtendedView();
                            PH.RCRPCOD = o.PH.CRPC.Descripcion.Substring(0, 4).ToUpper().Trim();
                            PH.RCRPCRT = o.PH.CRPC.MatriculaCRPC != null ? o.PH.CRPC.MatriculaCRPC.Substring(0, 10).ToUpper().Trim() : String.Empty;
                            PH.RPECCOD = o.PH.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                            PH.RTALCOD = o.PH.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim();
                            PH.RTALCUIT = o.PH.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();
                            PH.RPROAPYN = o.PH.Clientes.Descripcion.Length >= 34 ? o.PH.Clientes.Descripcion.Substring(0, 34).ToUpper().Trim() : o.PH.Clientes.Descripcion.ToUpper().Trim();
                            PH.RPROTDOC = o.PH.Clientes.DocumentosClientes.Descripcion.Length > 3 ? o.PH.Clientes.DocumentosClientes.Descripcion.Substring(0, 4).ToUpper().Trim() : o.PH.Clientes.DocumentosClientes.Descripcion.ToUpper().Trim();
                            PH.RPRONDOC = o.PH.Clientes.NroDniCliente.Length >= 13 ? o.PH.Clientes.NroDniCliente.Substring(0, 13).ToUpper().Trim() : o.PH.Clientes.NroDniCliente.ToUpper().Trim();
                            PH.RPRODMCL = o.PH.Clientes.CalleCliente.Length >= 50 ? o.PH.Clientes.CalleCliente.Substring(0, 50).ToUpper().Trim() : o.PH.Clientes.CalleCliente.ToUpper().Trim();
                            PH.RPROLCLD = o.PH.Clientes.Localidades.Descripcion.Length >= 35 ? o.PH.Clientes.Localidades.Descripcion.Substring(0, 35).ToUpper().Trim() : o.PH.Clientes.Localidades.Descripcion.ToUpper().Trim();
                            PH.RPROPRVN = o.PH.Clientes.Localidades.Provincias.Descripcion.Length >= 30 ? o.PH.Clientes.Localidades.Provincias.Descripcion.Substring(0, 30).ToUpper().Trim() : o.PH.Clientes.Localidades.Provincias.Descripcion.ToUpper().Trim();
                            var cp = o.PH.Clientes.Localidades.CodigoPostal != null ? o.PH.Clientes.Localidades.CodigoPostal : String.Empty;
                            PH.RPROCDPT = (cp.Length >= 4 ? cp.Substring(0, 4) : cp).ToString().ToUpper().Trim();
                            PH.RPROTLFN = o.PH.Clientes.TelefonoCliente.Length >= 10 ? o.PH.Clientes.TelefonoCliente.Substring(0, 10).ToUpper().Trim() : o.PH.Clientes.TelefonoCliente.ToUpper().Trim();
                            PH.RPRODMNO = o.PH.Vehiculos.Descripcion.ToUpper().Trim();
                            PH.RCILCODH = o.CilindrosUnidad.Cilindros.Descripcion.Length >= 4 ? o.CilindrosUnidad.Cilindros.Descripcion.Substring(0, 4).ToUpper().Trim() : o.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();
                            PH.RCILNSER = o.CilindrosUnidad.Descripcion.Length >= 25 ? o.CilindrosUnidad.Descripcion.Substring(0, 25).ToUpper().Trim() : o.CilindrosUnidad.Descripcion.ToUpper().Trim();
                            PH.RCILMESF = o.CilindrosUnidad.MesFabCilindro.Value.ToString("00");
                            PH.RCILANOF = o.CilindrosUnidad.AnioFabCilindro.Value.ToString("00");
                            string materialCilindro = !string.IsNullOrWhiteSpace(o.CilindrosUnidad.Cilindros.MaterialCilindro) ? o.CilindrosUnidad.Cilindros.MaterialCilindro : string.Empty;
                            PH.RCILMTRL = materialCilindro.Length > 15 ? materialCilindro.Substring(0, 15).ToUpper().Trim() : materialCilindro;
                            PH.RCILCPCD = o.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString();
                            PH.RCILREV = o.NroOperacionCRPC.HasValue ? o.NroOperacionCRPC.Value.ToString() : string.Empty;
                            PH.RREVRSLT = o.Rechazado.Value ? "0" : "1";

                            InspeccionesPHLogic inspeccionesLogic = new InspeccionesPHLogic();
                            var inspecciones = inspeccionesLogic.ReadAllInspeccionesByIDPhCil(o.ID);
                            //fallas
                            PH.RREVGLOB = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.GLOBOS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVABO1 = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ABOLLADURAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVABOE = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ABOLLADURAS_CON_ESTRIAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVFISU = inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.FISURA_EXTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.FISURA_INTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.FISURA_ROSCA)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            PH.RREVLAMI = inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.LAMINADO_EXTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.LAMINADO_INTERIOR)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVPINC = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.PINCHADURAS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            
                            PH.RREVDEFR = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DEFECTOS_DEL_CUELLO) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVDESL = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DESGASTE_LOCAL) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            PH.RREVCOR = inspecciones.Where(x => (x.IdInspeccion.Equals(INSPECCIONES.CORROSION_GENERALIZADA_EXTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.CORROSION_GENERALIZADA_INTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.CORROSION_LOCALIZADA_EXTERIOR) ||
                                                                    x.IdInspeccion.Equals(INSPECCIONES.CORROSION_LOCALIZADA_INTERIOR)) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            
                            PH.RREVOVAL = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.OVALADO) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            
                            PH.RREVFDME = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DEFORMACION_ROSCA) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";

                            //PH.RREVEVSA = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.EXP) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            PH.RREVPERM = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.FALTA_MATERIAL) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            
                            PH.RREVDPFC = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DAÑO_POR_FUEGO) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            
                            //PH.RREVOTR1 = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.OTROS) && x.ValorInspeccion.Value).FirstOrDefault() != null ? "1" : "";
                            //PH.RREVOTR2 = "";
                            //fin fallas

                            PH.RREVFREV = o.PH.FechaOperacion.ToString("dd/MM/yyyy");
                            PH.RREVFVEN = o.PH.FechaOperacion.ToString("dd/MM/yyyy");
                            PH.NROCERTIFICADO = o.NroCertificadoPH;
                            PH.XRECTIPOPR = "A";
                            PH.XRECFECMODE = DateTime.Now.ToString("dd/MM/yyyy");
                            PH.XRECFECTRF = DateTime.Now.ToString("dd/MM/yyyy");

                            lstREV.Add(PH);
                            #endregion

                            
                            //Cambiar El estado a PH Informada                           
                            logic.CambiarEstado(o.ID, EstadosPH.Informada, "Oblea Informada", this.UsuarioID);
                            
                        }

                    }
                    ss.Complete();
                }
                catch (Exception ex)
                {
                    MessageBoxCtrl1.MessageBox(null, $"Ha ocurrido un error: {ex.Message}", MessageBoxCtrl.TipoWarning.Error);              
                }
                finally
                {
                    ss.Dispose();
                }
            }

            if (!noHaSeleccionadoFila)
            {
                GeneradorTXTInformePH.GenerarREVTxt(lstREV, dirPath);

                MessageBoxCtrl1.MessageBox(null, "Se han generado los archivos para informar al ente.", "InformarPH.aspx", MessageBoxCtrl.TipoWarning.Success);
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, "Debe seleccionar al menos una ficha para informar.", MessageBoxCtrl.TipoWarning.Warning);
            }            

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}