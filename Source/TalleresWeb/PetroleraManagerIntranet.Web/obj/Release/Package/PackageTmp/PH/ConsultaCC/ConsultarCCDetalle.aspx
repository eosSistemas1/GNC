<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarCCDetalle.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ConsultarCCDetalle" %>

<%@ Register Src="~/PH/UserControls/ConsultaCilindrosPH.ascx" TagPrefix="uc1" TagName="ConsultaCilindrosPH" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main id="central" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h3>Carta Compromiso de Conformidad</h3>
                </div>

                <hr>

                <div class="col-sm-1"><strong>Taller:</strong></div>
                <div class="col-sm-11">
                    <Controls:CboTalleres ID="cboTalleres" runat="server"></Controls:CboTalleres>
                </div>

                <div class="col-sm-12">
                    <br>
                    <p>Señores de <strong>Petrolera ItaloArgentina</strong>.</p>
                    <p>Me dirijo a ustedes en relación con lo dispuesto en el procedimiento de implementación de la REVISIÓN PERIÓDICA DE CILINDROS PARA GNC, Anexo III, Norma NAG-E 444, que forman parte de las normas del ENTE NACIONAL REGULADOR DEL GAS. Al respecto, manifiesto mi conformidad, para el supuesto caso que el cilindro o válvula resultare condenado, con la consiguiente destrucción por la aplicación de la normativa mencionada. Referente a la válvula, eximo de responsabilidad al CENTRO DE REVISIÓN si la misma se dañara o rompiera como consecuencia del desarme. Dicha conformidad se refiere a los siguientes cilindros y válvulas de mi propiedad:</p>
                </div>

                <hr>

                <div class="col-md-12">
                    <div class="col-md-12 no-padding">
                        <uc1:ConsultaCilindrosPH runat="server" ID="ConsultaCilindrosPH" />
                    </div>
                </div>

                <hr>

                <div class="col-sm-6">
                    <div class="col-sm-3 no-padding">
                        <p>Tipo de Documento:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <Controls:CboTiposDocumentos ID="cboTipoDocumento" runat="server" AutomaticLoad="true" css="form-control" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>Nro. de Doc.:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>
                            <input type="text" id="txtNumeroDocumento" class="form-control" runat="server" clientidmode="static" />
                        </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p><strong>Propietario:</strong>&nbsp;</p>
                    </div>
                    <div class="col-sm-9 no-padding">
                        <input type="text" id="txtNombre" class="form-control mg" runat="server" clientidmode="static" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>Domicilio:&nbsp;</p>
                    </div>
                    <div class="col-sm-9 no-padding">
                        <input type="text" id="txtDomicilio" class="form-control mg" runat="server" clientidmode="static" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>Localidad:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <Controls:CboLocalidades ID="cboLocalidad" runat="server" AutomaticLoad="true" css="form-control" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>CP:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>
                            <input type="text" id="txtCodigoPostal" class="form-control mg" runat="server" clientidmode="static" />
                        </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>Teléfono:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>
                            <input type="text" id="txtTelefono" class="form-control" runat="server" clientidmode="static" />
                        </p>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="col-sm-3 no-padding">
                        <p>Dominio:&nbsp;</p>
                    </div>
                    <div class="col-sm-9 no-padding">
                        <input type="text" id="txtDominio" class="form-control mg" runat="server" clientidmode="static" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p><strong>Marca:</strong>&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="txtMarca" class="form-control mg" runat="server" clientidmode="static" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p><strong>Modelo:</strong>&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="txtModelo" class="form-control mg" runat="server" clientidmode="static" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <p>Oblea Nº:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="txtNroOblea" class="form-control nn mg" runat="server" maxlength="20" />
                    </div>
                    <div class="col-sm-3 no-padding">
                        <%--<p>PEC:&nbsp;</p>--%>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <%--<input type="text" name="" id="" class="form-control mg"/>--%>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="col-sm-6"></div>
                    <div class="col-sm-2"><strong>PEC:</strong></div>
                    <div class="col-sm-4">
                        <Controls:CboPEC ID="cboPEC" runat="server" ></Controls:CboPEC>
                    </div>
                </div>
                <hr>

                <div class="col-sm-12">
                    <p>Señor Propietario del/los cilindros</p>
                    <p>Acuso recibo de su Carta de conformidad, con fecha <strong><%# DateTime.Now.ToString("dd/MM/yyyy") %></strong> y de los cilindros que allí se detallan, aceptando llevar a cabo la Revisión Periódica de los mismos y las tareas complementarias por su cuenta y orden.</p>
                </div>

                <hr>

                <div class="col-sm-6 pull-right">
                    <div class="col-sm-6 no-padding">
                        <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                    </div>
                    <div class="col-sm-6 no-padding">
                        <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

    </main>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
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

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBox" />

    <script>

        function ShowPopupImprimir(phID) {

            $("#btnShowPopupImprimir").click();

            var url = "ImprimirPH.aspx?id=" + phID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

        $('#txtDominio').change(function () {
            if ($('#txtDominio').val() == "") return;

            $.ajax({
                url: "/PHService.asmx/GetVehiculoByDominio",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: "{ 'dominio' : '" + $("#txtDominio").val() + "'}",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data != null && data.d != null) {
                        $('#txtMarca').val(data.d.VehiculoMarca);
                        $('#txtModelo').val(data.d.VehiculoModelo);
                    }
                    else {
                        $('#txtMarca').val("");
                        $('#txtModelo').val("");
                    }
                },
                error: function () {
                    $('#txtMarca').val("");
                    $('#txtModelo').val("");
                }
            });

        });

        $('#txtNumeroDocumento').change(function () {

            if ($('#txtNumeroDocumento').val() == "") return;

            var tipoDocumento = $("#<%= cboTipoDocumento.ClientID %> option:selected").val();
            var numeroDocumento = $("#txtNumeroDocumento").val();

            $.ajax({
                url: "/PHService.asmx/GetClienteByDocumento",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: "{ 'tipoDocumento' : '" + tipoDocumento + "', 'numeroDocumento' : '" + numeroDocumento + "'}",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data != null && data.d != null) {
                        $('#txtNombre').val(data.d.ClienteNombreApellido);
                        $('#txtDomicilio').val(data.d.ClienteDomicilio);
                        <%--    $("#<%= cboTipoDocumento.ClientID %>").val(data.d.ClienteLocalidadID);--%>
                        $('#<%= cboLocalidad.ClientID%> option:selected').text(data.d.ClienteLocalidad);
                        $('#txtTelefono').val(data.d.ClienteTelefono);
                    }
                    else {
                        $('#txtNombre').val("");
                        $('#txtDomicilio').val("");
                    }
                },
                error: function () {
                    $('#txtNombre').val("");
                    $('#txtDomicilio').val("");
                }
            });

        });
    </script>

</asp:Content>
