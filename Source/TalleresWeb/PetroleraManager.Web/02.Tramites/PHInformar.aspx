<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PHInformar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.PHInformar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
<script language="javascript" type="text/javascript"  >
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

        function IsCheckBox(chk)
        {
            if (chk.type == 'checkbox') return true;
            else return false;
        }
    </script>
   
    <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Informar pruebas hidráulicas:"></PLs:PLLabel>
        </h2>
        <PLs:PLLabel ID="lblUltimaActualización" runat="server" Text=""></PLs:PLLabel>&nbsp;
        <PLs:PLButton ID="lnkVerArchivos" runat="server" Text="       Ver Archivos" CausesValidation="false"
            OnClientClick="window.open('../Archivos/PH/index.aspx','VerArchivosEnte');" 
            Height="35px" Style="background: transparent url(../Imagenes/Iconos/modificar.png) center left no-repeat;" />
    <div style="overflow: auto; height: 380px; width: 100%; text-align: center;">
        <PLs:PLGridView ID="grdPH" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID" 
            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow">
            <Columns>
                <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Nro. oblea" DataField="Descripcion" />
                <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                <asp:TemplateField HeaderText="Eliminar">
                    <HeaderTemplate>
                        <PLs:PLCheckBox ID="chkTodos" runat="server" onclick="Check(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <PLs:PLCheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle CssClass="GridRow"></EditRowStyle>
            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
            <RowStyle CssClass="GridRow"></RowStyle>
        </PLs:PLGridView>
        <br />
        <PLs:PLLabel ID="lblMensaje" runat="server" Visible="false" Text="No hay ph disponibles para ser informadas." CssClass="failureNotification"></PLs:PLLabel>
    </div>
    <div style="width: 100%; text-align: right;">
        <PLs:PLButton ID="lnkAceptar" runat="server" Text="       Informar" CausesValidation="false"
            OnClientClick="this.disabled=true" UseSubmitBehavior="False" OnClick="lnkAceptar_Click"
            Height="35px" Style="background: transparent url(../Imagenes/Iconos/correcta.png) center left no-repeat;" />
        &nbsp;&nbsp;
        <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Volver" CausesValidation="false"
            OnClientClick="window.location='ObleasConsultar.aspx';" Height="35px" Style="background: transparent url(../Imagenes/Iconos/volver.png) center left no-repeat;"
            OnClick="lnkCancelar_Click" />&nbsp;&nbsp;
    </div>
</asp:Content>
