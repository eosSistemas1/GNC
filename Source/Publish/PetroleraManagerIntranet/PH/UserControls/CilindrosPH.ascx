<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CilindrosPH.ascx.cs" Inherits="PetroleraManagerIntranet.Web.PH.UserControls.CilindrosPH" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>


<asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True"
    class="table table-bordered table-condensed" HeaderStyle-CssClass="text-center"
    ShowHeaderWhenEmpty="true" DataKeyNames="ID" OnRowDataBound="gdv_RowDataBound"
    OnRowCommand="grdDetalle_RowCommand">
    <EmptyDataTemplate>
        <table border="0" style="width: 100%;">
            <tr>
                <td style="width: 12%">
                    <asp:TextBox ID="txtCodigoCil" runat="server" Width="98%" MaxLength="4" class="form-control" ClientIDMode="Static" onchange="codigoChanged();" />
                </td>
                <td style="width: 12%">
                    <asp:TextBox ID="txtSerieCil" runat="server" Width="98%" MaxLength="20" class="form-control" />
                </td>
                <td style="width: 12%">
                    <asp:Label ID="txtMarcaCil" runat="server" Width="98%" class="form-control" ClientIDMode="Static" readonly="true" />
                </td>
                <td style="width: 12%">
                    <asp:Label ID="txtCapacidadCil" runat="server" Width="99%" MaxLength="20" class="form-control" readonly="true" ClientIDMode="Static" />
                </td>
                <td style="width: 7%">
                    <asp:TextBox ID="txtMesFabricacionCil" runat="server" Width="70px" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                </td>
                <td style="width: 6%">
                    <asp:TextBox ID="txtAnioFabricacionCil" runat="server" Width="70px" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
                </td>
                <td style="width: 12%">
                    <asp:TextBox ID="txtCodigoVal" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" />
                </td>
                <td style="width: 12%">
                    <asp:TextBox ID="txtSerieVal" runat="server" Width="99%" MaxLength="20" class="form-control" />
                </td>
                <td style="width: 12%">
                    <asp:TextBox ID="txtObservaciones" runat="server" Width="99%" MaxLength="200" class="form-control" />
                </td>
                <td style="width: 2%;" class="text-center">&nbsp;
                </td>
                <td style="width: 2%;" class="text-center">
                    <Controls:BtnAgregar ID="ibtAgregar" runat="server" OnClick="ibtAgregar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <HeaderStyle CssClass="text-center"></HeaderStyle>
    <Columns>
        <asp:TemplateField HeaderText="Cód. Homo. Ciliindro" FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblCodigoCil" runat="server" Text='<%# Eval("CodigoCil") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtCodigoCil" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static"  onchange="codigoChanged();" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Nº Serie Cilindro" HeaderStyle-Width="12%" FooterStyle-HorizontalAlign="Center"
            ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblSerieCil" runat="server" Text='<%# Eval("SerieCil") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtSerieCil" runat="server" Width="99%" MaxLength="20" class="form-control" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Marca" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblMarcaCil" runat="server" Text='<%# Eval("MarcaCil") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="txtMarcaCil" runat="server" Width="98%" class="form-control" ClientIDMode="Static" readonly="true" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Capacidad" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblCapacidadCil" runat="server" Text='<%# Eval("CapacidadCil") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="txtCapacidadCil" runat="server" Width="99%" MaxLength="20" class="form-control" onKeyPress="return soloNumeros(event)" readonly="true" ClientIDMode="Static" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mes Fabricación" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%">
            <ItemTemplate>
                <asp:Label ID="lblMesFabricacionCil" runat="server" Text='<%# Eval("MesFabricacionCil") %>' CssClass="spanClass" />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtMesFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Año Fabricación" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%">
            <ItemTemplate>
                <asp:Label ID="lblAnioFabricacionCil" runat="server" Text='<%# Eval("AnioFabricacionCil") %>' CssClass="spanClass" />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtAnioFabricacionCil" runat="server" Width="99%" class="form-control" MaxLength="2" onKeyPress="return soloNumeros(event)" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cod. Homolog. Válvula" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblCodigoVal" runat="server" Text='<%# Eval("CodigoVal") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtCodigoVal" runat="server" Width="99%" MaxLength="4" class="form-control" ClientIDMode="Static" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Nº Serie Válvula" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblSerieVal" runat="server" Text='<%# Eval("SerieVal") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtSerieVal" runat="server" Width="99%" MaxLength="20" class="form-control" ClientIDMode="Static" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Observaciones" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
            <ItemTemplate>
                <asp:Label ID="lblObservaciones" runat="server" Text='<%# Eval("Observaciones") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtObservaciones" runat="server" Width="99%" MaxLength="200" class="form-control" ClientIDMode="Static" />
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-Width="2%">
            <ItemTemplate>
                <span class="fa fa-edit"></span>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
            <ItemTemplate>

                <Controls:BtnEliminar ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
            </ItemTemplate>
            <FooterTemplate>
                <Controls:BtnAgregar ID="ibtAgregar" runat="server" OnClick="ibtAgregar_Click" CausesValidation="false" />
            </FooterTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

<uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

<!-- Modal Imprimir -->
<div class="modal fade" id="modalModificar">
    <div class="modal-dialog" style="left: 0px !important;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="location.reload();">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title">Modificar</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" id="hdnRowIndex" runat="server" clientidmode="static" />

                <div class="col-sm-12"><b>Cilindro:</b></div>

                <div class="col-sm-3">Cód. Homo:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtCodigoCilMod" maxlength="4" class="form-control" runat="server" clientidmode="static" onchange="codigoModChanged();" />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">N° Serie:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtSerieCilMod" maxlength="20" class="form-control" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">Marca:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtMarcaCilMod" maxlength="50" class="form-control" runat="server" clientidmode="static" readonly />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">Capacidad:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtCapacidadCilMod" maxlength="20" class="form-control" runat="server" clientidmode="static" readonly />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">Mes Fabricación:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtMesFabricacionCilMod" class="form-control" maxlength="2" onkeypress="return soloNumeros(event)" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">Año Fabricación:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtAnioFabricacionCilMod" class="form-control" maxlength="2" onkeypress="return soloNumeros(event)" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>
                <br />
                <div class="col-sm-12"><b>Válvula:</b></div>

                <div class="col-sm-3">Cód. Homo:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtCodigoValMod" maxlength="4" class="form-control" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>

                <div class="col-sm-3">N° Serie:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtSerieValMod" maxlength="20" class="form-control" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>

                <hr />

                <div class="col-sm-3">Observaciones:</div>
                <div class="col-sm-9">
                    <input type="text" id="txtObservacionesMod" maxlength="200" class="form-control" runat="server" clientidmode="static" />
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="modal-footer">
                <input type="button" id="btnAceptar" class="btn btn-primary btn-block nn" runat="server" onclick="CerrarModalModificar();" onserverclick="BtnAceptar_ServerClick" value="Aceptar" />
            </div>
        </div>
    </div>
</div>

<button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
    data-toggle="modal" data-target="#modalModificar">
    Modificar
</button>

<script type="text/javascript">
    $(document).ready(function () {

        SearchTextCil("txtCodigoCil");
        SearchTextVal("txtCodigoVal");

        SearchTextCil("txtCodigoCilMod");
        SearchTextCil("txtCodigoValMod");

    });

    function codigoChanged() {
        var codHomoCil = $("#txtCodigoCil").val();
        
        $.ajax({
            url: "/PHService.asmx/GetCilindroMarcaYCapacidad",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{ 'codigo' : '" + codHomoCil + "'}",
            dataFilter: function (data) { return data; },
            success: function (data) {
                var fields = data.d.split('|');
                $("#txtMarcaCil").text(fields[0]);
                $("#txtCapacidadCil").text(fields[1]);                
            },
            error: function (result) {
                alert("Error");
            }
        });
    }

    function codigoModChanged() {
        var codHomoCil = $("#txtCodigoCilMod").val();        
        $.ajax({
            url: "/PHService.asmx/GetCilindroMarcaYCapacidad",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{ 'codigo' : '" + codHomoCil + "'}",
            dataFilter: function (data) { return data; },
            success: function (data) {
                var fields = data.d.split('|');
                $("#txtMarcaCilMod").val(fields[0]);
                $("#txtCapacidadCilMod").val(fields[1]);                
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
   
    function SearchTextCil(txtID) {

        txtID = "#" + txtID;

        $(txtID).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/PHService.asmx/GetCilindrosCodHomAutoCompleteData",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{ 'txt' : '" + $(txtID).val() + "'}",
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
                        alert("Error");
                    }
                });
            },
            minLength: 2,
            delay: 1000,
        });
    }

    function SearchTextVal(txtID) {
        txtID = "#" + txtID
        $(txtID).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/PHService.asmx/GetValvulasCodHomAutoCompleteData",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: "{ 'txt' : '" + $(txtID).val() + "'}",
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
                        alert("Error");
                    }
                });
            },
            minLength: 2,
            delay: 1000
        });
    }

    function cargarDatos(idx, homoCil, serieCil, marca, capacidad, mesFabricacion, anioFabricacion, homoVal, serieVal, observaciones) {

        $("#hdnRowIndex").val(idx);

        $("#txtCodigoCilMod").val(homoCil);
        $("#txtSerieCilMod").val(serieCil);
        $("#txtMarcaCilMod").val(marca);
        $("#txtCapacidadCilMod").val(capacidad);
        $("#txtMesFabricacionCilMod").val(mesFabricacion);
        $("#txtAnioFabricacionCilMod").val(anioFabricacion);

        $("#txtCodigoValMod").val(homoVal);
        $("#txtSerieValMod").val(serieVal);

        $("#txtObservacionesMod").val(observaciones);

        CerrarModalModificar();
    }

    function CerrarModalModificar() {
        $("#btnShowPopup").click();
    }

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
    }
</script>
