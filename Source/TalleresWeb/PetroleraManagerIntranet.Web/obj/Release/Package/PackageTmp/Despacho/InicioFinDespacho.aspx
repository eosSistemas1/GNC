<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioFinDespacho.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Despacho.InicioFinDespacho" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=txtFechaDesde.ClientID %>').datepicker({ dateFormat: 'dd/mm/yy' });
            $('#<%=txtFechaHasta.ClientID %>').datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>

    <main id="central" role="main">
        <div id="contenido">
            <div class="row">
                <h4>Procesar Despacho</h4>
            </div>
            <div class="col-sm-1">
                <p>Desde:</p>
            </div>
            <div class="col-sm-2">
                <input type="text" id="txtFechaDesde" runat="server" style="width: 110px;" class="form-control mg" maxlength="10" />
            </div>
            <div class="col-sm-1">
                <p>Hasta:</p>
            </div>
            <div class="col-sm-2">
                <input type="text" id="txtFechaHasta" runat="server" style="width: 110px" class="form-control mg" maxlength="10" />
            </div>

            <asp:UpdatePanel ID="updDespachos" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnMostrarDespachoEntreFechas" />
                </Triggers>
                <ContentTemplate>
                    &nbsp; &nbsp;&nbsp;
            <button id="btnMostrarDespachoEntreFechas" runat="server" onserverclick="btnMostrarDespachoEntreFechas_ServerClick" class="btn-primary">Mostrar</button>
                    <div class="col-sm-12 text-left">

                        <asp:GridView ID="grdDespachos" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="false" DataKeyNames="ID"
                            OnRowDataBound="grdDespachos_RowDataBound"
                            EmptyDataText="<center>No hay despachos para procesar.</center>">
                            <Columns>
                                <asp:BoundField HeaderText="FECHA" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                <asp:BoundField HeaderText="N° DESPACHO" DataField="Descripcion" />
                                <asp:BoundField HeaderText="FLETE" DataField="Flete" />
                                <asp:BoundField HeaderText="ZONA" DataField="Zona" />
                                <asp:BoundField HeaderText="INICIO" DataField="FechaHoraSalida" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                <asp:TemplateField HeaderText="EDITAR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarDespacho" runat="server" class="btn-primary" Text=" Editar" OnClientClick='<%# string.Format("EditarDespacho(\"{0}\");", Eval("ID")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="INICIAR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnIniciarDespacho" runat="server" class="btn-primary" Text=" Iniciar" OnClientClick='<%# string.Format("ShowPopupIniciar(\"{0}\");", Eval("Descripcion")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FINALIZAR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnFinalizarDespacho" runat="server" class="btn-primary" Text=" Finalizar" OnClientClick='<%# string.Format("ShowPopupFinalizar(\"{0}\");", Eval("Descripcion")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RECHAZAR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnRechazarDespacho" runat="server" class="btn-primary" Text=" Rechazar" OnClientClick='<%# string.Format("RechazarDespacho(\"{0}\");", Eval("ID")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ELIMINAR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminarDespacho" runat="server" class="btn-primary" Text=" Eliminar" OnClientClick='<%# string.Format("ShowPopupEliminar(\"{0}\");", Eval("Descripcion")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IMPRIMIR" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnImprimirDespacho" runat="server" class="btn-primary" Text=" Imprimir" OnClientClick='<%# string.Format("ShowPopupImprimir(\"{0}\");", Eval("ID")) %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="despachoAProcesar" runat="server" ClientIDMode="Static" />

                        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </main>

    <div class="modal fade" id="modalIniciar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Iniciar despacho</h4>
                </div>
                <div class="modal-body" style="min-height: 70px;">
                    <div class="col-sm-12 text-center">
                        <asp:Label ID="lblMessageIniciar" runat="server" Text="Se iniciará el despacho: " />
                        <label id="despachoAIniciar" runat="server" clientidmode="Static" style="font-weight: bold;" />
                        <br />
                        <button id="btnIniciar" runat="server" onserverclick="btnIniciar_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-truck" aria-hidden="true"></i>Iniciar</button>
                    </div>
                </div>
                <div class="modal-footer"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modalFinalizar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Finalizar despacho</h4>
                </div>
                <div class="modal-body" style="min-height: 70px;">
                    <div class="col-sm-12 text-center">
                        <asp:Label ID="lblMessageFinalizar" runat="server" Text="Se finalizará el despacho: " />
                        <label type="text" id="despachoAFinalizar" runat="server" clientidmode="Static" style="font-weight: bold;" />
                        <br />
                        <button id="btnFinalizar" runat="server" onserverclick="btnFinalizar_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-truck" aria-hidden="true"></i>Finalizar</button>
                    </div>
                </div>
                <div class="modal-footer"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modalRechazar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Rechazar despacho</h4>
                </div>
                <div class="modal-body" style="min-height: 70px;">
                    <div class="col-sm-12 text-center text-nowrap">
                        <h3>Rechazar entrega:</h3>
                        <asp:Label ID="lblMessageRechazar" runat="server" Text="Despacho a rechazar:" />
                        <label type="text" id="despachoARechazar" runat="server" clientidmode="Static" style="font-weight: bold;" />
                        <br />
                        <span style="white-space: nowrap;">Taller:
                                <Controls:CboTalleres ID="cboTalleresRechazar" runat="server" Width="40%"></Controls:CboTalleres>
                        </span>
                        <br />
                        <button id="btnRechazar" runat="server" onserverclick="btnRechazar_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-remove" aria-hidden="true"></i>Rechazar</button>
                    </div>
                </div>
                <div class="modal-footer"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modalEliminar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Eliminar despacho</h4>
                </div>
                <div class="modal-body" style="min-height: 70px;">
                    <div class="col-sm-12 text-center">
                        <asp:Label ID="Label1" runat="server" Text="Se eliminará el despacho:" />
                        <label type="text" id="despachoAEliminar" runat="server" clientidmode="Static" style="font-weight: bold;" />
                        <br />
                        <button id="btnEliminar" runat="server" onserverclick="btnEliminar_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-remove" aria-hidden="true"></i>Eliminar</button>
                    </div>
                </div>
                <div class="modal-footer"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Imprimir despacho</h4>
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



    <button type="button" style="display: none;" id="btnShowPopupIniciar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalIniciar">
        AceptarDespacho
    </button>

    <button type="button" style="display: none;" id="btnShowPopupFinalizar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalFinalizar">
        FinalizarDespacho
    </button>

    <button type="button" style="display: none;" id="btnShowPopupRechazar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalRechazar">
        RechazarDespacho
    </button>

    <button type="button" style="display: none;" id="btnShowPopupEliminar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalEliminar">
        EliminarDespacho
    </button>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        ImprimirDespacho
    </button>

    <script type="text/javascript">       

        function ShowPopupIniciar(nroDespacho) {
            $("#despachoAIniciar").text(nroDespacho);
            setHidden(nroDespacho);
            $("#btnShowPopupIniciar").click();
        }

        function ShowPopupFinalizar(nroDespacho) {
            $("#despachoAFinalizar").text(nroDespacho);
            setHidden(nroDespacho);
            $("#btnShowPopupFinalizar").click();
        }

        function ShowPopupRechazar(nroDespacho) {
            $("#despachoARechazar").text(nroDespacho);
            setHidden(nroDespacho);
            $("#btnShowPopupRechazar").click();
        }

        function ShowPopupEliminar(nroDespacho) {
            $("#despachoAEliminar").text(nroDespacho);
            setHidden(nroDespacho);
            $("#btnShowPopupEliminar").click();
        }

        function setHidden(nroDespacho) {
            $("#despachoAProcesar").attr("value", nroDespacho);
            $("#despachoAProcesar").val(nroDespacho);
        }

        function ShowPopupImprimir(despachoID) {

            $("#btnShowPopupImprimir").click();

            var url = "ImprimirDespacho.aspx?id=" + despachoID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

        function EditarDespacho(despachoID) {
            var url = "Despacho.aspx?id=" + despachoID;

            location.href = url;
        }

        function RechazarDespacho(despachoID) {
            var url = "RechazarDespacho.aspx?id=" + despachoID;

            location.href = url;
        }
    </script>
</asp:Content>



