<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="EvaluarValvulas.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ControlCodigos.EvaluarValvulas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnUsuarioID" runat="server" clientmodeid="static" />
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>
                        <asp:Label ID="lblTitulo" runat="server" /></h4>
                </div>
            </div>
            <hr />

            <div>
                <strong>
                    <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" />
                </strong>
            </div>

            <div class="col-sm-12" style="max-height: 400px; overflow: auto;" >
                <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                    AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad"
                    OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay obleas pendientes.</center>">
                    <Columns>
                        <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº OPERACIÓN CRPC" />
                        <asp:BoundField DataField="Taller" HeaderText="TALLER" />
                        <asp:BoundField DataField="Dominio" HeaderText="DOMINIO" />
                        <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="CÓD. HOMOLOGACIÓN" />
                        <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº DE SERIE" />
                        <asp:TemplateField HeaderText="EVALUAR VÁLVULAS">
                            <ItemTemplate>
                                <span class="fa fa-edit"></span>
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

    <!-- Modal Cargar Resultados -->
    <div class="modal fade" id="modalCargarResultados">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Carga Resultado de Válvula</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnIDPhCilindro" />

                    <div class="col-sm-3">Taller:</div>
                    <div class="col-sm-9"><span id="txtTaller" /></div>
                    <br />
                    <div class="col-sm-4">N° Operación:</div>
                    <div class="col-sm-8"><span id="txtNroOperacion" /></div>
                    <br />
                    <div class="col-sm-4">Dominio:</div>
                    <div class="col-sm-8"><span id="txtDominio" /></div>
                    <br />
                    <div class="col-sm-4">Cód. Homologación:</div>
                    <div class="col-sm-8"><span id="txtHomoVal" /></div>
                    <br />
                    <div class="col-sm-4">N° Serie:</div>
                    <div class="col-sm-8"><span id="txtSerieVal" /></div>

                    <hr />

                    <div class="col-sm-6">
                        Rosca Incorrecta:
                    </div>
                    <div class="col-sm-6">
                        <input type="checkbox" id="txtRosca" class="form-control" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-6">
                        Funcionamiento Incorrecto:
                    </div>
                    <div class="col-sm-6">
                        <input type="checkbox" id="txtFunc" class="form-control" />
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-sm-6">
                        Observación:
                    </div>
                    <div class="col-sm-6">
                        <input type="text" id="txtObservacion" class="form-control" maxlength="200" />
                    </div>
                    <div class="clearfix"></div>

                    <br />
                    <br />
                    <div class="col-sm-12">&nbsp;</div>
                    <div class="col-sm-12">
                        <input type="button" id="btnAceptar" class="btn btn-primary btn-block nn" onclick="AceptarEvaluacionValvula();" value="Aceptar" />
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <button type="button" style="display: none;" id="btnShowPopupCargarResultados" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalCargarResultados">
        Cargar Resultados
    </button>

    <script lang="javascript">
        function imprimir(id) {
            $("#btnShowPopupImprimir").click();

            var url = "HojaRutaImprimir.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

        function cargarDatos(id, taller, nroInternoOperacion, dominio, homoVal, serieVal) {
            $("#hdnIDPhCilindro").val(id);

            $("#txtTaller").text(taller);
            $("#txtNroOperacion").text(nroInternoOperacion);
            $("#txtDominio").text(dominio);
            $("#txtHomoVal").text(homoVal);
            $("#txtSerieVal").text(serieVal);

            $("#btnShowPopupCargarResultados").click();
        }

        function AceptarEvaluacionValvula() {
            var id = $("#hdnIDPhCilindro").val();
            //si checkea los valores estan no funcionan ok
            var rosca = ($('#txtRosca').is(':checked')) ? "NO" : "SI";
            var func = ($("#txtFunc").is(':checked')) ? "NO" : "SI";
            var observacion = $("#txtObservacion").val();
            var usuarioID = $("#<%=hdnUsuarioID.ClientID%>").val();

            if (id != "") {
                $.ajax({
                    type: "POST",
                    url: "EvaluarValvulas.aspx/AceptarEvaluacionValvula",
                    data: '{id: "' + id + '", func: "' + func + '", rosca: "' + rosca + '", observacion: "' + observacion + '", idUsuario: "' + usuarioID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#btnShowPopupCargarResultados").click();
                        alert("La Válvula se procesó correctamente.");
                        location.reload();
                    },
                    failure: function (response) {
                        alert("Ha ocurrido un error. No se pueden actualizar los datos.");
                    }
                });
            }
            else {
                alert("El trámite no es correcto. No se pueden actualizar los datos.");
                return false;
            }
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>
</asp:Content>
