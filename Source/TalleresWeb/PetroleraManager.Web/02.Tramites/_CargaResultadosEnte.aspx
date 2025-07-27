<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargaResultadosEnte.aspx.cs" Inherits="PetroleraManager.Web.Tramites.CargaResultadosEnte" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Cargar resultados ente:"></PLs:PLLabel></h2>

    <fieldset>
        <legend>Arcvhivo ente <span style="font-weight:bold; color:green">OK</span></legend>
        <asp:FileUpload ID="fuArchivoOK" runat="server" />
    </fieldset>
    <fieldset>
        <legend>Arcvhivo ente <span style="font-weight:bold; color:red">ERRORES</span></legend>
        <asp:FileUpload ID="fuArchivoErrores" runat="server" />
    </fieldset>

    <div style="width: 100%; text-align: right;">
        <PLs:PLButton ID="lnkAceptar" runat="server" Text="       Procesar" CausesValidation="false"
            OnClientClick="this.disabled=true" UseSubmitBehavior="False" OnClick="lnkAceptar_Click"
            Height="35px" Style="background: transparent url(../Imagenes/Iconos/correcta.png) center left no-repeat;" />
        &nbsp;&nbsp;
        <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Volver" CausesValidation="false"
            OnClientClick="window.location='ObleasConsultar.aspx';" Height="35px" Style="background: transparent url(../Imagenes/Iconos/volver.png) center left no-repeat;"
            OnClick="lnkCancelar_Click" />&nbsp;&nbsp;
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

</asp:Content>