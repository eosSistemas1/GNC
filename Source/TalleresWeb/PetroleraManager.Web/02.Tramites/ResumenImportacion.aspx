<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ResumenImportacion.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ResumenImportacion" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updFiltros">
        <ContentTemplate>
            <fieldset>
                <legend>Estado</legend>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <Controls:CboEstadosFichasAsignar ID="cboEstadoFicha" runat="server" OnOnSelectedIndexChange="cboEstadoFichaAsignar_OnSelectedIndexChange" AutoPostback="true" />
                        </td>
                        <td style="width: 40%"></td>
                        <td style="width: 20%">
                           <asp:Button ID="btnImprimir" runat="server" Text="       Imprimir" CausesValidation="false"
                                         OnClientClick="SeleccionarControles();"
                                         OnClick="btnImprimir_Click" Height="35px" 
                                         Style="background: transparent url(/Imagenes/Iconos/imprimir.png) center left no-repeat;" />
                        </td>
                        <td style="width: 20%"></td>
                    </tr>
                </table>
            </fieldset>

            <h2>
                <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Consultar fichas técnicas:"></PLs:PLLabel></h2>

            <%--<div id="div" runat="server" style="overflow: auto; height: 405px; width: 100%">--%>
            <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                FadeTransitions="false" FramesPerSecond="100" TransitionDuration="150" AutoSize="None" ContentCssClass="accordionContent"
                Height="390px" RequireOpenedPane="false" SuppressHeaderPostbacks="True">
            </ajaxToolkit:Accordion>
            <%--</div>--%>
            <br />
            <center>     
        <b><PLs:PLLabel ID="lblMensaje" runat="server" Visible="false" Text="No hay trámites disponibles" CssClass="failureNotification"></PLs:PLLabel></b>
    </center>



            <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

            <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="overrideAlert"></div>

    <asp:HiddenField ID="chkSelected" runat="server" ClientIDMode="Static" />

    <script type="text/javascript">
        window.alert = function (title, message) {
            $('#overrideAlert').text(message).dialog({
                modal: true,
                title: title,
                buttons: {
                    'OK': function () {
                        $(this).dialog('close');
                    }
                }
            });
        };

        function SeleccionarControles() {
            var valores = $('#chkSelected').val();
            var inputs = document.querySelectorAll("input[type='checkbox']");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    valores = valores + inputs[i].id + '|';
                }
            }

            $('#chkSelected').val(valores);
            return true;
        }
    </script>
</asp:Content>
