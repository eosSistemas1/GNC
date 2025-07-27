<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="CargarResultadosEnte.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.CargarResultadosEnte" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>CARGAR RESULTADOS ENTE</h4>
            </div>
            <hr />
        </div>

        <div class="col-sm-12">
            <h4>Informes Pendientes:</h4>
            <div style="max-height: 150px; overflow: auto;">
                <asp:GridView ID="grdInformes" runat="server" AutoGenerateColumns="False" Width="100%"
                    class="table table-bordered table-condensed" DataKeyNames="ID, CantidadObleasBaja"
                    OnRowCommand="grdInformes_RowCommand" OnRowDataBound="grdInformes_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Número" DataField="Numero" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Fecha" DataField="FechaHora" ItemStyle-HorizontalAlign="Center" />
                        <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasEnviadas" ItemStyle-HorizontalAlign="Center" CommandName="enviadas" HeaderText="Enviadas" />
                        <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasAsignadas" ItemStyle-HorizontalAlign="Center" CommandName="asignadas" HeaderText="Asignadas" />
                        <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasRechazadas" ItemStyle-HorizontalAlign="Center" CommandName="rechazadas" HeaderText="Rechazadas" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSeleccionar" ImageUrl="~/img/Iconos/seleccionar.png" runat="server" CommandName="seleccionar" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Cargar Archivos" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" ImageUrl="~/img/Iconos/eliminar.png" runat="server" CommandName="eliminar" OnClientClick='return confirm("Confirma eliminar?");' CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnCorrecta" ImageUrl="~/img/Iconos/correcta.png" runat="server" CommandName="cerrar" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Cerrar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <hr />

        <div class="col-sm-12">
            <asp:Panel ID="panelArchivos" runat="server" Visible="false">
                <div class="col-sm-12">
                    <h4>
                        <asp:Label ID="lblTitulo" Text="text" runat="server" /></h4>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Arcvhivo ente <span style="font-weight: bold; color: green">OK</span></legend>
                                    <asp:FileUpload ID="fuArchivoOK" runat="server" />
                                </fieldset>
                            </td>
                            <td>
                                <fieldset>
                                    <legend>Arcvhivo ente <span style="font-weight: bold; color: red">ERRORES</span></legend>
                                    <asp:FileUpload ID="fuArchivoErrores" runat="server" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 pull-right">
                    <div class="col-sm-6 no-padding">
                        <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                    </div>
                    <div class="col-sm-6 no-padding">
                        <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_ServerClick" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="clearfix"></div>
        <div class="col-sm-12">
            <ajaxToolkit:ModalPopupExtender ID="mpeBajas" runat="server" TargetControlID="btnTarget" PopupControlID="panelBajas"
                BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="BtnCancelarBajas"
                CacheDynamicResults="false">
            </ajaxToolkit:ModalPopupExtender>

            <div style="display: none;">
                <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
            </div>

            <asp:Panel ID="panelBajas" runat="server"  Width="100%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4><asp:Label ID="lblTituloMsj" runat="server" Text="Trámites Bajas" /></h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="grdFichasBajas" runat="server" AutoGenerateColumns="False" Width="600px"
                            class="table table-bordered table-condensed" DataKeyNames="ID" 
                            EmptyDataText="<center><b>No hay fichas técnicas con operación baja.</b></center>">
                            <Columns>
                                <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Oblea Anterior" DataField="NumeroObleaAnterior" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Eliminar">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkTodos" runat="server" onclick="Check(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="GridRow"></EditRowStyle>
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <RowStyle CssClass="GridRow"></RowStyle>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-12 pull-right">
                            <div class="col-sm-6 no-padding">
                                <button type="button" class="btn btn-primary btn-block nn" id="BtnAceptarBajas" runat="server" onserverclick="BtnAceptarBajas_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                            </div>
                            <div class="col-sm-6 no-padding">
                                <button type="button" class="btn btn-danger btn-block nn" id="BtnCancelarBajas" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="clearfix"></div>
        <div class="col-sm-12">
            <ajaxToolkit:ModalPopupExtender ID="mpeEliminar" runat="server" TargetControlID="btnTarget" PopupControlID="panelEliminar"
                BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancelarEliminar"
                CacheDynamicResults="false">
            </ajaxToolkit:ModalPopupExtender>            

            <asp:Panel ID="panelEliminar" runat="server"  Width="100%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>
                            <asp:Label ID="Label1" runat="server" Text="Ingrese contraseña para eliminar" /></h4>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-12 pull-right">
                            <div class="col-sm-6 no-padding">
                                <button type="button" class="btn btn-primary btn-block nn" id="btnEliminar" runat="server" onserverclick="btnEliminar_Click" title="Eliminar" alt="Eliminar informe"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Eliminar</button>
                            </div>
                            <div class="col-sm-6 no-padding">
                                <button type="button" class="btn btn-danger btn-block nn" id="btnCancelarEliminar" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="clearfix"></div>
        <div class="col-sm-12">
            <ajaxToolkit:ModalPopupExtender ID="mpeDetalle" runat="server" TargetControlID="btnTarget" PopupControlID="panelDetalle"
                BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="BtnCancelarDetalle"
                CacheDynamicResults="false">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="panelDetalle" runat="server"  Width="100%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4><asp:Label ID="lblDetalle" runat="server" Text="Trámites Bajas" /></h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="grdDetalle" runat="server" AutoGenerateColumns="False" Width="600px"
                            class="table table-bordered table-condensed" DataKeyNames="ID" 
                            EmptyDataText="<center><b>No hay fichas técnicas.</b></center>">
                            <Columns>
                                <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Oblea Anterior" DataField="NumeroObleaAnterior" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />                                
                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Operación" DataField="Operacion" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-12 pull-right">
                            <div class="col-sm-6 no-padding"></div>
                            <div class="col-sm-6 no-padding">
                                <button type="button" class="btn btn-danger btn-block nn" id="BtnCancelarDetalle" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    </div>

    <script type="text/javascript">
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


</asp:Content>
