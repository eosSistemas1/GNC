<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="CilindrosEnProceso.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ConsultaCC.CilindrosEnProceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnUsuarioID" runat="server" ClientIDMode="static" />
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-6">
                    <h3>
                        <asp:Label ID="lblTitulo" runat="server" /></h3>
                    <br />
                    <p>
                        <strong>
                            <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
                    </p>
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
                            <asp:TemplateField HeaderText="IMPRIMIR">
                                <ItemTemplate>
                                    <span class="fa fa-print"></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ELIMINAR">
                                <ItemTemplate>
                                    <span class="fa fa-eraser"></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CARGAR RESULTADOS">
                                <ItemTemplate>
                                    <span class="fa fa-edit"></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                
            </div>
        </div>
    </main>

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

    <!-- Modal Cargar Resultados -->
    <div class="modal fade" id="modalCargarResultados">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Cargar Resultados</h4>
                </div>
                <div class="modal-body">
                    <div class="col-sm-2">
                        <input type="hidden" id="hdnIDPhCilindro" />
                        Aprobado:
                    </div>
                    <div class="col-sm-2">
                        <input type="checkbox" id="chkResultado" />
                    </div>
                    <div class="col-sm-3">
                        N° Certificado:
                    </div>
                    <div class="col-sm-1">
                        <asp:Label ID="lblCodigoCRPC" Text="" runat="server" Font-Bold="true" /></div>
                    <div class="col-sm-4">
                        <input type="text" id="txtNumeroCertificado" class="form-control" maxlength="6"  onkeypress="return soloNumeros(event);" onblur="padCeros();"  onfocus="this.select();" readonly="true"/>
                    </div>      
                    <br />
                    <div class="col-sm-3">Observaciones:</div>
                    <div class="col-sm-9">
                        <textarea id="txtObservaciones" name="txtObservaciones" rows="5" class="form-control"></textarea>                        
                    </div>
                    <br /><br /><br /><br /><br /><br />
                    <div class="col-sm-12">&nbsp;</div>
                    <div class="col-sm-12">
                        <input type="button" id="btnAceptar" class="btn btn-primary btn-block nn" onclick="AceptarCargarResultados();" value="Aceptar" />
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

            var url = "../ControlCodigos/HojaRutaImprimir.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

        function eliminar(id, idUsuario) {

            if (confirm("Eliminar PH?")) {

                $.ajax({
                    type: "POST",
                    url: "CilindrosEnProceso.aspx/Eliminar",
                    data: '{id: "' + id + '", idUsuario: "' + idUsuario + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });

            }
            else {
                return false;
            }
        }

        function cargarDatos(id, nroCertificado) {
            
            $("#hdnIDPhCilindro").val(id);
            $("#btnShowPopupCargarResultados").click();
            $("#txtNumeroCertificado").val(nroCertificado);
        }

        function AceptarCargarResultados() {

            var id = $("#hdnIDPhCilindro").val();
            var resultado = $('#chkResultado').is(':checked');
            var numeroCertificado = $("#txtNumeroCertificado").val();
            var usuarioID = $("#hdnUsuarioID").val();
            var observaciones = $("#txtObservaciones").val();

            if (numeroCertificado != "") {
                $.ajax({
                    type: "POST",
                    url: "CilindrosEnProceso.aspx/AceptarCargarResultados",
                    data: '{id: "' + id + '", resultado: "' + resultado + '", numeroCertificado: "' + numeroCertificado + '", idUsuario: "' + usuarioID + '", observaciones: "' + observaciones + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#btnShowPopupCargarResultados").click();
                        alert("Se ha cargado el resultado correctamente.");
                        location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            else
            {
                alert("Debe ingresar el número de certificado.")
            }
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }

        function padCeros() {
            debugger;
            var numeroCertificado = $("#txtNumeroCertificado").val();
            numeroCertificado = numeroCertificado.padStart(6, "0");
            $("#txtNumeroCertificado").val(numeroCertificado);                       
        }

    </script>
</asp:Content>
