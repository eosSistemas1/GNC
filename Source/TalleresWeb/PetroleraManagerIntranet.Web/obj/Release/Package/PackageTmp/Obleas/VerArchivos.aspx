<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerArchivos.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.VerArchivos" %>

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
                    <button type="button" class="btn btn-danger" aria-label="" name="" id="btnCancelar" runat="server" title="Cancelar" onclick="window.history.back();"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                </div>
                <div class="col-sm-4"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div style="max-height: 150px; overflow: auto;">
                    <asp:GridView ID="grdArchivos" runat="server" AutoGenerateColumns="False" Width="100%"
                        class="table table-bordered table-condensed"
                        DataKeyNames="ID" OnRowCommand="grdArchivos_RowCommand" HeaderStyle-HorizontalAlign="Center"
                        EmptyDataText="<center><strong>No se encontraron archivos.</strong></center>">
                        <Columns>
                            <asp:BoundField HeaderText="Número" DataField="NumeroInforme" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha" DataField="FechaHoraInforme" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="USR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkUsr" runat="server" AlternateText='<%# Eval("descripcionUSR") %>' CommandArgument='<%# Eval("urlUSR") %>' CommandName="TXT" ImageUrl="~/img/Iconos/seleccionar.png" />
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="lnkUsrZip" runat="server" AlternateText='<%# Eval("descripcionUSR") %>' CommandArgument='<%# Eval("urlUSR") %>' CommandName="ZIP" ImageUrl="~/img/Iconos/zip.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REG" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkReg" runat="server" AlternateText='<%# Eval("descripcionREG") %>' CommandArgument='<%# Eval("urlREG") %>' CommandName="TXT" ImageUrl="~/img/Iconos/seleccionar.png" />
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="lnkRegZip" runat="server" AlternateText='<%# Eval("descripcionREG") %>' CommandArgument='<%# Eval("urlREG") %>' CommandName="ZIP" ImageUrl="~/img/Iconos/zip.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkCil" runat="server" AlternateText='<%# Eval("descripcionCIL") %>' CommandArgument='<%# Eval("urlCIL") %>' CommandName="TXT" ImageUrl="~/img/Iconos/seleccionar.png" />
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="lnkCilZip" runat="server" AlternateText='<%# Eval("descripcionCIL") %>' CommandArgument='<%# Eval("urlCIL") %>' CommandName="ZIP" ImageUrl="~/img/Iconos/zip.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VAL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkVal" runat="server" AlternateText='<%# Eval("descripcionVAL") %>' CommandArgument='<%# Eval("urlVAL") %>' CommandName="TXT" ImageUrl="~/img/Iconos/seleccionar.png" />
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="lnkValZip" runat="server" AlternateText='<%# Eval("descripcionVAL") %>' CommandArgument='<%# Eval("urlVAL") %>' CommandName="ZIP" ImageUrl="~/img/Iconos/zip.png" />
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
