<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Provincias.aspx.cs" Inherits="PetroleraManager.Web.Sistema.ResponsableTecnico" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Responsable Técnico</legend>
        <table width="100%" border="0">
            <tr>
                <td width="400px">
                    <PLs:PLTextBox ID="txtFiltro" runat="server" LabelText="Buscar:" ClientIDMode="Static" />
                </td>
                <td width="40px">
                    <PLs:PLImageButton ID="btnBuscar" runat="server" AlternateText="Buscar" ToolTip="Buscar" ClientIDMode="Static"
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
                <PLs:PLLabel ID="msjError" runat="server"></PLs:PLLabel>
                <PLs:PLButton ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                <PLs:PLButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false"/>
            </p>

            <PLs:PLTabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" style="height:150px">
                <PLs:PLTabPanel runat="server" HeaderText="Descripción" ID="TabPanel1">
                    <ContentTemplate>
                        <table width="100%" border="0" style="vertical-align: top;">
                            <tr>
                                <td>
                                    <PLs:PLHidden ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
                                    <PLs:PLTextBox ID="txtDescripcion" runat="server" LabelText="Nombre y Apellido:" Required="true" />
                                    <Controls:CboTiposDocumentos ID="cboTiposDocumentos" runat="server" LabelText="Tipo Documento:" AutomaticLoad="true" />
                                    <PLs:PLTextBox ID="txtNumeroDocumento" runat="server" LabelText="Nro. Documento:" Required="true" onKeyPress="return soloNumeros(event)" />
                                    <PLs:PLTextBox ID="txtMatricula" runat="server" LabelText="Matrícula:" />
                                    <PLs:PLTextBox ID="txtTitulo" runat="server" LabelText="Título:" />
                                    <PLs:PLHidden ID="txtActivo" runat="server" LabelText="Activo:" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </PLs:PLTabPanel>
            </PLs:PLTabContainer>
        </fieldset>
    </asp:Panel>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

<script type="text/javascript">
     function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
     }
</script>

</asp:Content>
