<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ObleasInformar.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ObleasInformar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

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
               else
               {
                   elements[i].checked = false;
               }
           }

       }

       function IsCheckBox(chk) {
           if (chk.type == 'checkbox') return true;
           else return false;
       }
    </script>

    <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Informar fichas técnicas:"></PLs:PLLabel>
        </h2>
        <PLs:PLLabel ID="lblUltimaActualización" runat="server" Text=""></PLs:PLLabel>&nbsp;
        <PLs:PLLinkButton ID="btnVerAarchivos" runat="server" PostBackUrl="VerArchivos.aspx" CausesValidation="false">Ver Archivos</PLs:PLLinkButton>    

    <div style="overflow: auto; height: 300px; width: 100%; text-align: center;">
        <PLs:PLGridView ID="grdFichas" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID" 
            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
            EmptyDataText="<b>No hay fichas técnicas para informar</b>" OnRowCommand="grdFichas_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Oblea Anterior" DataField="Descripcion" />
                <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Operación" DataField="Operacion" />
                <asp:TemplateField HeaderText="Eliminar">
                    <HeaderTemplate>
                        <PLs:PLCheckBox ID="chkTodos" runat="server" onclick="Check(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <PLs:PLCheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <Controls:ImgBtnImprimir ID="ibtAgregar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"/>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <Controls:ImgBtnReEvaluar ID="ibtReevaluar" runat="server" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm ('Desea re-evaluar la ficha seleccionada?');"/>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle CssClass="GridRow"></EditRowStyle>
            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
            <RowStyle CssClass="GridRow"></RowStyle>
        </PLs:PLGridView>        
    </div>
    <div style="width: 100%; text-align: right;">
        <Controls:BtnAceptar ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click" OnClientClick='return confirm("Generar Informe?");' />        
        &nbsp;&nbsp;
        <Controls:BtnCancelar ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" />
        &nbsp;&nbsp;
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

</asp:Content>
