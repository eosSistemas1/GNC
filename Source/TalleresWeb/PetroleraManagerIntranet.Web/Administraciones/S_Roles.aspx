<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="S_Roles.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.S_Roles" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        $(document).ready(function () {
            $('#myTable').DataTable({
                bLengthChange: false,
                bInfo: false,
                destroy: true,
                language: {
                    search: "",
                    emptyTable: "No hay datos para mostrar",
                    zeroRecords: "No hay datos para mostrar",
                    searchPlaceholder: "Ingrese filtro...",
                    paginate: {
                        "first": "Primera",
                        "last": "Ultima",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }
            });
            $("#myTable_filter input").addClass("form-control");
        });

    </script>

    <div id="contenido">
        <div class="row">
            <div class="col-sm-10">
                <h4>ROLES</h4>
            </div>

            <div class="col-sm-2 no-padding" style="float: right" id="NuevoRol" runat="server">
                <a href="S_Roles.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
            </div>
            <hr />
        </div>        

        <div id="divBuscar" runat="server" class="col-sm-12 no-padding" style="">
            <table class="table table-bordered table-hover" id='myTable'>
                <thead>
                    <tr>
                        <th>Descripci&oacute;n:</th>
                        <th>Consultar</th>
                        <th>Modificar</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="tablaDatos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 70%;"><%#Eval("Descripcion")%></td>
                                <td align="center"><a href="S_Roles.aspx?a=C&id=<%#Eval("ID")%>"><i class="fa fa-search"></i></a></td>
                                <td align="center"><a href="S_Roles.aspx?a=M&id=<%#Eval("ID")%>"><i class="fa fa-pencil-square-o"></i></a></td>
                                <td align="center"><a href="S_Roles.aspx?a=B&id=<%#Eval("ID")%>" onclick="return confirm ('Desea eliminar el item seleccionado?');"><i class="fa fa-eraser"></i></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div id="divDatos" runat="server">

            <p style="font-weight: bold; font-size: 13px; margin-bottom: 0px; margin-top: 2px; padding-bottom: 0px; padding-top: 2px;" id="AccionUsuario" runat="server"></p>
            <br />

            <asp:HiddenField ID="hddID" runat="server" />
            <div class="col-sm-12">

                <div class="row">

                    <div class="col-sm-3 no-padding">
                        <input type="hidden" id="IdRol" runat="server" />
                    </div>

                </div>
                <div class="row">

                    <div class="col-sm-2 no-padding">Codigo Rol</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="CodigoRol" runat="server" class="form-control mg" placeholder="Codigo Rol">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2 no-padding">Rol</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="DescRol" runat="server" class="form-control mg" placeholder="Descripcion">
                    </div>
                </div>
                <div class="row">


                    <div class="col-sm-2 no-padding">Hora inicio semana</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraInicioSemanaM" runat="server" maxlength="2" class="form-control mg" placeholder="Hora Inicio Semana M" onkeypress="return soloNumeros(event)">
                    </div>
                    <br />
                </div>
                <div class="row">
                    <div class="col-sm-2 no-padding">Hora Fin de semana</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraFinSemanaM" runat="server" class="form-control mg" maxlength="2" placeholder="Hora Fin Semana M" onkeypress="return soloNumeros(event)">
                    </div>
                    <br />
                </div>
                <div class="row">
                    <div class="col-sm-2 no-padding">Hora Inicio Semana T</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraInicioSemanaT" runat="server" class="form-control mg" maxlength="2" placeholder="Hora Inicio Semana T" onkeypress="return soloNumeros(event)">
                    </div>
                </div>
                <div class="row">
                    <br />

                    <div class="col-sm-2 no-padding">Hora Fin Semana T</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraFinSemanaT" runat="server" class="form-control mg" maxlength="2" placeholder="Hora Fin Semana T" onkeypress="return soloNumeros(event)">
                    </div>
                    <br />
                </div>
                <div class="row">
                    <div class="col-sm-2 no-padding">Hora Inicio Sabado</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraInicioSabado" runat="server" class="form-control mg" maxlength="2" placeholder="Hora Inicio Sabado" onkeypress="return soloNumeros(event)">
                    </div>
                    <br />
                </div>
                <div class="row">
                    <div class="col-sm-2 no-padding">Hora Fin Sabado</div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="HoraFinSabado" runat="server" class="form-control mg" maxlength="2" placeholder="Hora Fin Sabado" onkeypress="return soloNumeros(event)">
                    </div>
                    <br />
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="S_Roles.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
                </div>
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
    </div>

    <script>

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }

    </script>
</asp:Content>
