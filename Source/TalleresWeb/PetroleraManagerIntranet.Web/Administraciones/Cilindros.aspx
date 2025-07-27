<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cilindros.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Cilindros" %>

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
                <h4>CILINDROS</h4>
            </div>
            <hr />
        </div>

        <div class="no-padding" style="float: right" id="NuevoCilindro" runat="server">
            <a href="Cilindros.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
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
                                <td align="center"><a href="Cilindros.aspx?a=C&id=<%#Eval("ID")%>"><i class="fa fa-search"></i></a></td>
                                <td align="center"><a href="Cilindros.aspx?a=M&id=<%#Eval("ID")%>"><i class="fa fa-pencil-square-o"></i></a></td>
                                <td align="center"><a href="Cilindros.aspx?a=B&id=<%#Eval("ID")%>" onclick="return confirm ('Desea eliminar el item seleccionado?');"><i class="fa fa-eraser"></i></a></td>
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

                <div class="col-sm-3">
                    <p>Cód. Homologación:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtDescripcion" runat="server" class="form-control mg" maxlength="4">
                </div>
                <div class="col-sm-3">
                    <p>Capacidad:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtCapacidad" runat="server" class="form-control mg" maxlength="3" onkeypress="return soloNumeros(event)">
                </div>

                <div class="col-sm-3">
                    <p>Marca:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <Controls:CboMarcasCilindros ID="cboMarcaCilindro" runat="server" CssClass="form-control mg"></Controls:CboMarcasCilindros>
                </div>
                <div class="col-sm-3">
                    <input type="button" id="btnNuevaMarca" value="..." onclick="habilitarNuevaMarca()" title="Nueva Marca" />
                </div>
                <div class="col-sm-3">
                    <input type="text" id="txtNuevaMarca" runat="server" class="form-control mg" maxlength="50" style="visibility: hidden" placeholder="Ingrese nueva marca">
                </div>


                <div class="col-sm-3">
                    <p>Matrícula:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtMatricula" runat="server" class="form-control mg" maxlength="50">
                </div>
                <div class="col-sm-3">
                    <p>Modelo:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtModelo" runat="server" class="form-control mg" maxlength="50">
                </div>

                <div class="col-sm-3">
                    <p>Espesor Admisible:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtEspesorAdmisible" runat="server" class="form-control mg" maxlength="10">
                </div>
                <div class="col-sm-3">
                    <p>Diámetro:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtDiametro" runat="server" class="form-control mg" maxlength="10">
                </div>

                <div class="col-sm-3">
                    <p>Largo:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtLargo" runat="server" class="form-control mg" maxlength="10">
                </div>
                <div class="col-sm-3">
                    <p>Rotura:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtRotura" runat="server" class="form-control mg" maxlength="50">
                </div>

                <div class="col-sm-3">
                    <p>Material:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtMaterial" runat="server" class="form-control mg" maxlength="10">
                </div>
                <div class="col-sm-3">
                    <p>Norma:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtNorma" runat="server" class="form-control mg" maxlength="50">
                </div>

                <div class="col-sm-3">
                    <p>Fluencia:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtFluencia" runat="server" class="form-control mg" maxlength="10">
                </div>
                <div class="col-sm-3">
                    <p>Dureza:&nbsp;</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="txtDureza" runat="server" class="form-control mg" maxlength="10">
                </div>

            </div>

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="Cilindros.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
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

        function habilitarNuevaMarca() {
            $("#<%=txtNuevaMarca.ClientID%>").attr("style", "visibility: visible");
            $("#<%=txtNuevaMarca.ClientID%>").focus();
        }
    </script>
</asp:Content>
