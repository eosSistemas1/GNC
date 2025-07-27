<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarReguladores.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscCargarReguladores" %>
<%@ Register src="MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>REGULADORES</legend>
            <div style="overflow: auto; min-height: 100px; height: 100px">
                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                    ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDReg, IDRegUni, MSDBRegID" 
                    OnRowCommand="grdDetalle_RowCommand"
                    HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow"
                    CssClass="Grid">
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="5%"></td>
                                <td width="25%" align="left">
                                    <input type="hidden" id="hddID" runat="server" />
                                    <PLs:PLTextBox ID="txtCodigoReg" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                                </td>
                                <td width="25%" align="center">
                                    <PLs:PLTextBox ID="txtSerieReg" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
                                </td>
                                <td width="30%" align="center">
                                    <Controls:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" Width="100%" WidthCbo="99%" />
                                </td>
                                <td width="15%" align="center">
                                    <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Imagenes/iconos/agregar.png"
                                        Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <b><PLs:PLLabel ID="lblItem" runat="server" Text='<%#  Container.DataItemIndex + 1 %>' /></b>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblCodigo" runat="server" Text='<%# Eval("CodigoReg") %>' />
                                <PLs:PLTextBox ID="txtCodigoReg" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <input type="hidden" id="hddID" runat="server" />
                                <PLs:PLTextBox ID="txtCodigoReg" runat="server" WidthTxt="99%" MaxLenghtTxt="4" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLLabel ID="lblSerieReg" runat="server" Text='<%# Eval("NroSerieReg") %>' />
                                <PLs:PLTextBox ID="txtSerieReg" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <PLs:PLTextBox ID="txtSerieReg" runat="server" WidthTxt="99%" MaxLenghtTxt="20" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MSDB" HeaderStyle-Width="30%" FooterStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLHidden ID="lblMSDBRegID" runat="server" Text='<%# Eval("MSDBRegID") %>' />
                                <PLs:PLLabel ID="lblMSDBReg" runat="server" Text='<%# Eval("MSDBReg") %>' />
                                <Controls:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true"  Visible="false" WidthCbo="99%"/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <Controls:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" WidthCbo="99%"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <PLs:PLImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Imagenes/iconos/eliminar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                    AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el regulador?');" />
                                    &nbsp;<PLs:PLImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                    ImageUrl="~/Imagenes/iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    Width="14px" AlternateText="Modificar" />
                                <PLs:PLImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/Imagenes/iconos/agregar.png"
                                    CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
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
        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>