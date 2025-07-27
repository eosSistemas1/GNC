<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformesObleasRealizadas.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesObleasRealizadas" %>

<%@ Register Src="~/UserControls/FiltrosFechas.ascx" TagPrefix="uc1" TagName="FiltrosFechas" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>




<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Obleas Realizadas</legend>
        <table style="width: 100%">
            <tr>
                <td style="width: 70%">
                    <uc1:FiltrosFechas runat="server" ID="filtrosFechas" />
                </td>
                <td style="width: 30%; text-align: center;">
                    <Controls:BtnAceptar ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
                    &nbsp;
                    <Controls:BtnPdf ID="btnPdf" runat="server" Text="PDF" OnClick="btnPdf_Click" />
                    &nbsp;
                    <Controls:BtnExcel ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <uc1:MessageBoxCtrl runat="server" id="MessageBoxCtrl" />
    
    <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />
</asp:Content>

