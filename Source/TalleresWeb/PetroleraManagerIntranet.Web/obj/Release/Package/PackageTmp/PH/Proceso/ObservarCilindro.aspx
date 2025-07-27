<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObservarCilindro.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Proceso.ObservarCilindro" %>

<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionVisual.ascx" TagPrefix="uc1" TagName="InspeccionVisual" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionRoscas.ascx" TagPrefix="uc1" TagName="InspeccionRoscas" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/MedicionEspesores.ascx" TagPrefix="uc1" TagName="MedicionEspesores" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionExterior.ascx" TagPrefix="uc1" TagName="InspeccionExterior" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/RegistroPesos.ascx" TagPrefix="uc1" TagName="RegistroPesos" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/PruebaHidraulica.ascx" TagPrefix="uc1" TagName="PruebaHidraulica" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionInterior.ascx" TagPrefix="uc1" TagName="InspeccionInterior" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12">
        <h4>OBSERVAR CILINDRO</h4>
    </div>
    <main id="central-nm" role="main">

        <div class="col-sm-12">
            <div class="col-sm-12 text-left">
                <span><strong>Estación 1 :</strong> Verficar marcado del cilindro</span>
            </div>
            <div class="clearfix"></div>
            <div class="col-sm-12">
                <div class="col-sm-12 text-center"><b>Inspeccion Visual</b></div>
                <uc1:InspeccionVisual runat="server" ID="InspeccionVisual" />
            </div>
            <div class="col-sm-6">
                <div class="col-sm-12 text-center"><b>Roscas</b></div>
                <uc1:InspeccionRoscas runat="server" ID="InspeccionRoscas" />
            </div>            
            <div class="col-sm-6">
                <div class="col-sm-12 text-center"><b>Espesores</b></div>
                <uc1:MedicionEspesores runat="server" ID="MedicionEspesores" />
            </div>
            <hr />
            <div class="col-sm-6">
                <div class="col-sm-12 text-center"><b>Exterior</b></div>
                <uc1:InspeccionExterior runat="server" ID="InspeccionExterior" />
            </div>
            <div class="col-sm-6">
                <div class="col-sm-12 text-center"><b>Pesos</b></div>
                <uc1:RegistroPesos runat="server" ID="RegistroPesos" />
            </div>
            <div class="clearfix"></div>
        </div>

        <div class="col-sm-12 text-left">
            <span><strong>Estación 2 :</strong> Registro peso con agua</span>
            <uc1:PruebaHidraulica runat="server" ID="PruebaHidraulica" />
        </div>

        <div class="col-sm-12 text-left">
            <span><strong>Estación 3 :</strong> Inspección interior</span>
            <uc1:InspeccionInterior runat="server" ID="InspeccionInterior" />
        </div>
    </main>

    <div class="col-sm-8"></div>
    <div class="col-sm-2 text-right">
        <button id="btnGuardar" type="button" class="btn btn-primary btn-block nn" aria-label="" title="Guardar" alt="Aceptar" runat="server" onserverclick="btnGuardar_ServerClick">Guardar &nbsp<i class="fa fa-check" aria-hidden="true"></i></button>
    </div>
    <div class="col-sm-2 text-right">
        <button id="btnCancelar" type="button" class="btn btn-danger btn-block nn" aria-label="" title="Cancelar" alt="Cancelar" runat="server" onserverclick="btnCancelar_ServerClick">Cancelar &nbsp<i class="fa fa-close" aria-hidden="true"></i></button>
    </div>

</asp:Content>
