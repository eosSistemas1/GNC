<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarPH.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.AsignarPH" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>IMPRIMIR CERTIFICADOS</h4>
            </div>
            <hr />
        </div>

        <div class="col-sm-12" style="overflow: auto; height: 380px; width: 100%; text-align: center;">
            <asp:GridView ID="grdPH" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID"
                class="table table-bordered table-hover">
                <Columns>
                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Nro. oblea" DataField="ObleaAnterior" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                    <asp:BoundField HeaderText="Cod. Homologación" DataField="CodigoHomologacion" />
                    <asp:BoundField HeaderText="Serie Cilindro" DataField="NumeroSerie" />
                    <asp:BoundField HeaderText="Cliente" DataField="Cliente" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a id="seleccionar" runat="server" clientidmode="static" onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' class="btn btn-block btn-lg"><i class="fa fa-2x fa-print "></i></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRow"></EditRowStyle>
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <RowStyle CssClass="GridRow"></RowStyle>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Text="No hay ph disponibles para ser informadas." CssClass="failureNotification"></asp:Label>
        </div>

        <div class="col-sm-12 pull-right">
            <div class="col-sm-10 no-padding"></div>
            <div class="col-sm-2 no-padding">
                <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
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
                    <h4 class="modal-title">Imprimir Certificado PH</h4>
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

            var url = "ImprimirCertificadoPH.aspx?id=" + id;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
