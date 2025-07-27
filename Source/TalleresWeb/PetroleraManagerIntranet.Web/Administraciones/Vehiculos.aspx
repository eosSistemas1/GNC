<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Vehiculos" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                        info: "Mostrando _START_ de _END_ de _TOTAL_ vehiculos en total",
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
                        "url": '<%= ResolveUrl("Vehiculos.aspx/GetData") %>',
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
                                    data = '<a href="Vehiculos.aspx?a=C&id=' + data + '"><i class="fa fa-search"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<a href="Vehiculos.aspx?a=M&id=' + data + '"><i class="fa fa-pencil-square-o"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = "<a class='borrar' href='Vehiculos.aspx?a=B&id=" + data + "' onclick='return myFunction()'><i class='fa fa-eraser'></i></a>";
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
            <div class="col-sm-10">
                <h4>VEH&Iacute;CULOS</h4>
            </div>

            <div class="col-sm-2 no-padding" style="float: right" id="NuevoVehiculo" runat="server">
                <a href="Vehiculos.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
            </div>
            <hr />
        </div>

        <input id="txtFiltro" runat="server" type="hidden" name="" class="form-control">



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
                <div class="col-sm-3">Dominio:</div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtDominio" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">Marca:</div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">Modelo:</div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">Año:</div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">Es R.A.:</div>
                <div class="col-sm-3">
                    <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkEsRA" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                <div class="col-sm-6"></div>
            </div>
            <div class="row">
                <div class="col-sm-3">Nro. R.A.:</div>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtNumeroRA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">Es Inyección:</div>
                <div class="col-sm-3">
                    <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkEsInyeccion" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                <div class="col-sm-6"></div>
            </div>


            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="vehiculos.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
                </div>
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    </div>

    <script>
        function myFunction() {

            if (confirm("Desea eliminar este item?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
