<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformesLibroDiario.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesLibroDiario" %>

<%@ Register Src="~/UserControls/FiltrosFechas.ascx" TagPrefix="uc1" TagName="FiltrosFechas" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Libro Diario</legend>
        <table style="width: 100%">
            <tr>
                <td style="width: 60%">
                    <uc1:FiltrosFechas runat="server" ID="filtrosFechas" />
                </td>
                <td style="width: 40%; vertical-align: top; padding-top: 10px;">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100px; font-weight: bold;">Cliente:</td>
                            <td>
                                <asp:TextBox ID="txtCliente" runat="server" MaxLength="100" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; font-weight: bold;">Dominio:</td>
                            <td>
                                <asp:TextBox ID="txtDominio" runat="server" MaxLength="10" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right; padding-top: 8px;">
                                <Controls:BtnAceptar ID="btnVer" runat="server" Text="Ver" OnClick="btnVer_Click" />
                                &nbsp;
                                <Controls:BtnPdf ID="btnPdf" runat="server" Text="PDF" OnClick="btnPdf_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>

    <uc1:MessageBoxCtrl runat="server" id="MessageBoxCtrl" />

    <fieldset id="fldResultados" runat="server" visible="false">
        <legend>Resultados</legend>
        <div style="overflow: auto; width: 100%;">
            <PLs:PLGridView ID="grdLibroDiario" runat="server" AutoGenerateColumns="False" Width="100%"
                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader"
                RowStyle-CssClass="GridRow" RowStyle-Height="18px"
                EmptyDataText="<center>No hay operaciones para los filtros ingresados.</center>">
                <Columns>
                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="65px" />
                    <asp:BoundField HeaderText="Operación" DataField="Operacion" ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="Cliente" DataField="NombreCliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                    <asp:BoundField HeaderText="Vehículo" DataField="Vehiculo" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-Width="70px" />
                    <asp:BoundField HeaderText="Cód. Reg." DataField="CodigoRegulador" ItemStyle-Width="70px" />
                    <asp:BoundField HeaderText="Nro. Reg." DataField="NroSerieRegulador" ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="Cód. Cil." DataField="CodigosCilindros" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="Nro. Cil." DataField="NrosSeriesCilindros" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90px" />
                    <asp:BoundField HeaderText="Cód. Vál." DataField="CodigosValvulas" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px" />
                    <asp:BoundField HeaderText="Nro. Vál." DataField="NrosSeriasValvulas" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90px" />
                    <asp:BoundField HeaderText="Oblea Ant." DataField="NroObleaAnterior" ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="Oblea Nueva" DataField="NroObleaNueva" ItemStyle-Width="80px" />
                </Columns>
                <EditRowStyle CssClass="GridRow"></EditRowStyle>
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <RowStyle CssClass="GridRow"></RowStyle>
            </PLs:PLGridView>
        </div>
    </fieldset>

    <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />
</asp:Content>
