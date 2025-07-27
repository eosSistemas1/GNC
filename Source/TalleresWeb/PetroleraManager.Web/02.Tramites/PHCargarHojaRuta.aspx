<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PHCargarHojaRuta.aspx.cs" Inherits="PetroleraManager.Web.Tramites.PHCargarHojaRuta" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Procesar PH:"></PLs:PLLabel></h2>
    <fieldset>
        <legend>Datos PH</legend>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosNroOperacion" runat="server" LabelText="Nro. Operación: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCodHomCilindro" runat="server" LabelText="Cod. Hom. Cil.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCliente" runat="server" LabelText="Cliente: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosSerieCilindro" runat="server" LabelText="Nro. Serie Cil.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosDominio" runat="server" LabelText="Dominio: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCodHomValvula" runat="server" LabelText="Cod. Hom. Val.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosFecha" runat="server" LabelText="Fecha: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosSerieValvula" runat="server" LabelText="Nro. Serie Val.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosTaller" runat="server" LabelText="Taller: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosObservacion" runat="server" LabelText="Observación: " />
                </td>
            </tr>
        </table>
    </fieldset>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="10%">
            </td>
            <td width="60%">
                <asp:Wizard ID="Wizard1" runat="server" CssClass="accordionHeader" Width="100%" ActiveStepIndex="1"
                    CancelButtonText="Cancelar" FinishCompleteButtonText="Terminar" FinishPreviousButtonText="Anterior"
                    OnNextButtonClick="Wizard_NextButtonClick" StartNextButtonText="Siguiente" StepPreviousButtonText="Anterior"
                    OnFinishButtonClick="Wizard_FinishButtonClick">
                    <StepNextButtonStyle CssClass="Button" />
                    <StepStyle Width="50%" />
                    <StartNextButtonStyle CssClass="Button" />
                    <SideBarStyle BackColor="#E0E0E0" BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Top"
                        Width="250px" />
                    <NavigationButtonStyle CssClass="Button" />
                    <NavigationStyle Width="50%" />
                    <WizardSteps>
                        <asp:WizardStep ID="paso1" runat="server" Title="Paso 1: Verificación de la marcación"
                            StepType="Start">
                            <fieldset>
                                <legend>Verificación de la marcación</legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            Coincide con registro:
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkCoincideConRegistro" runat="server" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Válvula funciona:
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkValvulaFunciona" runat="server" />
                                        </td>
                                        <td>
                                            <PEARGNC:CboMarcasValvulas ID="cboMarcaValvula" runat="server" LabelText="Marca"
                                                AutomaticLoad="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Roscas Defectuosa
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkRoscasDefectuosa" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtSerieVal" runat="server" LabelText="Serie" MaxLenghtTxt="15" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <PLs:PLTextBox ID="txtObservaciones" runat="server" LabelText="Observaciones" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso2" runat="server" Title="Paso 2: Medición de espesores" StepType="Step">
                            <fieldset>
                                <legend>Medición de espesores</legend>
                                <asp:UpdatePanel ID="updPanelPaso2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <PLs:PLLabel ID="lblEspesorMinAdmisible" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtLecturaParedMinima" runat="server" Mask="9999.99" LabelText="Lectura Pared (mín) mm.:"
                                                        MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtLecturaFondo" runat="server" Mask="9999.99" LabelText="Lectura Fondo mm.:"
                                                        MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtLecturaParedMaxima" runat="server" Mask="9999.99" LabelText="Lectura Pared (MAX) mm.:"
                                                        MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                                <td>
                                                    <PEARGNC:CboPHTipoFondo ID="cboTipoFondo" runat="server" LabelText="Fondo tipo:"
                                                        AutomaticLoad="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <PLs:PLButton ID="btnValidarPaso2" runat="server" OnClick="BtnValidarPaso2" Text="Validar" />
                                                </td>
                                                <td align="center">
                                                    Resultado: <strong>
                                                        <PLs:PLLabel ID="lblResultadoPaso2" runat="server" /></strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso3" runat="server" Title="Paso 3: Registro de pesos" StepType="Step">
                            <fieldset>
                                <legend>Registro de pesos</legend>
                                <asp:UpdatePanel ID="updPanelPaso3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%" cellspacing="2">
                                            <tr>
                                                <td align="center">
                                                    <b>Marcado</b>
                                                </td>
                                                <td align="center">
                                                    <b>Lecturas</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtPesoVacioMarcado" runat="server" LabelText="Peso Marcado (Kg.):"
                                                        Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtPesoVacioActual" runat="server" LabelText="Peso Vacío (Kg.):"
                                                        Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtPesoConAgua" runat="server" LabelText="Peso c/Agua (Kg.):"
                                                        Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtCapacidadMarcado" runat="server" LabelText="Capacidad Marcada (Kg.):"
                                                        Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                                <td>
                                                    <PLs:PLTextBoxMasked ID="txtPesoAguaContenida" runat="server" LabelText="Peso del agua contenida (Kg.):"
                                                        Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <PLs:PLButton ID="btnValidarPaso3" runat="server" OnClick="BtnValidarPaso3" Text="Validar" />
                                                </td>
                                                <td align="center">
                                                    Resultado: <strong>
                                                        <PLs:PLLabel ID="lblResultadoPaso3" runat="server" /></strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso4" runat="server" Title="Paso 4: Prueba Hidráulica" StepType="Step">
                            <fieldset>
                                <legend>Prueba Hidráulica</legend>
                                <asp:UpdatePanel ID="updPanelPaso4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend>Condiciones de la PH</legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <PLs:PLTextBoxMasked ID="txtPHCapacCilindro" runat="server" LabelText="Capacidad del Cilindro (Kg.):"
                                                            Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                    </td>
                                                    <td>
                                                        <PEARGNC:CboPHPresiondePrueba ID="cboPHPresionPrueba" runat="server" LabelText="Presión de prueba"
                                                            AutomaticLoad="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <PEARGNC:CboPHBureta ID="CboPHBureta" runat="server" LabelText="Bureta usada:" AutomaticLoad="true" />
                                                    </td>
                                                    <td>
                                                        <PLs:PLTextBoxMasked ID="txtPHTempAgua" runat="server" LabelText="Temperatura del agua (°C)"
                                                            Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <fieldset>
                                            <legend>Prueba Hidráulica</legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <PLs:PLTextBoxMasked ID="txtPHLecturaBuretaMax" runat="server" LabelText="Lectura Bureta (Max.) cc."
                                                            Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                    </td>
                                                    <td>
                                                        <PLs:PLTextBoxMasked ID="txtPHLecturaBuretaFinal" runat="server" LabelText="Lectura Bureta (Final) cc."
                                                            Mask="999.99" MaskType="Number" InputDirection="RightToLeft" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Expansiones:</strong>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Total (cc)
                                                    </td>
                                                    <td>
                                                        <PLs:PLLabel ID="lblPHExpansionTotal" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Elástica (cc)
                                                    </td>
                                                    <td>
                                                        <PLs:PLLabel ID="lblPHExpansionElastica" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Permanente (%)
                                                    </td>
                                                    <td>
                                                        <PLs:PLLabel ID="lblPHExpansionPermanente" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <PLs:PLButton ID="btnValidarPaso4" runat="server" OnClick="BtnValidarPaso4" Text="Validar" />
                                                    </td>
                                                    <td align="center">
                                                        Resultado: <strong>
                                                            <PLs:PLLabel ID="lblResultadoPaso4" runat="server" /></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso5" runat="server" Title="Paso 5: Inspección exterior" StepType="Step">
                            <fieldset>
                                <legend>Inspección exterior</legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            Globos
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkGlobos" runat="server" />
                                        </td>
                                        <td>
                                            Laminado
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkLaminado" runat="server" />
                                        </td>
                                        <td>
                                            Corrosión generalizada
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkCorrosionGeneralizada" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Abolladuras
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkAbolladuras" runat="server" />
                                        </td>
                                        <td>
                                            Pinchaduras
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkPinchaduras" runat="server" />
                                        </td>
                                        <td>
                                            Corrosión localizada
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkCorrosionLocalizada" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Abolladuras c/estrias
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkAbolladurasEstriadas" runat="server" />
                                        </td>
                                        <td>
                                            Desgaste local
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkDesgasteLocal" runat="server" />
                                        </td>
                                        <td>
                                            Picaduras aisladas
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkPicadurasAisladas" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Defectos en el cuello
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkDefectosEnCuello" runat="server" />
                                        </td>
                                        <td>
                                            Ovalado
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOvalado" runat="server" />
                                        </td>
                                        <td>
                                            Espesor insuficiente
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkEspesorInsuficiente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fisuras
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkFisuras" runat="server" />
                                        </td>
                                        <td>
                                            Daño por fuego
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="ChkDanioPorFuego" runat="server" />
                                        </td>
                                        <td>
                                            Expansión Volumetrica Excesiva
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkExpansionVolumExc" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Observaciones:
                                        </td>
                                        <td colspan="3">
                                            <PLs:PLLabel ID="lblObsPaso4" runat="server" />
                                        </td>
                                        <td colspan="2" align="center">
                                            Resultado: <strong>
                                                <PLs:PLLabel ID="lblResultadoPaso5" runat="server" /></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <PLs:PLTextArea ID="txtObsPaso5" runat="server" Rows="2" />
                                        </td>
                                        <td colspan="2" align="center">
                                            <PLs:PLButton ID="btnValidarPaso5" runat="server" OnClick="BtnValidarPaso5" Text="Validar" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso6" runat="server" Title="Paso 6: Inspección rosca" StepType="Step">
                            <fieldset>
                                <legend>Inspección rosca</legend>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <PLs:PLTextBox ID="txtTipoRosca" runat="server" LabelText="Tipo de rosca:" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fisuras
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspRoscaFisuras" runat="server" />
                                        </td>
                                        <td>
                                            Falta material
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspRoscaFaltaMat" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Deformación
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspRoscaDeformacion" runat="server" />
                                        </td>
                                        <td>
                                            Perfil incompleto
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspRoscaPerfInc" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:WizardStep>
                        <%--<asp:WizardStep ID="paso7" runat="server" Title="Paso 7: Inspección interior" StepType="Step">
                            <fieldset>
                                <legend>Inspección interior</legend>
                                <table width="100%">
                                    <tr>
                                        <td width="25%">
                                            Fisuras
                                        </td>
                                        <td width="25%">
                                            <PLs:PLCheckBox ID="chkInspInteriorFisuras" runat="server" />
                                        </td>
                                        <td width="25%">
                                            Corrosión generalizada
                                        </td>
                                        <td width="25%">
                                            <PLs:PLCheckBox ID="chkInspInteriorCorrosGeneral" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Laminado
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspInteriorLaminado" runat="server" />
                                        </td>
                                        <td>
                                            Corrosión localizada
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspInteriorCorrosLocal" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Picaduras aisladas
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkInspInteriorPicadurasAisladas" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:WizardStep>
                        <asp:WizardStep ID="paso8" runat="server" Title="Paso 8: Oficina técnica" StepType="Step">
                            <fieldset>
                                <legend>Oficina técnica</legend>
                                <table width="70%">
                                    <tr>
                                        <td>
                                            Control de tara
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtOfTecnicaTara" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOfTecnicaTara" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Medición de espesores
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtOfTecnicaEspesor" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOfTecnicaEspesor" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Revisión visual
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtOfTecnicaVisual" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOfTecnicaVisual" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Prueba hidráulica
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtOfTecnicaHidraulica" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOfTecnicaHidraulica" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Revisión de rosca
                                        </td>
                                        <td>
                                            <PLs:PLTextBox ID="txtOfTecnicaRosca" runat="server" />
                                        </td>
                                        <td>
                                            <PLs:PLCheckBox ID="chkOfTecnicaRosca" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:WizardStep>--%>
                        <asp:WizardStep ID="paso9" runat="server" Title="Paso 9: Dictamen" StepType="Finish">
                            <fieldset>
                                <legend>Dictamen</legend>Resultado: 
                                <strong><PLs:PLLabel ID="lblResultadoPH" runat="server" /></strong>
                                <asp:LinkButton ID="lnkVerResumen" runat="server" Text="(ver resumen)" 
                                    onmouseover="this.style.cursor='hand'" 
                                    onmouseout="this.style.cursor='default'" OnClick="lnkVerResumen_Click" />
                            </fieldset>
                        </asp:WizardStep>
                    </WizardSteps>
                    <CancelButtonStyle CssClass="Button" />
                    <StepPreviousButtonStyle CssClass="Button" />
                    <StepNavigationTemplate>
                        <asp:Button ID="btnCancelar" runat="server" CssClass="Button" Text="Reiniciar carga"
                            OnClick="btnAnular_Click" CausesValidation="false" />
                        <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                            CssClass="Button" Text="Anterior" />
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" CssClass="Button"
                            Text="Siguiente" />
                    </StepNavigationTemplate>
                    <FinishNavigationTemplate>
                        &nbsp;<asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" CssClass="Button"
                            Text="Grabar" />
                    </FinishNavigationTemplate>
                    <SideBarTemplate>
                        <asp:DataList ID="SideBarList" runat="server">
                            <SelectedItemStyle Font-Bold="True" />
                            <ItemTemplate>
                                <asp:LinkButton ID="SideBarButton" runat="server" OnClientClick="return false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </SideBarTemplate>
                </asp:Wizard>
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
