<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Valvulas.aspx.cs" Inherits="PetroleraManager.Web.Sistema.Valvulas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Válvulas</legend>
        <table width="100%" border="0">
            <tr>
                <td width="350px">
                    <PLs:PLTextBox ID="txtFiltro" runat="server" LabelText="Buscar:" />
                </td>
                <td width="40px">
                    <PLs:PLImageButton ID="btnBuscar" runat="server" AlternateText="Buscar" ToolTip="Buscar"
                        OnClick="btnBuscar_Click" ImageUrl="~/Imagenes/Iconos/buscar.png" CausesValidation="False" />
                </td>
                <td align="center">
                    <PLs:PLButton ID="btnNuevo" runat="server" Text="Nuevo" CausesValidation="false"
                        OnClick="btnNuevo_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%" border="0">
                        <tr>
                            <td width="60%" valign="top">
                                <fieldset>
                                    <div style="max-height: 150px; overflow: auto;">
                                        <PLs:PLGridView ID="grdFiltro" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                                            DataKeyNames="ID" OnRowCommand="grdFiltro_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                                <asp:TemplateField HeaderText="Modificar" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Modificar" ImageUrl="~/Imagenes/Iconos/modificar.png"
                                                            ToolTip="Modificar" Width="20px" CommandName="modificar" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <PLs:PLImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                                            ToolTip="Eliminar" Width="20px" CommandName="eliminar" OnClientClick="return confirm ('Desea eliminar el item seleccionado?');"
                                                            CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </PLs:PLGridView>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="Panel1" runat="server">
        <fieldset>
            <p style="text-align: right;">
                <PLs:PLButton ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                <PLs:PLButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false"/>
            </p>
            <PLs:PLTabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" style="height:150px">
                <PLs:PLTabPanel runat="server" HeaderText="Válvula" ID="TabPanel1">
                    <ContentTemplate>
                        <PLs:PLHidden ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
                        <PLs:PLTextBoxMasked ID="txtDescripcion" runat="server" LabelText="Cód. Homologación:" Mask="LL99" MaskType="Number" />
                        <Controls:CboMarcasValvulas ID="cboMarcasValvulas" runat="server" LabelText="Marca:" AutomaticLoad="true" IsComboFilter="True" />
                    </ContentTemplate>
                </PLs:PLTabPanel>
            </PLs:PLTabContainer>
        </fieldset>
    </asp:Panel>
</asp:Content>
