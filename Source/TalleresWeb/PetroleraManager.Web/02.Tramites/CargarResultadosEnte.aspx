<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargarResultadosEnte.aspx.cs" Inherits="PetroleraManager.Web.Tramites.CargarResultadosEnte" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Cargar resultados ente:"></PLs:PLLabel></h2>

    <fieldset>
        <legend>Informes Pendientes:</legend>
        <div style="max-height: 150px; overflow: auto;">
            <asp:GridView ID="grdInformes" runat="server" AutoGenerateColumns="False" Width="100%"
                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                DataKeyNames="ID" OnRowCommand="grdInformes_RowCommand" OnRowDataBound="grdInformes_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Número" DataField="Numero" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHora" ItemStyle-HorizontalAlign="Center" />
                    <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasEnviadas" ItemStyle-HorizontalAlign="Center" CommandName="enviadas" HeaderText="Enviadas" />
                    <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasAsignadas" ItemStyle-HorizontalAlign="Center" CommandName="asignadas" HeaderText="Asignadas" />
                    <asp:ButtonField ButtonType="Link" DataTextField="CantidadObleasRechazadas" ItemStyle-HorizontalAlign="Center" CommandName="rechazadas" HeaderText="Rechazadas" />                   
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Iconos/seleccionar.png" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" CommandName="seleccionar" />
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Iconos/eliminar.png" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" CommandName="eliminar" />
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Iconos/correcta.png" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" CommandName="cerrar" />
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
    <br />
    <br />
    <asp:Panel ID="panelArchivos" runat="server" Visible="false">
        <fieldset>
            <strong>
                <asp:Label ID="lblTitulo" Text="text" runat="server" /></strong>
            <br />
            <br />

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
        </fieldset>
        <div style="width: 100%; text-align: right;">
        <Controls:BtnAceptar ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click" />        
        &nbsp;&nbsp;
        <Controls:BtnCancelar ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" />
        &nbsp;&nbsp;
    </div>

    </asp:Panel>

    <asp:Panel ID="pnlEliminar" runat="server" Visible="false" DefaultButton="btnEliminar">
        <asp:HiddenField ID="hddInformeID" runat="server" />
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
        <Controls:BtnAceptar ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" CausesValidation="false" />
        <Controls:BtnCancelar ID="btnCancelarEliminar" runat="server" OnClick="btnCancelarEliminar_Click" />
    </asp:Panel>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

    <div style="position: fixed; left: 0; top: 0;">

        <asp:HiddenField ID="hddInformeBajasID" runat="server" />

        <PLs:PLModalPopupExtender ID="mpeBajas" runat="server" TargetControlID="btnTarget" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="BtnCancelarBajas"
            CacheDynamicResults="false">
        </PLs:PLModalPopupExtender>

        <div style="display: none;">
            <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
        </div>

        <PLs:PLPanel ID="Panel1" runat="server" CssClass="CajaDialogo" Width="100%">
            <fieldset>
                <legend><span class="LabelLegend"><PLs:PLLabel ID="lblTituloMsj" runat="server" Text="Trámites Bajas" /></span></legend>
                
                 <PLs:PLGridView ID="grdFichasBajas" runat="server" AutoGenerateColumns="False" Width="600px" DataKeyNames="ID" 
                    AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                    EmptyDataText="<b>No hay fichas técnicas para informar</b>">
                    <Columns>
                        <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Oblea Anterior" DataField="NumeroObleaAnterior" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
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

            </fieldset>
            <br />
            <div style="width: 100%; text-align: center;">
                <Controls:BtnAceptar ID="BtnAceptarBajas" runat="server" OnClick="BtnAceptarBajas_Click" />
                <Controls:BtnCancelar ID="BtnCancelarBajas" runat="server" />
            </div>
            <br />
        </PLs:PLPanel>
    </div>

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

</asp:Content>
