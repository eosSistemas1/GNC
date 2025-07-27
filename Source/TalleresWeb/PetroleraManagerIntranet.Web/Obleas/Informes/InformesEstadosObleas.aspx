<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformesEstadosObleas.aspx.cs" Inherits="PetroleraManager.Web.Tramites.Informes.InformesEstadosObleas" %>

<%@ Register Src="~/UserControls/BuscarTaller.ascx" TagName="BuscarTaller" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4><span>CONSULTAR FICHAS TÉCNICAS</span></h4>
            </div>
            <hr />
        </div>

        <div class="row">
            <ul class="nav nav-pills nav-justified">
                <li class="active"><a data-toggle="pill" href="#ficha"><b>FICHA</b></a></li>
                <li><a data-toggle="pill" href="#regulador"><strong>REGULADOR</strong></a></li>
                <li><a data-toggle="pill" href="#cilindro"><strong>CILINDRO</strong></a></li>
                <li><a data-toggle="pill" href="#valvula"><strong>VALVULA</strong></a></li>
            </ul>

            <div class="tab-content">
                <div id="ficha" class="tab-pane fade in active">
                    <br />
                    <div class="col-sm-2"><strong>NRO. OBLEA:</strong></div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtNroObleaNueva" runat="server" ClientIDMode="Static" maxlenghttxt="12" />
                    </div>
                    <div class="col-sm-1"><strong>DOMINIO:</strong></div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtDominioVehiculo" runat="server" ClientIDMode="Static" maxlenghttxt="7" />
                    </div>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2"><strong>NRO. DOCUMENTO:</strong></div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtNroDocumento" runat="server" ClientIDMode="Static" maxlenghttxt="7" />
                    </div>
                </div>
                <div id="regulador" class="tab-pane fade">
                    <br />
                    <div class="col-sm-2"><strong>CÓD. HOMOLOGACIÓN:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCodRegulador" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-2"><strong>NRO. SERIE:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtSerieRegulador" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div id="cilindro" class="tab-pane fade">
                    <br />
                    <div class="col-sm-2"><strong>CÓD. HOMOLOGACIÓN:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCodCilindro" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-2"><strong>NRO. SERIE:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtSerieCilindro" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div id="valvula" class="tab-pane fade">
                    <br />
                    <div class="col-sm-2"><strong>CÓD. HOMOLOGACIÓN:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCodValvula" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-2"><strong>NRO. SERIE:</strong></div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtSerieValvula" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-sm-8 text-right"></div>
            <div class="col-sm-2 text-right">
                <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
            </div>
            <div class="col-sm-2 text-right">
                <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server"  onserverclick="btnCancelar_Click" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
            </div>
        </div>

        <div class="row" style="overflow: auto; height: 300px; width: 100%; text-align: center;">
            <asp:GridView ID="grdFichas" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="ID, IdEstadoFicha"
                AlternatingRowStyle-CssClass="GridAlternateRow" class="table table-bordered table-hover"
                OnRowDataBound="grdFichas_RowDataBound" OnRowCommand="grdFichas_RowCommand"
                EmptyDataText="<center>No hay obleas para los filtros ingresados.</center>">
                <Columns>
                    <asp:BoundField HeaderText="Nro Int Op." DataField="NroInternoOpercion" />
                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:BoundField HeaderText="Estado Ficha" DataField="EstadoFicha" />
                    <asp:BoundField HeaderText="Fecha Venc." DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Oblea">
                        <ItemTemplate>
                            <Controls:BtnModificar ID="btnModificar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarjeta">
                        <ItemTemplate>
                            <a id="btnImprimir" runat="server" clientidmode="static" onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' class="btn btn-block btn-lg"><i class="fa fa-print "></i></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRow"></EditRowStyle>
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <RowStyle CssClass="GridRow"></RowStyle>
            </asp:GridView>
        </div>
    </div>

     <uc2:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        Imprimir
    </button>

    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="window.location.href=window.location.href;">
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

    <script type="text/javascript">         
        function ShowPopupImprimir(phID) {

            $("#btnShowPopupImprimir").click();

            var url = "../ObleasImprimirTarjetaVerde.aspx?id=" + phID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>

</asp:Content>
