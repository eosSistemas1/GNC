<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PEC.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.PEC" %>

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
                        //"url": "PEC.aspx/GetData",
                        "url": '<%= ResolveUrl("PEC.aspx/GetData") %>',
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
                                    data = '<a href="PEC.aspx?a=C&id=' + data + '"><i class="fa fa-search"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<a href="PEC.aspx?a=M&id=' + data + '"><i class="fa fa-pencil-square-o"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "data": "ID",
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = "<a class='borrar' href='PEC.aspx?a=B&id=" + data + "' onclick='return validarEliminar()'><i class='fa fa-eraser'></i></a>";
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
                <h4>PEC</h4>
            </div>
            <hr />
        </div>

        <div class="no-padding" style="float: right" id="NuevoCliente" runat="server">
            <a href="PEC.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
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
                    <p>Matrícula:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtMatricula" runat="server" class="form-control mg" maxlength="4">
                </div>
                <div class="col-sm-3">
                    <p>Domicilio:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtDomicilio" runat="server" class="form-control mg" maxlength="50">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Razón social:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtRazonSocial" runat="server" class="form-control mg" maxlength="50">
                </div>
                <div class="col-sm-3">
                    <p>Localidad:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <Controls:CboLocalidades ID="cboLocalidad" runat="server" CssClass="form-control"></Controls:CboLocalidades>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Teléfono 1:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtTelefono" runat="server" class="form-control" maxlength="30">
                </div>
                <div class="col-sm-3">
                    <p>Teléfono 2:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtTelefono2" runat="server" class="form-control" maxlength="30">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <p>Email 1:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtMail" runat="server" class="form-control mg" maxlength="50">
                </div>
                <div class="col-sm-3">
                    <p>Email 2:&nbsp;</p>
                </div>
                <div class="col-sm-3">
                    <input type="text" name="" id="txtMail2" runat="server" class="form-control mg" maxlength="50">
                </div>
            </div>
            <hr />
            <div id="tallerRT" class="row">
                <div class="col-sm-1">
                    <strong>R.T.:</strong>
                </div>
                <div class="col-sm-3">
                    <asp:HiddenField ID="txtID" runat="server" />
                    <asp:HiddenField ID="txtIDPEC" runat="server" />
                    <Controls:CboRT ID="cboRT" runat="server" CssClass="form-control mg"></Controls:CboRT>
                </div>
                <div class="col-sm-1">
                    <p>Desde:</p>
                </div>
                <div class="col-sm-2">
                    <input type="text" id="calFechaDRT" runat="server" style="width: 110px" class="form-control mg" clientidmode="Static" maxlength="10" />
                </div>
                <div class="col-sm-1">
                    <p>Hasta:</p>
                </div>
                <div class="col-sm-2">
                    <input type="text" id="calFechaHRT" runat="server" style="width: 110px" class="form-control mg" clientidmode="Static" maxlength="10" />
                </div>
                <div class="col-sm-1">
                    <button id="btnGuardarRT" type="button" class="btn btn-primary btn-block nn" aria-label="" name="" runat="server" onserverclick="btnGuardarRT_Click" title="" alt="Guardar RT"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;</button>
                </div>

                <div class="col-sm-12">
                    <div style="height: 120px; overflow: auto;">
                        <asp:GridView ID="grdRT" runat="server" AutoGenerateColumns="False" Width="100%"
                            class="table table-bordered table-hover" DataKeyNames="ID, RTID, PECID"
                            OnRowCommand="grdRT_RowCommand" OnRowDataBound="grdRT_RowDataBound"
                            ShowHeader="true" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                <asp:BoundField HeaderText="Fecha Desde" DataField="FechaDesde" />
                                <asp:BoundField HeaderText="Fecha Hasta" DataField="FechaHasta" />
                                <asp:TemplateField HeaderText="Modificar" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnRun" Text="<i class='fa fa-pencil-square-o'></i>" ToolTip="Modificar"
                                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="modificar" />
                                        </button>                                            
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="PEC.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
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
