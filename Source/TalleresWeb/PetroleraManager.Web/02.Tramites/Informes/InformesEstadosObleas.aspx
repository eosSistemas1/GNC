<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InformesEstadosObleas.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesEstadosObleas" %>

<%@ Register Src="~/UserControls/BuscarTaller.ascx" TagName="BuscarTaller" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagName="PrintBoxCtrl" TagPrefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Filtros:</legend>

        <div style="width:100%; text-align:right;">
            <Controls:BtnAceptar ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" />
            &nbsp;&nbsp;
            <Controls:BtnCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />                        
        </div>

        <ajaxToolkit:TabContainer ID="tabFiltros" runat="server" ActiveTabIndex="0" Width="100%">
            <ajaxToolkit:TabPanel ID="pnlFicha" runat="server" ScrollBars="None" HeaderText="Ficha:">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <PLs:PLTextBox ID="txtNroObleaNueva" runat="server" ClientIDMode="Static" LabelText="NRO. OBLEA:" WidthTxt="90px" MaxLenghtTxt="12" />
                            </td>                            
                            <td>
                                <PLs:PLTextBox ID="txtDominioVehiculo" runat="server" ClientIDMode="Static" LabelText="DOMINIO:" WidthTxt="90px" MaxLenghtTxt="7" />
                            </td>
                            <td>
                                <PLs:PLTextBox ID="txtNroDocumento" runat="server" ClientIDMode="Static" LabelText="NRO. DOCUMENTO:" WidthTxt="90px" MaxLenghtTxt="7" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="pnlRegulador" runat="server"  ScrollBars="None" HeaderText="Regulador:">
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:150px;">Cód. Homologación:</td>
                            <td><asp:TextBox ID="txtCodRegulador" runat="server" MaxLength="10"></asp:TextBox></td>
                            <td style="width:150px;">Nro. Serie</td>
                            <td><asp:TextBox ID="txtSerieRegulador" runat="server" MaxLength="10"></asp:TextBox></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="pnlCilindro" runat="server" ScrollBars="None" HeaderText="Cilindro:">
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:150px;">Cód. Homologación:</td>
                            <td><asp:TextBox ID="txtCodCilindro" runat="server" MaxLength="10"></asp:TextBox></td>
                            <td style="width:150px;">Nro. Serie</td>
                            <td><asp:TextBox ID="txtSerieCilindro" runat="server" MaxLength="10"></asp:TextBox></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="pnlValvula" runat="server" ScrollBars="None" HeaderText="Válvula">
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:150px;">Cód. Homologación:</td>
                            <td><asp:TextBox ID="txtCodValvula" runat="server" MaxLength="10"></asp:TextBox></td>
                            <td style="width:150px;">Nro. Serie</td>
                            <td><asp:TextBox ID="txtSerieValvula" runat="server" MaxLength="10"></asp:TextBox></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>        
    </fieldset>
    <fieldset>
        <legend>Obleas:</legend>
        <div style="overflow: auto; height: 300px; width: 100%; text-align: center;">
            <PLs:PLGridView ID="grdFichas" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID, IdEstadoFicha" OnRowCommand="grdFichas_RowCommand"
                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow" OnRowDataBound="grdFichas_RowDataBound"
                EmptyDataText="<center>No hay obleas para los filtros ingresados.</center>" RowStyle-Height="18px">
                <Columns>
                    <asp:BoundField HeaderText="Nro Int Op." DataField="NroInternoOpercion" />
                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:BoundField HeaderText="Estado Ficha" DataField="EstadoFicha" />
                    <asp:BoundField HeaderText="Fecha Venc." DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Ver Oblea">
                        <ItemTemplate>
                            <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Ver Oblea" ImageUrl="../../Imagenes/Iconos/modificar.png"
                                ToolTip="Ver Oblea" Width="18px" CommandName="modificar" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarjeta">
                        <ItemTemplate>
                            <PLs:PLImageButton ID="btnImprimir" runat="server" AlternateText="Imprimir Tarjeta" ImageUrl="../../Imagenes/Iconos/imprimir.png"
                                ToolTip="Imprimir Tarjeta" Width="18px" CommandName="imprimir" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRow"></EditRowStyle>
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <RowStyle CssClass="GridRow"></RowStyle>
            </PLs:PLGridView>
        </div>
    </fieldset>


    <uc3:PrintBoxCtrl runat="server" ID="PrintBoxCtrl1" />
</asp:Content>
