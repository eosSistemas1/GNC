<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCilindrosValvulasPH.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscCargarCilindrosValvulasPH" %>
<%@ Register src="MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>CILINDROS</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDCilUni, IDValUni" OnRowCommand="gdv_RowCommand"
                    HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow"
                    CssClass="Grid">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="2%"></td>
                                <td width="10%" align="left">
                                    <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="96%" MaxLenghtTxt="4" OnOnTextChanged="CodigoCil_TextChanged" AutoPostBack="true" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="96%"  MaxLenghtTxt="20" OnOnTextChanged="SerieCil_TextChanged" AutoPostBack="true" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="txtCapacidad" runat="server" Mask="9999" MaskType="None" WidthTxt="40px"/>
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="txtCilFabMes" runat="server"  Mask="99" MaskType="None" WidthTxt="40px"/>
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="txtCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px" OnOnTextChanged="AnioCil_TextChanged" AutoPostBack="true" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="96%" MaxLenghtTxt="4" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="96%" MaxLenghtTxt="4" />
                                </td>
                                <td width="20%" align="center">
                                    <PLs:PLTextBox ID="txtObservaciones" runat="server" MaxLenghtTxt="100" WidthTxt="99%" />
                                </td>
                                <td width="8%" align="center">
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Imagenes/Iconos/agregar.png"
                                        Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblItem" runat="server" Text='<%#  Container.DataItemIndex + 1 %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCodigo" runat="server" Text='<%# Eval("CodigoCil") %>' />
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" OnOnTextChanged="CodigoCil_TextChanged" AutoPostBack="true" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" MaxLenghtTxt="4" OnOnTextChanged="CodigoCil_TextChanged" AutoPostBack="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieCil" runat="server" Text='<%# Eval("SerieCil") %>' />
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" OnOnTextChanged="SerieCil_TextChanged" AutoPostBack="true" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" MaxLenghtTxt="20" OnOnTextChanged="SerieCil_TextChanged" AutoPostBack="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Capacidad" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilCapacidad" runat="server" Text='<%# Eval("Capacidad") %>' />
                                <PLs:PLTextBoxMasked ID="txtCapacidad" runat="server"  Mask="99" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="txtCapacidad" runat="server"  Mask="99" MaskType="None" WidthTxt="40px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Mes" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabMes" runat="server" Text='<%# Eval("CilFabMes") %>' />
                                <PLs:PLTextBoxMasked ID="txtCilFabMes" runat="server" Mask="99" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="txtCilFabMes" runat="server" Mask="99" MaskType="None" WidthTxt="40px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Año" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabAnio" runat="server" Text='<%# Eval("CilFabAnio") %>' />
                                <PLs:PLTextBoxMasked ID="txtCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px" Visible="false" OnOnTextChanged="AnioCil_TextChanged" AutoPostBack="true" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="txtCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px" OnOnTextChanged="AnioCil_TextChanged" AutoPostBack="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Val." HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCodigoVal" runat="server" Text='<%# Eval("CodigoVal") %>' />
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie Val" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieVal" runat="server" Text='<%# Eval("SerieVal") %>' />
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Oservaciones" HeaderStyle-Width="20%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLTextBox ID="txtObservaciones" runat="server" WidthTxt="99%"  Text='<%# Eval("Observacion") %>' MaxLenghtTxt="15" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtObservaciones" runat="server" WidthTxt="99%" MaxLenghtTxt="15" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el cilindro?');" />
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

        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>