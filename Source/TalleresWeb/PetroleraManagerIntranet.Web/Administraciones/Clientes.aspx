<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Clientes" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <style>
        #myTable_filter input {
            border-radius: 5px;
        }

        .dataTables_filter {
            margin-right: 84%;
            width: 250px;
        }
    </style>

    <script>
        $(document).ready(function () {
            $('#myTable').DataTable(
                {
                    "columnDefs": [
                        { "width": "73%", "targets": [0] },
                        { "className": "text-center custom-middle-align", "targets": [1, 2, 3] }
                    ],
                    "bLengthChange": false,
                    "bInfo": false,
                    "destroy": true,
                    "language": {
                        search: "",
                        emptyTable: "No hay datos para mostrar",
                        zeroRecords: "No hay datos para mostrar",
                        processing: "Cargando...",
                        searchPlaceholder: "Ingrese filtro...",
                        paginate: {
                            "first": "Primera",
                            "last": "Ultima",
                            "next": "Siguiente",
                            "previous": "Anterior"
                        }
                    },
                    "processing": true,
                    "serverSide": true,
                    "ajax":
                    {
                        //"url": "Clientes.aspx/GetData",
                        "url": '<%= ResolveUrl("Clientes.aspx/GetData") %>',
                        "contentType": "application/json",
                        "type": "GET",
                        "dataType": "JSON",
                        "data": function (d) {
                            return d;

                        },
                        "dataSrc": function (json) {
                            json.draw = json.d.draw;
                            json.recordsTotal = json.d.recordsTotal;
                            json.recordsFiltered = json.d.recordsFiltered;
                            json.data = json.d.data;
                            var return_data = json;
                            return return_data.data;

                        }
                    },
                    "columns": [
                        { "data": "Descripcion" },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<a href="Clientes.aspx?a=C&id=' + data + '"><i class="fa fa-search"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<a href="Clientes.aspx?a=M&id=' + data + '"><i class="fa fa-pencil-square-o"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = "<a class='borrar' href='Clientes.aspx?a=B&id=" + data + "' onclick='return validarEliminar()'><i class='fa fa-eraser'></i></a>";
                                }
                                return data;
                            }
                        }
                    ]
                });
            $("#myTable_filter input").addClass("form-control");
        });

    </script>

    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>CLIENTES</h4>
            </div>
            <hr />
        </div>

        <div class="no-padding" style="float: right" id="NuevoCliente" runat="server">
            <a href="Clientes.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
        </div>

        <div id="divBuscar" runat="server" class="col-sm-12 no-padding">
            <table class="table table-bordered table-hover" id="myTable">
                <thead>
                    <tr>
                        <th>Descripci&oacute;n:</th>
                        <th>Consultar</th>
                        <th>Modificar</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>

            </table>
        </div>

        <div id="divDatos" runat="server">            

            <p style="font-weight: bold; font-size: 13px; margin-bottom: 0px; margin-top: 2px; padding-bottom: 0px; padding-top: 2px;" id="AccionUsuario" runat="server"></p>            

            <br />

            <asp:HiddenField ID="hddID" runat="server" />
            <div class="row">

                <div class="col-sm-3">
                    <p>Tipo de Documento:&nbsp;</p>
                </div>
                <div class="col-sm-3">

                    <Controls:CboTiposDocumentos ID="tipo_dni" runat="server" CssClass="form-control"></Controls:CboTiposDocumentos>
                </div>
                <div class="col-sm-3">
                    <p>Nro. de Doc.:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtNroDocumento" runat="server" class="form-control">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Nombre y Apellido:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtNombreApellido" runat="server" class="form-control mg">
                </div>
                <div class="col-sm-3">
                    <p>Email:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtMailCliente" runat="server" class="form-control mg">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Domicilio:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtDomicilio" runat="server" class="form-control mg">
                </div>
                <div class="col-sm-1">
                    <p>Número:&nbsp;</p>
                </div>
                <div class="col-sm-2">
                    <input type="text" name="" id="txtNroCalleCliente" runat="server" class="form-control mg">
                </div>
                <div class="col-sm-1">
                    <p>Piso:&nbsp;</p>
                </div>
                <div class="col-sm-2">
                    <input type="text" name="" id="txtPisoDptoCliente" runat="server" class="form-control mg">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Localidad:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <Controls:CboLocalidades ID="cboLocalidad" runat="server" CssClass="form-control"></Controls:CboLocalidades>
                </div>
                <div class="col-sm-3">
                    <p>Teléfono:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtTelefono" runat="server" class="form-control">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Celular:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <p>
                        <input type="text" name="" id="txtCelularCliente" runat="server" class="form-control">
                    </p>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="clientes.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
                </div>
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
    </div>

    <script>
        function validarEliminar() {

            if (confirm("Desea eliminar este item?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
