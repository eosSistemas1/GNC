<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCilindrosValvulasPH.ascx.cs" Inherits="TalleresWeb.Web.UserControls.uscCargarCilindrosValvulasPH" %>
<%@ Register src="MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>CILINDROS</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDCil, IDCilUni" OnRowCommand="gdv_RowCommand"
                    HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow"
                    CssClass="Grid">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="2%">
                                </td>
                                <td width="10%" align="left">
                                    <input type="hidden" id="hddID" runat="server" />
                                    <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="96%" MaxLenghtTxt="4" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="96%"  MaxLenghtTxt="20" />
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="cboCilFabMes" runat="server" Mask="99" MaskType="None" WidthTxt="40px"/>
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="cboCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px"/>
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="cboCilRevMes" runat="server"  Mask="99" MaskType="None" WidthTxt="40px"/>
                                </td>
                                <td width="10%" align="center">
                                    <PLs:PLTextBoxMasked ID="cboCilRevAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px"/>
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
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
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
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <input type="hidden" id="hddID" runat="server" />
                                <PLs:PLTextBox ID="txtCodigoCil" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieCil" runat="server" Text='<%# Eval("NroSerieCil") %>' />
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieCil" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Mes" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabMes" runat="server" Text='<%# Eval("CilFabMes") %>' />
                                <PLs:PLTextBoxMasked ID="cboCilFabMes" runat="server" Mask="99" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="cboCilFabMes" runat="server" Mask="99" MaskType="None" WidthTxt="40px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fab. Año" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilFabAnio" runat="server" Text='<%# Eval("CilFabAnio") %>' />
                                <PLs:PLTextBoxMasked ID="cboCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="cboCilFabAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rev. Mes" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilRevMes" runat="server" Text='<%# Eval("CilRevMes") %>' />
                                <PLs:PLTextBoxMasked ID="cboCilRevMes" runat="server"  Mask="99" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="cboCilRevMes" runat="server"  Mask="99" MaskType="None" WidthTxt="40px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rev. Año" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCilRevAnio" runat="server" Text='<%# Eval("CilRevAnio") %>' />
                                <PLs:PLTextBoxMasked ID="cboCilRevAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px" Visible="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBoxMasked ID="cboCilRevAnio" runat="server" Mask="9999" MaskType="None" WidthTxt="40px"/>
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
                        <asp:TemplateField HeaderText="Certif. PH" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
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
                        <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Images/Iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el cilindro?');" />
                                    <PLs:PLImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                    ImageUrl="~/Images/Iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    Width="14px" AlternateText="Modificar" />
                                <PLs:PLImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/Images/Iconos/agregar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    Visible="false" AlternateText="Aceptar" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                                    Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" HeaderText="Seleccionar">
                            <ItemTemplate>
                                <PLs:PLCheckBox ID="chkSeleccionar" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="ibtAgregar2" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                                    Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>

        <fieldset class="aField">
            <legend>VALVULAS</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdvValvulas" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDVal, IDValUni, MSDBValID, IdObleaCil"
                    OnRowCommand="gdvValvulas_RowCommand" OnRowDataBound="gdvValvulas_RowDataBound" HeaderStyle-CssClass="GridHeader"
                    RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow"
                    CssClass="Grid">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="10%" align="left">
                                    <asp:DropDownList ID="cboOrden" runat="server" WidthCbo="99%" />
                                </td>
                                <td width="20%" align="left">
                                    <input type="hidden" id="hddID" runat="server" />
                                    <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                                </td>
                                <td width="20%" align="center">
                                    <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
                                </td>
                                <td width="30%" align="center">
                                    <PEARGNC:CboMSDB ID="cboMSDBVal" runat="server" AutomaticLoad="true" WidthCbo="90%" />
                                </td>
                                <td width="20%" align="center">
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                                        Width="14px" OnClick="ibtAgregarValvula_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Cilindro" HeaderStyle-Width="10%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboOrden" runat="server" WidthCbo="99%" Enabled="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="cboOrden" runat="server" WidthCbo="99%" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo" HeaderStyle-Width="20%" FooterStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCodigo" runat="server" Text='<%# Eval("CodigoVal") %>' />
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <input type="hidden" id="hddID" runat="server" />
                                <PLs:PLTextBox ID="txtCodigoVal" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="20%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieVal" runat="server" Text='<%# Eval("NroSerieVal") %>' />
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieVal" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
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
                        <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Images/Iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar la Válvula?');" />
                                    &nbsp;<PLs:PLImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                    ImageUrl="~/Images/Iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    Width="14px" AlternateText="Modificar" />
                                <PLs:PLImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/Images/Iconos/agregar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    Visible="false" AlternateText="Aceptar" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                                    Width="14px" OnClick="ibtAgregarValvula_Click" CausesValidation="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>

        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>