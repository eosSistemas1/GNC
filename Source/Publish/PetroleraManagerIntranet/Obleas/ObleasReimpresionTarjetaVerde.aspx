<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasReimpresionTarjetaVerde.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ObleasReimpresionTarjetaVerde" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main id="central" role="main" onscroll="document.getElementById('hdnSDetCobcrollTop').value = this.scrollTop;">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>REIMPRIMIR TARJETA VERDE</h4>
                </div>
                <hr>
            </div>
            <div class="row">
                <div class="col-sm-3">Número de oblea (nueva):</div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNroOblea" runat="server" onKeyPress="return soloNumeros(event)" />
                </div>
                <div class="col-sm-3">Dominio:</div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtDominio" runat="server" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-8 text-right"></div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grdObleas" runat="server" AutoGenerateColumns="False" Width="100%"
                        AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                        DataKeyNames="ID" OnRowDataBound="grdObleas_DataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Anterior" DataField="NroObleaAnterior" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Nueva" DataField="NroObleaNueva" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha Habilitación" DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                    <a id="seleccionar" runat="server" clientidmode="static" onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' class="btn btn-block btn-lg" ><i class="fa fa-print "></i></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </main>

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

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
        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }

        function ShowPopupImprimir(phID) {

            $("#btnShowPopupImprimir").click();

            var url = "ObleasImprimirTarjetaVerde.aspx?id=" + phID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }
    </script>
</asp:Content>
