<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Localidades.aspx.cs" Inherits="PetroleraManager.Web.Sistema.Localidades" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Localidades</legend>
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
                <PLs:PLButton ID="btnAceptar" runat="server" Text="       Aceptar" CausesValidation="false"
                    UseSubmitBehavior="true" OnClick="btnAceptar_Click"
                    Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />

                <PLs:PLButton ID="btnCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                    OnClientClick="return confirm('Al cancelar se perderán los cambios realizados. Desea continuar?');" Height="35px"
                    Style="background: transparent url(/Imagenes/Iconos/volver.png) center left no-repeat;"
                    OnClick="btnCancelar_Click" />
            </p>

            <PLs:PLTabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" Style="height: 150px">
                <PLs:PLTabPanel runat="server" HeaderText="Descripción" ID="TabPanel1">
                    <ContentTemplate>
                        <table width="100%" border="0" style="vertical-align: top;">
                            <tr>
                                <td>
                                    <PLs:PLHidden ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
                                    <PLs:PLTextBox ID="txtDescripcion" runat="server" LabelText="Descripción:" Required="true" />
                                    <PLs:PLTextBox ID="txtCodigoPostal" runat="server" LabelText="Código Postal:" Required="false" MaxLenghtTxt="10" />
                                    <Controls:CboProvincias ID="cboProvincia" runat="server" LabelText="Provincia:" AutomaticLoad="true" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </PLs:PLTabPanel>
            </PLs:PLTabContainer>
        </fieldset>
    </asp:Panel>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />
</asp:Content>
