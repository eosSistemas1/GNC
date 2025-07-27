<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reguladores.ascx.cs" Inherits="PetroleraManager.Web.UserControls.Reguladores" %>
<%@ Register Src="MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
            class="table table-bordered table-condensed" HeaderStyle-CssClass="text-center"
            ShowHeaderWhenEmpty="true" DataKeyNames="ID, IDReg, IDRegUni, MSDBRegID"
            OnRowCommand="grdDetalle_RowCommand">
            <EmptyDataTemplate>
                <table border="0" style="width: 100%;">
                    <tr>
                        <td style="width: 5%;" class="text-center"></td>
                        <td style="width: 24%;" class="text-center">
                            <input type="hidden" id="hddID" runat="server" />
                            <asp:TextBox ID="txtCodigoReg" runat="server" Width="99%" MaxLength="4" class="form-control"  ClientIDMode="Static" />
                        </td>
                        <td style="width: 25%;" class="text-center">
                            <asp:TextBox ID="txtSerieReg" runat="server" Width="99%" MaxLength="20" class="form-control" />
                        </td>
                        <td style="width: 30%;" class="text-center">
                            <CONTROLS:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" Width="100%" WidthCbo="99%" css="form-control" />
                        </td>
                        <td style="width: 15%;" class="text-center">
                            <CONTROLS:BtnAgregar ID="ibtAgregar" runat="server" OnClick="ibtAgregar_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="text-center"></HeaderStyle>
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
                        <asp:TextBox ID="txtCodigoReg" runat="server" Width="99%" Visible="false" MaxLength="4" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input type="hidden" id="hddID" runat="server" />
                        <asp:TextBox ID="txtCodigoReg" runat="server" Width="99%" MaxLength="4" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Serie" HeaderStyle-Width="25%" FooterStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSerieReg" runat="server" Text='<%# Eval("NroSerieReg") %>' />
                        <asp:TextBox ID="txtSerieReg" runat="server" Width="99%" Visible="false" MaxLength="20" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtSerieReg" runat="server" Width="99%" MaxLength="20" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MSDB" HeaderStyle-Width="30%" FooterStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="lblMSDBRegID" runat="server" Value='<%# Eval("MSDBRegID") %>' />
                        <asp:Label ID="lblMSDBReg" runat="server" Text='<%# Eval("MSDBReg").ToString().ToUpper() %>' />
                        <CONTROLS:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" Visible="false" WidthCbo="99%" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <CONTROLS:CboMSDB ID="cboMSDBReg" runat="server" AutomaticLoad="true" WidthCbo="99%" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Images/iconos/eliminar.png"
                            CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                            AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el regulador?');" />
                        &nbsp;
                                <asp:ImageButton ID="btnModificar" runat="server" CommandName="modificar"
                                    ImageUrl="~/Images/iconos/modificar.png" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    Width="14px" AlternateText="Modificar" />
                        <asp:ImageButton ID="btnAceptar" runat="server" CommandName="aceptar" ImageUrl="~/Images/iconos/agregar.png"
                            CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                            Visible="false" AlternateText="Aceptar" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                            Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
      
        <script type="text/javascript">
            $(document).ready(function () {

                SearchText();

                function SearchText() {
                    $("#txtCodigoReg").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: "../../WebService1.asmx/GetReguladoresCodHomAutoCompleteData",
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: "{ 'txt' : '" + $("#txtCodigoReg").val() + "'}",
                                dataFilter: function (data) { return data; },
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item,
                                            value: item
                                        }
                                    }))
                                    //debugger;
                                },
                                error: function (result) {
                                    alert("Error");
                                }
                            });
                        },
                        minLength: 2,
                        delay: 1000
                    });
                }
            });
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
