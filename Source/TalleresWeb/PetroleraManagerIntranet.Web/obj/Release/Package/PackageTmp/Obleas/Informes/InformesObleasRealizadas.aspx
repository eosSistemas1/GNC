<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformesObleasRealizadas.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesObleasRealizadas" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>Obleas Realizadas</h4>
            </div>
            <hr />
        </div>

        <div class="row">
            <div class="col-sm-2">
                <p>Desde:</p>
            </div>
            <div class="col-sm-3">
                <input type="date" id="calFechaD" runat="server" />
            </div>
            <div class="col-sm-2">
                <p>Hasta:</p>
            </div>
            <div class="col-sm-3">
                <input type="date" id="calFechaH" runat="server" />
            </div>
            <div class="col-sm-2">
                <button type="button" class="btn btn-primary" aria-label="" name="" id="btnExcel" title="Excel" runat="server" onserverclick="btnExcel_Click"><i class="fa fa-print" aria-hidden="true"></i>&nbsp Excel</button>
            </div>
        </div>

        <%--<div class="col-sm-5">
        </div>
        <div class="col-sm-2">
            <strong>Visualizar en:</strong>
        </div>
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary" aria-label="" name="" id="btnPDF" title="PDF" runat="server" onserverclick="btnPdf_Click"><i class="fa fa-print" aria-hidden="true"></i>&nbsp PDF</button>
        </div>

        <div class="col-sm-1"></div>--%>
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />
</asp:Content>

