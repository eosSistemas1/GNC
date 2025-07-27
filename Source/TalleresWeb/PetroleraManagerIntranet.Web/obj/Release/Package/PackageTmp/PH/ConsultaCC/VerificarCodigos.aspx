<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="VerificarCodigos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ConsultaCC.VerificarCodigos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnUsuarioID" runat="server" clientmodeid="static" />
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4><asp:Label ID="lblTitulo" runat="server" /></h4>
                </div>
                <hr />
            </div>

            <div>
                <strong>
                    <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
            </div>

            <div class="col-sm-2 pull-right">
                <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
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
                        <asp:TemplateField HeaderText="VERIFICAR CÓDIGOS">
                            <ItemTemplate>
                                <span class="fa fa-edit"></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                    <h4 class="modal-title">Verificar Códigos</h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnIDPhCilindro" />

                    <div class="col-sm-6"></div>
                    <div class="col-sm-3"><b>VISUALIZADO</b></div>
                    <div class="col-sm-3"><b>CONFIRMAR</b></div>

                    <div class="col-sm-6">N° Serie</div>
                    <div class="col-sm-3"><span id="serieCil" /></div>
                    <div class="col-sm-3">
                        <input type="text" id="txtSerieCil" class="form-control" maxlength="20" runat="server" clientidmode="static" /></div>

                    <div class="col-sm-6">Cód. Homologación</div>
                    <div class="col-sm-3"><span id="homoCil" /></div>
                    <div class="col-sm-3">
                        <input type="text" id="txtHomoCil" class="form-control" maxlength="4" runat="server" clientidmode="static" /></div>

                    <div class="col-sm-6">N° Serie</div>
                    <div class="col-sm-3"><span id="serieVal" /></div>
                    <div class="col-sm-3">
                        <input type="text" id="txtSerieVal" class="form-control" maxlength="20" runat="server" clientidmode="static" /></div>

                    <div class="col-sm-6">Cód. Homologación</div>
                    <div class="col-sm-3"><span id="homoVal" /></div>
                    <div class="col-sm-3">
                        <input type="text" id="txtHomoVal" class="form-control" maxlength="4" runat="server" clientidmode="static" /></div>

                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAceptar" class="btn btn-primary btn-block nn" onclick="AceptarCargarResultados();" value="Aceptar" />
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

        function cargarDatos(id, serieCilLeido, homoCilLeido, serieValLeido, homoValLeido,
            serieCilOriginal, homoCilOriginal, serieValOriginal, homoValOriginal) {
            $("#hdnIDPhCilindro").val(id);

            $("#serieCil").text(serieCilLeido);
            $("#homoCil").text(homoCilLeido);
            $("#serieVal").text(serieValLeido);
            $("#homoVal").text(homoValLeido);

            $("#txtSerieCil").val(serieCilOriginal);
            $("#txtHomoCil").val(homoCilOriginal);
            $("#txtSerieVal").val(serieValOriginal);
            $("#txtHomoVal").val(homoValOriginal);

            $("#btnShowPopupCargarResultados").click();
        }

        function AceptarCargarResultados() {
            var id = $("#hdnIDPhCilindro").val();
            var serieCil = $("#txtSerieCil").val();
            var homoCil = $("#txtHomoCil").val();
            var serieVal = $("#txtSerieVal").val();
            var homoVal = $("#txtHomoVal").val();
            var usuarioID = $("#<%=hdnUsuarioID.ClientID%>").val();

            if (id != "") {
                $.ajax({
                    type: "POST",
                    url: "VerificarCodigos.aspx/AceptarVerificarCodigos",
                    data: '{id: "' + id + '", serieCil: "' + serieCil + '", homoCil: "' + homoCil + '", serieVal: "' + serieVal + '", homoVal: "' + homoVal + '", idUsuario: "' + usuarioID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#btnShowPopupCargarResultados").click();
                        alert("Se han verificado los códigos correctamente.");
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
