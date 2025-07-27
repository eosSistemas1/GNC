<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true"
    CodeBehind="RechazarDespacho.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Despacho.RechazarDespacho" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido">
        <h1>
            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h1>

        <div class="row">
            <div class="col-sm-12">
                <asp:Repeater ID="repeaterZonas" runat="server" OnItemDataBound="repeaterZonas_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnZonaTaller" runat="server" Value='<%# Eval("ZonaTaller")%>' />

                        <h1><%#Eval("ZonaTaller")%></h1>


                        <asp:GridView ID="grdZona" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                            class="table table-bordered table-condensed" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="TRAMITE" DataField="TipoTramite" HeaderStyle-Width="20%" />
                                <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" HeaderStyle-Width="10%" />
                                <asp:BoundField HeaderText="TALLER" DataField="Taller" />
                                <asp:TemplateField HeaderStyle-Width="10%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkTodos" runat="server" AutoPostBack="true" OnCheckedChanged="chkTodos_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox type="checkbox" ID="chkSeleccionado" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12" style="text-align: right;">
                <button id="btnRechazarTramiteCarrito" runat="server" onserverclick="btnRechazarTramiteCarrito_ServerClick"
                    type="button" class="btn btn-primary btn-sm text-center">
                    <i class="fa fa-trash" aria-hidden="true"></i>&nbsp Rechazar
                </button>
            </div>
        </div>
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

</asp:Content>
