<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargarReguladores.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.CargarReguladores" %>
<%@ Register Src="MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <h3><strong>REGULADORES</strong></h3>
            </div>
            <hr>
        </div>

        <div style="overflow: auto; min-height: 120px; height: 100px">
            <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
                ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDReg, IDRegUni, MSDBRegID"
                OnRowCommand="grdDetalle_RowCommand" class="table table-bordered table-hover">
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td width="25%" align="left">
                                <input type="hidden" id="hddID" runat="server" />
                                <asp:TextBox ID="txtCodigoReg" runat="server" MaxLength="4" CssClass="form-control nn" />
                            </td>
                            <td width="25%" align="center">
                                <asp:TextBox ID="txtSerieReg" runat="server" MaxLength="20" CssClass="form-control nn" />
                            </td>
                            <td width="30%" align="center">
                                <Controls:CboMSDB ID="cboMSDBReg" runat="server" class="form-control nn" />
                            </td>
                            <td width="15%" align="center">
                                <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/img/iconos/agregar.png"
                                    Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="table thead"></HeaderStyle>
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lblItem" runat="server" Text='<%#  Container.DataItemIndex + 1 %>' /></b>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Codigo" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CodigoReg") %>' />
                            <asp:TextBox ID="txtCodigoReg" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="4" CssClass="form-control nn" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <input type="hidden" id="hddID" runat="server" />
                            <asp:TextBox ID="txtCodigoReg" runat="server" WidthTxt="99%" MaxLenghtTxt="4" CssClass="form-control nn" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSerieReg" runat="server" Text='<%# Eval("NroSerieReg") %>' />
                            <asp:TextBox ID="txtSerieReg" runat="server" WidthTxt="99%" Visible="false" MaxLenghtTxt="20" CssClass="form-control nn" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSerieReg" runat="server" WidthTxt="99%" MaxLenghtTxt="20" CssClass="form-control nn"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MSDB" HeaderStyle-Width="30%" FooterStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="lblMSDBRegID" runat="server" Value='<%# Eval("MSDBRegID") %>' />
                            <asp:Label ID="lblMSDBReg" runat="server" Text='<%# Eval("MSDBReg") %>' />
                            <Controls:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" Visible="false" WidthCbo="99%" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <Controls:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/img/iconos/eliminar.png"
                                CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el regulador?');" />
                            &nbsp;<asp:ImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                ImageUrl="~/img/iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                Width="14px" AlternateText="Modificar" />
                            <asp:ImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/img/iconos/agregar.png"
                                CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                                Visible="false" AlternateText="Aceptar" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/img/Iconos/agregar.png"
                                Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
