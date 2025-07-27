<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CilindrosPH.ascx.cs" Inherits="PetroleraManager.Web.UserControls.CilindrosPH" %>
<%@ Register Src="MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
        <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
            class="table table-bordered table-condensed" HeaderStyle-CssClass="text-center"
            ShowHeaderWhenEmpty="true" DataKeyNames="ID"
            OnRowCommand="grdDetalle_RowCommand">
            <EmptyDataTemplate>
                <table border="0" style="width: 100%;">
                    <tr>
                        <td style="width: 11%">
                            <input ID="txtCodigoCil" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" />
                        </td>
                        <td style="width: 11%">
                            <input ID="txtSerieCil" runat="server" Width="99%" MaxLength="20" class="form-control" />
                        </td>
                        <td style="width: 11%">
                            <input id="txtMarcaCil" runat="server" width="99%" maxlength="50" class="form-control" clientidmode="Static" readonly />
                        </td>
                        <td style="width: 11%">
                            <input ID="txtCapacidadCil" runat="server" Width="99%" MaxLength="3" class="form-control" ClientIDMode="Static" readonly />
                        </td>
                        <td style="width: 7%">
                            <input ID="txtMesFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                        </td>
                        <td style="width: 6%">
                            <input ID="txtAnioFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                        </td>
                        <td style="width: 11%">
                            <input ID="txtCodigoVal" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" />
                        </td>
                        <td style="width: 11%">
                            <input ID="txtSerieVal" runat="server" Width="99%" MaxLength="20" class="form-control" />
                        </td>
                        <td style="width: 11%">
                            <input ID="txtObservaciones" runat="server" Width="99%" MaxLength="200" class="form-control" />
                        </td>
                        <td style="width: 4%;" class="text-center">
                            <CONTROLS:BtnAgregar ID="ibtAgregar" runat="server" OnClick="ibtAgregar_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="text-center"></HeaderStyle>
            <Columns>
                <asp:TemplateField HeaderText="Cod. Homolog. Ciliindro" FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoCil" runat="server" Text='<%# Eval("CodigoCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtCodigoCil" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" onkeypress="SearchTextCil();" onchange="BuscarMarca();" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nº Serie Ciliindro" HeaderStyle-Width="12%" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblSerieCil" runat="server" Text='<%# Eval("SerieCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtSerieCil" runat="server" Width="99%" MaxLength="20" class="form-control" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marca" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblMarcaCil" runat="server" Text='<%# Eval("MarcaCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtMarcaCil" runat="server" Width="99%" MaxLength="50" class="form-control" ClientIDMode="Static" ReadOnly />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Capacidad" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblCapacidadCil" runat="server" Text='<%# Eval("CapacidadCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtCapacidadCil" runat="server" Width="99%" MaxLength="3" class="form-control" ClientIDMode="Static" ReadOnly/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mes Fabricación" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblMesFabricacionCil" runat="server" Text='<%# Eval("MesFabricacionCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtMesFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Año Fabricación" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblAnioFabricacionCil" runat="server" Text='<%# Eval("AnioFabricacionCil") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtAnioFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cod. Homolog. Válvula" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoVal" runat="server" Text='<%# Eval("CodigoVal") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtCodigoVal" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" onkeypress="SearchTextVal();" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nº Serie Válvula" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblSerieVal" runat="server" Text='<%# Eval("SerieVal") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtSerieVal" runat="server" Width="99%" MaxLength="20" class="form-control" ClientIDMode="Static" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Observaciones" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblObservaciones" runat="server" Text='<%# Eval("Observaciones") %>' CssClass="spanClass" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <input ID="txtObservaciones" runat="server" Width="99%" MaxLength="200" class="form-control" ClientIDMode="Static" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="eliminar" ImageUrl="~/Images/iconos/eliminar.png"
                            CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" Width="14px"
                            AlternateText="Eliminar" OnClientClick="return confirm('Desea eliminar el cilindro?');" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="ibtAgregar" runat="server" ImageUrl="~/Images/Iconos/agregar.png"
                            Width="14px" OnClick="ibtAgregar_Click" CausesValidation="false" />
                    </FooterTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        
        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script>

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        if (key == 0) return true;
        return (key >= 48 && key <= 57)
    }

    $("#txtCodigoCil").change(function () {

        BuscarMarca();
    });

    function BuscarMarca() {
        if ($("#txtCodigoCil").val().length != 4) return;

        $.ajax({
            url: "../../WebService1.asmx/GetCilindroByCodigoHomologacion",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{'txt' : '" + $("#txtCodigoCil").val() + "'}",
            dataFilter: function (data) { return data; },
            success: function (data) {
                var datos = data.d.split('|');
                $("#txtMarcaCil").val(datos[0]);
                $("#txtCapacidadCil").val(datos[1]);
                $("#txtSerieCil").focus();
            },
            error: function (result) {
                $("#txtMarcaCil").val("");
                $("#txtCapacidadCil").val("");
            }
        });
    }

    $(document).ready(function () {
        SearchTextCil();
        SearchTextVal();
    });
    function SearchTextCil() {
        $("#txtCodigoCil").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "../../WebService1.asmx/GetCilindrosCodHomAutoCompleteData",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{ 'txt' : '" + $("#txtCodigoCil").val() + "'}",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item,
                                value: item
                            }
                        }))
                    },
                    error: function (result) {
                        //alert("Error");
                    }
                });
            },
            minLength: 2,
            delay: 1000
        });
    }

    function SearchTextVal() {
        $("#txtCodigoVal").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "../../WebService1.asmx/GetValvulasCodHomAutoCompleteData",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{ 'txt' : '" + $("#txtCodigoVal").val() + "'}",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item,
                                value: item
                            }
                        }))
                    },
                    error: function (result) {
                        //alert("Error");
                    }
                });
            },
            minLength: 2,
            delay: 1000
        });
    }

</script>
