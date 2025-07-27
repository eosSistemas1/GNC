<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="GenerarExcel.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ConsultaCC.GenerarExcel" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-6">
                    <h3>
                        <asp:Label ID="lblTitulo" runat="server" /></h3>
                    <br />
                    <p>
                        <strong>
                            <asp:Label ID="lblTituloPendientes" runat="server" Text="PH a verificar:" /></strong>
                    </p>
                </div>
                <div class="col-sm-2 pull-right">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>                   
                </div>

                <div class="col-sm-12">
                    <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                        AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad" ClientIDMode="Static"
                        OnRowDataBound="grdCilindros_RowDataBound"
                        EmptyDataText="<center>No hay obleas pendientes.</center>">
                        <Columns>
                            <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº OPERACIÓN CRPC" />
                            <asp:BoundField DataField="Taller" HeaderText="TALLER" />
                            <asp:BoundField DataField="Dominio" HeaderText="DOMINIO" />
                            <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="CÓD. HOMOLOGACIÓN" />
                            <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº DE SERIE" />
                            <asp:TemplateField HeaderText="IMPRIMIR">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" ClientIDMode="Static" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-2 pull-right">
                    <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Generar Excel" alt="Generar Excel" runat="server" onserverclick="GenerarExcel_ServerClick">Generar Excel&nbsp<i class="fa fa-table" aria-hidden="true"></i></button>
                </div>
            </div>

            <hr />

            <div id="divArchivos" runat="server">
                <h3><b>Archivos:</b></h3>
                <br />
                <div class="row">
                    <div class="col-sm-2">
                        <p>Desde:</p>
                    </div>
                    <div class="col-sm-3">
                        <input type="date" id="calFechaD" runat="server" />
                    </div>
                    <div class="col-sm-2">
                        <p>Hasta:</p>
                    </div>
                    <div class="col-sm-3">
                        <input type="date" id="calFechaH" runat="server" />
                    </div>
                    <div class="col-sm-2">
                        <button runat="server" onserverclick="Search_ServerClick" type="button" class="btn btn-default btn-block nn" aria-label="" title="Ver Archivos" alt="Ver Archivos">Ver Archivos &nbsp<i class="fa fa-search" aria-hidden="true"></i></button>
                    </div>
                </div>
                <div style="max-height: 150px; overflow: auto;">
                    <asp:GridView ID="grdArchivos" runat="server" AutoGenerateColumns="False" Width="100%"
                        class="table table-bordered table-hover"
                        OnRowCommand="grdArchivos_RowCommand" EmptyDataText="<center><strong>No se encontraron archivos.</strong></center>">
                        <Columns>
                            <asp:BoundField HeaderText="Archivo" DataField="NombreExcel" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha" DataField="FechaHoraExcel" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Descargar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkArchivo" runat="server" AlternateText='<%# Eval("UrlExcel") %>' CommandArgument='<%# Eval("UrlExcel") %>' ImageUrl="~/img/Iconos/seleccionar.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </main>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#chkAll").click(function () {
                this.checked = !(this.checked == true);

                $("#grdCilindros input:checkbox").attr("checked", function () {
                    if ($(this).attr('disabled') != 'disabled') {
                        this.checked = !(this.checked == true);
                    }
                    else {
                        this.checked = false;
                    }
                });

            });
        });
    </script>

</asp:Content>
