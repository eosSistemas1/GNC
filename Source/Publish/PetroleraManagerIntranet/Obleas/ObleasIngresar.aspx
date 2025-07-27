<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasIngresar.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ObleasIngresar" %>

<%@ Register Src="~/UserControls/CargarVehiculo.ascx" TagName="CargarVehiculo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/CargarCliente.ascx" TagName="uscCargarCliente" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CargarReguladores.ascx" TagName="uscCargarReguladores" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/CargarCilindrosValvulas.ascx" TagName="uscCargarCilindrosValvulas" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/BuscarTaller.ascx" TagName="BuscarTaller" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    

    <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>INGRESAR FICHA T&Eacute;CNICA</h4>
                </div>
                <hr />
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <uc6:BuscarTaller ID="BuscarTaller1" runat="server" />
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-2"><strong>Fecha:</strong></div>
                    <div class="col-sm-4">
                        <input type="text" id="calFecha" runat="server" clientidmode="Static" maxlength="10" class="form-control nn">
                    </div>
                    <div class="col-sm-6"></div>
                </div>
            </div>
            <br />
            <div class="row">

                <div class="col-sm-2"><strong>Tipo Operaci&oacute;n:</strong></div>
                <div class="col-sm-2">
                    <Controls:CboTiposOperaciones ID="cboTipoOperacion" runat="server" AutomaticLoad="true" AutoPostBack="true" OnSelectedIndexChanged="changeTipoOP" />
                </div>
                <div class="col-sm-3"><strong>Nro Oblea Anterior:</strong></div>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtNroObleaAnterior" runat="server" LabelText="Nro. Anterior:" Visible="false" ClientIDMode="Static" WidthTxt="80px" onKeyPress="return soloNumeros(event)" MaxLenghtTxt="20" OnTextChanged="txtNroObleaAnterior_OnTextChanged" AutoPostBack="true" />
                    <asp:Button ID="btnVerImagenes" runat="server" Text="Ver Imágenes" OnClick="btnVerImagenes_Click" CausesValidation="false" />
                </div>

                <div class="col-sm-1 invisible">
                    <button id="btnBuscarOblea" runat="server" onserverclick="btnBuscarOblea_Click" causesvalidation="false" clientidmode="Static"></button>
                </div>
            </div>

            <hr />

            <div class="row">
                <div class="col-sm-6">
                    <uc1:CargarVehiculo ID="uscCargarVehiculo1" runat="server" OnVehiculoChanged="uscCargarVehiculo1_VehiculoChanged" />
                </div>
                <div class="col-sm-6">
                    <uc2:uscCargarCliente ID="uscCargarCliente1" runat="server" OnClienteChanged="uscCargarCliente1_ClienteChanged" />
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-sm-12">
                    <uc3:uscCargarReguladores ID="uscCargarReguladores1" runat="server" />
                </div>
                <div class="row">
                </div>
                <div class="col-sm-12">
                    <uc4:uscCargarCilindrosValvulas ID="uscCargarCilindrosValvulas1" runat="server" />
                </div>
            </div>

            <div class="row">
                <div class="col-sm-6"><strong>OBSERVACIONES</strong></div>
                <div class="col-sm-6 text-right"><strong>ACCIONES</strong></div>
            </div>
            <hr />
            <div class="row">
                <div class="col-sm-6">
                    <asp:TextBox ID="txtObservaciones" runat="server" Rows="5" TextMode="MultiLine" Width="100%" class="form-control nn" />
                </div>

                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-primary btn-block nn"
                        id="lnkGuardar" runat="server" onserverclick="lnkGuardar_Click"
                        title="Guardar" alt="Guardar">
                        <i class="fa fa-check" aria-hidden="true"></i>&nbsp Guardar</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-warning btn-block nn"
                        id="lnkBloquear" runat="server" onserverclick="lnkBloquear_Click"
                        title="Bloquear" alt="Bloquear">
                        <i class="fa fa-bomb" aria-hidden="true"></i>&nbsp Bloquear</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-primary btn-block nn"
                        id="lnkAprobarConError" runat="server" onserverclick="lnkAprobarConError_Click"
                        title="Aprobar Con Error" alt="Aprobar Con Error">
                        <i class="fa fa-check-circle-o" aria-hidden="true"></i>&nbsp Ok C/Error</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-success btn-block nn"
                        id="lnkAprobar" runat="server" onserverclick="lnkAprobar_Click"
                        title="Aprobar" alt="Aprobar">
                        <i class="fa fa-check" aria-hidden="true"></i>&nbsp Aprobar</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-danger btn-block nn" id="lnkCancelar"
                        onclientclick="return confirm('Al cancelar se perderán los cambios realizados. Desea continuar?');"
                        runat="server" onserverclick="lnkCancelar_Click" title="Cancelar" alt="Cancelar">
                        <i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-danger btn-block nn" id="lnkGuardarCambios"
                        runat="server" onserverclick="lnkGuardarCambios_Click" title="Guardar cambios" alt="Guardar cambios">
                        <i class="fa fa-close" aria-hidden="true"></i>&nbsp Guardar</button>
                </div>
            </div>
        </div>

    <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
        CancelControlID="btnCancelar" CacheDynamicResults="false" OkControlID="btnAceptarProcesar" />

    <div style="display: none;">
        <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
    </div>

    <asp:Panel ID="Panel1" runat="server">
        <asp:HiddenField ID="hddEstadoficha" runat="server" />
        <asp:HiddenField ID="dscEstadoficha" runat="server" />

        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    <asp:Label ID="lblTituloMsj" runat="server" Text="" /></h4>
            </div>
            <div class="modal-body">
                <div>

                    <h4>Error <b>Dominio</b></h4>
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

                    <hr />

                    <h4>Error <b>Regulador</b></h4>
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

                    <hr />

                    <h4>Error <b>Cilindro</b></h4>
                    <div style="overflow: auto; min-height: 100px; height: 70px">
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

                    <hr />

                    <h4>Error <b>Válvula</b></h4>
                    <div style="overflow: auto; min-height: 100px; height: 70px">
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

                </div>
            </div>
            <div class="modal-footer">
                <div class="row center-block text-center" style="width: 100%; text-align: center">
                    <div class="col-sm-12 center">
                        <asp:Label ID="lblErrorMsj" Text="" runat="server" ForeColor="Red" />
                    </div>
                    <div class="col-sm-12 pull-right">
                        <div class="col-sm-6 no-padding">
                            <button type="button" class="btn btn-primary btn-block nn" id="btnAceptarProcesar" runat="server" onserverclick="btnAceptarProcesar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                        </div>
                        <div class="col-sm-6 no-padding">
                            <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div style="position: fixed; left: 0; top: 0;">
        <ajaxToolkit:ModalPopupExtender ID="mpeRT" runat="server" TargetControlID="btnTargetRT" PopupControlID="modal"
            BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancelarRT"
            CacheDynamicResults="false">
        </ajaxToolkit:ModalPopupExtender>

        <div style="display: none;">
            <asp:Button ID="btnTargetRT" runat="server" Text="Cancelar" />
        </div>

        <asp:Panel ID="modal" runat="server" CssClass="CajaDialogo" Style="display: none; width: 400px; height: 100px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Seleccione RT:
                    
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="cboRTTaller" runat="server"></asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-12 pull-right">
                        <div class="col-sm-6 no-padding">
                            <button type="button" class="btn btn-primary btn-block nn" id="btnAceptarRT" runat="server" onserverclick="btnAceptarRT_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                        </div>
                        <div class="col-sm-6 no-padding">
                            <button type="button" class="btn btn-danger btn-block nn" id="btnCancelarRT" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                        </div>
                    </div>
                </div>
        </asp:Panel>
    </div>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <div class="modal fade" id="modalImprimir" style="z-index: 99999 !important;">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="window.location.href=window.location.href;">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Imprimir</h4>
                </div>
                <div class="modal-body">
                    <iframe id="frameImprimir" style="width: 100%; height: 400px;"></iframe>
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <uc5:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    <uc1:PrintBoxCtrl ID="PrintBoxCtrl1" runat="server" />

    </ContentTemplate>
    </asp:UpdatePanel>

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
            inicializar();
        }

        function inicializar() {           
            $('#<%=txtNroObleaAnterior.ClientID %>').change(function () {
                if ($('#<%=txtNroObleaAnterior.ClientID %>').val() != "") {
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

        function ShowPopupImprimir(obleaID) {

            $("#btnShowPopupImprimir").click();

            var url = "ObleasImprimir.aspx?id=" + obleaID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>

</asp:Content>
