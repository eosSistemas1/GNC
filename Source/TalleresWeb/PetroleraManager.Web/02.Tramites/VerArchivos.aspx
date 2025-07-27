<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerArchivos.aspx.cs" Inherits="PetroleraManager.Web.Tramites.VerArchivos" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <table>
        <tr>
            <td>
                <span>FECHA:<input type="text" id="calFecha" runat="server" style="width: 90px" clientidmode="Static" maxlength="10"></span>
            </td>
            <td>
                <PLs:PLButton ID="lnkBuscar" runat="server" Text="       Buscar" CausesValidation="false"
                               OnClick="lnkBuscar_Click" Height="35px" 
                               Style="background: transparent url(/Imagenes/Iconos/buscar.png) center left no-repeat;" />
            </td>
            <td style="width:25%"></td>
        </tr>
    </table>
        
    <fieldset>
        <legend>Archivos:</legend>
        <div style="max-height: 150px; overflow: auto;">
           <asp:GridView ID="grdArchivos" runat="server" AutoGenerateColumns="False" Width="100%"
                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                DataKeyNames="ID" OnRowCommand="grdArchivos_RowCommand" EmptyDataText="<center><strong>No se encontraron archivos.</strong></center>">
                <Columns>
                    <asp:BoundField HeaderText="Número" DataField="NumeroInforme" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHoraInforme" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="USR" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkUsr" runat="server"  AlternateText='<%# Eval("descripcionUSR") %>' CommandArgument='<%# Eval("urlUSR") %>' ImageUrl="~/Imagenes/Iconos/seleccionar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REG" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkReg" runat="server" AlternateText='<%# Eval("descripcionREG") %>' CommandArgument='<%# Eval("urlREG") %>' ImageUrl="~/Imagenes/Iconos/seleccionar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CIL" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkCil" runat="server" AlternateText='<%# Eval("descripcionCIL") %>' CommandArgument='<%# Eval("urlCIL") %>' ImageUrl="~/Imagenes/Iconos/seleccionar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAL" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkVal" runat="server" AlternateText='<%# Eval("descripcionVAL") %>' CommandArgument='<%# Eval("urlVAL") %>' ImageUrl="~/Imagenes/Iconos/seleccionar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                     <asp:TemplateField HeaderText="ZIP" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkZIP" runat="server" AlternateText='DescargarZip' CommandArgument='<%# Eval("urlVAL") %>' ImageUrl="~/Imagenes/Iconos/seleccionar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
    
    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <script type="text/javascript" lang="javascript">
        $(function () {
            $("#calFecha").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>

</asp:Content>
