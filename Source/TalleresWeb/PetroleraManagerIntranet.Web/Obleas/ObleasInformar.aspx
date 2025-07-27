<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasInformar.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ObleasInformar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function Check(parentChk) {
            var elements = document.getElementsByTagName("input");
            for (i = 0; i < elements.length; i++) {
                if (parentChk.checked == true) {
                    if (IsCheckBox(elements[i])) {
                        elements[i].checked = true;
                    }
                }
                else {
                    elements[i].checked = false;
                }
            }

        }

        function IsCheckBox(chk) {
            if (chk.type == 'checkbox') return true;
            else return false;
        }
    </script>

    <main id="central" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4><span>INFORMAR FICHAS TÉCNICAS</span></h4>
                </div>
                <hr />
            </div>

            <div class="row">
                <div class="col-sm-6">
                    <b>Ultima Actualización: </b>
                    <asp:Label ID="lblUltimaActualizacion" runat="server" Text=""></asp:Label>&nbsp;
                </div>
                <div class="col-sm-6">
                    <asp:LinkButton ID="btnVerAarchivos" runat="server" PostBackUrl="VerArchivos.aspx" CssClass="btn btn-info" CausesValidation="false"><i class="glyphicon glyphicon-file"></i> Ver Archivos</asp:LinkButton>
                </div>
            </div>

            <div class="col-sm-12">
                <br>

                <div class="panel panel-default taller">
                    <div class="panel-heading" role="tab">
                        <h4 class="panel-title" data-toggle="collapse" data-target="#divFichasOK">
                            <a href="#">FICHAS PARA INFORMAR<strong><asp:Label ID="lblTituloFichasParaInformar" Text="" runat="server" ForeColor="Green" /></strong>
                                <i class="more-less glyphicon glyphicon-plus"></i>
                            </a>
                        </h4>
                    </div>

                    <div id="divFichasOK" class="collapse">
                        <div style="overflow: auto; height: 300px; width: 100%; text-align: center;">
                            <asp:GridView ID="grdFichasOK" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID"
                                AlternatingRowStyle-CssClass="GridAlternateRow" class="table table-bordered table-hover"
                                EmptyDataText="<b>No hay fichas técnicas para informar</b>" 
                                OnRowCommand="grdFichasOK_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Operación" DataField="Operacion" />
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkTodos" runat="server" onclick="Check(this);" ToolTip="Seleccionar Todos" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" ToolTip="Seleccionar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' title="Imprimir" class="btn-sm"><i class="fa fa-print fa-2x"></i></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <Controls:BtnReEvaluar ID="ibtReevaluar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm ('Desea re-evaluar la ficha seleccionada?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <hr />
                        <div class="col-sm-12 pull-right">
                            <div class="col-sm-10 no-padding"></div>
                            <div class="col-sm-2 no-padding">
                                <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" onclientclick='return confirm("Generar Informe?");' title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>             

                <div class="panel panel-default taller">
                    <div class="panel-heading" role="tab">
                        <h4 class="panel-title" data-toggle="collapse" data-target="#divFichasFaltanElementos">
                            <a href="#">FALTA ALGÚN ELEMENTO DEL TRÁMITE<strong><asp:Label ID="lblTituloFaltanElementos" Text="" runat="server" ForeColor="Red" /></strong>
                                <i class="more-less glyphicon glyphicon-plus"></i>
                            </a>
                        </h4>
                    </div>

                    <div id="divFichasFaltanElementos" class="collapse">
                        <div style="overflow: auto; height: 300px; width: 100%; text-align: center;">
                            <asp:GridView ID="grdFichasFaltanElementos" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID"
                                AlternatingRowStyle-CssClass="GridAlternateRow" class="table table-bordered table-hover"                                
                                EmptyDataText="<b>No hay fichas técnicas con error</b>"
                                OnRowCommand="grdFichasFaltanElementos_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Operación" DataField="Operacion" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' title="Imprimir" class="btn-sm"><i class="fa fa-print fa-2x"></i></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <Controls:BtnReEvaluar ID="ibtReevaluar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm ('Desea re-evaluar la ficha seleccionada?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default taller" style="visibility:hidden">
                    <div class="panel-heading" role="tab">
                        <h4 class="panel-title" data-toggle="collapse" data-target="#divFichasFaltaInformarPH">
                            <a href="#">FALTA INFORMAR PH<strong><asp:Label ID="lblTituloFaltaInformarPH" runat="server" Text="" ForeColor="Red"></asp:Label></strong>
                                <i class="more-less glyphicon glyphicon-plus"></i>
                            </a>
                        </h4>
                    </div>

                    <div id="divFichasFaltaInformarPH" class="collapse">
                        <div style="overflow: auto; height: 300px; width: 100%; text-align: center;">
                            <asp:GridView ID="grdFichasFaltaInformarPH" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID"
                                AlternatingRowStyle-CssClass="GridAlternateRow" class="table table-bordered table-hover"
                                OnRowCommand="grdFichasFaltaInformarPH_RowCommand"
                                EmptyDataText="<b>No hay fichas técnicas con error</b>">
                                <Columns>
                                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Operación" DataField="Operacion" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a onclick='<%# String.Format("ShowPopupImprimir(\"{0}\")", Eval("ID") ) %>' title="Imprimir" class="btn-sm"><i class="fa fa-print fa-2x"></i></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <Controls:BtnReEvaluar ID="ibtReevaluar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm ('Desea re-evaluar la ficha seleccionada?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <br>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-12 pull-right">
                    <div class="col-sm-10 no-padding">
                    </div>
                    <div class="col-sm-2 no-padding">
                        <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_ServerClick" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Imprimir -->
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

        <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
            data-toggle="modal" data-target="#modalImprimir">
            Imprimir
        </button>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    </main>

    <script>

        function ShowPopupImprimir(phID) {

            $("#btnShowPopupImprimir").click();

            var url = "ObleasImprimir.aspx?id=" + phID;

            var $iframe = $("#frameImprimir");
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
        }

    </script>

</asp:Content>
