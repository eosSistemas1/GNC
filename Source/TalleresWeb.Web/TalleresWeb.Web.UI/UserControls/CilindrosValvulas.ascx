<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CilindrosValvulas.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscCargarCilindrosValvulas" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <asp:HiddenField ID="hdnID" runat="server" />

        <asp:HiddenField ID="hdnIdObleaCilindro" runat="server" />
        <asp:HiddenField ID="hdnIDCilindro" runat="server" />
        <asp:HiddenField ID="hdnIDCilindroUnidad" runat="server" />

        <asp:HiddenField ID="hdnIdObleaValvula1" runat="server" />
        <asp:HiddenField ID="hdnIDValvula1" runat="server" />
        <asp:HiddenField ID="hdnIDValvula1Unidad" runat="server" />

        <asp:HiddenField ID="hdnIdObleaValvula2" runat="server" />
        <asp:HiddenField ID="hdnIDValvula2" runat="server" />
        <asp:HiddenField ID="hdnIDValvula2Unidad" runat="server" />

        <div class="col-sm-12">
            <div class="col-sm-12 no-padding">
                <table class="table table-bordered table-condensed">
                    <tr>
                        <th colspan="9" class="text-center">CILINDRO</th>
                        <th colspan="3" class="text-center">VALVULA 1</th>
                        <th colspan="3" class="text-center">VALVULA 2</th>
                        <th></th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="text-center">Cod. Homo</th>
                        <th class="text-center">N° Serie</th>
                        <th class="text-center">Fab. Mes</th>
                        <th class="text-center">Fab. Año</th>
                        <th class="text-center">Rev. Mes</th>
                        <th class="text-center">Rev. Año</th>
                        <th class="text-center">CRPC</th>
                        <th class="text-center">MSDB</th>
                        <th class="text-center">Certif. PH</th>

                        <th class="text-center">Cod.Homo</th>
                        <th class="text-center">N° Serie</th>
                        <th class="text-center">MSDB</th>

                        <th class="text-center">Cod.Homo</th>
                        <th class="text-center">N° Serie</th>
                        <th class="text-center">MSDB</th>

                        <th class="text-center">PH?</th>
                        <th style="min-width: 75px;">&nbsp;</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCodigoCil" runat="server" MaxLength="4" Width="40px" class="form-control" ClientIDMode="Static" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieCil" runat="server" MaxLength="20" Width="80px" class="form-control" /></td>
                        <td>
                            <asp:TextBox ID="txtCilFabMes" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" class="form-control" /></td>
                        <td>
                            <asp:TextBox ID="txtCilFabAnio" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" class="form-control" /></td>
                        <td>
                            <asp:TextBox ID="txtCilRevMes" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" class="form-control" /></td>
                        <td>
                            <asp:TextBox ID="txtCilRevAnio" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" class="form-control" /></td>
                        <td>
                            <CONTROLS:CboCRPC ID="cboCilCRPC" runat="server" AutomaticLoad="true" WidthCbo="75px" css="form-control" />
                        </td>
                        <td>
                            <CONTROLS:CboMSDB ID="cboMSDBCil" runat="server" WidthCbo="44px" css="form-control" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNroCertifPH" runat="server" MaxLength="10" Width="80px" class="form-control" /></td>

                        <td>
                            <asp:TextBox ID="txtCodigoVal1" runat="server" MaxLength="4" Width="60px" class="form-control" ClientIDMode="Static" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieVal1" runat="server" MaxLength="20" Width="80px" class="form-control" /></td>
                        <td>
                            <CONTROLS:CboMSDB ID="cboMSDBVal1" runat="server" WidthCbo="44px" css="form-control" />
                        </td>

                        <td>
                            <asp:TextBox ID="txtCodigoVal2" runat="server" MaxLength="4" Width="60px" class="form-control" ClientIDMode="Static" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieVal2" runat="server" MaxLength="20" Width="80px" class="form-control" /></td>
                        <td>
                            <CONTROLS:CboMSDB ID="cboMSDBVal2" runat="server" WidthCbo="44px" css="form-control" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkRealizaPH" runat="server" /></td>
                        <td style="width: 80px;">
                            <CONTROLS:BtnAgregar ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" CausesValidation="false" />
                            <CONTROLS:BtnModificar ID="btnModificar" runat="server" OnClick="btnModificar_Click" CausesValidation="false" Visible="false" />
                            <CONTROLS:BtnCancel ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>

                <hr />
                <asp:GridView ID="gdvCilindrosValvulas" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="true"
                    class="table table-bordered table-condensed" HeaderStyle-CssClass="text-center"                    
                    DataKeyNames="ID, IdObleaCilindro, IDCilindro, IDCilindroUnidad, CRPCCilindroID, MSDBCilindroID, IdObleaValvula1, IDValvula1, IDValvula1Unidad, MSDBValvula1ID, IdObleaValvula2, IDValvula2, IDValvula2Unidad, MSDBValvula2ID"
                    OnRowCommand="gdvCilindrosValvulas_RowCommand" OnRowDataBound="gdvCilindrosValvulas_RowDataBound">
                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CodigoCilindro" HeaderText="Cod. Homo" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="NroSerieCilindro" HeaderText="N° Serie" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CilindroFabMes" HeaderText="Fab. Mes" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CilindroFabAnio" HeaderText="Fab. Año" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CilindroRevMes" HeaderText="Rev. Mes" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CilindroRevAnio" HeaderText="Rev. Año" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CRPCCilindro" HeaderText="CRPC" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="MSDBCilindro" HeaderText="MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="NroCertificadoPH" HeaderText="N° Cert.PH" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CodigoValvula1" HeaderText="V_1 Cod.Homo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="NroSerieValvula1" HeaderText="V_1  N° Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="MSDBValvula1" HeaderText="V_1 MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="CodigoValvula2" HeaderText="V_2 Cod.Homo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="NroSerieValvula2" HeaderText="V_2 N° Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-CssClass="spanClass" DataField="MSDBValvula2" HeaderText="V_2 MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:CheckBoxField ItemStyle-CssClass="spanClass" DataField="realizaPH" HeaderText="PH?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <CONTROLS:BtnModificar ID="btnModificarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                                <CONTROLS:BtnEliminar ID="btnEliminarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

        <script type="text/javascript">
            function soloNumeros(e) {
                var key = window.Event ? e.which : e.keyCode
                if (key == 0) return true;
                return (key >= 48 && key <= 57)
            }
        </script>

        <script type="text/javascript">
            $(document).ready(function () {

                SearchTextCil();
                SearchTextVal1();
                SearchTextVal2();

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

                function SearchTextVal1() {
                    $("#txtCodigoVal1").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: "../../WebService1.asmx/GetValvulasCodHomAutoCompleteData",
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: "{ 'txt' : '" + $("#txtCodigoVal1").val() + "'}",
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

                function SearchTextVal2() {
                    $("#txtCodigoVal2").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: "../../WebService1.asmx/GetValvulasCodHomAutoCompleteData",
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: "{ 'txt' : '" + $("#txtCodigoVal2").val() + "'}",
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
