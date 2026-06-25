<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformesLibroDiario.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesLibroDiario" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>LIBRO DIARIO</h4>
            </div>
            <hr />
        </div>

        <div class="row">
            <div class="col-sm-1">
                <p>Desde:</p>
            </div>
            <div class="col-sm-2">
                <input type="date" id="calFechaD" runat="server" />
            </div>
            <div class="col-sm-1">
                <p>Hasta:</p>
            </div>
            <div class="col-sm-2">
                <input type="date" id="calFechaH" runat="server" />
            </div>
            <div class="col-sm-1">
                <p>Cliente:</p>
            </div>
            <div class="col-sm-2">
                <input type="text" id="txtCliente" runat="server" class="form-control" maxlength="100" />
            </div>
            <div class="col-sm-1">
                <p>Dominio:</p>
            </div>
            <div class="col-sm-1">
                <input type="text" id="txtDominio" runat="server" class="form-control" maxlength="10" />
            </div>
        </div>

        <div class="row" style="margin-top:10px;">
            <div class="col-sm-12">
                <button type="button" class="btn btn-primary" id="btnVer" runat="server" onserverclick="btnVer_Click"><i class="fa fa-search" aria-hidden="true"></i>&nbsp; Ver</button>
                &nbsp;
                <button type="button" class="btn btn-primary" id="btnPdf" runat="server" onserverclick="btnPdf_Click"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; PDF</button>
            </div>
        </div>

        <div class="row" style="margin-top:15px;">
            <div class="col-sm-12" id="divResultados" runat="server" visible="false">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="grdLibroDiario" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover table-condensed"
                        EmptyDataText="No hay operaciones para los filtros ingresados.">
                        <HeaderStyle BackColor="#4a6fa5" ForeColor="White" Font-Bold="true" />
                        <Columns>
                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="70px" />
                            <asp:BoundField HeaderText="Operación" DataField="Operacion" ItemStyle-Width="85px" />
                            <asp:BoundField HeaderText="Cliente" DataField="NombreCliente" ItemStyle-Width="150px" />
                            <asp:BoundField HeaderText="Vehículo" DataField="Vehiculo" ItemStyle-Width="100px" />
                            <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-Width="75px" />
                            <asp:BoundField HeaderText="Cód. Reg." DataField="CodigoRegulador" ItemStyle-Width="70px" />
                            <asp:BoundField HeaderText="Nro. Reg." DataField="NroSerieRegulador" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Cód. Cil." DataField="CodigosCilindros" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Nro. Cil." DataField="NrosSeriesCilindros" ItemStyle-Width="90px" />
                            <asp:BoundField HeaderText="Cód. Vál." DataField="CodigosValvulas" ItemStyle-Width="75px" />
                            <asp:BoundField HeaderText="Nro. Vál." DataField="NrosSeriasValvulas" ItemStyle-Width="90px" />
                            <asp:BoundField HeaderText="Oblea Ant." DataField="NroObleaAnterior" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Oblea Nueva" DataField="NroObleaNueva" ItemStyle-Width="85px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
    <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />
</asp:Content>
