<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PHIngresar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.PHIngresar" %>

<%@ Register Src="~/UserControls/uscCargarVehiculo.ascx" TagName="uscCargarVehiculo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uscCargarCliente.ascx" TagName="uscCargarCliente" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uscCargarCilindrosValvulasPH.ascx" TagName="uscCargarCilindrosValvulasPH" TagPrefix="uc3" %>
<%@ Register src="~/UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc4" %>
<%@ Register src="~/UserControls/BuscarTaller.ascx" tagname="BuscarTaller" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" border="0">
        <tr>
            <td width="50%">                
                <uc5:BuscarTaller ID="BuscarTaller1" runat="server" OnGridTalleresButtonClick="Talleres_Click" /> 
            </td>
            <td>
                <PLs:PLTextBoxMasked ID="txtFecha" Mask="99/99/9999" runat="server" MaskType="Date" LabelText="Fecha:" AutoPostBack="false" />
            </td>
        </tr>
        <tr>
            <td>
                <PLs:PLPanel ID="pnlOblea" runat="server">
                    <table width="100%" border="0">
                    <tr>
                      <td width="45%">
                        <PLs:PLTextBoxMasked ID="txtNroObleaAnterior" Mask="99999999" runat="server" MaskType="Number" LabelText="Nro. Oblea:" AutoPostBack="false" />
                    </td>
                    <td align="left">
                        <PLs:PLImageButton ID="btnBuscarOblea" runat="server" Text="Buscar" CausesValidation="false" OnClick="btnBuscarOblea_Click" ImageUrl="~/Imagenes/Iconos/buscar.png"/>
                    </td>
                    </tr>
                </table>
                </PLs:PLPanel>
            </td>            
            <td>
                <PEARGNC:CboCRPC ID="cboCRPC" runat="server" AutomaticLoad="true" LabelText="CRPC:" Enabled="false"/>
            </td>
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
                <uc3:uscCargarCilindrosValvulasPH ID="uscCargarCilindrosValvulasPH1" runat="server" PermiteSeleccionar="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <PLs:PLButton ID="lnkModificar" runat="server" Text="       Aceptar" CausesValidation="false" Visible="false"
                    OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkModificar_Click"
                    Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />
                <PLs:PLButton ID="lnkAceptar" runat="server" Text="       Enviar/Imprimir" CausesValidation="false"
                    OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkAceptar_Click"
                    Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />
                <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                    OnClientClick="window.location='inicio.aspx';" Height="35px" 
                    Style="background: transparent url(/Imagenes/Iconos/volver.png) center left no-repeat;" 
                    onclick="lnkCancelar_Click" />
            </td>
        </tr>
    </table>
    <uc4:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
