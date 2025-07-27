<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadosTramites.aspx.cs"
    Inherits="TalleresWeb.Web.UI.Tramites.EstadosTramites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <main id="central" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h3>Estado de Trámites:</h3>
                </div>
                <hr>
                <div class="col-md-12">


                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">

                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <h4 class="panel-title">
                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                        <i class="more-less glyphicon glyphicon-plus"></i>
                                        <asp:Label ID="lblObleas" Text="Obleas" runat="server" />
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">

                                    <asp:GridView ID="grdObleas" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false" DataKeyNames="ID, IdEstado"
                                        OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay obleas pendientes.</center>">
                                        <Columns>
                                            <asp:BoundField HeaderText="FECHA" DataField="FechaTramite" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                            <asp:BoundField HeaderText="TRÁMITE" DataField="TipoTramite" />
                                            <asp:BoundField HeaderText="OPERACIÓN" DataField="Operacion" />
                                            <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" />
                                            <asp:BoundField HeaderText="OBLEA ANTERIOR" DataField="ObleaAnterior" />
                                            <asp:BoundField HeaderText="OBLEA ASIGNADA" DataField="ObleaAsignada" />
                                            <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                                                <ItemTemplate>
                                                    <span class="fa fa-circle"></span><%# Eval("Estado")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OBSERVACION" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <a onclick="<%# string.Format("MostrarObservacion('{0}');", Eval("Observacion")) %>"><i class="fa fa-comment fa-1x"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMPRIMIR" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <a onclick="<%# string.Format("ImprimirOblea('{0}');", Eval("ID")) %>"><i class="fa fa-print fa-1x"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FOTOS" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <a onclick="<%# string.Format("ImprimirFotos('{0}');", Eval("ID")) %>"><i class="fa fa-picture-o fa-1x"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingTwo">
                                <h4 class="panel-title">
                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        <i class="more-less glyphicon glyphicon-plus"></i>
                                        <asp:Label ID="lblPH" Text="Pruebas hidráulicas" runat="server" />
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                <div class="panel-body">

                                    <asp:GridView ID="grdPH" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false" DataKeyNames="ID, IdEstado"
                                        OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay PH pendientes.</center>">
                                        <Columns>
                                            <asp:BoundField HeaderText="FECHA" DataField="FechaTramite" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                            <asp:BoundField HeaderText="TRÁMITE" DataField="TipoTramite" />
                                            <asp:BoundField HeaderText="OPERACIÓN" DataField="Operacion" />
                                            <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" />
                                            <asp:BoundField HeaderText="OBLEA ANTERIOR" DataField="ObleaAnterior" />
                                            <asp:BoundField HeaderText="OBLEA ASIGNADA" DataField="ObleaAsignada" />
                                            <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                                                <ItemTemplate>
                                                    <span class="fa fa-circle"></span><%# Eval("Estado")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMPRIMIR" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <a onclick="<%# string.Format("ImprimirPH('{0}');", Eval("ID")) %>"><i class="fa fa-print fa-2x"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingThree">
                                <h4 class="panel-title">
                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                        <i class="more-less glyphicon glyphicon-plus"></i>
                                        <asp:Label ID="lblPedidos" Text="Pedidos de Mercadería" runat="server" />
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                <div class="panel-body">

                                    <asp:GridView ID="grdPedido" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false" DataKeyNames="ID, IdEstado"
                                        OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay pedidos pendientes.</center>">
                                        <Columns>
                                            <asp:BoundField HeaderText="FECHA" DataField="FechaTramite" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                            <asp:BoundField HeaderText="TRÁMITE" DataField="TipoTramite" />
                                            <asp:BoundField HeaderText="OPERACIÓN" DataField="Operacion" />
                                            <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" />
                                            <asp:BoundField HeaderText="OBLEA ANTERIOR" DataField="ObleaAnterior" />
                                            <asp:BoundField HeaderText="OBLEA ASIGNADA" DataField="ObleaAsignada" />
                                            <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                                                <ItemTemplate>
                                                    <span class="fa fa-circle"></span><%# Eval("Estado")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- panel-group -->
                </div>
                <p>&nbsp;</p>
            </div>
        </div>

    </main>

    <!--MODAL ESTADO-->
    <div id="modalEstado" class="modal fade forms" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Estado de trámite</h4>
                </div>
                <div class="modal-body no-padding">
                    <fieldset>
                        <div class="col-sm-3 form-group">Nº oblea anterior:</div>
                        <div class="col-sm-3 form-group"><span>7242543</span></div>
                        <div class="col-sm-3 form-group">Estado de oblea</div>
                        <div class="col-sm-3 form-group estado-verde"><span>Aprobada</span></div>

                        <div class="col-sm-3 form-group">Nro cilindro:</div>
                        <div class="col-sm-3 form-group"><span>6758</span></div>
                        <div class="col-sm-3 form-group">Estado de P.H.1</div>
                        <div class="col-sm-3 form-group estado-verde"><span>Realizado</span></div>

                        <div class="col-sm-3 form-group">Nro cilindro:</div>
                        <div class="col-sm-3 form-group"><span>5683</span></div>
                        <div class="col-sm-3 form-group">Estado de P.H.2</div>
                        <div class="col-sm-3 form-group estado-amarillo"><span>En proceso</span></div>
                    </fieldset>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="">Aceptar</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Imprimir:</h4>
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

    <!-- Modal Observacion -->
    <div class="modal fade" id="modalObservacion">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Observación:</h4>
                </div>
                <div class="modal-body">
                    <label id="lblObservacion" />
                </div>
                <div class="modal-footer"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <button type="button" style="display: none;" id="btnShowPopupObservacion" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalObservacion">
        Observacion
    </button>

    <script type="text/javascript">
        function MostrarObservacion(observacion) {
            if (observacion == "") return;

            $("#lblObservacion").text(observacion);
            $("#btnShowPopupObservacion").click();
        }

        function ImprimirOblea(ID) {
            var url = "Obleas/ObleasImprimir.aspx?id=" + ID + "&fotos=false";
            Imprimir(url);
        }

        function ImprimirPH(ID) {
            var url = "PH/ImprimirPH.aspx?id=" + ID;
            Imprimir(url);
        }

        function ImprimirFotos(ID) {
            var url = "Obleas/FotosImprimir.aspx?id=" + ID;
            Imprimir(url);
        }

        function Imprimir(url) {
            $("#btnShowPopupImprimir").click();

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
            return false;
        }
    </script>
</asp:Content>
