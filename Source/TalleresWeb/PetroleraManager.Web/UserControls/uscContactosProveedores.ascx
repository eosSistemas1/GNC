<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscContactosProveedores.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.uscContactosProveedores" %>
<table border="0" width="100%">
    <tr>
        <td>
            <PLs:PLHidden ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
            <PLs:PLHidden ID="txtIDProveedor" runat="server" LabelText="ID:" Enabled="false" />
        </td>
        <td>
            <PLs:PLTextBox ID="txtNombre" runat="server" LabelText="Nombre:" Required="false" />
        </td>
        <td>
            <PLs:PLTextBox ID="txtDescripcion" runat="server" LabelText="Descripción:" Required="false" />
        </td>
        <td>
            <PLs:PLTextBox ID="txtTelefono" runat="server" LabelText="Teléfono:" Required="false" />
        </td>
        <td>
            <PLs:PLTextBox ID="txtCelular" runat="server" LabelText="Celular:" Required="false" />
        </td>
        <td>
            <PLs:PLTextBox ID="txtEmail" runat="server" LabelText="Email:" Required="false" />
        </td>
        <td align="center">
            <PLs:PLButton ID="btnNuevo" runat="server" Text="Grabar" CausesValidation="false"
                OnClick="btnNuevo_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <div style="max-height: 150px; overflow: auto;">
                <PLs:PLGridView ID="grdContactos" runat="server" AutoGenerateColumns="False" Width="100%"
                    AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                    DataKeyNames="ID,ProveedoresID" OnRowCommand="grdContactos_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                        <asp:BoundField HeaderText="Celular" DataField="Celular" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />

                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Modificar" ImageUrl="~/Imagenes/Iconos/modificar.png"
                                    ToolTip="Modificar"  CommandName="modificar" CommandArgument="<%# Container.DataItemIndex %>"
                                    CausesValidation="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                    ToolTip="Eliminar" CommandName="eliminar" OnClientClick="return confirm ('Desea eliminar el item seleccionado?');"
                                    CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </PLs:PLGridView>
            </div>
        </td>
    </tr>
</table>
