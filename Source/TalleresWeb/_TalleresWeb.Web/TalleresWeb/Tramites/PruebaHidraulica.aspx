<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true"
    CodeBehind="PruebaHidraulica.aspx.cs" Inherits="TalleresWeb.Web.PruebaHidraulica" %>

<%@ Register Src="~/UserControls/uscCargarVehiculo.ascx" TagName="uscCargarVehiculo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uscCargarCliente.ascx" TagName="uscCargarCliente" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uscCargarCilindrosValvulasPH.ascx" TagName="uscCargarCilindrosValvulasPH" TagPrefix="uc3" %>
<%@ Register src="~/UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td width="95%">
                <PLs:PLTextBoxMasked ID="txtNroObleaAnterior" Mask="99999999" runat="server" MaskType="Number"
                    LabelText="Nro. Oblea:" AutoPostBack="false" />
            </td>
            <td width="5%">
                <asp:Button ID="btnBuscarOblea" runat="server" Text="Buscar" CausesValidation="false" OnClick="btnBuscarOblea_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <PEARGNC:CboCRPC ID="cboCRPC" runat="server" AutomaticLoad="true" LabelText="CRPC:" Enabled="false"/>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:uscCargarVehiculo ID="uscCargarVehiculo1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc2:uscCargarCliente ID="uscCargarCliente1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc3:uscCargarCilindrosValvulasPH ID="uscCargarCilindrosValvulas1" runat="server" PermiteSeleccionar="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <PLs:PLButton ID="lnkAceptar" runat="server" Text="       Enviar/Imprimir" CausesValidation="false"
                    OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkAceptar_Click"
                    Height="35px" Style="background: transparent url(/Images/Iconos/correcta.png) center left no-repeat;" />
                <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                    OnClientClick="window.location='inicio.aspx';" Height="35px" 
                    Style="background: transparent url(/Images/Iconos/volver.png) center left no-repeat;" 
                    onclick="lnkCancelar_Click" />
            </td>
        </tr>
    </table>
    <uc4:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
