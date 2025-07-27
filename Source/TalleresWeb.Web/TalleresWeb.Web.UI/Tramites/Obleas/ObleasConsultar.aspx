<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasConsultar.aspx.cs" Inherits="TalleresWeb.Web.UI.Tramites.Obleas.ObleasConsultar" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>Consultar Obleas
                </h4>
            </div>
            <hr />
        </div>

        <div class="row">
            <div class="col-sm-2">
                <p>Desde:</p>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="calFechaD" runat="server" class="form-control" type="Date" MaxLength="10" />                
            </div>
            <div class="col-sm-2">
                <p>Hasta:</p>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="calFechaH" runat="server" class="form-control" type="Date" MaxLength="10" />                
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2">
                <p>Nro. Oblea:</p>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="nroOblea" runat="server" class="form-control" type="number" MaxLength="12" />                
            </div>
            <div class="col-sm-2">
                <p>Dominio:</p>
            </div>
            <div class="col-sm-4">
                <input type="text" id="dominio" runat="server" />
            </div>
        </div>

        <div class="col-sm-9"></div>        
        <div class="col-sm-2">
            <button type="button" class="btn btn-primary" aria-label="" name="" id="btnBuscar" runat="server" onserverclick="btnBuscar_ServerClick" title="Buscar"><i class="fa fa-search" aria-hidden="true"></i>&nbsp Buscar</button>
        </div>
        <div class="col-sm-1"></div>

        <hr />

        <div class="col-sm-12">            
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Fecha</th>
                        <th>Oblea Anterior</th>
                        <th>Dominio</th>
                        <th>Cliente</th>
                        <th>Operación</th>
                        <th>Fecha alta</th>
                        <th>Oblea Asignada</th>
                        <th>Ver / Imprimmir</th>
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
                                <td><%# string.Format("{0:dd/MM/yyyy}", Eval("FechaAlta"))%></td>
                                <td><%# Eval("NroObleaNueva")%></td>
                                <th><a href="#" onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>'><i class="fa fa-print"></i></a></th>
                            </tr>
                        </ItemTemplate>                        
                    </asp:Repeater>
                </tbody>
            </table>            
        </div>
        <div class="col-sm-12 text-center">   
            <b><asp:Label ID="defaultItem" runat="server" Visible="false"/></b>
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

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <script type="text/javascript">
        function ShowPopupImprimir(id) {
            
            $("#btnShowPopupImprimir").click();

            var url = "ObleasImprimir.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
