<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarValvulas.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.uscCargarValvulas" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>VALVULAS</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDVal, IDValUni, MSDBValID, IdObleaCil"
                    OnRowCommand="grdDetalle_RowCommand">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="10%" align="left">
                                    <PLs:PLComboBox ID="cboOrden" runat="server" WidthCbo="99%" />
                                </td>
                                <td width="25%" align="left">
                                    <input type="hidden" id="hddID" runat="server" />
                                    <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" />
                                </td>
                                <td width="25%" align="center">
                                    <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" />
                                </td>
                                <td width="30%" align="center">
                                    <PEARGNC:CboMSDB ID="cboMSDBVal" runat="server" AutomaticLoad="true" WidthCbo="90%" />
                                </td>
                                <td width="10%" align="center">
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Imagenes/Iconos/agregar.png"
                                        Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="GridFixedHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Cilindro" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLComboBox ID="cboOrden" runat="server" Visible="false" WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLComboBox ID="cboOrden" runat="server" WidthCbo="99%"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCodigo" runat="server" Text='<%# Eval("CodigoVal") %>' />
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" Visible="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <input type="hidden" id="hddID" runat="server" />
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieVal" runat="server" Text='<%# Eval("NroSerieVal") %>' />
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" Visible="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MSDB" HeaderStyle-Width="30%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLHidden ID="lblMSDBValID" runat="server" Text='<%# Eval("MSDBValID") %>' />
                                <PLs:PLLabel ID="lblMSDBVal" runat="server" Text='<%# Eval("MSDBVal") %>' />
                                <PEARGNC:CboMSDB ID="cboMSDBVal" runat="server" AutomaticLoad="true" Visible="false" WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboMSDB ID="cboMSDBVal" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="20px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar la Válvula?');" />
                                &nbsp;|&nbsp;<PLs:PLImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                    ImageUrl="~/Imagenes/Iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    Width="20px" AlternateText="Modificar" />
                                <PLs:PLImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/Imagenes/Iconos/agregar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="20px"
                                    Visible="false" AlternateText="Aceptar" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Imagenes/Iconos/agregar.png"
                                    Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>

        <PLs:PLModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancel"
            CacheDynamicResults="false">
        </PLs:PLModalPopupExtender>
        <div style="display: none;">
            <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
        </div>
        <PLs:PLPanel ID="Panel1" runat="server" CssClass="CajaDialogo" Width="100%">
            <fieldset>
                <legend><span class="LabelLegend">
                    <PLs:PLLabel ID="lblTituloMsj" runat="server" Text="Aviso" /></span></legend>
                <table width="100%">
                    <tr>
                        <td width="15%">
                            <PLs:PLImage ID="imgMsg" runat="server" />
                        </td>
                        <td width="85%">
                            <PLs:PLLabel ID="lblMsj" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <center>
                <PLs:PLButton ID="btnOk" runat="server" Text="Imprimir" Visible="false" />
                <PLs:PLButton ID="btnCancel" runat="server" Text="Aceptar" />
            </center>
            <br />
        </PLs:PLPanel>
    </ContentTemplate>
</asp:UpdatePanel>
