using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities = TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ObleasInformar : PageBase
    {

        #region Members

        private ObleasLogic obleasLogic;
        private InformeLogic informesLogic;
        private InformeDetalleLogic informeDetalleLogic;

        #endregion

        #region Properties

        private ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null) this.obleasLogic = new ObleasLogic();
                return this.obleasLogic;
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
            CheckBox chkTodos = (CheckBox)sender;
            foreach (GridViewRow gr in grdFichasOK.Rows)
            {
                if (gr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)gr.Cells[6].Controls[1];

                    chk.Checked = chkTodos.Checked;
                }
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            if (ViewState["PROCESANDO"] != null && (bool)ViewState["PROCESANDO"]) return;

            ViewState["PROCESANDO"] = true;

            List<GeneradorTXTInformeObleas.USRTxtExtendedView> lstUSR = new List<GeneradorTXTInformeObleas.USRTxtExtendedView>();
            List<GeneradorTXTInformeObleas.CILTxtExtendedView> lstCIL = new List<GeneradorTXTInformeObleas.CILTxtExtendedView>();
            List<GeneradorTXTInformeObleas.VALCILTxtExtendedView> lstVALCIL = new List<GeneradorTXTInformeObleas.VALCILTxtExtendedView>();
            List<GeneradorTXTInformeObleas.REGTxtExtendedView> lstREG = new List<GeneradorTXTInformeObleas.REGTxtExtendedView>();

            Int32 cantidadFichasEnviadas = 0;
            var informeCreado = new Entities.INFORME();

            Boolean noHaSeleccionadoFila = true;
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    foreach (GridViewRow gr in grdFichasOK.Rows)
                    {
                        Guid idItem = new Guid(grdFichasOK.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        CheckBox chk = (CheckBox)gr.Cells[6].Controls[1];
                        if (chk.Checked) cantidadFichasEnviadas++;
                    }

                    //creo la cabecera del informe
                    informeCreado = this.InformesLogic.CrearInforme(cantidadFichasEnviadas);

                    foreach (GridViewRow gr in grdFichasOK.Rows)
                    {
                        if (gr.RowType == DataControlRowType.DataRow)
                        {
                            Guid idItem = new Guid(grdFichasOK.DataKeys[gr.RowIndex].Values["ID"].ToString());
                            CheckBox chk = (CheckBox)gr.Cells[6].Controls[1];

                            if (chk.Checked)
                            {
                                noHaSeleccionadoFila = false;
                                // Agregar a la lista
                                var o = this.ObleasLogic.ReadDetalladoByID(idItem);

                                #region USR.GeneradorTXTInformeObleas

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

                                String documento = o.Clientes.IdTipoDniCliente == TiposDocumentos.CUIT ? Funciones.FormatearNroCuit(o.Clientes.NroDniCliente) :
                                                                                                         o.Clientes.NroDniCliente.Length > 13 ? o.Clientes.NroDniCliente.Substring(0, 13) : o.Clientes.NroDniCliente;
                                USR.UNRODOC = documento;


                                USR.UFECREV = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                USR.UFECMONT = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy");
                                USR.UFECHAB = USR.UCODGEST.ToUpper() != "B" ? o.FechaHabilitacion.Value.ToString("dd/MM/yyyy") : String.Empty;

                                DateTime? fechaVencimiento = USR.UCODGEST.ToUpper() != "B" ? this.ObleasLogic.GetFechaVencimientoOblea(o) : default(DateTime?);
                                USR.UFECVENHAB = USR.UCODGEST.ToUpper() != "B" && fechaVencimiento.HasValue ?
                                                  fechaVencimiento.Value.ToString("dd/MM/yyyy") : String.Empty;
                                USR.XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy");
                                USR.XTIPOPRREC = "A";
                                lstUSR.Add(USR);

                                #endregion

                                #region CIL.GeneradorTXTInformeObleas

                                foreach (Entities.ObleasCilindros oc in o.ObleasCilindros)
                                {
                                    GeneradorTXTInformeObleas.CILTxtExtendedView CIL = new GeneradorTXTInformeObleas.CILTxtExtendedView
                                    {
                                        PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim(),
                                        TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim(),
                                        TNROINTOPR = o.NroIntOperacTP.ToString(),
                                        CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim(),
                                        UDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper(),
                                        UTIPUSO = o.Uso.CodigoUsoEnte.Trim(),
                                        CILCODH = oc.CilindrosUnidad.Cilindros.Descripcion.Trim(),
                                        CNROSERIE = oc.CilindrosUnidad.Descripcion.Trim(),
                                        CCODGEST = oc.Operaciones.CodigoGestionEnte.Trim(),
                                        CFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy"),
                                        CCAPACIDAD = oc.CilindrosUnidad.Cilindros.CapacidadCil.HasValue
                                                     && !String.IsNullOrWhiteSpace(oc.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString()) ?
                                                     oc.CilindrosUnidad.Cilindros.CapacidadCil.ToString().Trim() : "0"
                                    };

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

                                    DateTime FechaVencCRPC = new DateTime(int.Parse(Funciones.FormatearAnio(CIL.CUPHANO)),
                                                                          int.Parse(CIL.CUPHMES),
                                                                          DateTime.DaysInMonth(int.Parse(Funciones.FormatearAnio(CIL.CUPHANO)), int.Parse(CIL.CUPHMES)));
                                    CIL.FECVENCCRPC = oc.CRPC.ID == CrossCutting.DatosDiscretos.CRPC.FAB ?
                                                        String.Empty : FechaVencCRPC.ToString("dd/MM/yyyy");


                                    CIL.CUPHRESULT = oc.Operaciones.CodigoGestionEnte.Trim() != "B" ? "A" : "R";
                                    CIL.NROCERTIFICADO = oc.NroCertificadoPH.ToUpper().Trim();
                                    CIL.XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy");
                                    CIL.XTIPOPRREC = "A";
                                    lstCIL.Add(CIL);

                                    #region VALCIL.GeneradorTXTInformeObleas

                                    foreach (Entities.ObleasValvulas ov in oc.ObleasValvulas)
                                    {
                                        GeneradorTXTInformeObleas.VALCILTxtExtendedView VALCIL = new GeneradorTXTInformeObleas.VALCILTxtExtendedView
                                        {
                                            PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim(),
                                            TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim(),
                                            TNROINTOPR = o.NroIntOperacTP.ToString().Trim(),
                                            CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim(),
                                            VCDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper(),
                                            TIPUSO = o.Uso.CodigoUsoEnte.Trim(),
                                            VCCODVALVULA = ov.Valvula_Unidad.Valvula.Descripcion.Trim(),
                                            VCNROSERIE = ov.Valvula_Unidad.Descripcion.Trim(),
                                            VCCODGEST = ov.Operaciones.CodigoGestionEnte.Trim(),
                                            VCFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy"),
                                            CILCODH = CIL.CILCODH.Trim(),
                                            CNROSERIE = CIL.CNROSERIE.Trim(),
                                            XTIPOPRREC = "A"
                                        };
                                        lstVALCIL.Add(VALCIL);
                                    }

                                    #endregion
                                }

                                #endregion

                                #region REG.GeneradorTXTInformeObleas

                                foreach (Entities.ObleasReguladores or in o.ObleasReguladores)
                                {
                                    GeneradorTXTInformeObleas.REGTxtExtendedView REG = new GeneradorTXTInformeObleas.REGTxtExtendedView
                                    {
                                        PECCOD = o.PEC.Descripcion.Substring(0, 4).ToUpper().Trim(),
                                        TALCUIT = o.Talleres.CuitTaller.Substring(0, 13).ToUpper().Trim(),
                                        TNROINTOPR = o.NroIntOperacTP.ToString().Trim(),
                                        CODIGOTALLER = o.Talleres.Descripcion.Substring(0, 7).ToUpper().Trim().Trim(),
                                        UDOMINIO = o.Vehiculos.Descripcion.Trim().ToUpper(),
                                        TIPUSO = o.Uso.CodigoUsoEnte.Trim().Trim(),
                                        RCODREG = or.ReguladoresUnidad.Reguladores.Descripcion.Trim(),
                                        RNROSERIE = or.ReguladoresUnidad.Descripcion.Trim(),
                                        RCODGEST = or.Operaciones.CodigoGestionEnte.Trim(),
                                        RFECGEST = o.FechaHabilitacion.Value.ToString("dd/MM/yyyy").Trim(),
                                        XFECMODREC = DateTime.Now.ToString("dd/MM/yyyy"),
                                        XTIPOPRREC = "A"
                                    };
                                    lstREG.Add(REG);
                                }

                                #endregion

                                Guid estadoActualID = Guid.Empty;

                                // Cambiar El estado a Informada o informada con error
                                if (o.IdEstadoFicha == ESTADOSFICHAS.AprobadaConError)
                                {
                                    estadoActualID = ESTADOSFICHAS.InformadaConError;
                                    this.ObleasLogic.CambiarEstado(o.ID, ESTADOSFICHAS.InformadaConError, "Informada Con Error", this.UsuarioID);
                                }
                                else
                                {
                                    estadoActualID = ESTADOSFICHAS.Informada;
                                    this.ObleasLogic.CambiarEstado(o.ID, ESTADOSFICHAS.Informada, "Informada", this.UsuarioID);
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
                    MessageBoxCtrl.MessageBox(null, $"Ha ocurrido un error: {ex.Message}", UserControls.MessageBoxCtrl.TipoWarning.Error);
                }
                finally
                {
                    ss.Dispose();
                }
            }

            if (noHaSeleccionadoFila)
            {
                MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos una ficha para informar.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            else
            {
                String dirPath = Archivos.GenerarPathArchivos(informeCreado.Numero);

                GeneradorTXTInformeObleas.GenerarUSRTxt(lstUSR, dirPath);
                GeneradorTXTInformeObleas.GenerarCILTxt(lstCIL, dirPath);
                GeneradorTXTInformeObleas.GenerarVALCILTxt(lstVALCIL, dirPath);
                GeneradorTXTInformeObleas.GenerarREGTxt(lstREG, dirPath);

                MessageBoxCtrl.MessageBox(null, "Se han generado los archivos para informar al ente.", UserControls.MessageBoxCtrl.TipoWarning.Success);

                CargarGrilla();
            }

            ViewState["PROCESANDO"] = false;
        }        

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
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
            lblUltimaActualizacion.Text = DateTime.Now.ToLongDateString();

            grdFichasOK.DataSource = null;
            grdFichasOK.DataBind();

            var param = new Entities.ObleasParameters();
            param.EstadoFichaAprobada = ESTADOSFICHAS.Aprobada;
            param.EstadoAprobadaConError = ESTADOSFICHAS.AprobadaConError;
            var obleas = this.ObleasLogic.ReadObleasAInformar(param);

            BindearGrillas(obleas);

            btnAceptar.Visible = obleas.Count > 0;
        }

        private void BindearGrillas(List<Entities.ObleasExtendedView> obleas)
        {
            var obleasOK = new List<Entities.ObleasExtendedView>();
            var obleasFaltanElementos = new List<Entities.ObleasExtendedView>();
            var obleasErrorPH = new List<Entities.ObleasExtendedView>();

            foreach (var oblea in obleas)
            {
                var obleaAValidar= this.ObleasLogic.ReadDetalladoByID(oblea.ID);

                if (ValidarFaltanElementos(obleaAValidar))
                {
                    obleasFaltanElementos.Add(oblea);
                    continue;
                }

                if (ValidarFaltaPH(obleaAValidar))
                {
                    obleasErrorPH.Add(oblea);
                    continue;
                }

                obleasOK.Add(oblea);
            }

            lblTituloFichasParaInformar.Text = $" ({obleasOK.Count()})";
            grdFichasOK.DataSource = obleasOK;
            grdFichasOK.DataBind();

            lblTituloFaltaInformarPH.Text = $" ({obleasErrorPH.Count()})";
            grdFichasFaltaInformarPH.DataSource = obleasErrorPH;
            grdFichasFaltaInformarPH.DataBind();

            lblTituloFaltanElementos.Text = $" ({obleasFaltanElementos.Count()})";
            grdFichasFaltanElementos.DataSource = obleasFaltanElementos;
            grdFichasFaltanElementos.DataBind();

        }

        private bool ValidarFaltaPH(Entities.Obleas oblea)
        {
            return ObleasLogic.ValidarFaltaPH(oblea);
        }

        private bool ValidarFaltanElementos(Entities.Obleas oblea)
        {
            return ObleasLogic.ValidarFaltanElementos(oblea);
        }

        protected void grdFichasOK_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idOblea = new Guid(grdFichasOK.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName.ToUpper() == "REEVALUAR")
            {
                this.ObleasLogic.CambiarEstado(idOblea, ESTADOSFICHAS.PendienteRevision, "Re-evaluación de ficha", this.UsuarioID);
                this.CargarGrilla();
            }
        }

        #endregion

        protected void grdFichasFaltaInformarPH_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idOblea = new Guid(grdFichasFaltaInformarPH.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName.ToUpper() == "REEVALUAR")
            {
                this.ObleasLogic.CambiarEstado(idOblea, ESTADOSFICHAS.PendienteRevision, "Re-evaluación de ficha", this.UsuarioID);
                this.CargarGrilla();
            }
        }

        protected void grdFichasFaltanElementos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idOblea = new Guid(grdFichasFaltanElementos.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName.ToUpper() == "REEVALUAR")
            {
                this.ObleasLogic.CambiarEstado(idOblea, ESTADOSFICHAS.PendienteRevision, "Re-evaluación de ficha", this.UsuarioID);
                this.CargarGrilla();
            }
        }
    }
}
