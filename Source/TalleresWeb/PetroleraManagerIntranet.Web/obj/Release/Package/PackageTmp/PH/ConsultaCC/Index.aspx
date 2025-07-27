<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Index" %>


<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main id="central" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>CONSULTAR CARTA COMPROMISO DE CONFORMIDAD</h4>
                </div>
                <hr>
            </div>

            <link href="../../css/messageButton.css" rel="stylesheet" />

            <div class="col-sm-3">
                <button id="Button1" onclick="window.location.href='CilindrosenProceso.aspx';" type="button" class="btn btn-primary btn-sm"><i class="fa fa-circle" aria-hidden="true"></i>CILINDROS EN PROCESO</button>
            </div>
            <div class="col-sm-3">
                <div class="notifications">
                    <div class="new-message">
                        <asp:Label ID="lblVerificarCodigos" Text="" runat="server" />
                    </div>
                    <div class="messages">
                        <button id="Button2" onclick="window.location.href='VerificarCodigos.aspx';" type="button" class="btn btn-primary btn-sm"><i class="fa fa-barcode" aria-hidden="true"></i>VERIFICAR CÓDIGOS</button>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <button id="Button5" onclick="window.location.href='ConsultaPorEstado.aspx';" type="button" class="btn btn-primary btn-sm"><i class="fa fa-external-link" aria-hidden="true"></i>CONSULTA POR ESTADO</button>
            </div>
            <div class="col-sm-3">
                <button id="Button4" onclick="window.location.href='GenerarExcel.aspx';" type="button" class="btn btn-primary btn-sm"><i class="fa fa-external-link" aria-hidden="true"></i>GENERAR EXCEL</button>
            </div>

            <hr />

            <div id="contenido">
                <div class="row">
                    <br />
                    <div class="col-sm-12">
                        <h4>
                            <asp:Label ID="lblTitulo" runat="server" Text="Despacho de trámites y mercadería"></asp:Label></h4>
                    </div>

                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblNorte" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Norte" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Norte</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblSur" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Sur" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Sur</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblEste" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Este" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Este</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblOeste" Text=" Oeste" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Oeste" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Oeste</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblComisionista" Text=" Comisionista" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Comisionista" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Comisionista</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2"></div>


                    <div class="col-sm-12">
                        <br>
                        <asp:Repeater ID="repeaterDespacho" runat="server" OnItemDataBound="repeaterDespacho_ItemDataBound">
                            <ItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="headingOne">
                                        <asp:HiddenField ID="hdnTallerID" runat="server" Value='<%# Eval("ID")%>' />
                                        <h4 class="panel-title" data-toggle="collapse" data-target='<%# "#"+ Eval("ID") %>'>
                                            <a href="#"><strong><%# Eval("Descripcion")%></strong>
                                                <i class="more-less glyphicon glyphicon-plus"></i>
                                            </a>
                                        </h4>
                                    </div>

                                    <div id='<%# Eval("ID")%>' class="collapse">
                                        <asp:GridView ID="grdTramites" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" DataKeyNames="ID, EstadoPHID"
                                            OnRowDataBound="grdTramites_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="FECHA" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                                <asp:BoundField HeaderText="CLIENTE" DataField="Cliente" />
                                                <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" />
                                                <asp:BoundField HeaderText="N° OBLEA" DataField="NroObleaHabilitante" />
                                                <asp:BoundField HeaderText="SERIE CILINDRO" DataField="NroSerieCilindro" />
                                                <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                                                    <ItemTemplate>
                                                        <span class="fa fa-circle"></span><%# Eval("EstadoPH")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IMPRIMIR" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <a onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("PHID") ) %>' class="btn btn-block btn-lg"><i class="fa fa-file-text-o fa-2x"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONSULTAR" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <a onclick='<%# String.Format("window.location=\"ConsultarCCDetalle.aspx?id={0}&e={1}&m={2}\"", Eval("PHID"), Eval("EstadoPHID"), true  ) %>' class="btn btn-block btn-lg"><i class="fa fa-search fa-2x"></i></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <br>
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
                            <h4 class="modal-title">Imprimir</h4>
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

            <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#modalImprimir">
                Imprimir
            </button>

            <script type="text/javascript">

                function ShowPopupImprimir(id) {

                    $("#btnShowPopupImprimir").click();

                    var url = "../ImprimirPH.aspx?id=" + id;

                    var $iframe = $("#frameImprimir");
                    if ($iframe.length) {
                        $iframe.attr('src', url);
                        return false;
                    }
                }

                function RedireccionarConsultar(id) {
                    window.location = '../IngresarCC.aspx?id=' + id;
                }
            </script>

            <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
        </div>
    </main>
</asp:Content>
