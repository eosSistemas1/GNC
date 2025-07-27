<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="ReimprimirHojaRuta.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ControlCodigos.ReimprimirHojaRuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>
                        <asp:Label ID="lblTitulo" runat="server" /></h4>
                </div>
                <hr />
            </div>

            <div>
                <strong>
                    <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
            </div>

            <div class="col-sm-12">
                <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                    AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad"
                    OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay obleas pendientes.</center>">
                    <Columns>
                        <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº OPERACIÓN CRPC" />
                        <asp:BoundField DataField="Taller" HeaderText="TALLER" />
                        <asp:BoundField DataField="Dominio" HeaderText="DOMINIO" />
                        <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="CÓD. HOMOLOGACIÓN" />
                        <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº DE SERIE" />
                        <asp:TemplateField HeaderText="IMPRIMIR">
                            <ItemTemplate>
                                <span class="fa fa-print"></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-4 pull-left">
                <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
            </div>
        </div>
    </main>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
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

    <script lang="javascript">
        function imprimir(id) {
            $("#btnShowPopupImprimir").click();

            var url = "HojaRutaImprimir.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
