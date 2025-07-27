<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasAsignar.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ObleasAsignar" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updFiltros">
        <ContentTemplate>
            <div id="contenido">
                <div class="row">
                    <div class="col-sm-12">
                        <h4>
                            <asp:Label ID="lblTituloPagina" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <hr />
                </div>

                <div class="col-sm-1">Estado:</div>
                <div class="col-sm-3">
                    <Controls:CboEstadosFichasAsignar ID="cboEstadoFicha" runat="server" OnSelectedIndexChanged="cboEstadoFichaAsignar_OnSelectedIndexChange" AutoPostBack="true" />
                </div>
                <div class="col-sm-8"></div>


                <div class="col-sm-12">
                    <%--<div id="div" runat="server" style="overflow: auto; height: 405px; width: 100%">--%>
                    <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" CssClass="panel panel-default" HeaderCssClass="panel-heading" HeaderSelectedCssClass="panel-heading"
                        FadeTransitions="false" FramesPerSecond="100" TransitionDuration="150" AutoSize="None" ContentCssClass="panel-title"
                        Height="390px" RequireOpenedPane="false" SuppressHeaderPostbacks="True">
                    </ajaxToolkit:Accordion>
                    <%--</div>--%>
                    <br />
                    <center>     
                            <b><asp:Label ID="lblMensaje" runat="server" Visible="false" Text="No hay trámites disponibles" CssClass="failureNotification"></asp:Label></b>
                        </center>
                </div>

                <div class="col-sm-10"></div>
                <div class="col-sm-2">
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CausesValidation="false"
                        OnClientClick="SeleccionarControles();"
                        OnClick="btnImprimir_Click" Height="35px"
                        class="btn btn-primary btn-block nn" />
                </div>

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
