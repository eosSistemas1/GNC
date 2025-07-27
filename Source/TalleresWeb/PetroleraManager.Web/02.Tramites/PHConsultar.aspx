<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PHConsultar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.PHConsultar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Consultar PH:"></PLs:PLLabel></h2>

    <%--<div id="div" runat="server" style="overflow: auto; height: 405px; width: 100%">--%>
     <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                FadeTransitions="false" FramesPerSecond="100" TransitionDuration="150" AutoSize="None" ContentCssClass="accordionContent"
                Height="390px" RequireOpenedPane="false" SuppressHeaderPostbacks="True"> 
    </ajaxToolkit:Accordion>  
    <%--</div>--%>
    <br />
    <PLs:PLLabel ID="lblMensaje" runat="server" Visible="false" Text="No hay trámites disponibles" CssClass="failureNotification"></PLs:PLLabel>

</asp:Content>
