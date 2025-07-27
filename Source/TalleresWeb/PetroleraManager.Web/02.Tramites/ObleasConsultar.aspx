<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ObleasConsultar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ObleasConsultar" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


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
                            <Controls:CboEstadosFichas ID="cboEstadoFicha" runat="server" OnOnSelectedIndexChange="cboEstadoFicha_OnSelectedIndexChange" AutoPostback="true" />
                        </td>
                        <td style="width: 30%">
                            <PLs:PLCalendar ID="calFechaD" runat="server" LabelText="Fecha Desde:" Visible="false" />
                        </td>
                        <td style="width: 30%">
                            <PLs:PLCalendar ID="calFechaH" runat="server" LabelText="Fecha Hasta:" Visible="false" />
                        </td>
                        <td style="width: 20%">
                            <PLs:PLButton ID="lnkBuscar" runat="server" Text="       Buscar" CausesValidation="false"
                                OnClick="lnkBuscar_Click" Height="35px"
                                Style="background: transparent url(/Imagenes/Iconos/buscar.png) center left no-repeat;" />
                        </td>
                        <td style="width: 50%"></td>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="overrideAlert"></div>

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
    </script>
</asp:Content>
