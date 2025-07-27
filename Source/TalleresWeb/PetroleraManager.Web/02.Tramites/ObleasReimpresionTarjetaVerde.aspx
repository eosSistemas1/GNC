<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasReimpresionTarjetaVerde.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ObleasReimpresionTarjetaVerde" %>
<%@ Register src="~/UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/PrintBoxCtrl.ascx" TagPrefix="uc1" TagName="PrintBoxCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
    <legend>Reimprimir Tarjeta Verde</legend>
    <asp:Panel runat="server" DefaultButton="btnAceptar">
    <table width="100%" border="0">
        <tr>
            <td>Número de oblea (nueva):</td>
            <td>
                <asp:TextBox ID="txtNroOblea" runat="server" onKeyPress="return soloNumeros(event)" />
            </td>
            <td>></td>
        </tr>
        <tr>
            <td>Dominio:</td>
            <td>
                <asp:TextBox ID="txtDominio" runat="server" />
            </td>
            <td style="width:50%; text-align:center">
                <Controls:BtnAceptar ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <table width="100%" border="0">
        <tr>
            <td colspan="3">
                <div style="max-height: 150px; overflow: auto;">
                    <asp:GridView ID="grdObleas" runat="server" AutoGenerateColumns="False" Width="100%"
                        AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                        DataKeyNames="ID" OnRowCommand="grdObleas_RowCommand" OnRowDataBound="grdObleas_DataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Anterior" DataField="NroObleaAnterior" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Nueva" DataField="NroObleaNueva" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha Habilitación" DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />                                                        
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <Controls:ImgBtnSeleccionar ID="seleccionar" runat="server" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</fieldset>

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
    
    <uc1:PrintBoxCtrl runat="server" ID="PrintBoxCtrl" />

    <script type="text/javascript">  
        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>
</asp:Content>
