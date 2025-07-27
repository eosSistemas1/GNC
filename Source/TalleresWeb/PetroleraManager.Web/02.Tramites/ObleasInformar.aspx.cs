using AjaxControlToolkit;
using CrossCutting.DatosDiscretos;
using PL.Fwk.Presentation.Web.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManager.Web.Tramites
{
    public partial class ObleasInformar : PageBase
    {

        #region Members

        private ObleasLogic logic;
        private InformeLogic informesLogic;
        private InformeDetalleLogic informeDetalleLogic;
        private Generic genericos;

        #endregion

        #region Properties

        private ObleasLogic Logic
        {
            get
            {
                if (this.logic == null) this.logic = new ObleasLogic();
                return this.logic;
            }
        }

        private InformeLogic InformesLogic
        {
            get
            {
                if (this.informesLogic == null) this.informesLogic = new InformeLogic();
                return this.informesLogic;
            }
        }

        private InformeDetalleLogic InformeDetalleLogic
        {
            get
            {
                if (this.informeDetalleLogic == null) this.informeDetalleLogic = new InformeDetalleLogic();
                return this.informeDetalleLogic;
            }
        }

        #endregion

        #region Methods

        protected void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            PL.Fwk.Presentation.Web.Controls.PLCheckBox chkTodos =
                (PL.Fwk.Presentation.Web.Controls.PLCheckBox)sender;
            foreach (GridViewRow gr in grdFichas.Rows)
            {
                if (gr.RowType == DataControlRowType.DataRow)
                {
                    PL.Fwk.Presentation.Web.Controls.PLCheckBox chk =
                        (PL.Fwk.Presentation.Web.Controls.PLCheckBox)gr.Cells[6].Controls[1];

                    chk.Checked = chkTodos.Checked;
                }
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            List<GeneradorTXTInformeObleas.USRTxtExtendedView> lstUSR = new List<GeneradorTXTInformeObleas.USRTxtExtendedView>();
            List<GeneradorTXTInformeObleas.CILTxtExtendedView> lstCIL = new List<GeneradorTXTInformeObleas.CILTxtExtendedView>();
            List<GeneradorTXTInformeObleas.VALCILTxtExtendedView> lstVALCIL = new List<GeneradorTXTInformeObleas.VALCILTxtExtendedView>();
            List<GeneradorTXTInformeObleas.REGTxtExtendedView> lstREG = new List<GeneradorTXTInformeObleas.REGTxtExtendedView>();

            Int32 cantidadFichasEnviadas = 0;
            INFORME informeCreado = new INFORME();

            Boolean noHaSeleccionadoFila = true;
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    foreach (GridViewRow gr in grdFichas.Rows)
                    {
                        Guid idItem = new Guid(grdFichas.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        PLCheckBox chk = (PLCheckBox)gr.Cells[6].Controls[1];
                        if (chk.Checked) cantidadFichasEnviadas++;
                    }

                    //creo la cabecera del informe
                    informeCreado = this.InformesLogic.CrearInforme(cantidadFichasEnviadas);

                    foreach (GridViewRow gr in grdFichas.Rows)
                    {
                        if (gr.RowType == DataControlRowType.DataRow)
                        {
                            Guid idItem = new Guid(grdFichas.DataKeys[gr.RowIndex].Values["ID"].ToString());
                            PLCheckBox chk = (PLCheckBox)gr.Cells[6].Controls[1];

                            if (chk.Checked)
                            {
                                noHaSeleccionadoFila = false;
                                // Agregar a la lista
                                var o = this.Logic.ReadDetalladoByID(idItem);

                                #region USR.txt

                                GeneradorTXTInformeObleas.USRTxtExtendedView USR = new GeneradorTXTInformeObleas.USRTxtExtendedView();
                                USR.PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                USR.TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();

                                USR.TNROINTOPR = o.NroIntOperacTP.ToString();
                                USR.CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim();

                                var rtPEC = o.IdRTPEC != null ? o.PEC.RT_PEC.FirstOrDefault(x => x.ID == o.IdRTPEC) :
                                                               o.PEC.RT_PEC.FirstOrDefault(x => x.RTID == PEC_RT.PEC_RT_Principal);
                                USR.TIPODOCRT_PEC = rtPEC != null ? rtPEC.RT.DocumentosClientes.Descripcion : String.Empty;          //tipo doc representante pec
                                USR.NRODOCRT_PEC = rtPEC != null ? rtPEC.RT.NroDniRT : String.Empty;                                 //nro doc representante pec

                                var rtTaller = o.Talleres.TalleresRT.FirstOrDefault(x => x.ID == o.IdTallerRT) != null ?
                                                            o.Talleres.TalleresRT.FirstOrDefault(x => x.ID == o.IdTallerRT) : null;

                                USR.TIPODOCRT_TALLER = rtTaller != null ? rtTaller.RT.DocumentosClientes.Descripcion : String.Empty;       //tipo doc representante taller
                                USR.NRODOCRT_TALLER = rtTaller != null ? rtTaller.RT.NroDniRT : String.Empty;                                    //nro doc representante taller

                                USR.UCODGEST = o.Operaciones.CodigoGestionEnte.ToUpper().Trim();
                                USR.UDESCGEST = o.Operaciones.Descripcion.Substring(0, 12).ToUpper().Trim();
                                USR.UOBLEAANT = String.IsNullOrWhiteSpace(o.Descripcion) ? "0" : o.Descripcion.Trim();
                                USR.UOBLEANEW = !String.IsNullOrWhiteSpace(o.NroObleaNueva) ? o.NroObleaNueva :
                                                                                              USR.UCODGEST == "B" ? "0" : String.Empty;
                                USR.UDOMINIO = o.Vehiculos.Descripcion.ToUpper();
                                USR.UMARCA = o.Vehiculos.MarcaVehiculo.Length <= 10 ? o.Vehiculos.MarcaVehiculo.ToUpper() : o.Vehiculos.MarcaVehiculo.Substring(0, 10).ToUpper();
                                USR.UMODELO = o.Vehiculos.ModeloVehiculo.Length <= 20 ? o.Vehiculos.ModeloVehiculo.ToUpper() : o.Vehiculos.ModeloVehiculo.Substring(0, 20).ToUpper();
                                USR.UANO = o.Vehiculos.AnioVehiculo.ToString().Trim();
                                USR.UTIPUSO = o.Uso.CodigoUsoEnte.Trim().ToUpper();
                                USR.UAPEYNOM = o.Clientes.Descripcion.Length >= 34 ? o.Clientes.Descripcion.Substring(0, 34).ToUpper().Trim() : o.Clientes.Descripcion.ToUpper().Trim();
                                USR.UCALLEYNRO = o.Clientes.CalleCliente.Length >= 43 ? o.Clientes.CalleCliente.Substring(0, 43).ToUpper().Trim() : o.Clientes.CalleCliente.ToUpper().Trim();
                                USR.ULOCALIDAD = o.Clientes.IdLocalidad != Guid.Empty ?
                                                    o.Clientes.Localidades.Descripcion.Length >= 18 ? o.Clientes.Localidades.Descripcion.Substring(0, 18).ToUpper().Trim() : o.Clientes.Localidades.Descripcion.ToUpper() : String.Empty;
                                USR.UPROVINCIA = (o.Clientes.IdLocalidad != Guid.Empty) && (o.Clientes.Localidades.IdProvincia != Guid.Empty) ?
                                                    o.Clientes.Localidades.Provincias.Descripcion.Length >= 20 ? o.Clientes.Localidades.Provincias.Descripcion.Substring(0, 20).ToUpper().Trim() : o.Clientes.Localidades.Provincias.Descripcion.ToUpper() : String.Empty;
                                USR.UCODPOSTAL = o.Clientes.Localidades.CodigoPostal != null ?
                                                    o.Clientes.Localidades.CodigoPostal.Length > 4 ? o.Clientes.Localidades.CodigoPostal.Substring(0, 4).ToUpper() : o.Clientes.Localidades.CodigoPostal.ToUpper() : String.Empty;
                                USR.UTELEFONO = String.IsNullOrWhiteSpace(o.Clientes.TelefonoCliente) || o.Clientes.TelefonoCliente == "0" ? "0" :
                                                    o.Clientes.TelefonoCliente.Length > 10 ? o.Clientes.TelefonoCliente.Substring(0, 10) : o.Clientes.TelefonoCliente;

                                USR.UTIPODOC = o.Clientes.DocumentosClientes.Descripcion.Length > 4 ? o.Clientes.DocumentosClientes.Descripcion.Substring(0, 4) : o.Clientes.DocumentosClientes.Descripcion;

                                String documento = o.Clientes.IdTipoDniCliente == TiposDocumentos.CUIT ? Genericos.Genericos.FormatearNroCuit(o.Clientes.NroDniCliente) :
                                                                                                         o.Clientes.NroDniCliente.Length > 13 ? o.Clientes.NroDniCliente.Substring(0, 13) : o.Clientes.NroDniCliente;
                                USR.UNRODOC = documento;


                                USR.UFECREV = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                USR.UFECMONT = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                USR.UFECHAB = USR.UCODGEST.ToUpper() != "B" ? o.FechaHabilitacion.Value.ToString("dd/MM/yyyy") : String.Empty;

                                DateTime? fechaVencimiento = USR.UCODGEST.ToUpper() != "B" ? this.Logic.GetFechaVencimientoOblea(o) : default(DateTime?);
                                USR.UFECVENHAB = USR.UCODGEST.ToUpper() != "B"  && fechaVencimiento.HasValue ?
                                                  fechaVencimiento.Value.ToString("dd/MM/yyyy") : String.Empty;
                                USR.XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy");
                                USR.XTIPOPRREC = "A";
                                lstUSR.Add(USR);

                                #endregion

                                #region CIL.txt

                                foreach (ObleasCilindros oc in o.ObleasCilindros)
                                {
                                    GeneradorTXTInformeObleas.CILTxtExtendedView CIL = new GeneradorTXTInformeObleas.CILTxtExtendedView();
                                    CIL.PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                    CIL.TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();
                                    CIL.TNROINTOPR = o.NroIntOperacTP.ToString();
                                    CIL.CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim();
                                    CIL.UDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper();
                                    CIL.UTIPUSO = o.Uso.CodigoUsoEnte.Trim();
                                    CIL.CILCODH = oc.CilindrosUnidad.Cilindros.Descripcion.Trim();
                                    CIL.CNROSERIE = oc.CilindrosUnidad.Descripcion.Trim();
                                    CIL.CCODGEST = oc.Operaciones.CodigoGestionEnte.Trim();
                                    CIL.CFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                    CIL.CCAPACIDAD = oc.CilindrosUnidad.Cilindros.CapacidadCil.HasValue
                                                     && !String.IsNullOrWhiteSpace(oc.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString()) ?
                                                     oc.CilindrosUnidad.Cilindros.CapacidadCil.ToString().Trim() : "0";

                                    String mes = oc.CilindrosUnidad.MesFabCilindro.Value < 10 ?
                                                oc.CilindrosUnidad.MesFabCilindro.Value.ToString().Replace("0", "") :
                                                oc.CilindrosUnidad.MesFabCilindro.Value.ToString();
                                    CIL.CMESFABR = mes;
                                    String anio = oc.CilindrosUnidad.AnioFabCilindro.ToString().Trim().Length == 4 ?
                                        oc.CilindrosUnidad.AnioFabCilindro.ToString().Trim().Substring(2, 2) :
                                        oc.CilindrosUnidad.AnioFabCilindro.ToString().Trim();
                                    CIL.CANOFABR = anio;
                                    CIL.CUPHCRPC = oc.CRPC.Descripcion.ToString().Trim();
                                    CIL.CUPHMES = oc.MesUltimaRevisionCil.ToString().Trim();
                                    CIL.CUPHANO = oc.AnioUltimaRevisionCil.ToString().Trim();

                                    DateTime FechaVencCRPC = new DateTime(int.Parse(Genericos.Genericos.FormatearAnio(CIL.CUPHANO)),
                                                                          int.Parse(CIL.CUPHMES),
                                                                          DateTime.DaysInMonth(int.Parse(Genericos.Genericos.FormatearAnio(CIL.CUPHANO)), int.Parse(CIL.CUPHMES)));
                                    CIL.FECVENCCRPC = oc.CRPC.ID == CrossCutting.DatosDiscretos.CRPC.FAB ?
                                                        String.Empty : FechaVencCRPC.ToString("dd/MM/yyyy");


                                    CIL.CUPHRESULT = oc.Operaciones.CodigoGestionEnte.Trim() != "B" ? "A" : "R";
                                    CIL.NROCERTIFICADO = oc.NroCertificadoPH.ToUpper().Trim();
                                    CIL.XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy");
                                    CIL.XTIPOPRREC = "A";
                                    lstCIL.Add(CIL);

                                    #region VALCIL.txt

                                    foreach (ObleasValvulas ov in oc.ObleasValvulas)
                                    {
                                        GeneradorTXTInformeObleas.VALCILTxtExtendedView VALCIL = new GeneradorTXTInformeObleas.VALCILTxtExtendedView();
                                        VALCIL.PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                        VALCIL.TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();
                                        VALCIL.TNROINTOPR = o.NroIntOperacTP.ToString().Trim();
                                        VALCIL.CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim();
                                        VALCIL.VCDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper();
                                        VALCIL.TIPUSO = o.Uso.CodigoUsoEnte.Trim();
                                        VALCIL.VCCODVALVULA = ov.Valvula_Unidad.Valvula.Descripcion.Trim();
                                        VALCIL.VCNROSERIE = ov.Valvula_Unidad.Descripcion.Trim();
                                        VALCIL.VCCODGEST = ov.Operaciones.CodigoGestionEnte.Trim();
                                        VALCIL.VCFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                        VALCIL.CILCODH = CIL.CILCODH.Trim();
                                        VALCIL.CNROSERIE = CIL.CNROSERIE.Trim();
                                        VALCIL.XTIPOPRREC = "A";
                                        lstVALCIL.Add(VALCIL);
                                    }

                                    #endregion
                                }

                                #endregion

                                #region REG.txt

                                foreach (ObleasReguladores or in o.ObleasReguladores)
                                {
                                    GeneradorTXTInformeObleas.REGTxtExtendedView REG = new GeneradorTXTInformeObleas.REGTxtExtendedView();
                                    REG.PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim();
                                    REG.TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim();
                                    REG.TNROINTOPR = o.NroIntOperacTP.ToString().Trim();
                                    REG.CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim().Trim();
                                    REG.UDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper();
                                    REG.TIPUSO = o.Uso.CodigoUsoEnte.Trim().Trim();
                                    REG.RCODREG = or.ReguladoresUnidad.Reguladores.Descripcion.Trim();
                                    REG.RNROSERIE = or.ReguladoresUnidad.Descripcion.Trim();
                                    REG.RCODGEST = or.Operaciones.CodigoGestionEnte.Trim();
                                    REG.RFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy").Trim();
                                    REG.XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy");
                                    REG.XTIPOPRREC = "A";
                                    lstREG.Add(REG);
                                }

                                #endregion

                                Guid estadoActualID = Guid.Empty;

                                // Cambiar El estado a Informada o informada con error
                                if (o.IdEstadoFicha == ESTADOSFICHAS.AprobadaConError)
                                {
                                    estadoActualID = ESTADOSFICHAS.InformadaConError;
                                    this.Logic.CambiarEstado(o.ID, ESTADOSFICHAS.InformadaConError, "Informada Con Error", SiteMaster.IdUsuarioLogueado);
                                }
                                else
                                {
                                    estadoActualID = ESTADOSFICHAS.Informada;
                                    this.Logic.CambiarEstado(o.ID, ESTADOSFICHAS.Informada, "Informada", SiteMaster.IdUsuarioLogueado);
                                }

                                // genero detalle del informe
                                this.InformeDetalleLogic.InsertarDetalleObleaInforme(informeCreado.ID, o.ID, estadoActualID);
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

                String dirPath = this.GenerarPathArchivos(informeCreado.Numero);

                GeneradorTXTInformeObleas.GenerarUSRTxt(lstUSR, dirPath);
                GeneradorTXTInformeObleas.GenerarCILTxt(lstCIL, dirPath);
                GeneradorTXTInformeObleas.GenerarVALCILTxt(lstVALCIL, dirPath);
                GeneradorTXTInformeObleas.GenerarREGTxt(lstREG, dirPath);

                CargarGrilla();
            }
        }

        private string GenerarPathArchivos(Int64 nroInforme)
        {
            String dia = DateTime.Now.Date.Day.ToString("00");
            String mes = DateTime.Now.Month.ToString("00");
            String anio = DateTime.Now.Year.ToString("0000");

            String path = String.Format("{0}{1}\\{2}\\{3}\\{4}\\emitidos", GetDinamyc.UrlArchivosEnte
                                                                         , anio
                                                                         , mes
                                                                         , dia
                                                                         , nroInforme);

            return path;
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

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

            grdFichas.DataSource = null;
            grdFichas.DataBind();

            ObleasParameters param = new ObleasParameters();
            param.EstadoFichaAprobada = ESTADOSFICHAS.Aprobada;
            param.EstadoAprobadaConError = ESTADOSFICHAS.AprobadaConError;
            var obleas = this.Logic.ReadObleasAInformar(param);

            grdFichas.DataSource = obleas;
            grdFichas.DataBind();

            lnkAceptar.Visible = obleas.Count > 0;
        }

        #endregion

        protected void grdFichas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idOblea = new Guid(grdFichas.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName.ToUpper() == "IMPRIMIR")
            {                
                String urlObleasImprimir = SiteMaster.UrlBase + @"02.Tramites/ObleasImprimir.aspx?id=" + idOblea;
                MessageBoxCtrl.MessageBox(null, "Imprimir/Visualizar ficha", String.Empty, UserControls.MessageBoxCtrl.TipoWarning.Success, urlObleasImprimir, "Imprmir/Visualizar");
            }

            if (e.CommandName.ToUpper() == "REEVALUAR")
            {
                this.Logic.CambiarEstado(idOblea, ESTADOSFICHAS.PendienteRevision, "Re-evaluación de ficha", SiteMaster.IdUsuarioLogueado);
                this.CargarGrilla();
            }
        }
    }
}