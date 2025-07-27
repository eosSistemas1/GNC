<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarProveedor.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.BuscarProveedor" %>
<table id="tblToolBar" cellspacing="2" cellpadding="1" width="100%" align="left"
    border="0">
    <tr>
        <td align="right">
            <PLs:PLLabel ID="lblSearch" runat="server">Proveedor:</PLs:PLLabel>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlColumnList" CssClass="ToolBar_DropDownList" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <PLs:PLTextBox ID="txtSearch" runat="server"></PLs:PLTextBox>
        </td>
        <td align="left">
            <PLs:PLImageButton ID="btnSearchP" runat="server" ImageUrl="~/Imagenes/Iconos/buscar.png"
                CausesValidation="false" OnClick="btnSearch_Click" ClientIDMode="Static" />
        </td>
        <td width="100%">
            <PLs:PLLabel ID="lblInfo" CssClass="failureNotification" runat="server" EnableTheming="False"
                Font-Size="X-Small" ForeColor="Red"></PLs:PLLabel>
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
        <legend><span class="LabelLegend">Proveedores</span></legend>
        <div style="overflow: auto; height: 300px;">
        <PLs:PLGridView ID="dgProveedores" runat="server" Width="100%" CellPadding="2" AllowSorting="True"
            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
            AutoGenerateColumns="False" OnSelectedIndexChanged="dgProveedores_SelectedIndexChanged" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Descripcion" ReadOnly="True" HeaderText="Razón Social">
                </asp:BoundField>
                <asp:BoundField DataField="Domicilio" ReadOnly="True" HeaderText="Domicilio"></asp:BoundField>
                <asp:BoundField DataField="CUIT" ReadOnly="True" HeaderText="CUIT"></asp:BoundField>
                <asp:ButtonField Text="Seleccionar" ButtonType="Image" ImageUrl="~/Imagenes/Iconos/seleccionar.png"
                    CommandName="Select"></asp:ButtonField>
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
