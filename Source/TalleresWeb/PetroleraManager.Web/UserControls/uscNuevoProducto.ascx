<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscNuevoProducto.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.uscNuevoProducto" %>

<table width="100%" border="0">
    <tr>
        <td width="100%">
            <asp:UpdatePanel ID="updPanel" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Nuevo Producto:</legend>
                        <table width="100%" border="0" id="tblNvoProducto" runat="server">
                            <tr>
                                <td>
                                    <PEARGNC:CboTipoProducto ID="cboTipoProducto" runat="server" AutomaticLoad="true" LabelText="Tipo Producto:" />
                                    <PLs:PLTextBox ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
                                    <PLs:PLTextBox ID="txtDescripcion" runat="server" LabelText="Descripción:" Required="true" />
                                    <PEARGNC:CboRubro ID="cboRubro" runat="server" AutomaticLoad="true" LabelText="Rubro:" />
                                    <PLs:PLTextBox ID="txtPrecioCompra" runat="server" LabelText="Precio de Compra:" />
                                    <PLs:PLTextBox ID="txtPrecioVenta" runat="server" LabelText="Precio de Venta:" />
                                    <PLs:PLTextBox ID="txtStockMinimo" runat="server" LabelText="Stock Mín.:" />
                                    <PLs:PLTextBox ID="txtStockActual" runat="server" LabelText="Stock Actual:" />
                                    <PEARGNC:CboBaseImponible ID="cboBaseImponible" runat="server" AutomaticLoad="true" LabelText="Base Imponible:" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
