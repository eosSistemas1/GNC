<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reguladores.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Reguladores" %>

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
            <div class="col-sm-12">
                <h4>REGULADORES</h4>
            </div>
            <hr />
        </div>

        <div class="no-padding" style="float: right" id="NuevoRegulador" runat="server">
            <a href="Reguladores.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
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
                                <td align="center"><a href="Reguladores.aspx?a=C&id=<%#Eval("ID")%>"><i class="fa fa-search"></i></a></td>
                                <td align="center"><a href="Reguladores.aspx?a=M&id=<%#Eval("ID")%>"><i class="fa fa-pencil-square-o"></i></a></td>
                                <td align="center"><a href="Reguladores.aspx?a=B&id=<%#Eval("ID")%>" onclick="return confirm ('Desea eliminar el item seleccionado?');"><i class="fa fa-eraser"></i></a></td>
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

                    <div class="col-sm-3">
                        <p>Cod. Homologacion:</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="CodigoHomologacionRegulador" maxlength="4" runat="server" class="form-control mg" required="required" />
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>
                <div class="row">

                    <div class="col-sm-3">
                        <p>Marca:&nbsp;</p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <Controls:CboMarcasRegulador ID="cboMarcasRegulador" runat="server" CssClass="form-control mg"></Controls:CboMarcasRegulador>

                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>

                <div class="row">
                    <div class="col-sm-3">
                        <p>Modelo: </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="Modelo" runat="server" class="form-control mg">
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>

                <div class="row">
                    <div class="col-sm-3">
                        <p>Matr&iacute;cula OC: </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="MatriculaOC" runat="server" class="form-control mg">
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>

                <div class="row">
                    <div class="col-sm-3">
                        <p>Caudal: </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="txtCaudal" runat="server" class="form-control mg" onkeypress="return filterFloat(event,this);" />
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <p>Tipo: </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="Tipo" runat="server" class="form-control mg">
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <p>Etapa: </p>
                    </div>
                    <div class="col-sm-3 no-padding">
                        <input type="text" id="txtEtrapas" runat="server" class="form-control mg">
                    </div>
                    <div class="col-sm-6 no-padding"></div>
                </div>

            </div>

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="Reguladores.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
                </div>
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
    </div>

    <script type="text/javascript">

        function filterFloat(evt, input) {

            // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43

            var key = window.Event ? evt.which : evt.keyCode;

            var chark = String.fromCharCode(key);

            var tempValue = input.value + chark;

            if (key >= 48 && key <= 57) {

                if (filter(tempValue) === false) {

                    return false;

                } else {

                    return true;

                }

            } else {

                if (key == 8 || key == 13 || key == 0) {

                    return true;

                } else if (key == 44) {

                    if (filter(tempValue) === false) {

                        return false;

                    } else {

                        return true;

                    }

                } else {

                    return false;

                }

            }

        }

        function filter(__val__) {

            var preg = /^([0-9]+\,?[0-9]{0,2})$/;

            if (preg.test(__val__) === true) {

                return true;

            } else {

                return false;

            }
        }

    </script>

</asp:Content>
