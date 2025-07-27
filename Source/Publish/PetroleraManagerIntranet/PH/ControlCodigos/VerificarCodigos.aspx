<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="VerificarCodigos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ControlCodigos.VerificarCodigos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnUsuarioID" runat="server" clientmodeid="static" />
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-6">
                    <h4>
                        <asp:Label ID="lblTitulo" runat="server" />VERIFICAR CÓDIGOS</h4>
                </div>
                <hr />
            </div>

            <div>
                <strong>
                    <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
            </div>

            <div class="col-sm-12">
                <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                    AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad"
                    OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay obleas pendientes.</center>">
                    <Columns>
                        <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº OPERACIÓN CRPC" />
                        <asp:BoundField DataField="Taller" HeaderText="TALLER" />
                        <asp:BoundField DataField="Dominio" HeaderText="DOMINIO" />
                        <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="CÓD. HOMOLOGACIÓN" />
                        <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº DE SERIE" />
                        <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                            <ItemTemplate>
                                <span class="fa fa-circle"></span><%# Eval("Estado")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-4 pull-left">
                <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
            </div>
        </div>
    </main>



    <!-- Modal Aceptar -->
    <div class="modal fade" id="modalAceptar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirmar códigos</h4>

                    <input type="hidden" id="hdnID" runat="server" clientmodeid="static" />
                </div>
                <div class="modal-body">
                    <div class="col-sm-4 no-padding">Nº Operación CRPC</div>
                    <div class="col-sm-8 no-padding">
                        <input type="text" id="txtNroOperacionCRPC" class="form-control nn mg" readonly="true" />
                    </div>

                    <div class="col-sm-4 no-padding">Dominio</div>
                    <div class="col-sm-8 no-padding">
                        <input type="text" id="txtDominio" class="form-control nn mg" readonly="true" />
                    </div>

                    <div class="col-sm-4 no-padding">Taller:</div>
                    <div class="col-sm-8 no-padding">
                        <input type="text" id="txtNombreTaller" class="form-control nn mg" readonly="true" />
                    </div>

                    <hr />

                    <div class="clearfix"></div>

                    <div class="col-sm-4 no-padding">
                        <p>Cód. Homologación Cilindro:</p>
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtCodigoHomologacionCilindro" class="form-control nn mg" readonly="true" tabindex="-1" />
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtCodigoHomologacionCilindroLeido" class="form-control nn mg" maxlength="4" style="visibility: hidden" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-4 no-padding">
                        <p>Nº Serie Cilindro:</p>
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtNroSerieCilindro" class="form-control nn mg" readonly="true" tabindex="-1" />
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtNumeroSerieCilindroLeido" class="form-control nn mg" maxlength="20" style="visibility: hidden" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-4 no-padding">
                        <p>Cód. Homologación Válvula:</p>
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtCodigoHomologacionValvula" class="form-control nn mg" readonly="true" tabindex="-1" />
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtCodigoHomologacionValvulaLeido" class="form-control nn mg" maxlength="4" style="visibility: hidden" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-4 no-padding">
                        <p>Nº Serie Válvula:</p>
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtNroSerieValvula" class="form-control nn mg" readonly="true" tabindex="-1" />
                    </div>
                    <div class="col-sm-4 no-padding">
                        <input type="text" id="txtNumeroSerieValvulaLeido" class="form-control nn mg" maxlength="20" style="visibility: hidden" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-12 no-padding text-center">
                        <label for="chkSolicitaVerificacion">Solicita verificación</label>
                        <input id="chkSolicitaVerificacion" type="checkbox" onchange="SolicitaVerificacion();" />
                    </div>
                    <br />
                    <div class="col-sm-6 no-padding">
                        <button id="btnAceptar" type="button" class="btn btn-primary btn-block nn" aria-label="" title="Confirmar" alt="Confirmar" onclick="return aceptar();"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Enviar</button>
                    </div>
                    <div class="col-sm-6 no-padding">
                        <button type="button" class="btn btn-danger btn-block nn" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">Cerrar</span></button>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Imprimir</h4>
                </div>
                <div class="modal-body">
                    <iframe id="frameImprimir" style="width: 100%; height: 400px;"></iframe>
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <button type="button" style="display: none;" id="btnShowPopupAceptar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalAceptar">
        Imprimir
    </button>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <script lang="javascript">

        function openModal(id,
            nroOperacionCRPC,
            dominio,
            razonSocialTaller,
            serieCilindro,
            homologacionCilindro,
            serieValvula,
            homologacionValvula) {

            $('#<%=hdnID.ClientID %>').val(id);

            $('#txtNroOperacionCRPC').val(nroOperacionCRPC);
            $('#txtDominio').val(dominio);
            $('#txtNombreTaller').val(razonSocialTaller);

            $('#txtCodigoHomologacionCilindro').val(homologacionCilindro);
            $('#txtNroSerieCilindro').val(serieCilindro);

            $('#txtCodigoHomologacionValvula').val(homologacionValvula);
            $('#txtNroSerieValvula').val(serieValvula);

            $("#btnShowPopupAceptar").click();
        }

        function aceptar() {
            var id = $("#<%=hdnID.ClientID%>").val();
            var solicitar = $('#chkSolicitaVerificacion').prop("checked");
            var homoCilindroLeido = $('#txtCodigoHomologacionCilindroLeido').val() || '';
            var serieCilindroLeido = $('#txtNumeroSerieCilindroLeido').val() || '';
            var homoValvulaLeido = $('#txtCodigoHomologacionValvulaLeido').val() || '';
            var serieValvulaLeido = $('#txtNumeroSerieValvulaLeido').val() || '';
            var usuarioID = $("#<%=hdnUsuarioID.ClientID%>").val();

            if (solicitar && homoCilindroLeido == '' && serieCilindroLeido.trim() == ''
                && homoValvulaLeido.trim() == '' && serieValvulaLeido.trim() == '') {
                alert("Debe ingresar al menos un nro. serie o cod. de homologación.");
                return false;
            }


            $.ajax({
                type: "POST",
                url: "VerificarCodigos.aspx/Aceptar",
                data: '{id: "' + id + '", nroSerieCilLeido: "' + serieCilindroLeido + '", codHomoCilLeido: "' + homoCilindroLeido + '", nroSerieValLeido: "' + serieValvulaLeido + '", codHomoValLeido: "' + homoValvulaLeido + '", solicitarRevision: "' + solicitar + '", usuarioID: "' + usuarioID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    OnSuccess(response, solicitar);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });


        }

        function OnSuccess(response, solicitar) {
            $("#btnShowPopupAceptar").click();

            if (!solicitar) {
                imprimir(response.d);
            }
            else {
                location.reload();
            }
        }

        function imprimir(id) {
            $("#btnShowPopupImprimir").click();

            var url = "HojaRutaImprimir.aspx?id=" + id + "&ae=true";

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

        function SolicitaVerificacion() {

            $('#txtCodigoHomologacionCilindroLeido').val("");
            $('#txtNumeroSerieCilindroLeido').val("");
            $('#txtCodigoHomologacionValvulaLeido').val("");
            $('#txtNumeroSerieValvulaLeido').val("");

            var solicitar = $('#chkSolicitaVerificacion').prop("checked");

            if (solicitar) {
                $('#txtCodigoHomologacionCilindroLeido').removeAttr("style");
                $('#txtNumeroSerieCilindroLeido').removeAttr("style");
                $('#txtCodigoHomologacionValvulaLeido').removeAttr("style");
                $('#txtNumeroSerieValvulaLeido').removeAttr("style");
                $('#txtCodigoHomologacionCilindroLeido').focus();
            }
            else {
                $("#txtCodigoHomologacionCilindroLeido").attr("style", "visibility: hidden");
                $("#txtNumeroSerieCilindroLeido").attr("style", "visibility: hidden");
                $("#txtCodigoHomologacionValvulaLeido").attr("style", "visibility: hidden");
                $("#txtNumeroSerieValvulaLeido").attr("style", "visibility: hidden");
            }
        }
    </script>
</asp:Content>
