using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using PetroleraManager.Web.UserControls;
using System.Drawing;
using DatosDiscretos;
using TalleresWeb.Entities;
using System.Transactions;

namespace PetroleraManager.Web.Tramites
{
    public partial class PHCargarHojaRuta : PageBase
    {
        private String Aprobado { get { return "APROBADO"; } }
        private String Rechazado { get { return "RECHAZADO"; } }

        PHCilindrosLogic logic = new PHCilindrosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idCP"] != null)
                {
                    Guid idPhCilindro = new Guid(Request.QueryString["idCP"].ToString());

                    Wizard1.ActiveStepIndex = 0;

                    var cilindro = logic.ReadDetallado(idPhCilindro);

                    if (cilindro != null)
                    {
                        ViewState["IDPHCILINDRO"] = idPhCilindro.ToString();
                        txtDatosNroOperacion.Text = "falta";
                        txtDatosFecha.Text = cilindro.FechaHabilitacion.ToString("dd/MM/yyyy");
                        txtDatosSerieCilindro.Text = cilindro.SerieCil;
                        txtDatosCodHomCilindro.Text = cilindro.CodigoCil;
                        txtDatosSerieValvula.Text = cilindro.SerieVal;
                        txtDatosCodHomValvula.Text = cilindro.CodigoVal;
                        txtDatosCliente.Text = cilindro.NombreyApellido;
                        txtDatosDominio.Text = cilindro.Dominio;
                        txtDatosTaller.Text = cilindro.Taller;
                        txtDatosObservacion.Text = cilindro.Observacion;
                    }

                    if (String.IsNullOrEmpty(cilindro.AnioFabCil.ToString()) ||
                        String.IsNullOrEmpty(cilindro.CodigoCil) ||
                        String.IsNullOrEmpty(cilindro.MarcaCil) ||
                        cilindro.Capacidad.Equals(-1) ||
                        cilindro.Diametro.Equals(-1) ||
                        cilindro.Espesor.Equals(-1) ||
                        cilindro.Largo.Equals(-1) ||
                        String.IsNullOrEmpty(cilindro.Rotura) ||
                        String.IsNullOrEmpty(cilindro.NormaFabCil) ||
                        String.IsNullOrEmpty(cilindro.Material)
                        )
                    {
                        Wizard1.Enabled = false;
                        MessageBoxCtrl1.MessageBox(null, "Los datos del cilindro estan incompletos. Por favor completelos e intente nuevamente.", MessageBoxCtrl.TipoWarning.Warning);
                    }
                    else
                    {
                        Wizard1.Enabled = true;
                    }
                }
                else
                {
                    // si no viene el id bloqueo el aceptar y vuelvo a la pagina anterior
                    MessageBoxCtrl1.MessageBox(null, "El tramite seleccionado es inexistente.", "PHProcesadas.aspx", UserControls.MessageBoxCtrl.TipoWarning.Error);
                }
            }
        }

        protected void Wizard_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            int pasoActual = e.CurrentStepIndex + 1;
            String mensaje = ValidarCampos(pasoActual);
            if (mensaje == String.Empty)
            {
                switch (pasoActual)
                {
                    //si entra por aca es porque es valido y debe hacer la validacion
                    case 1:
                        // paso 1: Reconocimiento de valvula
                        break;
                    case 2:
                        #region paso 2: Espesor
                        
                        CilindrosLogic cilLogic = new CilindrosLogic();
                        var cil = cilLogic.ReadByCodigoHomologacion(txtDatosCodHomCilindro.Text).FirstOrDefault();
                        ViewState["EspesorMinAdmitido"] = cil.EspesorAdmisibleCil.HasValue ? cil.EspesorAdmisibleCil.Value : 0;
                        Paso2Calcular();
                        
                        if (lblResultadoPaso2.Text == Aprobado)
                        {
                            //si esta aprobado pasa al siguiente paso
                            chkEspesorInsuficiente.Checked = false;
                            //chkOfTecnicaEspesor.Checked = true;
                        }
                        else if (lblResultadoPaso2.Text == Rechazado)
                        {
                            //si esta rechazado pasa al paso de anomalias con la marca del defecto
                            Wizard1.ActiveStepIndex = 4;
                            chkEspesorInsuficiente.Checked = true;
                            lblObsPaso4.Text = "Cilindro rechazado por Espesor Insuficiente";
                            lblEspesorMinAdmisible.Text = "Espesor de pared mínimo admisible:" + ViewState["EspesorMinAdmitido"].ToString() + " mm, para el código homolog.";
                            //chkOfTecnicaEspesor.Checked = false;
                        }
                        #endregion
                        break;
                    //case 3:
                    //    #region paso 3: Pesos
                    //    Paso3Calcular();
                    //    if (lblResultadoPaso3.Text == Aprobado)
                    //    {
                    //        //si esta aprobado pasa al siguiente paso 
                    //        chkOfTecnicaTara.Checked = true;
                    //    }
                    //    else if (lblResultadoPaso3.Text == Rechazado)
                    //    {
                    //        //si esta rechazado pasa al paso de anomalias con la marca del defecto
                    //        Wizard1.ActiveStepIndex = 4;
                    //        chkDesgasteLocal.Checked = true;
                    //        lblObsPaso4.Text = "Cilindro rechazado por Pérdida de masa";
                    //        chkOfTecnicaTara.Checked = true;
                    //    }
                    //    #endregion
                    //    break;
                    //case 4:
                    //    Paso4Calcular();
                    //    if (lblResultadoPaso4.Text == Aprobado)
                    //    {
                    //        chkOfTecnicaHidraulica.Checked = true;
                    //    }
                    //    else if (lblResultadoPaso4.Text == Rechazado)
                    //    {
                    //        //si esta rechazado pasa al paso de anomalias con la marca del defecto
                    //        Wizard1.ActiveStepIndex = 4;
                    //        chkExpansionVolumExc.Checked = true;
                    //        lblObsPaso4.Text = "Cilindro expansión volumetrica excesiva";
                    //        chkOfTecnicaHidraulica.Checked = false;
                    //    }
                    //    break;
                }
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
                e.Cancel = true;
            }
        }

        private String ValidarCampos(int pasoActual)
        {
            //pasoActual++;
            String valor = String.Empty;
            if (pasoActual == 1) valor = Paso1Validar();
            if (pasoActual == 2) valor = Paso2Validar();
            if (pasoActual == 3) valor = Paso3Validar();
            if (pasoActual == 4) valor = Paso4Validar();
            if (pasoActual == 4) valor = Paso5Validar();
            if (pasoActual == 5) valor = Paso6Validar();
            if (pasoActual == 6) valor = Paso7Validar();
            if (pasoActual == 7) valor = Paso8Validar();
            if (pasoActual == 8) valor = Paso9Validar();
            return valor;
        }

        #region paso1
        private String Paso1Validar()
        {
            String valor = String.Empty;
            if (cboMarcaValvula.SelectedValue == Guid.Empty) valor += "- Seleccione marca de valvula. <br />";
            if (txtSerieVal.Text.Trim() == String.Empty) valor += "- Ingrese nro de serie de valvula.";

            return valor;
        }
        #endregion

        #region paso2
        private String Paso2Validar()
        {
            String valor = String.Empty;
            if (txtLecturaParedMinima.Text.Trim() == String.Empty) valor += "- Ingrese lectura de pared mínima.";

            return valor;
        }
        protected void BtnValidarPaso2(Object sender, EventArgs e)
        {
            String mensaje = Paso2Validar();
            if (mensaje.Equals(String.Empty))
            {
                Paso2Calcular();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }
        }
        private void Paso2Calcular()
        {
            Decimal espesorMinAdmitido = ViewState["EspesorMinAdmitido"] != null ? Decimal.Parse(ViewState["EspesorMinAdmitido"].ToString()) : 0;
            Decimal espesorMinLeido = Decimal.Parse(txtLecturaParedMinima.Text);
            if (espesorMinAdmitido <= espesorMinLeido)
            {
                lblResultadoPaso2.ForeColor = Color.Green;
                lblResultadoPaso2.Text = Aprobado;
            }
            else
            {
                lblResultadoPaso2.ForeColor = Color.Red;
                lblResultadoPaso2.Text = Rechazado;
            }

            updPanelPaso2.Update();
        }
        #endregion

        #region paso3
        private String Paso3Validar()
        {
            String valor = String.Empty;
            if (txtPesoVacioMarcado.Text.Trim() == String.Empty) valor += "- Ingrese el peso marcado. <br />";
            if (txtCapacidadMarcado.Text.Trim() == String.Empty) valor += "- Ingrese la capacidad marcada. <br />";
            if (txtPesoVacioActual.Text.Trim() == String.Empty) valor += "- Ingrese el peso vacío. <br />";
            if (txtPesoConAgua.Text.Trim() == String.Empty) valor += "- Ingrese el peso lleno. <br />";

            return valor;
        }
        protected void BtnValidarPaso3(Object sender, EventArgs e)
        {
            String mensaje = Paso3Validar();
            if (mensaje.Equals(String.Empty))
            {
                Paso3Calcular();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }
        }
        private void Paso3Calcular()
        {
            Boolean valido1 = false;
            Boolean valido2 = false;

            if ((txtPesoVacioMarcado.Text.Replace("___,__", "") != String.Empty) && (txtPesoVacioActual.Text.Replace("___,__", "") != String.Empty))
            {
                Decimal pesoCilMarcado = Decimal.Parse(txtPesoVacioMarcado.Text);
                Decimal pesoCilVacio = Decimal.Parse(txtPesoVacioActual.Text);
                if ((((pesoCilMarcado - pesoCilVacio) * 100) / pesoCilMarcado) < 10)
                {
                    valido1 = true;
                }

                txtPesoConAgua.Focus();
            }


            if ((txtPesoVacioMarcado.Text.Replace("___,__", "") != String.Empty) &&
                (txtPesoVacioActual.Text.Replace("___,__", "") != String.Empty) &&
                (txtPesoConAgua.Text.Replace("___,__", "") != String.Empty))
            {
                Decimal pesoCilMarcado = Decimal.Parse(txtPesoVacioMarcado.Text);
                Decimal pesoCilVacio = Decimal.Parse(txtPesoVacioActual.Text);
                Decimal pesoCilLLeno = Decimal.Parse(txtPesoConAgua.Text);
                Decimal coef = new Decimal(0.7);

                txtPesoAguaContenida.Text = (pesoCilLLeno - pesoCilVacio - coef).ToString();

                if ((((pesoCilMarcado - pesoCilVacio) * 100) / pesoCilMarcado) < 10)
                {
                    valido2 = true;
                }

                txtCapacidadMarcado.Focus();

            }

            if (valido1.Equals(true) && valido2.Equals(true))
            {
                lblResultadoPaso3.ForeColor = Color.Green;
                lblResultadoPaso3.Text = Aprobado;
            }
            else
            {
                lblResultadoPaso3.ForeColor = Color.Red;
                lblResultadoPaso3.Text = Rechazado;
            }

            updPanelPaso3.Update();
        }
        #endregion

        #region paso4
        private String Paso4Validar()
        {
            String valor = String.Empty;
            if (txtPHCapacCilindro.Text.Equals(String.Empty)) valor += "- Ingrese la capacidad del cilindro. <br />";
            if (cboPHPresionPrueba.SelectedIndex == -1) valor += "- Seleccione presión de prueba.";
            if (CboPHBureta.SelectedIndex == -1) valor += "- Seleccione bureta usada.";
            if (txtPHTempAgua.Text.Replace("_", "").Replace(",", "").Replace(".", "").Equals(String.Empty))
            {
                valor += "- Ingrese la temperatura del agua. <br />";
            }
            else
            {
                var tmpPhAgua = Decimal.Parse(txtPHTempAgua.Text);
                if ((tmpPhAgua < 4) || (tmpPhAgua > 37)) valor += "- Ingrese una temperatura de agua válida. <br />";
            }

            if (txtPHLecturaBuretaMax.Text.Equals(String.Empty)) valor += "- Ingrese lectura bureta Máx.";
            if (txtPHLecturaBuretaFinal.Text.Equals(String.Empty)) valor += "- Ingrese lectura bureta Final";

            return valor;
        }
        protected void BtnValidarPaso4(Object sender, EventArgs e)
        {
            String mensaje = Paso4Validar();
            if (mensaje.Equals(String.Empty))
            {
                Paso4Calcular();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
                updPanelPaso4.Update();
            }

            updPanelPaso4.Update();
        }
        private void Paso4Calcular()
        {
            //condiciones ph
            Decimal capacidad = Decimal.Parse(txtPHCapacCilindro.Text);
            Decimal presion = Decimal.Parse(cboPHPresionPrueba.SelectedValueString);
            Decimal tempetatura = Decimal.Parse(txtPHTempAgua.Text);
            Decimal bureta = Decimal.Parse(CboPHBureta.SelectedValueString);

            //prueba hidraulica
            Decimal volumenMax = Decimal.Parse(txtPHLecturaBuretaMax.Text);
            Decimal volumenFinal = Decimal.Parse(txtPHLecturaBuretaFinal.Text);

            //Compresibilidad
            Decimal F = new Decimal(0.001370);
            Decimal W = ((new Decimal(97.5) + (bureta * new Decimal(0.99623) / 1000)) / new Decimal(0.45359));
            Decimal P = presion / new Decimal(0.0689475699987084);
            Decimal compresibilidad = F * W * P;

            //TODO: falta ver en la formula del excel que va en la casilla B8.
            Decimal expansionTotal = (bureta - compresibilidad);
            lblPHExpansionTotal.Text = expansionTotal.ToString(GetDinamyc.FormatoNumerico2d);
            lblPHExpansionPermanente.Text = volumenFinal.ToString(GetDinamyc.FormatoNumerico2d);
            lblPHExpansionElastica.Text = (expansionTotal - volumenFinal).ToString(GetDinamyc.FormatoNumerico2d);

            Decimal porcentajeExpansionPermanente = (100 * (volumenFinal / expansionTotal));

            lblResultadoPaso4.Text = Rechazado;
            lblResultadoPaso4.ForeColor = Color.Red;

            if (porcentajeExpansionPermanente < 10)
            {
                lblResultadoPaso4.ForeColor = Color.Green;
                lblResultadoPaso4.Text = Aprobado;
            }

            updPanelPaso4.Update();
        }
        #endregion

        #region paso5
        private String Paso5Validar()
        {
            String valor = String.Empty;
            //if (txtSerieVal.Text.Trim() == String.Empty) valor += "- Ingrese nro de serie de valvula.";
            return valor;
        }
        protected void BtnValidarPaso5(Object sender, EventArgs e)
        {
            String mensaje = Paso5Validar();
            if (mensaje.Equals(String.Empty))
            {
                Paso5Calcular();
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }
        }
        private void Paso5Calcular()
        {
            Boolean valor = true;

            if (chkGlobos.Checked) valor = false;
            if (chkAbolladuras.Checked) valor = false; // TODO: aca deberia cargar los datos de la abolladura
            if (chkAbolladurasEstriadas.Checked) valor = false; // TODO: aca deberia cargar los datos de la abolladura estriada
            if (chkFisuras.Checked) valor = false;
            if (chkLaminado.Checked) valor = false;
            if (chkPinchaduras.Checked) valor = false;
            if (chkRoscasDefectuosa.Checked) valor = false;
            if (chkDesgasteLocal.Checked) valor = false;
            if (chkCorrosionGeneralizada.Checked) valor = false;
            if (chkCorrosionLocalizada.Checked) valor = false;
            if (chkOvalado.Checked) valor = false; // TODO: aca deberia cargar los datos de la ovalizacion
            if (!chkCoincideConRegistro.Checked) valor = false; // TODO: ver si esta bien  Marcación defectuosa
            // TODO: Falta Expansión volumétrica excesiva
            // TODO: Pérdida de masa
            if (ChkDanioPorFuego.Checked) valor = false;

            if (valor)
            {
                lblResultadoPaso5.ForeColor = Color.Green;
                lblResultadoPaso5.Text = Aprobado;

                //si aprueba ingresa el nro de planilla frmNroPlanilla
            }
            else
            {
                lblResultadoPaso5.ForeColor = Color.Red;
                lblResultadoPaso5.Text = Rechazado;
            }
        }
        #endregion

        #region paso6
        private String Paso6Validar()
        {
            String valor = String.Empty;
            //if (cboMarcaValvula.SelectedValue == Guid.Empty) valor += "- Seleccione marca de valvula. <br />";
            //if (txtSerieVal.Text.Trim() == String.Empty) valor += "- Ingrese nro de serie de valvula.";

            return valor;
        }
        #endregion

        #region paso7
        private String Paso7Validar()
        {
            String valor = String.Empty;
            //if (cboMarcaValvula.SelectedValue == Guid.Empty) valor += "- Seleccione marca de valvula. <br />";
            //if (txtSerieVal.Text.Trim() == String.Empty) valor += "- Ingrese nro de serie de valvula.";

            return valor;
        }
        #endregion

        #region paso8
        private String Paso8Validar()
        {
            String valor = String.Empty;
            //if (cboMarcaValvula.SelectedValue == Guid.Empty) valor += "- Seleccione marca de valvula. <br />";
            //if (txtSerieVal.Text.Trim() == String.Empty) valor += "- Ingrese nro de serie de valvula.";

            return valor;
        }
        #endregion

        #region paso9
        private String Paso9Validar()
        {
            lblResultadoPH.Text = Aprobado;
            
            if (lblResultadoPaso2.Text.Equals(Rechazado)) lblResultadoPH.Text = Rechazado;
            if (lblResultadoPaso3.Text.Equals(Rechazado)) lblResultadoPH.Text = Rechazado;
            if (lblResultadoPaso4.Text.Equals(Rechazado)) lblResultadoPH.Text = Rechazado;
            if (lblResultadoPaso5.Text.Equals(Rechazado)) lblResultadoPH.Text = Rechazado;

            if (lblResultadoPH.Text == Aprobado)
            {
                lblResultadoPH.ForeColor = Color.Green;
                return String.Empty;
            }
            else
            {
                lblResultadoPH.ForeColor = Color.Red;
                return String.Empty;
            }
        }
        #endregion

        protected void Wizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Guid idPHcilindro = new Guid(ViewState["IDPHCILINDRO"].ToString());
            String msgValidar = String.Empty;

            Paso9Validar();

            if ((lblResultadoPH.Text != Aprobado) && (lblResultadoPH.Text != Rechazado)) msgValidar += "- Debe completar la revisión.";
   
            if (msgValidar.Equals(String.Empty))
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        //grabar y volver a la pagina de procesadas
                        PHCilindrosLogic logic = new PHCilindrosLogic();
                        var cilindro = logic.Read(idPHcilindro);
                        cilindro.ObservacionValvulaCilindro = "";
                        //cilindro.NroOperacionCRPC = "";
                        //cilindro.NroCertificadoPH = "";
                        cilindro.IdPEC = DatosDiscretos.PEC.PEAR;
                        
                        cilindro.Rechazado = lblResultadoPH.Text.Equals(Aprobado) ? false : true;
                       
                        //cilindro.FuncValvula = "";
                        //cilindro.RoscaValvula = "";
                        //cilindro.ObservacionValvula = "";
                        cilindro.PesoMarcadoCilindro = Double.Parse(txtPesoVacioMarcado.Text);
                        cilindro.PesoVacioCilindro = Double.Parse(txtPesoVacioActual.Text);
                        cilindro.PesoAguaCilindro = Double.Parse(txtPesoAguaContenida.Text);
                        cilindro.LecturaAFondoCilindro = Double.Parse(txtLecturaFondo.Text);
                        cilindro.LecturaAParedCilindro = Double.Parse(txtLecturaParedMaxima.Text);
                        cilindro.LecturaBParedCilindro = Double.Parse(txtLecturaParedMinima.Text); ;
                        //cilindro.LecturaBFondoCilindro = "";                        
                        //cilindro.LecturaCPaedCilindro = "";
                        cilindro.LecturaMaxBureta = Double.Parse(txtPHLecturaBuretaMax.Text);
                        cilindro.LecturaMinBureta = Double.Parse(txtPHLecturaBuretaFinal.Text);
                        cilindro.TipoFondoCilindro = cboTipoFondo.SelectedValueString;
                        cilindro.PresionPruebaCilindro = Double.Parse(cboPHPresionPrueba.SelectedValueString);
                        cilindro.NroBureta = int.Parse(CboPHBureta.SelectedValueString.Replace(",", "").Replace(".", "").Replace("0", ""));                      
                        cilindro.ObservacionPH += " " + txtObservaciones.Text;
                        cilindro.IdEstadoPH = DatosDiscretos.ESTADOSPH.Finalizada;
                        logic.Update(cilindro);

                        AgregarInspecciones(idPHcilindro);

                        ss.Complete();

                        String urlRetorno = "PHProcesadas.aspx";
                        String estado = cilindro.Rechazado.Value ? "RECHAZADO" : "APROBADO";
                        if (!cilindro.Rechazado.Value)
                        {
                            // si esta aprobado permite imprimir
                            String urlImprimir = SiteMaster.UrlBase + @"02.Tramites/PHImprimirCertificado.aspx?id=" + idPHcilindro;
                            MessageBoxCtrl1.MessageBox(null, "La hoja de ruta se grabó correctamente. Estado: " + estado, urlRetorno, MessageBoxCtrl.TipoWarning.Success, urlImprimir, "Imprmir");
                        }
                        else
                        {
                            // SI FUE RECHAZADO NO LE PERMITO IMPRIMIR
                            MessageBoxCtrl1.MessageBox(null, "La hoja de ruta se grabó correctamente. Estado: " + estado, urlRetorno, MessageBoxCtrl.TipoWarning.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCtrl1.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
                    }
                    finally
                    {
                        ss.Dispose();
                    }
                }
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, msgValidar, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void AgregarInspecciones(Guid idPHcilindro)
        {
            try
            {
                InspeccionesPHLogic inspeccionesLogic = new InspeccionesPHLogic();
                InspeccionesPH inspeccion = new InspeccionesPH();

                if (chkAbolladuras.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.ABOLLADURAS;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if ((chkCorrosionGeneralizada.Checked) || (chkCorrosionLocalizada.Checked))
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.CORROSION;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkInspRoscaDeformacion.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.DEFORMACIONMARCADO;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkDesgasteLocal.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.DESGASTELOCALIZADO;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkAbolladurasEstriadas.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.ESTRIAS;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkExpansionVolumExc.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.EXPANSIONEXCESIVA;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkFisuras.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.FISURAS;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (ChkDanioPorFuego.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.FUEGO;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkGlobos.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.GLOBOS;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkLaminado.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.LAMINADO;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkOvalado.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.OVALADO;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkInspRoscaFaltaMat.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.PERDIDAMASA;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkPinchaduras.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.PINCHADURAS;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }
                if (chkRoscasDefectuosa.Checked)
                {
                    inspeccion = new InspeccionesPH();
                    inspeccion.ID = Guid.NewGuid();
                    inspeccion.IdInspeccion = INSPECCIONES.ROSCADEFECTUOSA;
                    inspeccion.IdPHCilndro = idPHcilindro;
                    inspeccion.ObservacionesInspeccion = txtObservaciones.Text;
                    inspeccionesLogic.Add(inspeccion);
                }

            }
            catch 
            {
                throw new NotImplementedException("- Error al grabar las inspecciones. Revise los datos e intente nuevamente.");
            }
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, false);
        }

        protected void lnkVerResumen_Click(object sender, EventArgs e)
        {
            //TODO: aca tengo que poner los datos en una var de session.!!!!!
            Guid idPhCilindro = new Guid(Request.QueryString["idCP"].ToString());
            PHCilindrosLogic logic = new PHCilindrosLogic();
            PHCilindros cilindro = logic.Read(idPhCilindro);
            cilindro.ObservacionValvulaCilindro = "";
            //cilindro.NroOperacionCRPC = "";
            cilindro.NroCertificadoPH = "";  //TODO:falta poner dato
            cilindro.IdPEC = DatosDiscretos.PEC.PEAR;

            cilindro.Rechazado = lblResultadoPH.Text.Equals(Aprobado) ? false : true;

            cilindro.FuncValvula = "";          //TODO:falta poner dato
            cilindro.RoscaValvula = "";         //TODO:falta poner dato
            cilindro.ObservacionValvula = "";   //TODO:falta poner dato
            cilindro.PesoMarcadoCilindro = txtPesoVacioMarcado.Text != String.Empty? Double.Parse(txtPesoVacioMarcado.Text) : 0;
            cilindro.PesoVacioCilindro = txtPesoVacioActual.Text != String.Empty ? Double.Parse(txtPesoVacioActual.Text) : 0;
            cilindro.PesoAguaCilindro = txtPesoAguaContenida.Text != String.Empty ? Double.Parse(txtPesoAguaContenida.Text) : 0;
            cilindro.CapacidadMarcada = txtCapacidadMarcado.Text != String.Empty ? Double.Parse(txtCapacidadMarcado.Text) : 0;
            cilindro.LecturaAFondoCilindro = txtLecturaFondo.Text != String.Empty ? Double.Parse(txtLecturaFondo.Text) : 0;
            cilindro.LecturaAParedCilindro = txtLecturaParedMaxima.Text != String.Empty ? Double.Parse(txtLecturaParedMaxima.Text) : 0;
            cilindro.LecturaBParedCilindro = txtLecturaParedMinima.Text != String.Empty ? Double.Parse(txtLecturaParedMinima.Text) : 0;
            cilindro.LecturaMaxBureta = txtPHLecturaBuretaMax.Text != String.Empty ? Double.Parse(txtPHLecturaBuretaMax.Text) : 0;
            cilindro.LecturaMinBureta = txtPHLecturaBuretaFinal.Text != String.Empty ? Double.Parse(txtPHLecturaBuretaFinal.Text) : 0;
            cilindro.TipoFondoCilindro = cboTipoFondo.SelectedValueString;
            cilindro.PresionPruebaCilindro = Double.Parse(cboPHPresionPrueba.SelectedValueString);
            cilindro.NroBureta = int.Parse(CboPHBureta.SelectedValueString.Replace(",", "").Replace(".", "").Replace("0", ""));
            cilindro.ObservacionPH += " " + txtObservaciones.Text;
            cilindro.IdEstadoPH = DatosDiscretos.ESTADOSPH.Finalizada;

            Session.Add("PH_RESUMEN", cilindro);

            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('PhverResumen.aspx');", true);
        }
    }
}