<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="IngresarDatos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Proceso.IngresarDatos" %>

<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionVisual.ascx" TagPrefix="uc1" TagName="InspeccionVisual" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/RegistroPesos.ascx" TagPrefix="uc1" TagName="RegistroPesos" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/MedicionEspesores.ascx" TagPrefix="uc1" TagName="MedicionEspesores" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionExterior.ascx" TagPrefix="uc1" TagName="InspeccionExterior" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/PruebaHidraulica.ascx" TagPrefix="uc1" TagName="PruebaHidraulica" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionRoscas.ascx" TagPrefix="uc1" TagName="InspeccionRoscas" %>
<%@ Register Src="~/PH/UserControls/ProcesosPHPasos/InspeccionInterior.ascx" TagPrefix="uc1" TagName="InspeccionInterior" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <ajaxToolkit:ToolkitScriptManager runat="server"></ajaxToolkit:ToolkitScriptManager>

    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-8">
                    <h4>
                        <asp:Label ID="lblTitulo" runat="server" /></h4>
                </div>

                <div class="col-sm-4">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <th>Nº de Revisión: <strong>67730</strong></th>
                                <th>
                                    <strong>
                                        <asp:Label ID="lblFechaYHora" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy -  HH:mm}") %>' /></strong>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="col-sm-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <td>Nº Serie Cil.: <strong>
                                    <asp:Label ID="lblNumeroSerieCilindro" runat="server" /></strong></td>
                                <td>Cód. Hom. Cil.: <strong>
                                    <asp:Label ID="lblCodigoHomologacionCilindro" runat="server" /></strong></td>
                                <td>Nº Serie Val.: <strong>
                                    <asp:Label ID="lblNumeroSerieValvula" runat="server" /></strong></td>
                                <td>Cód. Hom. Val.: <strong>
                                    <asp:Label ID="lblCodigoHomologacionValvula" runat="server" /></strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Taller: <strong>
                                    <asp:Label ID="lblTaller" runat="server" /></strong></td>
                                <td>Marca Cil.: <strong>
                                    <asp:Label ID="lblMarcaCilindro" runat="server" /></strong></td>
                                <td colspan="2">Marca Val.: <strong>
                                    <asp:Label ID="lblMarcaValvula" runat="server" /></strong></td>
                            </tr>
                            <tr>
                                <td>Capacidad: <strong>
                                    <asp:Label ID="lblCapacidadCilindro" runat="server" />
                                    cm3</strong></td>
                                <td>Diámetro: <strong>
                                    <asp:Label ID="lblDiametroCilindro" runat="server" /></strong></td>
                                <td>Hom. Fabricación: <strong>falta</strong></td>
                                <td>Fecha Fabricación: <strong>
                                    <asp:Label ID="lblFechaFabricacionCilindro" runat="server" /></strong></td>
                            </tr>
                            <tr>
                                <td>Pared Mín.: <strong>
                                    <asp:Label ID="lblParedMinimo" runat="server" />
                                    cm</strong></td>
                                <td>Fondo Mín.: <strong>
                                    <asp:Label ID="lblFondoMinimo" runat="server" />
                                    cm</strong></td>
                                <td colspan="2">Fecha Última Revisión: <strong>
                                    <asp:Label ID="lblFechaUltimaRevision" runat="server" /></strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <hr />
            <div class="col-xs-12">
                <uc1:InspeccionVisual runat="server" ID="InspeccionVisual" Visible="false" />
                <uc1:RegistroPesos runat="server" ID="RegistroPesos" Visible="false" />
                <uc1:MedicionEspesores runat="server" ID="MedicionEspesores" Visible="false" />
                <uc1:InspeccionExterior runat="server" ID="InspeccionExterior" Visible="false" />
                <uc1:PruebaHidraulica runat="server" ID="PruebaHidraulica" Visible="false" />
                <uc1:InspeccionRoscas runat="server" ID="InspeccionRoscas" Visible="false" />
                <uc1:InspeccionInterior runat="server" ID="InspeccionInterior" Visible="false" />
            </div>

            <hr />

            <div class="col-sm-4 pull-left">
                <button type="button" class="btn btn-danger btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
            </div>

            <div class="col-sm-8 pull-right">
                <div class="col-sm-5"></div>
                <div class="col-sm-3">
                    <button id="btnAceptar" type="button" class="btn btn-primary btn-block nn" aria-label="" title="Aceptar" alt="Aceptar" runat="server" onserverclick="btnAceptar_ServerClick">Aceptar &nbsp<i class="fa fa-chevron-down" aria-hidden="true"></i></button>
                </div>
                <div class="col-sm-4">
                    <button id="btnAceptarYContinuar" type="button" class="btn btn-success btn-block nn" aria-label="" title="Aceptar" alt="Aceptar y Continuar" runat="server" onserverclick="btnAceptarYContinuar_ServerClick">Aceptar y Continuar &nbsp<i class="fa fa-chevron-right" aria-hidden="true"></i></button>
                </div>
            </div>


        </div>

    </main>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <div class="clearfix"></div>

</asp:Content>

