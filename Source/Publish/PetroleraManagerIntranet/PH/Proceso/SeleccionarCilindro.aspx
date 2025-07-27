<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="SeleccionarCilindro.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Proceso.SeleccionarCilindro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <main id="central-nm" role="main">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-6">
                    <h4><asp:Label ID="lblTitulo" runat="server" /></h4>
                </div>
                <hr />
            </div>

            <div>
                <strong>
                    <asp:Label ID="lblTituloPendientes" runat="server" Text="PENDIENTES" /></strong>
            </div>

            <div class="col-sm-12">
                <asp:GridView ID="grdCilindros" runat="server" class="table table-bordered table-hover"
                    AutoGenerateColumns="false" DataKeyNames="ID, IDCilindroUnidad"
                    OnRowDataBound="grd_RowDataBound" EmptyDataText="<center>No hay obleas pendientes.</center>">
                    <Columns>
                        <asp:BoundField DataField="NroOperacionCRPC" HeaderText="Nº Int. Operación" />
                        <asp:BoundField DataField="NumeroSerieCilindro" HeaderText="Nº de Serie:" />
                        <asp:BoundField DataField="CodigoHomologacionCilindro" HeaderText="Cód. de Homologación:" />
                        <asp:BoundField DataField="Taller" HeaderText="Taller:" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-4 pull-left">
                <button type="button" class="btn btn-primary btn-block nn" aria-label="" title="Volver" alt="Volver" onclick="window.location.href = 'index.aspx'">Volver &nbsp<i class="fa fa-chevron-left" aria-hidden="true"></i></button>
            </div>
        </div>

    </main>

    <script lang="javascript">
        function openWindow(id) {
            var url = "IngresarDatos.aspx?id=" + id;
            window.location.href = url;
        }
    </script>
</asp:Content>
