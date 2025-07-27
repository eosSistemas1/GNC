<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscNuevoLote.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscNuevoLote" %>

<%@ Register src="MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>

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
                                    <PLs:PLHidden ID="txtID" runat="server" />
                                    <PLs:PLCalendar ID="calFechaAlta" runat="server" LabelText="Fecha Alta:" />
                                    <PLs:PLTextBoxMasked ID="txtNroObleaDesde" runat="server" LabelText="Nro Desde:" Required="true" Mask="99999999" MaskType="None" />
                                    <PLs:PLTextBoxMasked ID="txtNroObleaHasta" runat="server" LabelText="Nro Hasta:" Required="true" Mask="99999999" MaskType="None" />
                                    <PLs:PLTextBoxMasked ID="txtAnioLote" runat="server" LabelText="Año Lote:" Required="true" Mask="9999" MaskType="None" />
                                    <PLs:PLTextBoxMasked ID="txtCantidadObleas" runat="server" LabelText="Cantidad Obleas Lote:" Required="true" Mask="9999" MaskType="None" />
                                    <PLs:PLCheckBox ID="chkActivo" runat="server" Text="Activo" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
