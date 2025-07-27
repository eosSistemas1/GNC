<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarOblea.aspx.cs" Inherits="PetroleraManager.Web.Tramites.EliminarOblea" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Eliminar Oblea</legend>

        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <PLs:PLLabelLabel ID="lblObleaAnterior" runat="server" LabelText="Nro. Oblea Anterior:" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <PLs:PLLabelLabel ID="lblTitular" runat="server" LabelText="Titular:" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <PLs:PLLabelLabel ID="lblVehiculo" runat="server" LabelText="Vehículo:" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <PLs:PLLabelLabel ID="lblTaller" runat="server" LabelText="Taller:" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <PLs:PLLabelLabel ID="lblObservaciones" runat="server" LabelText="Observaciones:" />
                </td>
                <td>
                    <PLs:PLTextArea ID="txtObservaciones" runat="server" Height="50px" Rows="20" Columns="50"></PLs:PLTextArea>
                </td>
            </tr>
        </table>
        <hr />
        <asp:Panel runat="server" class="right" DefaultButton="btnAceptar" >            
            <Controls:BtnAceptar ID="btnAceptar" runat="server" Text="Eliminar" OnClick="btnAceptar_Click" />            
            &nbsp;&nbsp;
            <Controls:BtnCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />
        </asp:Panel>
    </fieldset>

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
