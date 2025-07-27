<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCilindros.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.uscCargarCilindros" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>CILINDROS</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDCil, IDCilUni" OnRowCommand="grdDetalle_RowCommand">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="2%">
                                </td>
                                <td width="10%" align="left">
                                    <input type="hidden" id="hddID" runat="server" />
                                    <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="96%" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="96%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboMes ID="cboCilFabMes" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboAnio ID="cboCilFabAnio" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboMes ID="cboCilRevMes" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboAnio ID="cboCilRevAnio" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboCRPC ID="cboCilCRPC" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PEARGNC:CboMSDB ID="cboMSDBCil" runat="server" AutomaticLoad="true" Width="90%"
                                        WidthCbo="99%" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtNroCertifPH" runat="server" MaxLenghtTxt="15" WidthTxt="99%" />
                                </td>
                                <td width="8%" align="center">
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Imagenes/Iconos/agregar.png"
                                        Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="GridFixedHeader"></HeaderStyle>
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
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" Visible="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <input type="hidden" id="hddID" runat="server" />
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieCil" runat="server" Text='<%# Eval("NroSerieCil") %>' />
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" Visible="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Mes" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabMes" runat="server" Text='<%# Eval("CilFabMes") %>' />
                                <PEARGNC:CboMes ID="cboCilFabMes" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboMes ID="cboCilFabMes" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Año" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabAnio" runat="server" Text='<%# Eval("CilFabAnio") %>' />
                                <PEARGNC:CboAnio ID="cboCilFabAnio" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboAnio ID="cboCilFabAnio" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rev. Mes" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilRevMes" runat="server" Text='<%# Eval("CilRevMes") %>' />
                                <PEARGNC:CboMes ID="cboCilRevMes" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboMes ID="cboCilRevMes" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rev. Año" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilRevAnio" runat="server" Text='<%# Eval("CilRevAnio") %>' />
                                <PEARGNC:CboAnio ID="cboCilRevAnio" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboAnio ID="cboCilRevAnio" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CRPC" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLHidden ID="lblCRPCCilID" runat="server" Text='<%# Eval("CRPCCilID") %>' />
                                <PLs:PLLabel ID="lblCRPCCil" runat="server" Text='<%# Eval("CRPCCil") %>' />
                                <PEARGNC:CboCRPC ID="cboCilCRPC" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboCRPC ID="cboCilCRPC" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MSDB" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLHidden ID="lblMSDBCilID" runat="server" Text='<%# Eval("MSDBCilID") %>' />
                                <PLs:PLLabel ID="lblMSDBCil" runat="server" Text='<%# Eval("MSDBCil") %>' />
                                <PEARGNC:CboMSDB ID="cboMSDBCil" runat="server" AutomaticLoad="true" Visible="false"
                                    WidthCbo="99%" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PEARGNC:CboMSDB ID="cboMSDBCil" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nro. Cert. PH" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblNroCertifPH" runat="server" Text='<%# Eval("NroCertificadoPH") %>' />
                                <PLs:PLTextBox ID="txtNroCertifPH" runat="server" WidthTxt="99%" Visible="false"
                                    MaxLenghtTxt="15" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtNroCertifPH" runat="server" WidthTxt="99%" MaxLenghtTxt="15" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="20px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el cilindro?');" />
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
