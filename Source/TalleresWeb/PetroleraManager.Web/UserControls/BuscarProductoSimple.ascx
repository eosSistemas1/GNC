<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarProductoSimple.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.BuscarProductoSimple" %>
<table id="tblToolBar" cellspacing="0" cellpadding="0" width="100%" align="left"
    border="0">
    <tr>
        <td align="right">
            
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlColumnList" CssClass="ToolBar_DropDownList" runat="server"
                Visible="false">
            </asp:DropDownList>
        </td>
        <td>
            <PLs:PLTextBox ID="txtSearch" runat="server" WidthTxt="75%"></PLs:PLTextBox>
        </td>
        <td align="center" width="30px">
            <PLs:PLImageButton ID="btnSearchP" name="btnSearchP" runat="server" ImageUrl="~/Imagenes/Iconos/buscar.png"
                CausesValidation="false" OnClick="btnSearch_Click" ClientIDMode="Static" />
        </td>
    </tr>
    <tr>
        <td width="100%" colspan="4">
            <PLs:PLLabel ID="lblInfo" CssClass="messageLabel" runat="server" EnableTheming="False"
                ForeColor="Red"></PLs:PLLabel>
        </td>
    </tr>
</table>

<PLs:PLModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="Panel1"
    BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCerrarBuscador"
    CacheDynamicResults="false">
</PLs:PLModalPopupExtender>
<div style="display: none;">
    <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
</div>
<PLs:PLPanel ID="Panel1" runat="server" CssClass="CajaDialogo" Width="100%">
    <fieldset>
        <legend><span class="LabelLegend">Productos</span></legend>
        <div style="overflow: auto; height: 300px;">
        <PLs:PLGridView ID="dgProducto" runat="server" Width="100%" CellPadding="2" AllowSorting="True"
            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
            AutoGenerateColumns="False" OnSelectedIndexChanged="dgProducto_SelectedIndexChanged"
            DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Descripcion" ReadOnly="True" HeaderText="Descripción"></asp:BoundField>
                <asp:BoundField DataField="CodigoProducto" ReadOnly="True" HeaderText="Código"></asp:BoundField>
                <asp:ButtonField Text="Seleccionar" ButtonType="Image" ImageUrl="~/Imagenes/Iconos/seleccionar.png"
                    ItemStyle-HorizontalAlign="center" CommandName="Select"></asp:ButtonField>
            </Columns>
        </PLs:PLGridView>
        </div>
    </fieldset>
    <br />
    <center>
        <PLs:PLButton ID="btnCerrarBuscador" runat="server" Text="Cancelar" />
    </center>
    <br />
</PLs:PLPanel>
