<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerArchivos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.VerArchivos" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>Ver Archivos</h4>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-2"><strong>Fecha:</strong></div>
                <div class="col-sm-2">
                    <input type="text" id="calFecha" runat="server" clientidmode="Static" maxlength="10" class="form-control nn">
                </div>
                <div class="col-sm-2">
                    <button type="button" class="btn btn-primary" aria-label="" name="" id="btnBuscar" runat="server" onserverclick="btnBuscar_ServerClick" title="Buscar"><i class="fa fa-search" aria-hidden="true"></i>&nbsp Buscar</button>
                </div>
                <div class="col-sm-2">
                    <button type="button" class="btn btn-danger" aria-label="" name="" id="btnCancelar" runat="server" title="Cerrar" onclick="window.close();"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cerrar</button>
                </div>
                <div class="col-sm-4"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div style="max-height: 150px; overflow: auto;">
                    <asp:GridView ID="grdArchivos" runat="server" AutoGenerateColumns="False" Width="100%"
                        class="table table-bordered table-condensed" OnRowCommand="grdArchivos_RowCommand"
                        EmptyDataText="<center><strong>No se encontraron archivos.</strong></center>">
                        <Columns>
                            <asp:BoundField HeaderText="Archivo" DataField="FileName" ItemStyle-HorizontalAlign="Left" />                            
                            <asp:TemplateField HeaderText="USR" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkUsr" runat="server" AlternateText='<%# Eval("FileName") %>' CommandArgument='<%# Eval("FilePath") %>' ImageUrl="~/img/Iconos/seleccionar.png" />
                                </ItemTemplate>
                            </asp:TemplateField>                           
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <script>
        $(function () {
            $("#<%= calFecha.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>

</asp:Content>
