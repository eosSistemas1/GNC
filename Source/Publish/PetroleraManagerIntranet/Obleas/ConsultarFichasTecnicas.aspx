<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarFichasTecnicas.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ConsultarFichasTecnicas" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/messageButton.css" rel="stylesheet" />
    <link href="../css/despacho.css" rel="stylesheet" />

    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>
                    <asp:Label ID="lblTituloPagina" Text="" runat="server" />
                </h4>
            </div>
            <hr />
        </div>

        <div class="row">
            <div class="col-sm-2">
                <p>Desde:</p>
            </div>
            <div class="col-sm-4">
                <input type="date" id="calFechaD" runat="server" />
            </div>
            <div class="col-sm-2">
                <p>Hasta:</p>
            </div>
            <div class="col-sm-4">
                <input type="date" id="calFechaH" runat="server" />
            </div>
        </div>


        <div class="col-sm-2">
            <p>Estado:</p>
        </div>
        <div class="col-sm-5">
            <Controls:CboEstadosFichas ID="cboEstadoFicha" runat="server" OnSelectedIndexChanged="cboEstadoFicha_SelectedIndexChanged" AutoPostBack="true" />
        </div>
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary" aria-label="" name="" id="" title="Imprimir"><i class="fa fa-print" aria-hidden="true"></i>&nbsp Imprimir</button>
        </div>
        <div class="col-sm-2">            
            <button type="button" class="btn btn-primary" aria-label="" name="" id="btnBuscar" runat="server" onserverclick="btnBuscar_ServerClick" title="Buscar"><i class="fa fa-search" aria-hidden="true"></i>&nbsp Buscar</button>
        </div>
        <div class="col-sm-1"></div>

        <hr>

        <div class="col-sm-1"></div>
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
                    <asp:Label ID="lblOeste" Text="" runat="server" />
                </div>
                <div class="messages">
                    <button id="Oeste" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Oeste</button>
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="notifications">
                <div class="new-message">
                    <asp:Label ID="lblComisionista" Text="" runat="server" />
                </div>
                <div class="messages">
                    <button id="Comisionista" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Comisionista</button>
                </div>
            </div>
        </div>
        <div class="col-sm-1"></div>

        <div class="col-sm-12">
            <asp:Repeater ID="repeaterZonas" runat="server" OnItemDataBound="repeaterZonas_ItemDataBound">
                <ItemTemplate>
                    <br>
                    <asp:HiddenField ID="hdnTallerID" runat="server" Value='<%# Eval("ID")%>' />
                    <p><strong><%# Eval("Descripcion")%></strong></p>
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Fecha:</th>
                                <th>Oblea Anterior:</th>
                                <th>Dominio:</th>
                                <th>Cliente:</th>
                                <th>Operación</th>
                                <th>Obs.</th>
                                <th>Fecha alta</th>
                                <th>Oblea Asignada:</th>
                                <th>Vencimiento</th>
                                <th>Consultar</th>
                                <th>Eliminar</th>
                                <th>Reimprimir</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterTaller" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# string.Format("{0:dd/MM/yyyy}", Eval("FechaHabilitacion"))%></td>
                                        <td><%# Eval("NroObleaAnterior")%></td>
                                        <td><%# Eval("Dominio")%></td>
                                        <td><%# Eval("NombreyApellido")%></td>
                                        <td><%# Eval("Operacion")%></td>
                                        <th>
                                            <%--<a href="#" onclick='<%# string.IsNullOrWhiteSpace(Eval("Observacion").ToString())?"":"alert()" %>'><i class="fa fa-commenting"></i></a>--%>
                                        </th>
                                        <td><%# string.Format("{0:dd/MM/yyyy}", Eval("FechaAlta"))%></td>
                                        <td><%# Eval("NroObleaNueva")%></td>
                                        <td></td> 
                                        <th><a onclick="window.location.replace('<%# String.Format("ObleasIngresar.aspx?id={0}", Eval("ID") ) %>');"><i class="fa fa-file-text"></i></a></th>
                                        <th><a onclick="window.location.replace('<%# String.Format("EliminarOblea.aspx?id={0}", Eval("ID") ) %>');"><i class="fa fa-trash"></i></a></th>
                                        <th><a href="#" onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>'><i class="fa fa-print"></i></a></th>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>

                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Imprimir Oblea</h4>
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

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <script type="text/javascript">

        function ShowPopupImprimir(id) {

            $("#btnShowPopupImprimir").click();

            var url = "ImprimirOblea.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
