using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Data;
using AjaxControlToolkit;
using DatosDiscretos;
using System.Transactions;


namespace PetroleraManager.Web.Tramites
{
    public partial class PHInformar : PageBase
    {
        Generic genericos = new Generic();
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
            lblUltimaActualización.Text = "Ultima Actualización: " + DateTime.Now.ToLongDateString();

            grdPH.DataSource = null;
            grdPH.DataBind();

            PHParameters param = new PHParameters();
            param.Estado = DatosDiscretos.ESTADOSPH.Finalizada;
            var PH = logic.ReadAllByEstado(param);

            if (PH.Count > 0)
            {
                grdPH.DataSource = PH;
                grdPH.DataBind();
                lblMensaje.Visible = false;
                lnkAceptar.Visible = true;
            }
            else
            {
                lnkAceptar.Visible = false;
                lblMensaje.Visible = true;
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            List<Txt.REVTxtExtendedView> lstREV = new List<Txt.REVTxtExtendedView>();
           
            //String dirPath = @"C:\PetroleraNew\Archivos\Informados\" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + @"\";

            String dirPath = MapPath("~/Archivos/PH/").ToString();
                ////SiteMaster.UrlBase + @"Archivo\" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + @"\";

            Boolean noHaSeleccionadoFila = true;
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    foreach (GridViewRow gr in grdPH.Rows)
                    {
                        if (gr.RowType == DataControlRowType.DataRow)
                        {

                            Guid idItem = new Guid(grdPH.DataKeys[gr.RowIndex].Values["ID"].ToString());
                            PL.Fwk.Presentation.Web.Controls.PLCheckBox chk =
                                (PL.Fwk.Presentation.Web.Controls.PLCheckBox)gr.Cells[6].Controls[1];

                            if (chk.Checked)
                            {
                                noHaSeleccionadoFila = false;
                                // Agregar a la lista 
                                var o = logic.ReadDetalladoByID(idItem);

                                #region USR.txt
                                Txt.REVTxtExtendedView PH = new Txt.REVTxtExtendedView();
                                PH.RCRPCOD = o.PH.CRPC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                PH.RCRPCRT = o.PH.CRPC.MatriculaCRPC!=null? o.PH.CRPC.MatriculaCRPC.Substring(0, 10).ToUpper().Trim():String.Empty;
                                PH.RPECCOD = o.PH.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                PH.RTALCOD = o.PH.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim();
                                PH.RTALCUIT = o.PH.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();
                                PH.RPROAPYN = o.PH.Clientes.Descripcion.Length >= 34 ? o.PH.Clientes.Descripcion.Substring(0, 34).ToUpper().Trim() : o.PH.Clientes.Descripcion.ToUpper().Trim();
                                PH.RPROTDOC = o.PH.Clientes.DocumentosClientes.Descripcion.Length > 3 ? o.PH.Clientes.DocumentosClientes.Descripcion.Substring(0, 4).ToUpper().Trim() : o.PH.Clientes.DocumentosClientes.Descripcion.ToUpper().Trim();
                                PH.RPRONDOC = o.PH.Clientes.NroDniCliente.Length >= 13 ? o.PH.Clientes.NroDniCliente.Substring(0, 13).ToUpper().Trim() : o.PH.Clientes.NroDniCliente.ToUpper().Trim();
                                PH.RPRODMCL = o.PH.Clientes.CalleCliente.Length>=50? o.PH.Clientes.CalleCliente.Substring(0, 50).ToUpper().Trim() : o.PH.Clientes.CalleCliente.ToUpper().Trim();
                                PH.RPROLCLD = o.PH.Clientes.Localidades.Descripcion.Length >= 35 ? o.PH.Clientes.Localidades.Descripcion.Substring(0, 35).ToUpper().Trim() : o.PH.Clientes.Localidades.Descripcion.ToUpper().Trim(); ;
                                PH.RPROPRVN = o.PH.Clientes.Localidades.Provincias.Descripcion.Length >= 30 ? o.PH.Clientes.Localidades.Provincias.Descripcion.Substring(0, 30).ToUpper().Trim() : o.PH.Clientes.Localidades.Provincias.Descripcion.ToUpper().Trim();
                                var cp = o.PH.Clientes.Localidades.CodigoPostal != null ? o.PH.Clientes.Localidades.CodigoPostal : String.Empty;
                                PH.RPROCDPT = (cp.Length >= 4 ? cp.Substring(0, 4) : cp).ToString().ToUpper().Trim();
                                PH.RPROTLFN = o.PH.Clientes.TelefonoCliente.Length >= 10 ? o.PH.Clientes.TelefonoCliente.Substring(0, 10).ToUpper().Trim() : o.PH.Clientes.TelefonoCliente.ToUpper().Trim();
                                PH.RPRODMNO = o.PH.Vehiculos.Descripcion.ToUpper().Trim();
                                PH.RCILCODH = o.CilindrosUnidad.Cilindros.Descripcion.Length >= 4 ? o.CilindrosUnidad.Cilindros.Descripcion.Substring(0, 4).ToUpper().Trim() : o.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();
                                PH.RCILNSER = o.CilindrosUnidad.Descripcion.Length >= 25 ? o.CilindrosUnidad.Descripcion.Substring(0, 25).ToUpper().Trim() : o.CilindrosUnidad.Descripcion.ToUpper().Trim();
                                PH.RCILMESF = o.CilindrosUnidad.MesFabCilindro.Value.ToString("00");
                                PH.RCILANOF = o.CilindrosUnidad.AnioFabCilindro.Value.ToString("00");
                                PH.RCILMTRL = o.CilindrosUnidad.Cilindros.MaterialCilindro.Length > 15 ? o.CilindrosUnidad.Cilindros.MaterialCilindro.Substring(0, 15).ToUpper().Trim() : o.CilindrosUnidad.Cilindros.MaterialCilindro;
                                PH.RCILCPCD = o.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString();
                                PH.RCILREV = o.NroOperacionCRPC.Value.ToString();
                                PH.RREVRSLT = o.Rechazado.Value ? "0" : "1";

                                InspeccionesPHLogic inspeccionesLogic = new InspeccionesPHLogic();
                                var inspecciones = inspeccionesLogic.ReadAllByIDPhCil(o.ID);
                                //fallas
                                PH.RREVGLOB = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.GLOBOS)) != null ? "1" : "";
                                PH.RREVABO1 = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ABOLLADURAS)) != null ? "1" : "";
                                PH.RREVABOE = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ESTRIAS)) != null ? "1" : "";
                                PH.RREVFISU = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.FISURAS)) != null ? "1" : "";
                                PH.RREVLAMI = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.LAMINADO)) != null ? "1" : "";
                                PH.RREVPINC = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.PINCHADURAS)) != null ? "1" : "";
                                PH.RREVDEFR = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.ROSCADEFECTUOSA)) != null ? "1" : "";
                                PH.RREVDESL = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DESGASTELOCALIZADO)) != null ? "1" : "";
                                PH.RREVCOR = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.CORROSION)) != null ? "1" : "";
                                PH.RREVOVAL = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.OVALADO)) != null ? "1" : "";
                                PH.RREVFDME = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.DEFORMACIONMARCADO)) != null ? "1" : "";
                                PH.RREVEVSA = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.EXPANSIONEXCESIVA)) != null ? "1" : "";
                                PH.RREVPERM = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.PERDIDAMASA)) != null ? "1" : "";
                                PH.RREVDPFC = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.FUEGO)) != null ? "1" : "";
                                PH.RREVOTR1 = inspecciones.Where(x => x.IdInspeccion.Equals(INSPECCIONES.OTROS)) != null ? "1" : "";
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

                              

                                // Cambiar El estado a PH Informada
                                o.IdEstadoPH = DatosDiscretos.ESTADOSPH.Informada;
                                logic.Update(o);
                            }
                        }
                    }
                    ss.Complete();
                }
                catch (Exception ex)
                {
                    string script = @"<script type='text/javascript'>alert('Ha ocurrido un error: " + ex.Message + "'); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
                finally
                {
                    ss.Dispose();
                }
            }

            if (noHaSeleccionadoFila)
            {
                string script = @"<script type='text/javascript'>alert('Debe seleccionar al menos una ficha para informar.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            else
            {
                string script = @"<script type='text/javascript'>alert('Se han generado los archivos para informar al ente'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                CargarGrilla();
            }
            

            Txt.GenerarREVTxt(lstREV, dirPath);
            
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}