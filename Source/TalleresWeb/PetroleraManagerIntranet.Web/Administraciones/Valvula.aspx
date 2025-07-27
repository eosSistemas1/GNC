<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Valvula.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Valvula" %>

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
                <h4>V&Aacute;LVULAS</h4>
            </div>
            <div class="col-sm-2 no-padding" style="float: right" id="NuevaValvula" runat="server">
                <a href="Valvula.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
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
                                <td align="center"><a href="Valvula.aspx?a=C&id=<%#Eval("ID")%>"><i class="fa fa-search"></i></a></td>
                                <td align="center"><a href="Valvula.aspx?a=M&id=<%#Eval("ID")%>"><i class="fa fa-pencil-square-o"></i></a></td>
                                <td align="center"><a href="Valvula.aspx?a=B&id=<%#Eval("ID")%>" onclick="return confirm ('Desea eliminar el item seleccionado?');"><i class="fa fa-eraser"></i></a></td>
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
            <div class="row">

                <div class="col-sm-3">
                    <p>Codigo de homologaci&oacute;n de la v&aacute;lvula:</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <input type="text" id="CodHomologacionValvula" maxlength="4" runat="server" class="form-control mg" required="required" />
                </div>
                <div class="col-sm-6 no-padding"></div>
            </div>
            <div class="row">
                <br />
                <div class="col-sm-3">
                    <p>Marca:</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <Controls:CboMarcasValvulas ID="cboMarcasValvulas" runat="server" CssClass="form-control mg"></Controls:CboMarcasValvulas>

                </div>
                <div class="col-sm-6 no-padding"></div>
            </div>
            <br />

            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="Valvula.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
                </div>
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
    </div>
</asp:Content>
