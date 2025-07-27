<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Talleres.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Administraciones.Talleres" %>

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
                <h4>TALLERES</h4>
            </div>
            <hr />
        </div>

        <div class="no-padding" style="float: right" id="NuevoTaller" runat="server">
            <a href="Talleres.aspx?a=A" class="btn btn-primary btn-block nn"><i class="fa fa-plus" aria-hidden="true"></i>&nbsp Nuevo</a>
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
                                <td align="center"><a href="Talleres.aspx?a=C&id=<%#Eval("ID")%>"><i class="fa fa-search"></i></a></td>
                                <td align="center"><a href="Talleres.aspx?a=M&id=<%#Eval("ID")%>"><i class="fa fa-pencil-square-o"></i></a></td>
                                <td align="center"><a href="Talleres.aspx?a=B&id=<%#Eval("ID")%>" onclick="return confirm ('Desea eliminar el taller seleccionado?');"><i class="fa fa-eraser"></i></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div id="divDatos" runat="server">

            <p style="font-weight: bold; font-size: 13px; margin-bottom: 0px; margin-top: 2px; padding-bottom: 0px; padding-top: 2px;" id="AccionUsuario" runat="server"></p>
            <br />

            <!-- Tab panes -->
            <div style="padding-top: 20px">
                <div id="taller">
                    <div class="row">
                        <div class="col-sm-1">
                            <p>Matricula:</p>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtMatricula" runat="server" class="form-control mg" required maxlength="7" />
                        </div>
                        <div class="col-sm-2">
                            <p>Razon social: </p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtRazonSocial" runat="server" class="form-control mg" required maxlength="50">
                        </div>
                        <div class="col-sm-1">
                            <p>Cuit:</p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtCuit" runat="server" class="form-control mg" required maxlength="11">
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-1">
                            <p>Ciudad:</p>
                        </div>
                        <div class="col-sm-3">
                            <Controls:CboLocalidades ID="cboLocalidad" runat="server" CssClass="form-control mg"></Controls:CboLocalidades>
                        </div>
                        <div class="col-sm-1">
                            <p>Domicilio: </p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtDomicilio" runat="server" class="form-control mg" required maxlength="50">
                        </div>
                        <div class="col-sm-1">
                            <p>Zona:</p>
                        </div>
                        <div class="col-sm-3">
                            <Controls:CboZona ID="cboZona" runat="server" CssClass="form-control mg"></Controls:CboZona>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1">
                            <p>Telefono:</p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtTelefono" runat="server" class="form-control mg" onkeypress="return soloNumeros(event)" maxlength="30">
                        </div>
                        <div class="col-sm-1">
                            <p>Fax:</p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtFax" runat="server" class="form-control mg" required maxlength="30">
                        </div>
                        <div class="col-sm-1">
                            <p>Contacto:</p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtContacto" runat="server" class="form-control mg" required maxlength="10">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1">
                            <p>Mail:</p>
                        </div>
                        <div class="col-sm-3">
                            <input type="text" id="txtMail" runat="server" class="form-control mg" required maxlength="50">
                        </div>
                        <div class="col-sm-1">
                            <p>Venc. contrato:</p>
                        </div>
                        <div class="col-sm-2">
                            <input type="date" id="calFechaVencimientoContrato" runat="server" class="form-control mg" required>
                        </div>
                        <div class="col-sm-1">
                            <p>Nro. Int Operaci&oacute;n:</p>
                        </div>
                        <div class="col-sm-1">
                            <input type="text" id="txtUltimoNroIntOperacion" runat="server" class="form-control mg" required="required" onkeypress="return soloNumeros(event)" maxlength="10">
                        </div>
                        <div class="col-sm-1">
                            <p>Horario de atenci&oacute;n:</p>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtHorarioAtencion" runat="server" class="form-control mg" required maxlength="50">
                        </div>
                    </div>
                </div>
                <hr />
                <div id="tallerRT" class="row">
                    <div class="col-sm-1">
                        <p>R.T.:</p>
                    </div>
                    <div class="col-sm-3">
                        <asp:HiddenField ID="txtIDTalleresRT" runat="server" />
                        <Controls:CboRT ID="cboRT" runat="server" CssClass="form-control mg"></Controls:CboRT>
                    </div>
                    <div class="col-sm-1">
                        <p>Desde:</p>
                    </div>
                    <div class="col-sm-1">
                        <input type="text" id="calFechaDRT" runat="server" style="width: 90px" class="form-control mg" clientidmode="Static" maxlength="10" />
                    </div>
                    <div class="col-sm-1">
                        <p>Hasta:</p>
                    </div>
                    <div class="col-sm-1">
                        <input type="text" id="calFechaHRT" runat="server" style="width: 90px" class="form-control mg" clientidmode="Static" maxlength="10" />
                    </div>
                    <div class="col-sm-2">
                        <asp:CheckBox ID="esPrincipal" Text="Principal" runat="server" TextAlign="Right" />
                    </div>
                    <div class="col-sm-1">
                        <button id="btnGuardarRT" type="button" class="btn btn-primary btn-block nn" aria-label="" name="" runat="server" onserverclick="btnGuardarRT_Click" title="" alt="Guardar RT"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;</button>
                    </div>

                    <div class="col-sm-12">
                        <div style="height: 120px; overflow: auto;">
                            <asp:GridView ID="grdRT" runat="server" AutoGenerateColumns="False" Width="100%"
                                class="table table-bordered table-hover" DataKeyNames="ID, TalleresID, EsRTPrincipal"
                                OnRowCommand="grdRT_RowCommand" OnRowDataBound="grdRT_RowDataBound"
                                ShowHeader="true" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                    <asp:BoundField HeaderText="Fecha Desde" DataField="FechaDesdeRTT" />
                                    <asp:BoundField HeaderText="Fecha Hasta" DataField="FechaHastaRTT" />
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
            </div>
            <br />
            <div class="col-sm-12">
                <div class="col-sm-8 no-padding"></div>
                <div class="col-sm-2 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Guardar</button>
                </div>
                <div class="col-sm-2 no-padding">
                    <a href="Talleres.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
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



