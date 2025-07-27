<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="ModificarInspeccionVisual.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Proceso.ModificarInspeccionVisual" %>

<%@ Register Src="~/PH/UserControls/ConsultaCilindrosPH.ascx" TagPrefix="uc1" TagName="ConsultaCilindrosPH" %>
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
                <uc1:ConsultaCilindrosPH runat="server" ID="ConsultaCilindrosPH" />
            </div>

            <hr />

            <div class="col-sm-4 pull-left">
                <button type="button" class="btn btn-danger btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
            </div>

            <div class="col-sm-8 pull-right">
                <div class="col-sm-5"></div>
                <div class="col-sm-3"></div>
                <div class="col-sm-4">
                    <button id="btnAceptar" type="button" class="btn btn-primary btn-block nn" aria-label="" title="Aceptar" alt="Aceptar" runat="server" onserverclick="btnAceptar_ServerClick">Aceptar &nbsp<i class="fa fa-chevron-down" aria-hidden="true"></i></button>
                </div>
            </div>


        </div>

    </main>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBox" />

    <div class="clearfix"></div>

</asp:Content>