<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasIngresar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ObleasIngresar" %>

<%@ Register Src="~/UserControls/uscCargarVehiculo.ascx" TagName="uscCargarVehiculo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uscCargarCliente.ascx" TagName="uscCargarCliente" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uscCargarReguladores.ascx" TagName="uscCargarReguladores" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/uscCargarCilindrosValvulas.ascx" TagName="uscCargarCilindrosValvulas" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/BuscarTaller.ascx" TagName="BuscarTaller" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
    <input type="hidden" id="hdnSDetCobcrollTop" runat="server" value="0" clientidmode="Static" />

    <div id="divScroll" onscroll="document.getElementById('hdnSDetCobcrollTop').value = this.scrollTop;" style="width: 100%;">
        <table style="width: 100%; border: 0;">
            <tr>
                <td style="width: 30%">
                    <fieldset>
                        <legend>SELECCIONE TALLER:</legend>
                        <uc6:BuscarTaller ID="BuscarTaller1" runat="server" />
                    </fieldset>
                </td>
                <td style="width: 70%">
                    <fieldset>
                        <legend>TIPO DE OPERACION, NRO. DE OBLEA Y FECHA</legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <span>FECHA:<input type="text" id="calFecha" runat="server" style="width: 90px" clientidmode="Static" maxlength="10"></span>
                                </td>
                                <td style="width: 30%">
                                    <Controls:CboTiposOperaciones ID="cboTipoOperacion" runat="server" AutomaticLoad="true" LabelText="OPERACIÓN: "
                                        AutoPostback="true" OnOnSelectedIndexChange="changeTipoOP" />
                                </td>
                                <td style="width: 30%">
                                    <PLs:PLTextBox ID="txtNroObleaAnterior" runat="server" LabelText="Nro. Anterior:" Visible="false" ClientIDMode="Static" WidthTxt="80px" onKeyPress="return soloNumeros(event)" MaxLenghtTxt="20" OnOnTextChanged="txtNroObleaAnterior_OnTextChanged" AutoPostBack="true" />
                                </td>
                                <td style="width: 10%">   
                                    <asp:Button ID="btnVerImagenes" runat="server" Text="Ver Imágenes" OnClick="btnVerImagenes_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>                
            </tr>
        </table>     


            <button ID="btnBuscarOblea" runat="server" onserverclick="btnBuscarOblea_Click" causesvalidation="false"  clientidmode="Static" ></button>

        <table style="width: 100%; border: 0;">
            <tr>
                <td style="width: 50%">
                    <uc1:uscCargarVehiculo ID="uscCargarVehiculo1" runat="server" OnVehiculoChanged="uscCargarVehiculo1_VehiculoChanged" />
                </td>
                <td style="width: 50%">
                    <uc2:uscCargarCliente ID="uscCargarCliente1" runat="server" OnClienteChanged="uscCargarCliente1_ClienteChanged" />
                </td>
            </tr>
        </table>

        <uc3:uscCargarReguladores ID="uscCargarReguladores1" runat="server" />

        <uc4:uscCargarCilindrosValvulas ID="uscCargarCilindrosValvulas1" runat="server" />

        <table width="100%" border="0">
            <tr>
                <td valign="top" style="width: 50%;">
                    <fieldset class="aField">
                        <legend>OBSERVACIONES</legend>
                        <PLs:PLTextField ID="txtObservaciones" runat="server" Rows="5" WidthTxt="100%" />
                    </fieldset>
                </td>
                <td align="right">
                    <fieldset class="aField" style="min-height: 76px">
                        <legend>ACCIONES</legend>
                        <PLs:PLButton ID="lnkGuardar" runat="server" Text="       Guardar/Imprimir" CausesValidation="false"
                            UseSubmitBehavior="true" OnClick="lnkGuardar_Click"
                            Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />

                        <PLs:PLButton ID="lnkBloquear" runat="server" Text="       Bloquear" CausesValidation="false"
                            OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkBloquear_Click" Visible="false"
                            Height="35px" Style="background: transparent url(/Imagenes/Iconos/bloqueada.png) center left no-repeat;" />

                        <PLs:PLButton ID="lnkAprobarConError" runat="server" Text="       Aprobar Con Error" CausesValidation="false"
                            OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkAprobarConError_Click"
                            Height="35px" Style="background: transparent url(/Imagenes/Iconos/warning.png) center left no-repeat;" />

                        <PLs:PLButton ID="lnkAprobar" runat="server" Text="       Aprobar" CausesValidation="false"
                            OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkAprobar_Click"
                            Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />

                        <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                            OnClientClick="return confirm('Al cancelar se perderán los cambios realizados. Desea continuar?');" Height="35px"
                            Style="background: transparent url(/Imagenes/Iconos/volver.png) center left no-repeat;"
                            OnClick="lnkCancelar_Click" />

                        <br />
                        <br />
                        <br />
                        <br />
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>

    <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
        CancelControlID="btnCancelar" CacheDynamicResults="false" OkControlID="btnAceptarProcesar" />
    <div style="display: none;">
        <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
    </div>
    <asp:Panel ID="Panel1" runat="server" CssClass="CajaDialogo" Style="display: none; width: 800px; height: 550px;">
        <asp:HiddenField ID="hddEstadoficha" runat="server" />
        <asp:HiddenField ID="dscEstadoficha" runat="server" />
        <div class="row">
            <h4 class="modal-title">
                <asp:Label ID="lblTituloMsj" runat="server" Text="" />
            </h4>
            <hr />
            <div>
                <fieldset>
                    <legend>Error <b>Dominio</b></legend>
                    <table>
                        <tr>
                            <td><strong>Dominio Actual</strong></td>
                            <td><strong>Dominio Nuevo</strong></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblErrorDominioActual" runat="server" /></td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtErrorDominio" runat="server" MaxLenghtTxt="7" /></td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset>
                    <legend>Error <b>Regulador</b></legend>
                    <table width="100%">
                        <tr>
                            <td><strong>Cód. Homolog. Actual</strong></td>
                            <td><strong>Cód. Homolog. Nuevo</strong></td>
                            <td><strong>Nro. Serie Actual</strong></td>
                            <td><strong>Nro. Serie Nuevo</strong></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblErrorCodHomoREGActual" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtErrorCodHomoREG" runat="server" Style="width: 100px" MaxLength="4" /></td>
                            <td>
                                <asp:Label ID="lblErrorSerieREGActual" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtErrorSerieREG" runat="server" Style="width: 100px" MaxLength="20" /></td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset>
                    <legend>Error <b>Cilindro</b></legend>
                    <div style="overflow: auto; min-height: 100px; height: 100px">
                        <asp:GridView ID="grdCilindros" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="false"
                            ShowHeaderWhenEmpty="true" DataKeyNames="IDCilindroOblea" CssClass="Grid" HeaderStyle-CssClass="GridHeader"
                            RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow">
                            <Columns>
                                <asp:BoundField HeaderText="Cód. Homologación Actual" DataField="CodHomologacionActual" />
                                <asp:TemplateField HeaderText="Cód. Homologación Nuevo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCodHomoCIL" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Nro. Serie Actual" DataField="NroSerieActual" />
                                <asp:TemplateField HeaderText="Nro. Serie Nuevo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSerieCIL" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
                <br />
                <fieldset>
                    <legend>Error <b>Válvula</b></legend>
                    <div style="overflow: auto; min-height: 100px; height: 100px">
                        <asp:GridView ID="grdValvulas" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="false"
                            ShowHeaderWhenEmpty="true" DataKeyNames="IDValvulaOblea" CssClass="Grid" HeaderStyle-CssClass="GridHeader"
                            RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow">
                            <Columns>
                                <asp:BoundField HeaderText="Cód. Homologación Actual" DataField="CodHomologacionActual" />
                                <asp:TemplateField HeaderText="Cód. Homologación Nuevo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCodHomoVAL" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Nro. Serie Actual" DataField="NroSerieActual" />
                                <asp:TemplateField HeaderText="Nro. Serie Nuevo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSerieVAL" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
        <br />
        <div class="row center-block text-center" style="width: 100%; text-align: center">
            <asp:Label ID="lblErrorMsj" Text="" runat="server" ForeColor="Red" /><br />
            <Controls:BtnAceptar ID="btnAceptarProcesar" runat="server" OnClick="btnAceptarProcesar_Click" />
            <Controls:BtnCancelar ID="btnCancelar" runat="server" CssClass="close" />
        </div>
    </asp:Panel>

    <uc5:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    <uc1:PrintBoxCtrl ID="PrintBoxCtrl1" runat="server" />


    <div style="position: fixed; left: 0; top: 0;">
        <PLs:PLModalPopupExtender ID="mpeRT" runat="server" TargetControlID="btnTargetRT" PopupControlID="modal"
            BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancelarRT"
            CacheDynamicResults="false">
        </PLs:PLModalPopupExtender>

        <div style="display: none;">
            <asp:Button ID="btnTargetRT" runat="server" Text="Cancelar" />
        </div>

        <asp:Panel ID="modal" runat="server" CssClass="CajaDialogo" Style="display: none; width: 400px; height: 100px;">
            <div class="row" style="text-align:center; ">
                <h3 class="modal-title">
                    Seleccione RT: <asp:DropDownList ID="cboRTTaller" runat="server"></asp:DropDownList>
                </h3>
                <br />                
            </div>          
            <hr />
            <div class="row center-block text-center" style="text-align:center;">
                <Controls:BtnAceptar ID="btnAceptarRT" runat="server" OnClick="btnAceptarRT_Click" CausesValidation="false" />
                <Controls:BtnCancelar ID="btnCancelarRT" runat="server" CssClass="close" />
            </div>
        </asp:Panel>
    </div>


    <script type="text/javascript" lang="javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(pageLoaded);
        prm.add_beginRequest(beginRequest);
        var postbackElement;
        function beginRequest(sender, args) {
            postbackElement = args.get_postBackElement();
        }

        $(document).ready(function () {
            inicializar();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(inicializar);

        function pageLoaded(sender, args) {
            document.getElementById("divScroll").scrollTop = document.getElementById("hdnSDetCobcrollTop").value;
            inicializar();
        }

        function inicializar() {
            $("#txtNroObleaAnteriortxt").change(function () {
                if ($("#txtNroObleaAnteriortxt").val() != "") {
                    $('#btnBuscarOblea').trigger("click");
                }
            });
        }

        $(function () {
            $("#calFecha").datepicker({ dateFormat: 'dd/mm/yy' });
        });

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>

</asp:Content>
