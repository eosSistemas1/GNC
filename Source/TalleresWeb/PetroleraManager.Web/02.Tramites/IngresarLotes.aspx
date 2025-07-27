<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresarLotes.aspx.cs" Inherits="PetroleraManager.Web.Tramites.IngresarLotes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register Src="~/UserControls/uscNuevoLote.ascx" TagName="uscNuevoLote"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Lotes</legend>
        <table width="100%" border="0">
            <tr>
                <td width="350px">
                    <PLs:PLTextBox ID="txtFiltro" runat="server" LabelText="Buscar:"/>
                </td>
                <td width="40px">
                    <PLs:PLImageButton ID="btnBuscar" runat="server" AlternateText="Buscar" ToolTip="Buscar"
                        OnClick="btnBuscar_Click" ImageUrl="~/Imagenes/Iconos/buscar.png" 
                        CausesValidation="False"  />
                </td>
                <td align="center">
                    <PLs:PLButton ID="btnNuevo" runat="server" Text="Nuevo" CausesValidation="false" onclick="btnNuevo_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%" border="0">
                        <tr>
                            <td width="60%" valign="top">
                                <fieldset>
                                    <legend>Lotes:</legend>
                                    <PLs:PLGridView ID="grdFiltro" runat="server" AutoGenerateColumns="False" Width="100%"
                                        AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                                        DataKeyNames="ID,NroObleaDesde,NroObleaHasta,LoteActivo" onrowcommand="grdFiltro_RowCommand" 
                                        onrowdatabound="grdFiltro_RowDataBound" >
                                        <Columns>
                                            <asp:BoundField HeaderText="Descripción" />
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Modificar" ImageUrl="~/Imagenes/Iconos/modificar.png"
                                                        ToolTip="Modificar" Width="20px" CommandName="modificar" 
                                                        CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <PLs:PLImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                                        ToolTip="Eliminar" Width="20px" CommandName="eliminar" OnClientClick="return confirm ('Desea eliminar el item seleccionado?');"
                                                        CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </PLs:PLGridView>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <AjaxControlToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget"
            PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
            CancelControlID="btnCancelar" CacheDynamicResults="false" />
            <div style="display:none;">
                <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
            </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="CajaDialogo">
            <uc1:uscNuevoLote ID="uscNuevoLote1" runat="server" />
            <center>
            <PLs:PLButton ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
            <PLs:PLButton ID="btnCancelar" runat="server" Text="Cancelar" />
            </center>
        </asp:Panel>
        
    </fieldset>

</asp:Content>
