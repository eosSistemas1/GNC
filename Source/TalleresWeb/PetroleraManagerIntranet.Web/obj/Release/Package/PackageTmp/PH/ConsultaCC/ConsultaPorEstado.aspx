<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="ConsultaPorEstado.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ConsultaCC.ConsultaPorEstado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-6">
                    <h3>
                        <asp:Label ID="lblTitulo" runat="server" />
                    </h3>
                    <br />
                    <p>
                        <strong><asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
                    </p>
                </div>
                <div class="col-sm-2 pull-right">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
                </div>

                <div class="clearfix"></div>

                <div class="col-sm-1"></div> 
                <div class="col-sm-1">Desde:</div>
                <div class="col-sm-2"><input id="fDesde" type="date" runat="server" /></div>
                <div class="col-sm-1">Hasta:</div>
                <div class="col-sm-2"><input id="fHasta" type="date" runat="server" /></div>
                <div class="col-sm-1">Estado:</div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="cboEstados" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboEstados_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <a onserverclick="Recargar" runat="server" class="btn btn-block btn-lg" ><i class="fa fa-refresh fa-2x"></i></a>                   
                </div>                
                

                <div class="col-sm-12">
                    <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                        AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad, IDEstadoPH"
                        EmptyDataText="<center>No hay obleas para el estado seleccionado.</center>">
                        <Columns>
                            <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº OPERACIÓN CRPC" />
                            <asp:BoundField DataField="Taller" HeaderText="TALLER" />
                            <asp:BoundField DataField="Dominio" HeaderText="DOMINIO" />
                            <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="CÓD. HOMOLOGACIÓN" />
                            <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº DE SERIE" />                             
                            <asp:TemplateField HeaderText="VER/IMPRIMIR" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <a onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("IDPH") ) %>' class="btn btn-block btn-lg" ><i class="fa fa-file-text-o fa-2x"></i></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MODIFICAR" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <a onclick='<%# String.Format("window.location=\"ConsultarCCDetalle.aspx?id={0}&e={1}&m={2}\"", Eval("IDPH"), Eval("IDEstadoPH"), false  ) %>' class="btn btn-block btn-lg" ><i class="fa fa-search fa-2x"></i></a>                                                
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </main>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
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
      function ShowPopupImprimir(id) {

            $("#btnShowPopupImprimir").click();

            var url = "../ImprimirPH.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
